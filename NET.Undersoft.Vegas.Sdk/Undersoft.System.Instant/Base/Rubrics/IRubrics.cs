/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.IRubrics.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (28.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant
{
    using System.Multemic;

    public interface IRubrics : IUnique, IDeck<MemberRubric>
    {
        #region Properties

        IFigures Figures { get; set; }

        IRubrics KeyRubrics { get; set; }

        FieldMappings Mappings { get; set; }

        int[] Ordinals { get; }

        #endregion

        #region Methods

        byte[] GetBytes(IFigure figure);

        long GetHashKey(IFigure figure, uint seed = 0);

        byte[] GetKeyBytes(IFigure figure, uint seed = 0);

        void SetHashKey(IFigure figure, uint seed = 0);

        void Update();

        #endregion
    }
}
