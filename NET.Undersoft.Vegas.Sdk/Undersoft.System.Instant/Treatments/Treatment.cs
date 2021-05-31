using System.Linq;
using System.Instant.Linking;

namespace System.Instant.Treatments
{
    public class Treatment
    {
        private IFigures figures;

        public Treatment(IFigures Figures)
        {
            figures = Figures;
        }

        private MemberRubrics  replicativeRubrics;
        public  MemberRubrics  ReplicativeRubrics
        {
            get
            {
                if (replicativeRubrics == null)
                {
                    if (replicativeRubrics == null)
                        UpdateAggregatives();
                    else
                        UpdateReplicatives();
                }
                return replicativeRubrics;
            }
        }

        public MemberRubrics   UpdateReplicatives()
        {
            replicativeRubrics = new MemberRubrics();
            replicativeRubrics.Put(aggregativeRubrics.AsValues().Where(p => p.AggregateOperand == AggregateOperand.Bind));
            return replicativeRubrics;
        }

        private MemberRubrics  aggregativeRubrics;
        public  MemberRubrics  AggregativeRubrics
        {
            get
            {
                if (aggregativeRubrics == null)
                {
                    UpdateAggregatives();
                }
                return aggregativeRubrics;
            }
        }

        public MemberRubrics   UpdateAggregatives()
        {
            AggregateOperand parsed = new AggregateOperand();
            Links targetLinks = figures.Linker.Links;
            aggregativeRubrics = new MemberRubrics();
            MemberRubric[] _aggregateRubrics = figures.Rubrics.AsValues()
                                                               .Where(c => (c.RubricName.Split('#').Length > 1) ||
                                                                  (c.AggregatePattern != null &&
                                                                  c.AggregateOperand != AggregateOperand.None) ||
                                                                  c.AggregateOperand != AggregateOperand.None).ToArray();
            foreach (MemberRubric c in _aggregateRubrics)
            {
                c.AggregatePattern = (c.AggregatePattern != null) ? c.AggregatePattern : (c.AggregateOperand != AggregateOperand.None) ? new MemberRubric(c) { RubricName = c.RubricName } : new MemberRubric(c) { RubricName = c.RubricName.Split('#')[1] };
                c.AggregateOperand = c.AggregateOperand != AggregateOperand.None ? c.AggregateOperand : (Enum.TryParse(c.RubricName.Split('#')[0], true, out parsed)) ? parsed : AggregateOperand.None;
                c.AggregateIndex = (targetLinks.Cast<Link>().Where(cr => cr.Target.Figures.Rubrics.AsValues()
                                              .Where(ct => ct.RubricName == ((c.AggregatePattern != null) ?
                                              c.AggregatePattern.RubricName :
                                              c.RubricName.Split('#')[1])).Any()).Any()) ?
                             targetLinks.Cast<Link>().Where(cr => cr.Target.Figures.Rubrics.AsValues()
                                              .Where(ct => ct.RubricName == ((c.AggregatePattern != null) ?
                                              c.AggregatePattern.RubricName :
                                              c.RubricName.Split('#')[1])).Any()).ToArray().Select(ix => targetLinks.IndexOf(ix)).ToArray()
                                              : null;
                c.AggregateOrdinal = targetLinks.Cast<Link>().Where(cr => cr.Target.Figures.Rubrics.AsValues()
                                    .Where(ct => ct.RubricName == ((c.AggregatePattern != null) ?
                                     c.AggregatePattern.RubricName :
                                     c.RubricName.Split('#')[1])).Any())
                                     .Select(cr => cr.Target.Figures.Rubrics.AsValues()
                                    .Where(ct => ct.RubricName == ((c.AggregatePattern != null) ?
                                     c.AggregatePattern.RubricName :
                                     c.RubricName.Split('#')[1]))
                                     .Select(o => o.RubricId).FirstOrDefault()).ToArray();
            }

            aggregativeRubrics.Put(_aggregateRubrics);
            aggregativeRubrics.AsValues().Where(j => j.AggregateIndex != null).Select(p => p.AggregateLinks = new Links(targetLinks.Cast<Link>().Where((x, y) => p.AggregateIndex.Contains(y)).ToArray()));

            UpdateReplicatives();

            return aggregativeRubrics;
        }

        private MemberRubrics  summaryRubrics;
        public  IRubrics       SummaryRubrics
        {
            get
            {
                if (summaryRubrics == null)
                {
                    UpdateSummatives();
                }
                return summaryRubrics;
            }
        }

        public  MemberRubrics  UpdateSummatives()
        {
            AggregateOperand parsed = new AggregateOperand();
            summaryRubrics = new MemberRubrics();
            Figure summaryFigure = new Figure(figures.Rubrics.AsValues().Where(c =>
                                               (c.RubricName.Split('=').Length > 1 || 
                                               (c.SummaryOperand != AggregateOperand.None))).Select(c =>
                                               (new MemberRubric(c)
                                               {
                                                   SummaryPattern = (c.SummaryPattern != null) ? c.SummaryPattern :
                                                                    (c.RubricName.Split('=').Length > 1) ?
                                                                    new MemberRubric(c) { RubricName = c.RubricName.Split('=')[1] } : null,
                                                   SummaryOperand = (Enum.TryParse(c.RubricName.Split('=')[0], true, out parsed)) ? parsed : c.SummaryOperand
                                               })).ToArray(), "Summary_" + figures.GetType().Name);
            figures.Summary = summaryFigure.Generate();
            summaryRubrics = (MemberRubrics)summaryFigure.Rubrics;
            return summaryRubrics;
        }
    }
}