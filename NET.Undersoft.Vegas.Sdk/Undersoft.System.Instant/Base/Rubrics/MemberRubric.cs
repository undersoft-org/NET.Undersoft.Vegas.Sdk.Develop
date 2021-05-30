using System.Reflection;
using System.Linq;
using System.Uniques;
using System.Instant.Linking;
using System.Instant.Treatments;

namespace System.Instant
{
    public class MemberRubric : MemberInfo, IRubric
    {                       
        public MemberRubric(IMemberRubric member)
        {
            RubricInfo = ((MemberInfo)member);
            RubricName = member.RubricName;
            RubricId = member.RubricId;
            Visible = member.Visible;
            Editable = member.Editable;
            if (RubricInfo.MemberType == MemberTypes.Method)
                SystemSerialCode = new Ussn((new String(RubricParameterInfo
                                            .SelectMany(p => p.ParameterType.Name)
                                                .ToArray()) + "_" + RubricName).UniqueKey64());
            else
                SystemSerialCode = new Ussn(RubricName.UniqueKey64());


        }
        public MemberRubric(MemberRubric member) : this(member.RubricInfo != null ? (IMemberRubric)member.RubricInfo : (IMemberRubric)member)
        {
            FigureType = member.FigureType;
            FigureField = member.FigureField;
            FigureFieldId = member.FigureFieldId;
            RubricOffset = member.RubricOffset;
            IsKey = member.IsKey;
            IsIdentity = member.IsIdentity;
            IsAutoincrement = member.IsAutoincrement;
            IdentityOrder = member.IdentityOrder;
            Required = member.Required;
            DisplayName = member.DisplayName;
        }
        public MemberRubric(MethodRubric method) : this((IMemberRubric)method)
        {
        }
        public MemberRubric(FieldRubric field) : this((IMemberRubric)field)
        {
        }
        public MemberRubric(PropertyRubric property) : this((IMemberRubric)property)
        {
        }
        public MemberRubric(MethodInfo method) : this((IMemberRubric)new MethodRubric(method))
        {
        }
        public MemberRubric(PropertyInfo property) : this((IMemberRubric)new PropertyRubric(property))
        {
        }
        public MemberRubric(FieldInfo field) : this((IMemberRubric)new FieldRubric(field))
        {
        }

        public MemberRubrics Rubrics { get; set; }

        public Type FigureType { get; set; }
        public FieldInfo FigureField { get; set; }
        public int FigureFieldId { get; set; }
        public MemberInfo RubricInfo { get; set; }
        public IMemberRubric VirtualInfo => (IMemberRubric)RubricInfo;

        public Type RubricReturnType { get => MemberType == MemberTypes.Method ? ((MethodRubric)RubricInfo).RubricReturnType : null; }
        public ParameterInfo[] RubricParameterInfo { get => MemberType == MemberTypes.Method ? ((MethodRubric)RubricInfo).RubricParameterInfo : null; }
        public Module RubricModule { get => MemberType == MemberTypes.Method ? ((MethodRubric)RubricInfo).RubricModule : null; }
        public string RubricName { get; set; }
        public string DisplayName { get; set; }
        public Type RubricType { get { return VirtualInfo.RubricType; } set { VirtualInfo.RubricType = value; } }

        public int RubricId { get; set; }
        public int RubricSize { get { return VirtualInfo.RubricSize; } set { VirtualInfo.RubricSize = value; } }
        public int RubricOffset { get; set; }
        public short IdentityOrder { get; set; }

        public bool Visible { get; set; }
        public bool Editable { get; set; }
        public bool Required { get; set; }
        public bool IsKey { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsAutoincrement { get; set; }
        public bool IsDBNull { get; set; }
        public bool IsColossus { get; set; }

        public IRubric          AggregatePattern { get; set; }
        public AggregateOperand AggregateOperand { get; set; }
        public int[]            AggregateIndex   { get; set; }
        public int[]            AggregateOrdinal { get; set; }

        public Links     AggregateLinks { get; set; }

        public int              SummaryOrdinal { get; set; }
        public IRubric          SummaryPattern { get; set; }
        public AggregateOperand SummaryOperand { get; set; }        

        public object[] RubricAttributes
        { get { return VirtualInfo.RubricAttributes; } set { VirtualInfo.RubricAttributes = value; } }

        public override Type DeclaringType => FigureType != null ? FigureType : RubricInfo.DeclaringType;
        public override MemberTypes MemberType => RubricInfo.MemberType;
        public override string Name => RubricInfo.Name;
        public override Type ReflectedType => RubricInfo.ReflectedType;

        public IUnique Empty => Ussn.Empty;

        public long UniqueKey { get => systemSerialCode.UniqueKey; set => systemSerialCode.UniqueKey = value; }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return RubricInfo.GetCustomAttributes(inherit);
        }
        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return RubricInfo.GetCustomAttributes(attributeType, inherit);
        }
        public override bool     IsDefined(Type attributeType, bool inherit)
        {
            return RubricInfo.IsDefined(attributeType, inherit);
        }

        public byte[] GetBytes()
        {
            return systemSerialCode.GetBytes();
        }
        public byte[] GetUniqueBytes()
        {
            return systemSerialCode.GetUniqueBytes();
        }
        public void   SetUniqueKey(long value)
        {
            systemSerialCode.UniqueKey = value;
        }
        public long   GetUniqueKey()
        {
            return systemSerialCode.UniqueKey;
        }

        public bool Equals(IUnique other)
        {
           return UniqueKey == other.UniqueKey;
        }
        public int CompareTo(IUnique other)
        {
            return (int)(UniqueKey - other.UniqueKey);
        }

        public void SetUniqueSeed(uint seed)
        {
            systemSerialCode.SetUniqueSeed(seed);
        }

        public uint GetUniqueSeed()
        {
            return systemSerialCode.GetUniqueSeed();
        }

        private Ussn systemSerialCode;
        public Ussn SystemSerialCode { get => systemSerialCode; set => systemSerialCode = value; }
        public uint UniqueSeed { get => systemSerialCode.UniqueSeed; set => systemSerialCode.UniqueSeed = value; }
    }
}
