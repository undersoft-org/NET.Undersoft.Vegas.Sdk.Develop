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
        private Ussn serialcode;

        public Links()
        {
        }
        public Links(IList<Link> links)
        {
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
                base[linkid] = value;
            }
        }

        public IList<Link> Collect(IList<string> LinkNames = null)
        {
            if (LinkNames != null)
                return LinkNames.Select(l => this[l]).Where(f => f != null).ToArray();
            else
                return this.AsValues().ToArray();
        }
        public IList<Link> Collect(IList<Link> links = null)
        {
            if (links != null)
                return links.Select(l => this[l.Name]).Where(f => f != null).ToArray();
            else
                return this.AsValues().ToArray();
        }

        public IList<Link> GetTarget(string TargetName)
        {
            return AsValues().Where(c => c.TargetName == TargetName).ToArray();
        }
        public IList<Link> GetOrigin(string OriginName)
        {
            return AsValues().Where(c => c.OriginName == OriginName).ToArray();
        }

        public override ICard<Link> EmptyCard()
        {
            return new Card64<Link>();
        }

        public override ICard<Link> NewCard(ulong  key, Link value)
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
            var card = NewCard(value);
            Linker.Map.Links.Add(card);
            return InnerAdd(card);
        }

        protected override ICard<Link> InnerPut(Link value)
        {
            var card = NewCard(value);
            Linker.Map.Links.Put(card);
            return InnerPut(card);
        }

        public Ussn SerialCode
        {
            get => serialcode;
            set => serialcode = value;
        }

        public IUnique Empty => Ussn.Empty;

        public new ulong UniqueKey
        {
            get => serialcode.UniqueKey;
            set => serialcode.UniqueKey = value;
        }

        public ulong UniqueSeed
        {
            get => serialcode.UniqueSeed;
            set => serialcode.UniqueSeed = value;
        }

        public int CompareTo(IUnique other)
        {
            return serialcode.CompareTo(other);
        }

        public bool Equals(IUnique other)
        {
            return serialcode.Equals(other);
        }

        public byte[] GetBytes()
        {
            return serialcode.GetBytes();
        }

        public byte[] GetUniqueBytes()
        {
            return serialcode.GetUniqueBytes();
        }
    }
}
