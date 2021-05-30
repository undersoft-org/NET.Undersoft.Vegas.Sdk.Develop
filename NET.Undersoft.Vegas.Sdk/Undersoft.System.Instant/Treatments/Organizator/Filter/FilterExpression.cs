using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Uniques;

namespace System.Instant.Treatments
{
    public class OrganizeExpression
    {
        private System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();        

        private Expression<Func<IFigure, bool>> Expression
        { get; set; }

        public Expression<Func<IFigure, bool>> Organize
        { get { return CreateExpression(Stage); } }
        public FilterTerms Conditions;
        public int Stage
        { get; set; }

        public OrganizeExpression()
        {
            Conditions = new FilterTerms();
            nfi.NumberDecimalSeparator = ".";
            Stage = 1;
        }

        public Expression<Func<IFigure, bool>> this[int stage]
        {
            get
            {
                return CreateExpression(stage);
            }
        }

        public Expression<Func<IFigure, bool>> CreateExpression(int stage = 1)
        {
            Expression<Func<IFigure, bool>> exps = null;
            List<FilterTerm> fcs = Conditions.Get(stage);
            Expression = null;
            LogicType previousLogic = LogicType.And;
            foreach (FilterTerm fc in fcs)
            {
                exps = null;
                if (fc.Operand != OperandType.Contains)
                {
                    if (Expression != null)
                        if (previousLogic != LogicType.Or)
                            Expression = Expression.And(CaseConditioner(fc, exps));
                        else
                            Expression = Expression.Or(CaseConditioner(fc, exps));
                    else
                        Expression = CaseConditioner(fc, exps);
                    previousLogic = fc.Logic;
                }
                else
                {
                    HashSet<int> list = new HashSet<int>((fc.Value.GetType() == typeof(string)) ? fc.Value.ToString().Split(';')
                                                         .Select(p => Convert.ChangeType(p, fc.OrganizeRubric.RubricType).GetHashCode()) :
                                                         (fc.Value.GetType() == typeof(List<object>)) ? ((List<object>)fc.Value)
                                                         .Select(p => Convert.ChangeType(p, fc.OrganizeRubric.RubricType).GetHashCode()) : null);

                    if (list != null && list.Count > 0)
                        exps = (r => list.Contains(r[fc.OrganizeRubric.RubricName].GetHashCode()));

                    if (Expression != null)
                        if (previousLogic != LogicType.Or)
                            Expression = Expression.And(exps);
                        else
                            Expression = Expression.Or(exps);
                    else
                        Expression = exps;
                    previousLogic = fc.Logic;
                }
            }
            return Expression;
        }
        private Expression<Func<IFigure, bool>> CaseConditioner(FilterTerm fc, Expression<Func<IFigure, bool>> ex)
        {
            if (fc.Value != null)
            {
                object Value = fc.Value;
                OperandType Operand = fc.Operand;
                if (Operand != OperandType.Like && Operand != OperandType.NotLike)
                {
                    switch (Operand)
                    {
                        case OperandType.Equal:
                            ex = (r => r[fc.OrganizeRubric.FigureFieldId] != null ? 
                            fc.OrganizeRubric.RubricType == typeof(IUnique) ||
                            fc.OrganizeRubric.RubricType == typeof(string) ||
                            fc.OrganizeRubric.RubricType == typeof(DateTime) ?
                            r[fc.OrganizeRubric.FigureFieldId].ComparableInt64(fc.OrganizeRubric.RubricType)                                    
                                .Equals(Value.ComparableInt64(fc.OrganizeRubric.RubricType)) :
                            r[fc.OrganizeRubric.FigureFieldId].ComparableDouble(fc.OrganizeRubric.RubricType)
                                 .Equals(Value.ComparableDouble(fc.OrganizeRubric.RubricType)) : 
                              false);
                            break;
                        case OperandType.EqualOrMore:
                            ex = (r => r[fc.OrganizeRubric.FigureFieldId] != null ?
                             fc.OrganizeRubric.RubricType == typeof(IUnique) || fc.OrganizeRubric.RubricType == typeof(string) || fc.OrganizeRubric.RubricType == typeof(DateTime) ?
                              r[fc.OrganizeRubric.FigureFieldId].ComparableInt64(fc.OrganizeRubric.RubricType) >= (Value.ComparableInt64(fc.OrganizeRubric.RubricType)) :
                            r[fc.OrganizeRubric.FigureFieldId].ComparableDouble(fc.OrganizeRubric.RubricType) >= (Value.ComparableDouble(fc.OrganizeRubric.RubricType)) : false);
                            break;
                        case OperandType.More:
                            ex = (r => r[fc.OrganizeRubric.FigureFieldId] != null ?
                             fc.OrganizeRubric.RubricType == typeof(IUnique) || fc.OrganizeRubric.RubricType == typeof(string) || fc.OrganizeRubric.RubricType == typeof(DateTime) ?
                              r[fc.OrganizeRubric.FigureFieldId].ComparableInt64(fc.OrganizeRubric.RubricType) > (Value.ComparableInt64(fc.OrganizeRubric.RubricType)) :
                            r[fc.OrganizeRubric.FigureFieldId].ComparableDouble(fc.OrganizeRubric.RubricType) > (Value.ComparableDouble(fc.OrganizeRubric.RubricType)) : false);
                            break;
                        case OperandType.EqualOrLess:
                            ex = (r => r[fc.OrganizeRubric.FigureFieldId] != null ?
                             fc.OrganizeRubric.RubricType == typeof(IUnique) || fc.OrganizeRubric.RubricType == typeof(string) || fc.OrganizeRubric.RubricType == typeof(DateTime) ?
                              r[fc.OrganizeRubric.FigureFieldId].ComparableInt64(fc.OrganizeRubric.RubricType) <= (Value.ComparableInt64(fc.OrganizeRubric.RubricType)) :
                            r[fc.OrganizeRubric.FigureFieldId].ComparableDouble(fc.OrganizeRubric.RubricType) <= (Value.ComparableDouble(fc.OrganizeRubric.RubricType)) : false);
                            break;
                        case OperandType.Less:
                            ex = (r => r[fc.OrganizeRubric.FigureFieldId] != null ?
                             fc.OrganizeRubric.RubricType == typeof(IUnique) || fc.OrganizeRubric.RubricType == typeof(string) || fc.OrganizeRubric.RubricType == typeof(DateTime) ?
                              r[fc.OrganizeRubric.FigureFieldId].ComparableInt64(fc.OrganizeRubric.RubricType) < (Value.ComparableInt64(fc.OrganizeRubric.RubricType)) :
                            r[fc.OrganizeRubric.FigureFieldId].ComparableDouble(fc.OrganizeRubric.RubricType) < (Value.ComparableDouble(fc.OrganizeRubric.RubricType)) : false);
                            break;
                        default:
                            break;
                    }
                }
                else if (Operand != OperandType.NotLike)
                    ex = (r => r[fc.OrganizeRubric.FigureFieldId] != null ?
                    Convert.ChangeType(r[fc.OrganizeRubric.FigureFieldId], fc.OrganizeRubric.RubricType).ToString()
                        .Contains(Convert.ChangeType(Value, fc.OrganizeRubric.RubricType).ToString()) : 
                            false);
                else
                    ex = (r => r[fc.OrganizeRubric.FigureFieldId] != null ?
                    !Convert.ChangeType(r[fc.OrganizeRubric.FigureFieldId], fc.OrganizeRubric.RubricType).ToString()
                        .Contains(Convert.ChangeType(Value, fc.OrganizeRubric.RubricType).ToString()) : 
                            false);
            }
            return ex;
        }
    }
}
