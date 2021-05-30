/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Links.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Linking
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Multemic;
    using System.Uniques;

    #region Enums

    public enum LinkSite
    {
        Origin,
        Target
    }

    #endregion

    public class Links : SharedAlbum<Link>, IUnique
    {
        public Links()
        {
        }
        public Links(ICollection<Link> links)
        {
            links.Select(l => l.Links = this).ToArray();
            Add(links);
        }

        public Link this[string linkName]
        {
            get
            {
                return base[linkName];
            }
            set
            {
                value.Links = this;
                base[linkName] = value;
            }
        }
        public new Link this[int linkid]
        {
            get
            {
                return base[linkid];
            }
            set
            {
                value.Links = this;
                base[linkid] = value;
            }
        }

        public ICollection<Link> Collect(ICollection<string> LinkNames = null)
        {
            if (LinkNames != null)
                return LinkNames.Select(l => this[l]).Where(f => f != null).ToArray();
            else
                return this.Cast<Link>().ToArray();
        }
        public ICollection<Link> Collect(ICollection<Link> links)
        {
            if (links != null)
                return links.Select(l => this[l.Name]).Where(f => f != null).ToArray();
            else
                return this.Cast<Link>().ToArray();
        }

        public Link[] GetTarget(string TargetName)
        {
            return AsValues().Where(c => c.TargetName == TargetName).ToArray();
        }
        public Link[] GetOrigin(string OriginName)
        {
            return AsValues().Where(c => c.OriginName == OriginName).ToArray();
        }

        public override ICard<Link> EmptyCard()
        {
            return new Card64<Link>();
        }

        public override ICard<Link> NewCard(long key, Link value)
        {
            return new Card64<Link>(key, value);
        }
        public override ICard<Link> NewCard(object key, Link value)
        {
            return new Card64<Link>(key, value);
        }
        public override ICard<Link> NewCard(ICard<Link> value)
        {
            return new Card64<Link>(value);
        }
        public override ICard<Link> NewCard(Link value)
        {
            return new Card64<Link>(value);
        }

        public override ICard<Link>[] EmptyCardTable(int size)
        {
            return new Card64<Link>[size];
        }

        public override ICard<Link>[] EmptyCardList(int size)
        {
            cards = new Card64<Link>[size];
            return cards;
        }

        private ICard<Link>[] cards;
        public ICard<Link>[] Cards { get => cards; }

        protected override bool InnerAdd(Link value)
        {
            value.Links = this;
            return InnerAdd(NewCard(value));
        }

        protected override ICard<Link> InnerPut(Link value)
        {
            value.Links = this;
            return InnerPut(NewCard(value));
        }

        public Ussn SystemSerialCode
        { get; set; }

        public IUnique Empty => Ussn.Empty;

        public long UniqueKey
        { get => SystemSerialCode.UniqueKey; set => SystemSerialCode.SetUniqueKey(value); }
        public uint UniqueSeed
        {
            get => SystemSerialCode.UniqueSeed;
            set => SystemSerialCode.SetUniqueSeed(value);
        }

        public int CompareTo(IUnique other)
        {
            return SystemSerialCode.CompareTo(other);
        }

        public bool Equals(IUnique other)
        {
            return SystemSerialCode.Equals(other);
        }

        public byte[] GetBytes()
        {
            return SystemSerialCode.GetBytes();
        }

        public long GetUniqueKey()
        {
            return SystemSerialCode.UniqueKey;
        }

        public byte[] GetUniqueBytes()
        {
            return SystemSerialCode.GetUniqueBytes();
        }

        public void SetUniqueKey(long value)
        {
            SystemSerialCode.SetUniqueKey(value);
        }

        public void SetUniqueSeed(uint seed)
        {
            SystemSerialCode.SetUniqueSeed(seed);
        }

        public uint GetUniqueSeed()
        {
            return SystemSerialCode.GetUniqueSeed();
        }
    }
}
