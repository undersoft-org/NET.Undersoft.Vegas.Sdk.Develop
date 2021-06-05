/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Mathset.SubMathset.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (05.06.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Mathset
{
    using System.Instant;
    using System.Reflection.Emit;

    /// <summary>
    /// Defines the <see cref="SubMathset" />.
    /// </summary>
    [Serializable]
    public class SubMathset : LeftFormula
    {
        #region Fields

        public int startId = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SubMathset"/> class.
        /// </summary>
        /// <param name="evalRubric">The evalRubric<see cref="MathRubric"/>.</param>
        /// <param name="formuler">The formuler<see cref="Mathset"/>.</param>
        public SubMathset(MathRubric evalRubric, Mathset formuler)
        {
            if (evalRubric != null) Rubric = evalRubric;

            SetDimensions(formuler);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the colCount.
        /// </summary>
        public int colCount
        {
            get { return Formuler.Rubrics.Count; }
        }

        /// <summary>
        /// Gets the Data.
        /// </summary>
        public IFigures Data
        {
            get { return Formuler.Data; }
        }

        /// <summary>
        /// Gets the FieldId.
        /// </summary>
        public int FieldId { get => Rubric.FigureFieldId; }

        /// <summary>
        /// Gets or sets the Formuler.
        /// </summary>
        public Mathset Formuler { get; set; }

        /// <summary>
        /// Gets the rowCount.
        /// </summary>
        public int rowCount
        {
            get { return Data.Count; }
        }

        /// <summary>
        /// Gets or sets the Rubric.
        /// </summary>
        public MathRubric Rubric { get; set; }

        /// <summary>
        /// Gets the RubricName.
        /// </summary>
        public string RubricName { get => Rubric.RubricName; }

        /// <summary>
        /// Gets the RubricType.
        /// </summary>
        public Type RubricType { get => Rubric.RubricType; }

        /// <summary>
        /// Gets the Size.
        /// </summary>
        public override MathsetSize Size
        {
            get { return new MathsetSize(rowCount, colCount); }
        }

        /// <summary>
        /// Gets or sets the SubFormuler.
        /// </summary>
        public SubMathset SubFormuler { get; set; }

        #endregion

        #region Methods

        // Compilation First Pass: add a reference to the array variable
        // Code Generation: access the element through the i index
        /// <summary>
        /// The Compile.
        /// </summary>
        /// <param name="g">The g<see cref="ILGenerator"/>.</param>
        /// <param name="cc">The cc<see cref="CompilerContext"/>.</param>
        public override void Compile(ILGenerator g, CompilerContext cc)
        {
            if (cc.IsFirstPass())
            {
                cc.Add(Data);
            }
            else
            {
                CompilerContext.GenLocalLoad(g, cc.GetSubIndexOf(Data));           // index

                g.Emit(OpCodes.Ldc_I4, FieldId);
                g.EmitCall(OpCodes.Callvirt, typeof(IFigure).GetMethod("get_Item", new Type[] { typeof(int) }), null);
                g.Emit(OpCodes.Unbox_Any, RubricType);
                g.Emit(OpCodes.Conv_R8);
            }
        }

        /// <summary>
        /// The CompileAssign.
        /// </summary>
        /// <param name="g">The g<see cref="ILGenerator"/>.</param>
        /// <param name="cc">The cc<see cref="CompilerContext"/>.</param>
        /// <param name="post">The post<see cref="bool"/>.</param>
        /// <param name="partial">The partial<see cref="bool"/>.</param>
        public override void CompileAssign(ILGenerator g, CompilerContext cc, bool post, bool partial)
        {
            if (cc.IsFirstPass())
            {
                cc.Add(Data);
                return;
            }

            int i1 = cc.GetIndexVariable(0);

            if (!post)
            {
                if (!partial)
                {
                    CompilerContext.GenLocalLoad(g, cc.GetIndexOf(Data));

                    if (startId != 0)
                        g.Emit(OpCodes.Ldc_I4, startId);

                    g.Emit(OpCodes.Ldloc, i1);

                    if (startId != 0)
                        g.Emit(OpCodes.Add);

                    g.EmitCall(OpCodes.Callvirt, typeof(IFigures).GetMethod("get_Item", new Type[] { typeof(int) }), null);
                    CompilerContext.GenLocalStore(g, cc.GetSubIndexOf(Data));
                    CompilerContext.GenLocalLoad(g, cc.GetSubIndexOf(Data));
                }
                else
                {
                    CompilerContext.GenLocalLoad(g, cc.GetSubIndexOf(Data));
                }
                g.Emit(OpCodes.Ldc_I4, FieldId);

            }
            else
            {
                if (partial)
                {
                    g.Emit(OpCodes.Dup);
                    CompilerContext.GenLocalStore(g, cc.GetBufforIndexOf(Data));           // index
                }

                g.Emit(OpCodes.Box, typeof(double));
                g.EmitCall(OpCodes.Callvirt, typeof(IFigure).GetMethod("set_Item", new Type[] { typeof(int), typeof(object) }), null);


                if (partial)
                    CompilerContext.GenLocalLoad(g, cc.GetBufforIndexOf(Data));           // index
            }
        }

        /// <summary>
        /// The SetDimensions.
        /// </summary>
        /// <param name="formuler">The formuler<see cref="Mathset"/>.</param>
        public void SetDimensions(Mathset formuler = null)
        {
            if (!ReferenceEquals(formuler, null))
                Formuler = formuler;
            Rubric.SubFormuler = this;
        }

        #endregion
    }
}
