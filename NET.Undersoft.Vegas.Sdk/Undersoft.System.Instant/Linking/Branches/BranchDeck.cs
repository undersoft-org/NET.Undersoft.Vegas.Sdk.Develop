/*************************************************
   Copyright (c) 2021 Undersoft

   LinkBranch.cs
              
   @author: Dariusz Hanc                                                  
   @date: (28.05.2021)                                            
   @licence MIT                                       
 *************************************************/

namespace System.Instant.Linking
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Multemic;
    using System.Uniques;

    [Serializable]
    public class BranchDeck : CardList<ICard<IFigure>>, IUnique
    {
        #region Fields

        private ICard<ICard<IFigure>>[] cards;
        private Usid serialcode;

        #endregion

        #region Constructors

        public BranchDeck(LinkMember member, ICard<IFigure> value) : base(5, HashBits.bit64)
        {
            Member = member;
            var card = NewCard(value);
            UniqueKey = card.UniquesAsKey();
            InnerAdd(card);
        }
        public BranchDeck(LinkMember member, ICard<IFigure> value, int _cardSize) : base(_cardSize, HashBits.bit64)
        {
            Member = member;
            var card = NewCard(value);
            UniqueKey = card.UniquesAsKey();
            InnerAdd(card);
        }
        public BranchDeck(LinkMember member, ICollection<ICard<IFigure>> collections, int _cardSize = 5) : base(_cardSize, HashBits.bit64)
        {
            Member = member;
            if (collections.Any())
            {
                var card = NewCard(collections.First());
                UniqueKey = card.UniquesAsKey();
                InnerAdd(card);
            }
            foreach (var card in collections.Skip(1))
                InnerAdd(card);
        }
        public BranchDeck(LinkMember member, IEnumerable<ICard<IFigure>> collections, int _cardSize = 5) : base(_cardSize, HashBits.bit64)
        {
            Member = member;
            if (collections.Any())
            {
                var card = NewCard(collections.First());
                UniqueKey = card.UniquesAsKey();
                InnerAdd(card);
            }
            foreach (var card in collections.Skip(1))
                InnerAdd(card);
        }

        #endregion

        #region Properties

        public ICard<ICard<IFigure>>[] Cards { get => cards; }

        public IUnique Empty => Usid.Empty;

        public new long UniqueKey { get => serialcode.UniqueKey; set => serialcode.UniqueKey = value; }

        public LinkMember Member { get; set; }

        public uint UniqueSeed { get => Member.UniqueSeed; set => Member.UniqueSeed = value; }

        public Usid SerialCode { get => serialcode; set => serialcode = value; }

        #endregion

        #region Methods

        public int CompareTo(IUnique other)
        {
            return SerialCode.CompareTo(other);
        }

        public override ICard<ICard<IFigure>> EmptyCard()
        {
            return new BranchCard(Member);
        }

        public override ICard<ICard<IFigure>>[] EmptyCardTable(int size)
        {
            return new BranchCard[size];
        }

        public bool Equals(IUnique other)
        {
            return SerialCode.Equals(other);
        }

        public byte[] GetBytes()
        {
            return SerialCode.GetBytes();
        }

        public byte[] GetUniqueBytes()
        {
            return SerialCode.GetUniqueBytes();
        }

        public override ICard<ICard<IFigure>> NewCard(ICard<ICard<IFigure>> value)
        {
            return new BranchCard(value, Member);
        }

        public override ICard<ICard<IFigure>> NewCard(ICard<IFigure> value)
        {
            return new BranchCard(value, Member);
        }

        public override ICard<ICard<IFigure>> NewCard(long key, ICard<IFigure> value)
        {
            return new BranchCard(key, value, Member);
        }

        public override ICard<ICard<IFigure>> NewCard(object key, ICard<IFigure> value)
        {
            return new BranchCard(key, value, Member);
        }

        protected override bool InnerAdd(ICard<IFigure> value)
        {
            var card = NewCard(value);
            if (UniqueKey == 0)
                UniqueKey = card.UniquesAsKey();
            return InnerAdd(card);
        }

        protected override ICard<ICard<IFigure>> InnerPut(ICard<IFigure> value)
        {
            return InnerPut(NewCard(value));
        }

        #endregion
    }
}
