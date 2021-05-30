﻿namespace System.Instant.Treatments
{
    public interface IFilterTerm
    {
        string RubricName { get; set; }

        LogicType Logic { get; set; }

        OperandType Operand { get; set; }

        OrganizeStage Stage { get; set; }

        object Value { get; set; }
    }
}