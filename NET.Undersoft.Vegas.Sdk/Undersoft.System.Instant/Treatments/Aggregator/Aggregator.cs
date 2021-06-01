﻿using System.Collections.Generic;
using System.Linq;
using System.Sets;
using System.Instant.Linking;
using System.Instant.Treatments;

namespace System.Instant.Treatments
{
    public static class Aggregator
    {
        //public static  IFigures Aggregate(this IFigures figures, bool onlyView = false)
        //{
        //    return;//Result(figures.View, onlyView);
        //}

        //private static IFigures Result(IFigures figures, bool onlyView)
        //{

        //    MemberRubric[] aggregateRubrics = figures.Treatment.AggregateRubrics.ToArray();
        //    if (aggregateRubrics.Length > 0)
        //    {
        //        HashSet<int> targetLinkIds = new HashSet<int>();

        //        aggregateRubrics.Where(j => j.AggregateIndex != null)
        //                          .Select(j => targetLinkIds
        //                            .Add(j.AggregateIndex[0])).ToArray();

        //        FigureLink[] allOrView = figures.Linkmap
        //                                                .TargetLinks.AsValues()
        //                                                    .Cast<FigureLink>()
        //                                                        .Where(d => d.Target.Figures.Query.Terms.Count > 0).ToArray();
        //        int[] ids = null;
        //        IDeck<LinkBranch> subresult;

        //        if (onlyView)
        //        {
        //            ids = allOrView.Select(r => figures.Linkmap.TargetLinks.IndexOf(r)).Where(id => targetLinkIds.Contains(id)).ToArray();
        //            subresult = figures.Linkmap.CreateTargetLinks(allOrView);
        //        }
        //        else
        //        {
        //            ids = targetLinkIds.ToArray();
        //            subresult = figures.Linkmap.CreateTargetLinks(figures.Linkmap.TargetLinks.AsValues().Cast<FigureLink>().ToArray());
        //        }

        //        if (subresult.Count > 0)
        //        {
        //            LinkNode[] targetBranches = subresult.Select((j, y) => j != null && ids.Contains(y) ? j : null).Cast<LinkNode>().ToArray();
        //            try
        //            {
        //                targetBranches.Select((o, y) => 
        //                aggregateRubrics.Where(s =>
        //                        (s.AggregateIndex != null) &&
        //                        (s.AggregatePattern != null &&
        //                        s.AggregateIndex[0] == y) &&
        //                        o.Targets.Count > 0).Select(s =>
        //                         o.Origins.First.Value[s.FigureFieldId] =

        //                         (s.AggregateOperand == AggregateOperand.Default &&
        //                            o.Origins.First.Value[s.FigureFieldId] == s.RubricType.GetDefault()) ?
        //                          o.Targets
        //                            .Select(f => f[s.AggregateOrdinal[0]])

        //                                .First() :

        //                         (s.AggregateOperand == AggregateOperand.Bind ||
        //                          s.AggregateOperand == AggregateOperand.First) ?
        //                          o.Targets
        //                            .Select(f => f[s.AggregateOrdinal[0]])

        //                                .First() :

        //                         (s.AggregateOperand == AggregateOperand.Last) ?
        //                          o.Targets
        //                            .Select(f => f[s.AggregateOrdinal[0]])

        //                                .Last() :

        //                         (s.AggregateOperand == AggregateOperand.Sum) ?
        //                          o.Targets
                                 
        //                                .Sum(f => f[s.AggregateOrdinal[0]] is DateTime ?
                                 
        //                         ((DateTime)f[s.AggregateOrdinal[0]]).ToOADate() :
        //                                Convert.ToDouble(f[s.AggregateOrdinal[0]])) :

        //                         (s.AggregateOperand == AggregateOperand.Min) ?
        //                         o.Targets

        //                                .Min(f => f[s.AggregateOrdinal[0]] is DateTime ?
                                 
        //                         ((DateTime)f[s.AggregateOrdinal[0]]).ToOADate() :
        //                                Convert.ToDouble(f[s.AggregateOrdinal[0]])) :

        //                         (s.AggregateOperand == AggregateOperand.Max) ?
        //                         o.Targets

        //                                .Max(f => f[s.AggregateOrdinal[0]] is DateTime ?

        //                            ((DateTime)f[s.AggregateOrdinal[0]]).ToOADate() :
        //                               Convert.ToDouble(f[s.AggregateOrdinal[0]])) :

        //                             (s.AggregateOperand == AggregateOperand.Count) ?
        //                         o.Targets

        //                                .Count() :

        //                            (s.AggregateOperand == AggregateOperand.Avg) ?
        //                               Convert.ChangeType(
        //                         o.Targets
                                 
        //                                .Average(f => 
                                        
        //                         f[s.AggregateOrdinal[0]] is DateTime ?
        //                                ((DateTime)f[s.AggregateOrdinal[0]]).ToOADate() :
        //                                    Convert.ToDouble(f[s.AggregateOrdinal[0]])), typeof(string)) :

        //                            (s.AggregateOperand == AggregateOperand.Bis) ?
        //                          o.Targets
        //                                .Select(f => (f[s.AggregateOrdinal[0]] != DBNull.Value) ?
        //                                        f[s.AggregateOrdinal[0]].ToString() : "")
                                           
        //                                .Aggregate((x, u) => x + " " + u) : ""

        //                          ).ToArray()).ToArray();
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //            return figures;
        //        }
        //    }
        //    return figures;
        //}
    }

    [Serializable]
    public enum AggregateOperand
    {
        None,
        Sum,
        Avg,
        Min,
        Max,
        Bis,
        First,
        Last,
        Bind,
        Count,
        Default
    }
}
