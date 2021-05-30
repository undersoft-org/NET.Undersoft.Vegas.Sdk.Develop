/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.LinkBranches.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Linking
{
    using System.Multemic;

    [Serializable]
    public class LinkBranches : SharedMultiAlbum<LinkBranch>
    {
        #region Constructors

        public LinkBranches(int capacity = 17) : base(capacity)
        {
        }
        public LinkBranches(Links links, int capacity = 17) : base(capacity)
        {
            Links = links;
        }

        #endregion

        #region Properties

        public Links Links { get; set; }

        #endregion

        #region Methods

        public override ICard<LinkBranch> EmptyCard()
        {
            return new BranchCard();
        }

        public override ICard<LinkBranch>[] EmptyCardList(int size)
        {
            return new BranchCard[size];
        }

        public override ICard<LinkBranch>[] EmptyCardTable(int size)
        {
            return new BranchCard[size];
        }

        public override ICard<LinkBranch> NewCard(ICard<LinkBranch> card)
        {
            return new BranchCard(card);
        }

        public override ICard<LinkBranch> NewCard(LinkBranch card)
        {
            return new BranchCard(card);
        }

        public override ICard<LinkBranch> NewCard(long key, LinkBranch value)
        {
            return new BranchCard(key, value);
        }

        public override ICard<LinkBranch> NewCard(object key, LinkBranch value)
        {
            return new BranchCard(key, value);
        }

        #endregion
    }
}
