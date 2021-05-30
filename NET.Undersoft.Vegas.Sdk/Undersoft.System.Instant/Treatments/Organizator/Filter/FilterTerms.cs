﻿/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.FilterTerms.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Treatments
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    #region Enums

    [Serializable]
    public enum OperandType
    {
        Equal,
        EqualOrMore,
        EqualOrLess,
        More,
        Less,
        Like,
        NotLike,
        Contains,
        None
    }
    [Serializable]
    public enum LogicType
    {
        And,
        Or
    }
    [Serializable]
    public enum OrganizeStage
    {
        None,
        First,
        Second,
        Third
    }

    #endregion

    [Serializable]
    public class FilterTerm : ICloneable, IFilterTerm
    {
        #region Fields

        public string valueTypeName;
        [NonSerialized] private IFigures figures;
        [NonSerialized] private Type valueType;

        #endregion

        #region Constructors

        public FilterTerm()
        {
            Stage = OrganizeStage.First;
        }
        public FilterTerm(IFigures figures)
        {
            Stage = OrganizeStage.First;
            this.figures = figures;
        }
        public FilterTerm(IFigures figures, string filterColumn, string operand, object value, string logic = "And", int stage = 1)
        {
            RubricName = filterColumn;
            OperandType tempOperand1;
            Enum.TryParse(operand, true, out tempOperand1);
            Operand = tempOperand1;
            Value = value;
            LogicType tempLogic;
            Enum.TryParse(logic, true, out tempLogic);
            Logic = tempLogic;
            this.figures = figures;
            if (figures != null)
            {
                MemberRubric[] filterRubrics = this.figures.Rubrics.AsValues().Where(c => c.RubricName == RubricName).ToArray();
                if (filterRubrics.Length > 0)
                {
                    OrganizeRubric = filterRubrics[0]; ValueType = OrganizeRubric.RubricType;
                }
            }
            Stage = (OrganizeStage)Enum.ToObject(typeof(OrganizeStage), stage);
        }
        public FilterTerm(MemberRubric filterColumn, OperandType operand, object value, LogicType logic = LogicType.And, OrganizeStage stage = OrganizeStage.First)
        {
            Operand = operand;
            Value = value;
            Logic = logic;
            ValueType = filterColumn.RubricType;
            RubricName = filterColumn.RubricName;
            OrganizeRubric = filterColumn;
            Stage = stage;
        }
        public FilterTerm(string filterColumn, OperandType operand, object value, LogicType logic = LogicType.And, OrganizeStage stage = OrganizeStage.First)
        {
            RubricName = filterColumn;
            Operand = operand;
            Value = value;
            Logic = logic;
            Stage = stage;
        }
        public FilterTerm(string filterColumn, string operand, object value, string logic = "And", int stage = 1)
        {
            RubricName = filterColumn;
            OperandType tempOperand1;
            Enum.TryParse(operand, true, out tempOperand1);
            Operand = tempOperand1;
            Value = value;
            LogicType tempLogic;
            Enum.TryParse(logic, true, out tempLogic);
            Logic = tempLogic;
            Stage = (OrganizeStage)Enum.ToObject(typeof(OrganizeStage), stage);
        }

        #endregion

        #region Properties

        public IFigures Figures
        {
            get
            {
                return figures;
            }
            set
            {
                figures = value;
                if (OrganizeRubric == null && value != null)
                {
                    MemberRubric[] filterRubrics = figures.Rubrics.AsValues()
                             .Where(c => c.RubricName == RubricName).ToArray();
                    if (filterRubrics.Length > 0)
                    {
                        OrganizeRubric = filterRubrics[0];
                        ValueType = OrganizeRubric.RubricType;
                    }
                }
            }
        }

        public MemberRubric OrganizeRubric { get; set; }

        [DisplayName("Pos")]
        public int Index { get; set; }

        public LogicType Logic { get; set; }

        public OperandType Operand { get; set; }

        public string RubricName { get; set; }

        public OrganizeStage Stage { get; set; } = OrganizeStage.First;

        public object Value { get; set; }

        public Type ValueType
        {
            get
            {
                if (valueType == null && valueTypeName != null)
                    valueType = Type.GetType(valueTypeName);
                return valueType;
            }
            set
            {
                valueType = value;
                valueTypeName = value.FullName;
            }
        }

        #endregion

        #region Methods

        public object Clone()
        {
            FilterTerm clone = (FilterTerm)this.MemberwiseClone();
            clone.OrganizeRubric = OrganizeRubric;
            return clone;
        }

        public FilterTerm Clone(object value)
        {
            FilterTerm clone = (FilterTerm)this.MemberwiseClone();
            clone.OrganizeRubric = OrganizeRubric;
            clone.Value = value;
            return clone;
        }

        public bool Compare(FilterTerm term)
        {
            if (RubricName != term.RubricName)
                return false;
            if (!Value.Equals(term.Value))
                return false;
            if (!Operand.Equals(term.Operand))
                return false;
            if (!Stage.Equals(term.Stage))
                return false;
            if (!Logic.Equals(term.Logic))
                return false;

            return true;
        }

        #endregion
    }

    [Serializable]
    public class FilterTerms : Collection<FilterTerm>, ICollection
    {
        #region Fields

        [NonSerialized] private IFigures figures;

        #endregion

        #region Constructors

        public FilterTerms()
        {
        }
        public FilterTerms(IFigures figures)
        {
            this.Figures = figures;
        }

        #endregion

        #region Properties

        public IFigures Figures
        {
            get { return figures; }
            set { figures = value; }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        #endregion

        #region Methods

        public new int Add(FilterTerm value)
        {
            value.Figures = figures;
            value.Index = ((IList)this).Add(value);
            return value.Index;
        }

        public void Add(IFilterTerm item)
        {
            Add(new FilterTerm(item.RubricName, item.Operand, item.Value, item.Logic, item.Stage));
        }

        public object AddNew()
        {
            return (object)((IBindingList)this).AddNew();
        }

        public void Renew(ICollection<FilterTerm> terms)
        {
            bool diffs = false;
            if (Count != terms.Count)
            {
                diffs = true;
            }
            else
            {
                foreach (FilterTerm term in terms)
                {
                    if (Contains(term.RubricName))
                    {
                        int same = 0;
                        foreach (FilterTerm myterm in Get(term.RubricName))
                        {
                            if (!myterm.Compare(term))
                                same++;
                        }
                        if (same != 0)
                        {
                            diffs = true;
                            break;
                        }
                    }
                    else
                    {
                        diffs = true;
                        break;
                    }
                }
            }

            if (diffs)
            {
                Clear();
                foreach (FilterTerm term in terms)
                    Add(term);
            }
        }

        public void Add(ICollection<FilterTerm> terms)
        {
            foreach (FilterTerm term in terms)
            {
                term.Figures = Figures;
                term.Index = Add(term);
            }
        }

        public FilterTerms Clone()
        {
            FilterTerms ft = new FilterTerms();
            foreach (FilterTerm t in this)
            {
                FilterTerm _t = new FilterTerm(t.RubricName, t.Operand, t.Value, t.Logic, t.Stage);
                ft.Add(_t);
            }
            return ft;
        }

        public bool Contains(IFilterTerm item)
        {
            return Contains(new FilterTerm(item.RubricName, item.Operand, item.Value, item.Logic, item.Stage));
        }

        public void CopyTo(IFilterTerm[] array, int arrayIndex)
        {
            Array.Copy(this.AsQueryable().Cast<IFilterTerm>().ToArray(), array, Count);
        }

        public FilterTerm Find(FilterTerm data)
        {
            foreach (FilterTerm lDetailValue in this)
                if (lDetailValue == data)    // Found it
                    return lDetailValue;
            return null;    // Not found
        }

        public List<FilterTerm> Get(int stage)
        {
            OrganizeStage filterStage = (OrganizeStage)Enum.ToObject(typeof(OrganizeStage), stage);
            return this.AsEnumerable().Where(c => filterStage.Equals(c.Stage)).ToList();
        }

        public List<FilterTerm> Get(List<string> RubricNames)
        {
            return this.AsEnumerable().Where(c => RubricNames.Contains(c.OrganizeRubric.RubricName)).ToList();
        }

        public FilterTerm[] Get(string RubricName)
        {
            return this.AsEnumerable().Where(c => c.RubricName == RubricName).ToArray();
        }

        public bool Contains(string RubricName)
        {
            return this.AsEnumerable().Where(c => c.RubricName == RubricName).Any();
        }

        public int IndexOf(object value)
        {
            for (int i = 0; i < Count; i++)
                if (ReferenceEquals(this[i], value))    // Found it
                    return i;
            return -1;
        }

        public bool Remove(IFilterTerm item)
        {
            return Remove(new FilterTerm(item.RubricName, item.Operand, item.Value, item.Logic, item.Stage));
        }

        public void Remove(ICollection<FilterTerm> value)
        {
            foreach (FilterTerm term in value)
                Remove(term);
        }

        public void Reset()
        {
            this.Clear();
        }

        public void SetRange(FilterTerm[] data)
        {
            for (int i = 0; i < data.Length; i++)
                this[i] = data[i];
        }

        #endregion
    }

    public static class FilterOperand
    {
        public static string StringOperand(OperandType operand)
        {
            string operandString = "";
            switch (operand)
            {
                case OperandType.Equal:
                    operandString = "=";
                    break;
                case OperandType.EqualOrMore:
                    operandString = ">=";
                    break;
                case OperandType.More:
                    operandString = ">";
                    break;
                case OperandType.EqualOrLess:
                    operandString = "<=";
                    break;
                case OperandType.Less:
                    operandString = "<";
                    break;
                case OperandType.Like:
                    operandString = "like";
                    break;
                case OperandType.NotLike:
                    operandString = "!like";
                    break;
                default:
                    operandString = "=";
                    break;
            }
            return operandString;
        }

        public static OperandType ParseOperand(string operandString)
        {
            OperandType _operand = OperandType.None;
            switch (operandString)
            {
                case "=":
                    _operand = OperandType.Equal;
                    break;
                case ">=":
                    _operand = OperandType.EqualOrMore;
                    break;
                case ">":
                    _operand = OperandType.More;
                    break;
                case "<=":
                    _operand = OperandType.EqualOrLess;
                    break;
                case "<":
                    _operand = OperandType.Less;
                    break;
                case "like":
                    _operand = OperandType.Like;
                    break;
                case "!like":
                    _operand = OperandType.NotLike;
                    break;
                default:
                    _operand = OperandType.None;
                    break;
            }
            return _operand;
        }
    }
}