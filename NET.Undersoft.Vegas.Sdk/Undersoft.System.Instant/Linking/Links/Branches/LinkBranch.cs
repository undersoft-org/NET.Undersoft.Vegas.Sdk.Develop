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
    public class LinkBranch : CardList<ICard<IFigure>>, IUnique
    {
        #region Fields

        private ICard<ICard<IFigure>>[] cards;

        #endregion

        #region Constructors

        public LinkBranch(LinkMember member, ICard<IFigure> value) : base(5, HashBits.bit64)
        {
            Member = member;
            var card = NewCard(value);
            KeyBlock = card.IdentitiesToKey();
            InnerAdd(card);
        }
        public LinkBranch(LinkMember member, ICard<IFigure> value, int _cardSize) : base(_cardSize, HashBits.bit64)
        {
            Member = member;
            var card = NewCard(value);
            KeyBlock = card.IdentitiesToKey();
            InnerAdd(card);
        }
        public LinkBranch(LinkMember member, ICollection<ICard<IFigure>> collections, int _cardSize = 5) : base(_cardSize, HashBits.bit64)
        {
            Member = member;
            if (collections.Any())
            {
                var card = NewCard(collections.First());
                KeyBlock = card.IdentitiesToKey();
                InnerAdd(card);
            }
            foreach (var card in collections.Skip(1))
                InnerAdd(card);
        }
        public LinkBranch(LinkMember member, IEnumerable<ICard<IFigure>> collections, int _cardSize = 5) : base(_cardSize, HashBits.bit64)
        {
            Member = member;
            if (collections.Any())
            {
                var card = NewCard(collections.First());
                KeyBlock = card.IdentitiesToKey();
                InnerAdd(card);
            }
            foreach (var card in collections.Skip(1))
                InnerAdd(card);
        }

        #endregion

        #region Properties

        public ICard<ICard<IFigure>>[] Cards { get => cards; }

        public IUnique Empty => Usid.Empty;

        public long KeyBlock { get => SystemSerialCode.KeyBlock; set => SystemSerialCode.SetHashKey(value); }

        public LinkMember Member { get; set; }

        public uint SeedBlock { get => Member.SeedBlock; set => Member.SetHashSeed(value); }

        public Usid SystemSerialCode { get; set; }

        #endregion

        #region Methods

        public int CompareTo(IUnique other)
        {
            return SystemSerialCode.CompareTo(other);
        }

        public override ICard<ICard<IFigure>> EmptyCard()
        {
            return new LinkCard(Member);
        }

        public override ICard<ICard<IFigure>>[] EmptyCardTable(int size)
        {
            return new LinkCard[size];
        }

        public bool Equals(IUnique other)
        {
            return SystemSerialCode.Equals(other);
        }

        public byte[] GetBytes()
        {
            return SystemSerialCode.GetBytes();
        }

        public long GetHashKey()
        {
            return SystemSerialCode.GetHashKey();
        }

        public uint GetHashSeed()
        {
            return Member.GetHashSeed();
        }

        public byte[] GetKeyBytes()
        {
            return SystemSerialCode.GetKeyBytes();
        }

        public override ICard<ICard<IFigure>> NewCard(ICard<ICard<IFigure>> value)
        {
            return new LinkCard(value, Member);
        }

        public override ICard<ICard<IFigure>> NewCard(ICard<IFigure> value)
        {
            return new LinkCard(value, Member);
        }

        public override ICard<ICard<IFigure>> NewCard(long key, ICard<IFigure> value)
        {
            return new LinkCard(key, value, Member);
        }

        public override ICard<ICard<IFigure>> NewCard(object key, ICard<IFigure> value)
        {
            return new LinkCard(key, value, Member);
        }

        public void SetHashKey(long value)
        {
            SystemSerialCode.SetHashKey(value);
        }

        public void SetHashSeed(uint seed)
        {
            Member.SetHashSeed(seed);
        }

        protected override bool InnerAdd(ICard<IFigure> value)
        {
            var card = NewCard(value);
            if (KeyBlock == 0)
                KeyBlock = card.IdentitiesToKey();
            return InnerAdd(card);
        }

        protected override ICard<ICard<IFigure>> InnerPut(ICard<IFigure> value)
        {
            return InnerPut(NewCard(value));
        }

        #endregion
    }
}
