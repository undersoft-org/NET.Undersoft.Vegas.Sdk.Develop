﻿/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.LinkCard.cs
   
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
    using System.Linq;
    using System.Multemic;
    using System.Runtime.InteropServices;
    using System.Uniques;

    public class BranchCard : Card<ICard<IFigure>>, IFigure, IEquatable<ICard<IFigure>>, IComparable<ICard<IFigure>>
    {
        public BranchCard(LinkMember member)
        {
            Member = member;
        }
        public BranchCard(object key, ICard<IFigure> value, LinkMember member) : base(key, value)
        {
            Member = member;
        }
        public BranchCard(long key, ICard<IFigure> value, LinkMember member) : base(key, value)
        {
            Member = member;
        }
        public BranchCard(ICard<IFigure> value, LinkMember member) : base(value)
        {
            Member = member;
        }
        public BranchCard(ICard<ICard<IFigure>> value, LinkMember member) : base(value)
        {
            Member = member;
        }

        public object this[int fieldId]
        {
            get => ((IFigure)this.value)[fieldId];
            set => ((IFigure)this.value)[fieldId] = value;
        }
        public object this[string propertyName]
        {
            get => ((IFigure)this.value)[propertyName];
            set => ((IFigure)this.value)[propertyName] = value;
        }

        public override void Set(object key, ICard<IFigure> value)
        {
            this.value = value;
            this.value.UniqueKey = key.UniqueKey();
        }
        public override void Set(ICard<IFigure> value)
        {
            this.value = value;
        }
        public override void Set(ICard<ICard<IFigure>> card)
        {
            this.value = card.Value;
        }

        public override bool Equals(long key)
        {
            return Key == key;
        }
        public override bool Equals(object y)
        {
            return Key.Equals(y.UniqueKey());
        }
        public          bool Equals(ICard<IFigure> other)
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
        public override int CompareTo(ICard<ICard<IFigure>> other)
        {
            return (int)(Key - other.Key);
        }
        public          int CompareTo(ICard<IFigure> other)
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

        public override    int[] UniqueOrdinals()
        {
            return Member.KeyRubrics.Ordinals;
        }
        public override object[] UniqueValues()
        {
            return Member.KeyRubrics.Ordinals.Select(x => value.Value[x]).ToArray();
        }
        public override     long UniquesAsKey()
        {
            return Member.KeyRubrics.Ordinals.Select(x => value.Value[x]).ToArray().UniqueKey();
        }

        public override long Key
        {
            get => value.UniqueKey;
            set => this.value.UniqueKey = value;
        }

        public override long UniqueKey
        {
            get => value.UniqueKey;
            set => this.value.UniqueKey = value;
        }

        public object[] ValueArray
        {
            get
            {
                return ((IFigure)this.value).ValueArray;
            }
            set
            {
                ((IFigure)this.value).ValueArray = value;
            }
        }

        public Ussn SerialCode
        {
            get => value.Value.SerialCode;
            set => this.value.Value.SerialCode = value;
        }

        public LinkMember Member { get; set; }

    }
}