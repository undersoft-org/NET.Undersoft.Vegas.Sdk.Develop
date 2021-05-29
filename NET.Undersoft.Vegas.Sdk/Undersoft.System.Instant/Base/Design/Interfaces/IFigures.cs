using System.Multemic;
using System.Instant.Treatments;
using System.Instant.Linking;

namespace System.Instant
{
    public interface IFigures : IDeck<IFigure>, IFigure, IFigureFormatter
    {
        IInstant Instant { get; set; }

        bool Prime { get; set; } 

        new IFigure this[int index] { get; set; }

        object this[int index, string propertyName] { get; set; }

        object this[int index, int fieldId] { get; set; }        

        ICard<IFigure>[] Cards { get; } 

        IRubrics Rubrics { get; set; }

        IRubrics KeyRubrics { get; set; }

        IFigure NewFigure();

        int Length { get; }

        Type FigureType { get; set; }

        int FigureSize { get; set; }

        Type Type { get; set; }

        IFigures Picked { get; set; }

        IFigure Summary { get; set; }

        FigureFilter Filter { get; set; }

        FigureSort Sort { get; set; }

        Func<IFigure, bool> Picker { get; set; }

        Treatment Treatment { get; set; }

        Links Links { get; set; }

        IDeck<IComputation> Computations { get; set; }
    }
}