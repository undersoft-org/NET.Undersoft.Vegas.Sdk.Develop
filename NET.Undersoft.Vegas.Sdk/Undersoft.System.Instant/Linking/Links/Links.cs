using System.Multemic;
using System.Collections.Generic;
using System.Linq;
using System.Uniques;

namespace System.Instant.Linking
{
    [JsonArray]
    [Serializable]
    public class Links : SharedAlbum<Link>, IUnique
    {
        public Links()
        {
            LinkKeys = new Album<int[]>(5);
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
                var lgcy = this[linkName];                
                if (lgcy != null && lgcy.Links == null)
                    lgcy.Links = this;
                return lgcy;
            }

            set
            {
                value.Links = this;
                this[linkName] = value;
            }
        }
        public new Link this[int linkid]
        {
            get
            {
                var lgcy = this[linkid];
                if (lgcy != null && lgcy.Links == null)
                    lgcy.Links = this;
                return lgcy;
            }

            set
            {
                value.Links = this;
                this[linkid] = value;
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

        public Link GetTarget(string TargetName)
        {
            return this.Cast<Link>().Where(c => c.TargetName == TargetName).FirstOrDefault();           
        }
        public Link GetOrigin(string OriginName)
        {
            return this.Cast<Link>().Where(c => c.OriginName == OriginName).FirstOrDefault();
        }

        public IDeck<int[]> LinkKeys { get; set; }

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
            return InnerAdd(NewCard(value));
        }
        protected override ICard<Link> InnerPut(Link value)
        {
            return InnerPut(NewCard(value));
        }

        private Ussn systemSerialCode;
        public Ussn SystemSerialCode
        { get => systemSerialCode; set => systemSerialCode = value; }

        public IUnique Empty => Ussn.Empty;

        public long KeyBlock
        { get => SystemSerialCode.KeyBlock; set => systemSerialCode.KeyBlock = value; }
        public uint SeedBlock
        {
            get => systemSerialCode.SeedBlock;
            set => systemSerialCode.SeedBlock = value;
        }

        public int CompareTo(IUnique other)
        {
            return systemSerialCode.CompareTo(other);
        }

        public bool Equals(IUnique other)
        {
            return systemSerialCode.Equals(other);
        }

        public byte[] GetBytes()
        {
            return systemSerialCode.GetBytes();
        }

        public long GetHashKey()
        {
            return systemSerialCode.GetHashKey();
        }

        public byte[] GetKeyBytes()
        {
            return systemSerialCode.GetKeyBytes();
        }

        public void SetHashKey(long value)
        {
            systemSerialCode.SetHashKey(value);
        }

        public void SetHashSeed(uint seed)
        {
            systemSerialCode.SetHashSeed(seed);
        }

        public uint GetHashSeed()
        {
            return systemSerialCode.GetHashSeed();
        }
    }
  
    public enum LinkSite
    {
        Origin,
        Target
    }
}
