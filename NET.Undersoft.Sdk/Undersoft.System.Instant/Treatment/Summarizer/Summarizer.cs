﻿using System.Linq;

namespace System.Instant.Treatment
{  
    public static class Summarizer
    {
        public static IFigure Summarize(this IFigures figures, bool onlyFiltered = false)
        {
            return Result(figures.Picked, onlyFiltered);
        }

        private static IFigure Result(IFigures figures, bool onlyFiltered = true)
        {
            IRubrics summaryRubrics = figures.Treatment.SummaryRubrics;
            if (summaryRubrics.Count > 0)
            {
                object[] result = summaryRubrics.AsValues().AsParallel().SelectMany(s =>
                       new object[]
                       {
                           (!string.IsNullOrEmpty(s.RubricName)) ?
                            (s.SummaryOperand == AggregateOperand.Sum) ?
                                Convert.ChangeType(figures
                                
                                .Sum
                                
                                (j => (j[s.SummaryOrdinal] is DateTime) ?
                                ((DateTime)j[s.SummaryOrdinal]).ToOADate() :
                                   Convert.ToDouble(j[s.FigureFieldId])), typeof(object)) :
                                (s.SummaryOperand == AggregateOperand.Min) ?
                                Convert.ChangeType(figures
                                
                                .Min
                                
                                (j => (j[s.SummaryOrdinal] is DateTime) ?
                                            ((DateTime)j[s.SummaryOrdinal]).ToOADate() :
                                                Convert.ToDouble(j[s.FigureFieldId])), typeof(object)) :
                                 (s.SummaryOperand == AggregateOperand.Max) ?
                                Convert.ChangeType(figures
                                
                                .Max
                                
                                (j => (j[s.SummaryOrdinal] is DateTime) ?
                                            ((DateTime)j[s.SummaryOrdinal]).ToOADate() :
                                                Convert.ToDouble(j[s.FigureFieldId])), typeof(object)) :
                                 (s.SummaryOperand == AggregateOperand.Avg) ?
                               Convert.ChangeType(figures
                               
                               .Average
                               
                               (j => (j[s.SummaryOrdinal] is DateTime) ?
                                            ((DateTime)j[s.SummaryOrdinal]).ToOADate() :
                                                Convert.ToDouble(j[s.FigureFieldId])), typeof(object)) :
                                 (s.SummaryOperand == AggregateOperand.Bis) ?
                               Convert.ChangeType(figures.Select(j => (j[s.FigureFieldId] != DBNull.Value) ? j[s.FigureFieldId].ToString() : "")
                               
                               .Aggregate((x, y) => x + " " + y), typeof(object)) : null : null
                            }
                 ).ToArray();

                figures.Summary.ValueArray = result;

                return figures.Summary;
            }
            else
                return null;
        }
    }  
 
}
