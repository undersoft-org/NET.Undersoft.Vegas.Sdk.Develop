﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace System.Instant.Treatments
{
    [Serializable]
    public class FigureFilter
    {
        [NonSerialized] private FilterTerms termsBuffer;
        [NonSerialized] private FilterTerms termsReducer;
        [NonSerialized] private OrganizeExpression expression;
        [NonSerialized] private IFigures figures;
        [NonSerialized] public Func<IFigure, bool> Evaluator;

        public IFigures Figures
        { get { return figures; } set { figures = value; } }
        public FilterTerms Reducer
        { get; set; }
        public FilterTerms Terms
        { get; set; }

        public FigureFilter(IFigures figures)
        {
            this.figures = figures;
            expression = new OrganizeExpression();
            Reducer = new FilterTerms(figures);
            Terms = new FilterTerms(figures);
            termsBuffer = expression.Conditions;
            termsReducer = new FilterTerms(figures);
        }
        
        public Expression<Func<IFigure, bool>> GetExpression(int stage = 1)
        {
            termsReducer.Clear();
            termsReducer.Add(Reducer.AsEnumerable().Concat(Terms.AsEnumerable()).ToArray());
            expression.Conditions = termsReducer;
            termsBuffer = termsReducer;
            return expression.CreateExpression(stage);
        }

        public IFigure[] Organize(int stage = 1)
        {
            termsReducer.Clear();
            termsReducer.Add(Reducer.AsEnumerable().Concat(Terms.AsEnumerable()).ToArray());
            expression.Conditions = termsReducer;
            termsBuffer = termsReducer;
            return figures.AsEnumerable().AsQueryable().Where(expression.CreateExpression(stage).Compile()).ToArray();
        }
        public IFigure[] Organize(ICollection<IFigure> toOrganize, int stage = 1)
        {
            termsReducer.Clear();
            termsReducer.Add(Reducer.AsEnumerable().Concat(Terms.AsEnumerable()).ToArray());
            expression.Conditions = termsReducer;
            termsBuffer = termsReducer;
            return toOrganize.AsQueryable().Where(expression.CreateExpression(stage).Compile()).ToArray();
        }       
    }
}