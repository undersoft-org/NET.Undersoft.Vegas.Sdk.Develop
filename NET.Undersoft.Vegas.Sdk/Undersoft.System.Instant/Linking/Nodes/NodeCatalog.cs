/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.LinkNodes.cs
   
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
    public class NodeCatalog : SharedMassAlbum<BranchDeck>
    {
        #region Constructors

        public NodeCatalog(int capacity = 17) : base(capacity)
        {
        }
        public NodeCatalog(Links links, int capacity = 17) : base(capacity)
        {
            Links = links;
        }

        #endregion

        #region Properties

        public Links Links { get; set; }

        #endregion

        #region Methods

        public override ICard<BranchDeck> EmptyCard()
        {
            return new NodeCard();
        }

        public override ICard<BranchDeck>[] EmptyCardList(int size)
        {
            return new NodeCard[size];
        }

        public override ICard<BranchDeck>[] EmptyCardTable(int size)
        {
            return new NodeCard[size];
        }

        public override ICard<BranchDeck> NewCard(ICard<BranchDeck> card)
        {
            return new NodeCard(card);
        }

        public override ICard<BranchDeck> NewCard(BranchDeck card)
        {
            return new NodeCard(card);
        }

        public override ICard<BranchDeck> NewCard(long key, BranchDeck value)
        {
            return new NodeCard(key, value);
        }

        public override ICard<BranchDeck> NewCard(object key, BranchDeck value)
        {
            return new NodeCard(key, value);
        }

        #endregion
    }
}
