using System.Multemic;

namespace System.Instant
{
    public interface IRubrics : IUnique, IDeck<MemberRubric>
    {
        IFigures Figures { get; set; }
        IRubrics KeyRubrics { get; set; }
        FieldMappings Mappings { get; set; }

        int[] Ordinals { get; }

        void Update();

        byte[] GetBytes(IFigure figure);
        byte[] GetKeyBytes(IFigure figure, uint seed = 0);
        long GetHashKey(IFigure figure, uint seed = 0);
        void SetHashKey(IFigure figure, uint seed = 0);
    }
}