/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.BranchCard.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant
{
    using System.Extract;
    using System.Instant.Linking;
    using System.Multemic;
    using System.Runtime.InteropServices;
    using System.Uniques;

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class BranchCard : Card<LinkBranch>, IEquatable<LinkBranch>, IComparable<LinkBranch>
    {
        private long _key;

        public BranchCard()
        {
        }
        public BranchCard(object key, LinkBranch value) : base(key, value)
        {
        }
        public BranchCard(long key, LinkBranch value) : base(key, value)
        {
        }
        public BranchCard(LinkBranch value) : base(value)
        {

        }
        public BranchCard(ICard<LinkBranch> value) : base(value)
        {
        }

        public object this[int fieldId]
        {
            get => this.value[fieldId];
            set => this.value[fieldId] = (ICard<IFigure>)value;
        }
        public object this[string propertyName]
        {
            get => this.value[propertyName];
            set => this.value[propertyName] = (ICard<IFigure>)value;
        }

        public override void Set(long key, LinkBranch value)
        {
            this.value = value;
            Member = value.Member;
            Key = key.UniqueKey64(value.GetUniqueSeed());
        }
        public override void Set(object key, LinkBranch value)
        {
            this.value = value;
            Member = value.Member;
            Key = key.UniqueKey64(value.GetUniqueSeed());
        }
        public override void Set(LinkBranch value)
        {
            this.value = value;
            Member = value.Member;
            Key = value.UniqueKey64(value.GetUniqueSeed());
        }
        public override void Set(ICard<LinkBranch> card)
        {
            this.value = card.Value;
            this.Key = card.Key;
        }

        public override bool Equals(long key)
        {
            return Key == key;
        }
        public override bool Equals(object y)
        {
            return Key.Equals(y.UniqueKey());
        }
        public bool Equals(LinkBranch other)
        {
            return Key == other.UniqueKey;
        }

        public override int GetHashCode()
        {
            return Value.GetUniqueBytes().BitAggregate64to32().ToInt32();
        }

        public override int CompareTo(object other)
        {
            return (int)(Key - other.UniqueKey64());
        }
        public override int CompareTo(long key)
        {
            return (int)(Key - key);
        }
        public override int CompareTo(ICard<LinkBranch> other)
        {
            return (int)(Key - other.Key);
        }
        public int CompareTo(LinkBranch other)
        {
            return (int)(Key - other.UniqueKey);
        }

        public override byte[] GetBytes()
        {
            return value.GetBytes();
        }

        public unsafe override byte[] GetUniqueBytes()
        {
            return value.GetUniqueBytes();
        }

        public override int[]    UniqueOrdinals()
        {
            return Member.KeyRubrics.Ordinals;
        }
        public override object[] UniqueValues()
        {
            if (this.value.Count > 0)
                return this.value[0].UniqueValues();
            return null;
        }
        public override long UniquesAsKey()
        {
            if (this.value.Count > 0)
                return this.value[0].UniquesAsKey();
            return -1;
        }

        public override long Key
        {
            get => _key;
            set => _key = value;
        }

        public override long UniqueKey
        {
            get => value.UniqueKey;
            set => this.value.UniqueKey = value;
        }

        public LinkMember Member { get; set; }

        public LinkBranches Branches { get; set; }

        public override IUnique Empty => this.value.Empty;

        public override uint UniqueSeed
        { get => Member.UniqueSeed; set => Member.SetUniqueSeed(value); }

        public override int CompareTo(IUnique other)
        {
            return this.value.CompareTo(other);
        }

        public override bool Equals(IUnique other)
        {
            return this.value.Equals(other);
        }

        public override long GetUniqueKey()
        {
            return this.value.UniqueKey;
        }

        public override void SetUniqueKey(long value)
        {
            this.value.SetUniqueKey(value);
        }

        public override void SetUniqueSeed(uint seed)
        {
            Member.SetUniqueSeed(seed);
        }

        public override uint GetUniqueSeed()
        {
            return Member.GetUniqueSeed();
        }

    }
}
