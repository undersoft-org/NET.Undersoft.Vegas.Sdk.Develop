using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Instant
{
    public class Figure : IInstant
    {       
        private Type compiledType;
        private MemberRubrics propertyRubrics;
        private MemberRubrics fieldRubrics;

        private FigureMode mode { get; set; }

        public Figure(Type figureModelType, FigureMode modeType = FigureMode.Reference) : this(figureModelType, null,  modeType) { }
        public Figure(Type figureModelType, string figureTypeName, FigureMode modeType = FigureMode.Reference)
        {
            BaseType = figureModelType;

            if (modeType == FigureMode.Derived)
                IsDerived = true;

            Name = figureTypeName == null ? figureModelType.Name : figureTypeName;
            mode = modeType;

            Rubrics = fieldRubrics = new MemberRubrics(createMemberRurics(figureModelType.GetRuntimeFields().ToArray()));
            
            propertyRubrics = new MemberRubrics(createMemberRurics(figureModelType.GetRuntimeProperties().ToArray())
                                                  .Where(r => fieldRubrics.ContainsKey(r)));

            Rubrics.KeyRubrics = new MemberRubrics();
        }
        public Figure(IList<MemberInfo> figureMembers, FigureMode modeType = FigureMode.Reference) 
            : this(figureMembers.ToArray(), null, modeType) {}
        public Figure(IList<MemberInfo> figureMembers, string figureTypeName, FigureMode modeType = FigureMode.Reference)
        {
            Name = (figureTypeName != null && figureTypeName != "") ? figureTypeName : DateTime.Now.ToBinary().ToString();
            mode = modeType;

            Rubrics = fieldRubrics = new MemberRubrics(createMemberRurics(figureMembers));
            Rubrics.KeyRubrics = new MemberRubrics();         
        }
        public Figure(MemberRubrics figureRubrics, string figureTypeName, FigureMode modeType = FigureMode.Reference) 
            : this(figureRubrics.ToArray(), figureTypeName, modeType)
        {          
        }

        
        public bool IsDerived { get; set; }
        public Type BaseType { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public int Size { get; set; }
        public IRubrics Rubrics
        { get; set; }

        public object New()
        {
            if (this.Type == null)
                return Combine();
            return this.Type.New();
        }

        public IFigure Combine()
        {
            if (this.Type == null)
            {
                try
                {
                    switch (mode)
                    {
                        case FigureMode.Reference:
                            combineReferenceType();
                            break;
                        case FigureMode.ValueType:
                            combineValueType();
                            break;
                        case FigureMode.Derived:
                            combineDerivedType();
                            break;
                        default:
                            break;
                    }

                    Rubrics.AsValues().Where(m => m.FigureField != null)
                                          .Select((f, y) => new object[] {
                                           f.FieldId = y - 1,
                                           f.RubricId = y - 1
                                          }).ToArray();

                    foreach(var rubric in Rubrics)
                    {
                        try
                        {
                            rubric.RubricOffset = (int)Marshal.OffsetOf(this.Type, rubric.FigureField.Name);
                        }
                        catch (Exception ex)
                        {
                            
                        }
                        finally
                        {
                            rubric.RubricOffset = -1;
                        }
                    }

                    Rubrics.Update();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return newFigure();
        }

        private IFigure newFigure()
        {
            if (this.Type == null)
                return Combine();
            return (IFigure)this.Type.New();
        }

        private MemberRubric[] createMemberRurics(IList<MemberInfo> membersInfo)
        {
            return membersInfo.Select(m => !(m is MemberRubric) ? m.MemberType == MemberTypes.Field    ? new MemberRubric((FieldInfo)m) :
                                                                  m.MemberType == MemberTypes.Property ? new MemberRubric((PropertyInfo)m) :
                                                                  null : (MemberRubric)m).Where(p => p != null).ToArray();

        }

        private void combineReferenceType()
        {
            var ifcref = new FigureCompilerReference(this, fieldRubrics, propertyRubrics);
            compiledType = ifcref.CompileFigureType(Name);
            Rubrics.KeyRubrics.Add(ifcref.Identities.Values);
            this.Type = compiledType.New().GetType();
            Size = Marshal.SizeOf(this.Type);
            var firef = this.Type.GetRuntimeFields().ToArray();
            if (!Rubrics.AsValues().Where(m => m.Name == "SerialCode").Any())
            {
                var mr = new MemberRubric(firef[0]);
                mr.RubricName = "SerialCode";
                Rubrics.Insert(0, mr);
            }
            Rubrics.AsValues().Select((m, y) => m.FigureField = firef[y]).ToArray();
        }

        private void combineValueType()
        {
            var ifcvt = new FigureCompilerValueType(this, fieldRubrics, propertyRubrics);
            compiledType = ifcvt.CompileFigureType(Name);
            Rubrics.KeyRubrics.Add(ifcvt.Identities.Values);
            this.Type = compiledType.New().GetType();
            Size = Marshal.SizeOf(this.Type);
            var fivt = this.Type.GetRuntimeFields().ToArray();
            if (!Rubrics.AsValues().Where(m => m.Name == "SerialCode").Any())
            {
                var mr = new MemberRubric(fivt[0]);
                mr.RubricName = "SerialCode";
                Rubrics.Insert(0, mr);
            }
            Rubrics.AsValues().Select((m, y) => m.FigureField = fivt[y]).ToArray();
        }

        private void combineDerivedType()
        {
            var ifcdt = new FigureCompilerDerivedType(this, fieldRubrics, propertyRubrics);
            compiledType = ifcdt.CompileFigureType(Name);
            Rubrics.KeyRubrics.Add(ifcdt.Identities.Values);
            this.Type = compiledType.New().GetType();
            Size = Marshal.SizeOf(this.Type);
            var fidt = ifcdt.derivedFields;
            Rubrics.AsValues().Select((m, y) => m.FigureField = fidt[y].RubricInfo).ToArray();
            if (!Rubrics.AsValues().Where(m => m.Name == "SerialCode").Any())
            {
                var f = this.Type.GetField("serialcode", BindingFlags.NonPublic | BindingFlags.Instance);
                var mr = new MemberRubric(f);
                mr.RubricName = "SerialCode";
                mr.FigureField = f;
                Rubrics.Insert(0, mr);
            }

        }
    }
}