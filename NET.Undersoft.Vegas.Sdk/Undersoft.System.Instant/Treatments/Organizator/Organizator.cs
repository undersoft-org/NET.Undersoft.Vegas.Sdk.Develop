/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Organizeator.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Treatments
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Organizator
    {
        #region Methods

        public static IFigure[] Organize(this IFigure[] figureArray, Func<IFigure, bool> evaluator)
        {
            return figureArray.Where(evaluator).ToArray();
        }

        public static IFigures Organize(this IFigures figures, Func<IFigure, bool> evaluator)
        {
            IFigures view = figures.Organized = (IFigures)figures.Type.New();
            view.Add(figures.AsEnumerable().AsQueryable().Where(evaluator));
            return view;
        }

        public static IFigures Organize(this IFigures figures, IFigure[] appendfigures, int stage = 1)
        {
            FigureFilter Filter = figures.Filter;
            FigureSort Sort = figures.Sort;
            return ResolveOrganizing(figures, Filter, Sort, stage, appendfigures);
        }

        public static IFigures Organize(this IFigures figures, int stage = 1, FilterTerms filter = null, SortTerms sort = null, bool saveonly = false, bool clearonend = false)
        {
            FigureFilter Filter = figures.Filter;
            FigureSort Sort = figures.Sort;

            if (filter != null)
            {
                Filter.Terms.Renew(filter.AsEnumerable().ToArray());
            }
            if (sort != null)
            {
                Sort.Terms.Renew(sort.AsEnumerable().ToArray());
            }
            if (!saveonly)
            {
                IFigures result = ResolveOrganizing(figures, Filter, Sort, stage);
                if (clearonend)
                {
                    figures.Filter.Terms.Clear();
                    figures.Filter.Evaluator = null;
                    figures.Organized.Organizer = null;
                }
                return result;
            }
            return null;
        }

        public static IFigures Organize(this IFigures figures, List<FilterTerm> filterList, List<SortTerm> sortList, bool saveonly = false, bool clearonend = false, int stage = 1)
        {
            FigureFilter Filter = figures.Filter;
            FigureSort Sort = figures.Sort;
            if (filterList != null)
            {
                Filter.Terms.Renew(filterList);
            }
            if (sortList != null)
            {
                Sort.Terms.Renew(sortList);
            }
            if (!saveonly)
            {
                IFigures result = ResolveOrganizing(figures, Filter, Sort, stage);
                if (clearonend)
                {
                    figures.Filter.Terms.Clear();
                    figures.Filter.Evaluator = null;
                    figures.Organized.Organizer = null;
                }
                return result;
            }
            return null;
        }

        public static IFigures Organize(this IFigures figures, out bool sorted, out bool filtered, int stage = 1)
        {
            FigureFilter Filter = figures.Filter;
            FigureSort Sort = figures.Sort;

            filtered = (Filter.Terms.Count > 0) ? true : false;
            sorted = (Sort.Terms.Count > 0) ? true : false;
            return ResolveOrganizing(figures, Filter, Sort, stage);
        }

        private static IFigures ExecuteOrganizing(IFigures figures, FigureFilter filter, FigureSort sort, int stage = 1, IFigure[] appendfigures = null)
        {
            IFigures table = figures;
            IFigures _figures = null;
            IFigures view = figures.Organized;

            if (appendfigures == null)
                if (stage > 1)
                    _figures = view;
                else if (stage < 0)
                {
                    _figures = figures;
                    view = figures.Organized = (IFigures)figures.Type.New();
                }
                else
                {
                    _figures = table;
                }

            if (filter != null && filter.Terms.Count > 0)
            {
                filter.Evaluator = filter.GetExpression(stage).Compile();
                view.Organizer = filter.Evaluator;

                if (sort != null && sort.Terms.Count > 0)
                {
                    bool isFirst = true;
                    IEnumerable<IFigure> tsrt = null;
                    IOrderedQueryable<IFigure> ttby = null;
                    if (appendfigures != null)
                        tsrt = appendfigures.AsEnumerable().Where(filter.Evaluator);
                    else
                        tsrt = _figures.AsEnumerable().Where(filter.Evaluator);

                    foreach (SortTerm fcs in sort.Terms)
                    {
                        if (isFirst)
                            ttby = tsrt.AsQueryable().OrderBy(o => o[fcs.RubricName], fcs.Direction, Comparer<object>.Default);
                        else
                            ttby = ttby.ThenBy(o => o[fcs.RubricName], fcs.Direction, Comparer<object>.Default);
                        isFirst = false;
                    }

                    if (appendfigures != null)
                        view.Add(ttby.ToArray());
                    else
                    {
                        view.Clear();
                        view.Add(ttby.ToArray());
                    }

                }
                else
                {
                    if (appendfigures != null)
                        view.Add(appendfigures.AsQueryable().Where(filter.Evaluator).ToArray());
                    else
                    {
                        view.Clear();
                        view.Add(figures.AsEnumerable().AsQueryable().Where(filter.Evaluator).ToArray());
                    }
                }
            }
            else if (sort != null && sort.Terms.Count > 0)
            {
                view.Organizer = null;
                view.Filter.Evaluator = null;

                bool isFirst = true;
                IOrderedQueryable<IFigure> ttby = null;

                foreach (SortTerm fcs in sort.Terms)
                {
                    if (isFirst)
                        if (appendfigures != null)
                            ttby = appendfigures.AsQueryable().OrderBy(o => o[fcs.RubricName], fcs.Direction, Comparer<object>.Default);
                        else
                            ttby = _figures.AsEnumerable().AsQueryable().OrderBy(o => o[fcs.RubricName], fcs.Direction, Comparer<object>.Default);
                    else
                        ttby = ttby.ThenBy(o => o[fcs.RubricName], fcs.Direction, Comparer<object>.Default);

                    isFirst = false;
                }

                if (appendfigures != null)
                    view.Add(ttby);
                else
                    view.Add(ttby);
            }
            else
            {
                if (stage < 2)
                {
                    view.Organizer = null;
                    view.Filter.Evaluator = null;
                }

                if (appendfigures != null)
                    view.Add(appendfigures);
                else
                    view.Add(figures);
            }

            //  view.PagingDetails.ComputePageCount(view.Count);
            if (stage > 0)
            {
                table.Organized = view;
            }
            return view;
        }

        private static IFigures ResolveOrganizing(IFigures figures, FigureFilter Filter, FigureSort Sort, int stage = 1, IFigure[] appendfigures = null)
        {
            OrganizeStage filterStage = (OrganizeStage)Enum.ToObject(typeof(OrganizeStage), stage);
            int filtercount = Filter.Terms.AsEnumerable().Where(f => f.Stage.Equals(filterStage)).ToArray().Length;
            int sortcount = Sort.Terms.Count;

            if (filtercount > 0)
                if (sortcount > 0)
                    return ExecuteOrganizing(figures, Filter, Sort, stage, appendfigures);
                else
                    return ExecuteOrganizing(figures, Filter, null, stage, appendfigures);
            else if (sortcount > 0)
                return ExecuteOrganizing(figures, null, Sort, stage, appendfigures);
            else
                return ExecuteOrganizing(figures, null, null, stage, appendfigures);
        }

        #endregion
    }
}
