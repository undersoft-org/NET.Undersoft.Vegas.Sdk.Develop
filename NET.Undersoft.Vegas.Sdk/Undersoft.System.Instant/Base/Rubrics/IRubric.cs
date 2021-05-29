﻿using System.Instant.Linking;
using System.Instant.Treatments;

namespace System.Instant
{   
    public interface IRubric : IMemberRubric, IUnique
    {     
        short IdentityOrder { get; set; }
        bool Required { get; set; }
        bool IsKey { get; set; }
        bool IsIdentity { get; set; }
        bool IsAutoincrement { get; set; }
        bool IsDBNull { get; set; }
        bool IsColossus { get; set; }

        IRubric AggregatePattern { get; set; }
        AggregateOperand AggregateOperand { get; set; }
        int[] AggregateIndex { get; set; }
        int[] AggregateOrdinal { get; set; }

        Links AggregateLinks { get; set; }

        int SummaryOrdinal { get; set; }
        IRubric SummaryPattern { get; set; }
        AggregateOperand SummaryOperand { get; set; }
    }
}
