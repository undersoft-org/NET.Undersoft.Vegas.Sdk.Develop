﻿/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.MemberRubrics.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (28.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant
{
    using System.Collections.Generic;
    using System.Extract;
    using System.Linq;
    using System.Multemic;
    using System.Uniques;

    public partial class MemberRubrics : CardBook<MemberRubric>, IRubrics
    {
        #region Fields

        private int[] ordinals;

        #endregion

        #region Constructors

        public MemberRubrics()
            : base()
        {
        }
        public MemberRubrics(IEnumerable<MemberRubric> collection)
            : base(collection)
        {
        }
        public MemberRubrics(IList<MemberRubric> collection)
            : base(collection)
        {
        }

        #endregion

        #region Properties

        public IUnique Empty => Figures.Empty;

        public IFigures Figures { get; set; }

        public new long UniqueKey { get => Figures.UniqueKey; set => Figures.UniqueKey = value; }

        public IRubrics KeyRubrics { get; set; }

        public FieldMappings Mappings { get; set; }

        public int[] Ordinals { get => ordinals; }

        public uint UniqueSeed { get => Figures.UniqueSeed; set => Figures.UniqueSeed = value; }

        public Ussn SystemSerialCode { get => Figures.SystemSerialCode; set => Figures.SystemSerialCode = value; }

        public object[] ValueArray { get => Figures.ValueArray; set => Figures.ValueArray = value; }

        #endregion

        #region Methods

        public int CompareTo(IUnique other)
        {
            return Figures.CompareTo(other);
        }

        public override ICard<MemberRubric> EmptyCard()
        {
            return new RubricCard();
        }

        public override ICard<MemberRubric>[] EmptyCardList(int size)
        {
            return new RubricCard[size];
        }

        public override ICard<MemberRubric>[] EmptyCardTable(int size)
        {
            return new RubricCard[size];
        }

        public bool Equals(IUnique other)
        {
            return Figures.Equals(other);
        }

        public byte[] GetBytes()
        {
            return Figures.GetBytes();
        }

        public unsafe byte[] GetBytes(IFigure figure)
        {
            int size = Figures.FigureSize;
            byte* figurePtr = stackalloc byte[size * 2];
            byte* bufferPtr = figurePtr + size;
            figure.StructureTo(figurePtr);
            int destOffset = 0;
            foreach (var rubric in AsValues())
            {
                int l = rubric.RubricSize;
                Extractor.CopyBlock(bufferPtr, destOffset, figurePtr, rubric.RubricOffset, l);
                destOffset += l;
            }
            byte[] b = new byte[destOffset];
            fixed (byte* bp = b)
                Extractor.CopyBlock(bp, bufferPtr, destOffset);
            return b;
        }

        //public long GetUniqueKey()
        //{
        //    return Figures.UniqueKey;
        //}

        public unsafe long GetUniqueKey(IFigure figure, uint seed = 0)
        {
            int size = Figures.FigureSize;
            byte* figurePtr = stackalloc byte[size * 2];
            byte* bufferPtr = figurePtr + size;
            figure.StructureTo(figurePtr);
            int destOffset = 0;
            foreach (var rubric in AsValues())
            {
                int l = rubric.RubricSize;
                Extractor.CopyBlock(bufferPtr, destOffset, figurePtr, rubric.RubricOffset, l);
                destOffset += l;
            }
            return (long)UniqueCode64.ComputeUniqueKey(bufferPtr, destOffset, seed);
        }

        //public uint GetUniqueSeed()
        //{
        //    return Figures.GetUniqueSeed();
        //}

        public byte[] GetUniqueBytes()
        {
            return Figures.GetUniqueBytes();
        }

        public unsafe byte[] GetUniqueBytes(IFigure figure, uint seed = 0)
        {
            //return KeyRubrics.Ordinals.Select(x => figure[x]).ToArray().UniqueKey64();
            int size = Figures.FigureSize;
            byte* figurePtr = stackalloc byte[size * 2];
            byte* bufferPtr = figurePtr + size;
            figure.StructureTo(figurePtr);
            int destOffset = 0;
            foreach (var rubric in AsValues())
            {
                int l = rubric.RubricSize;
                Extractor.CopyBlock(bufferPtr, destOffset, figurePtr, rubric.RubricOffset, l);
                destOffset += l;
            }
            ulong hash = UniqueCode64.ComputeUniqueKey(bufferPtr, destOffset, seed);
            byte[] b = new byte[8];
            fixed (byte* bp = b)
                *((ulong*)bp) = hash;
            return b;
        }

        public override ICard<MemberRubric> NewCard(ICard<MemberRubric> value)
        {
            return new RubricCard(value);
        }
        public override ICard<MemberRubric> NewCard(long key, MemberRubric value)
        {
            return new RubricCard(key, value);
        }
        public override ICard<MemberRubric> NewCard(MemberRubric value)
        {
            return new RubricCard(value.UniqueKey, value);
        }
        public override ICard<MemberRubric> NewCard(object key, MemberRubric value)
        {
            return new RubricCard(key, value);
        }

        //public void SetUniqueKey(IFigure figure, uint seed = 0)
        //{
        //    figure.UniqueKey = GetUniqueKey(figure, seed);
        //}

        //public void SetUniqueKey(long value)
        //{
        //    Figures.SetUniqueKey(value);
        //}

        //public void SetUniqueSeed(uint seed)
        //{
        //    Figures.SetUniqueSeed(seed);
        //}

        public void Update()
        {
            ordinals = this.AsValues().Select(o => o.FigureFieldId).ToArray();
            if (KeyRubrics != null)
                KeyRubrics.Update();
        }

        #endregion
    }
}
