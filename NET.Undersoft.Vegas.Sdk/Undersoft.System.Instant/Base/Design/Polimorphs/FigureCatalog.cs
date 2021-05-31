using System.Uniques;
using System.Instant.Linking;
using System.Instant.Treatments;
using System.IO;
using System.Multemic;
using System.Runtime.InteropServices;

namespace System.Instant
{
    public abstract class FigureCatalog : SharedAlbum<IFigure>, IFigures
    {
        public IInstant Instant { get; set; }

        public abstract bool Prime { get; set; }

        public abstract object this[int index, string propertyName] { get; set; }

        public abstract object this[int index, int fieldId] { get; set; }       

        public abstract IRubrics Rubrics { get; set; }      

        public abstract IRubrics KeyRubrics { get; set; }

        public abstract IFigure NewFigure();

        public abstract  Type FigureType { get; set; }

        public abstract int FigureSize { get; set; }

        public abstract Ussn SerialCode { get; set; }

        public int Length { get; }

        public override ICard<IFigure> EmptyCard()
        {
            return new FigureCard(this);
        }

        public override ICard<IFigure> NewCard(long key, IFigure value)
        {
            return new FigureCard(key, value, this);
        }
        public override ICard<IFigure> NewCard(object key, IFigure value)
        {
            return new FigureCard(key, value, this);
        }
        public override ICard<IFigure> NewCard(IFigure value)
        {
            return new FigureCard(value, this);
        }
        public override ICard<IFigure> NewCard(ICard<IFigure> value)
        {
            return new FigureCard(value, this);
        }

        public override ICard<IFigure>[] EmptyCardTable(int size)
        {
            return new FigureCard[size];
        }

        public override ICard<IFigure>[] EmptyCardList(int size)
        {
            cards = new FigureCard[size];
            return cards;
        }

        private ICard<IFigure>[] cards;
        public ICard<IFigure>[] Cards { get => cards; }

        protected override bool InnerAdd(IFigure value)
        {
            return InnerAdd(NewCard(value));
        }

        protected override ICard<IFigure> InnerPut(IFigure value)
        {
            return InnerPut(NewCard(value));
        }

        public override ICard<IFigure> AddNew()
        {
            ICard<IFigure> newCard = NewCard(Unique.NewKey, NewFigure());
            if (InnerAdd(newCard))
                return newCard;
            return null;
        }
        public override ICard<IFigure> AddNew(long key)
        {
            ICard<IFigure> newCard = NewCard(key, NewFigure());
            if (InnerAdd(newCard))
                return newCard;
            return null;
        }
        public override ICard<IFigure> AddNew(object key)
        {
            ICard<IFigure> newCard = NewCard(__base_.UniqueKey(key), NewFigure());
            if (InnerAdd(newCard))
                return newCard;
            return null;
        }

        public object[] ValueArray { get => ToObjectArray(); set => Put(value); }

        public Type Type { get; set; }

        public IFigures Organized { get; set; }

        public IFigure Summary { get; set; }

        public FigureFilter Filter { get; set; }

        public FigureSort Sort { get; set; }

        public Func<IFigure, bool> Organizer { get; set; }

        public Links Links { get; set; } = new Links();

        private Treatment treatment;
        public  Treatment Treatment
        {
            get => treatment == null ? treatment = new Treatment(this) : treatment;
            set => treatment = value;
        }

        public IDeck<IComputation> Computations { get; set; }

        #region Uniques

        public IUnique Empty => Ussn.Empty;

        object IFigure.this[int fieldId] { get => this[fieldId]; set => this[fieldId] = (IFigure)value; }
        public object this[string propertyName] { get => this[propertyName]; set => this[propertyName] = (IFigure)value; }

        public byte[] GetBytes()
        {
            return SerialCode.GetBytes();
        }

        public byte[] GetUniqueBytes()
        {
            return SerialCode.GetUniqueBytes();
        }

        public new long UniqueKey { get => SerialCode.UniqueKey; set => SerialCode.SetUniqueKey(value); }

        public uint UniqueSeed
        {
            get => SerialCode.UniqueSeed;
            set => SerialCode.SetUniqueSeed(value);
        }

        //public void SetUniqueKey(long value)
        //{
        //    SerialCode.SetUniqueKey(value);
        //}

        //public long GetUniqueKey()
        //{
        //    return SerialCode.UniqueKey;
        //}

        //public void SetUniqueSeed(uint seed)
        //{
        //    SerialCode.SetUniqueSeed(seed);
        //}
        //public uint GetUniqueSeed()
        //{
        //    return SerialCode.GetUniqueSeed();
        //}

        public bool Equals(IUnique other)
        {
            return SerialCode.Equals(other);
        }

        public int CompareTo(IUnique other)
        {
            return SerialCode.CompareTo(other);
        }
    
        #endregion       

        #region Formatter

        public int SerialCount { get; set; }
        public int DeserialCount { get; set; }
        public int ProgressCount { get; set; }

        public int Serialize(Stream stream, int offset, int batchSize, SerialFormat serialFormat = SerialFormat.Binary)
        {
            throw new NotImplementedException();
        }
        public int Serialize(ISerialBlock buffor, int offset, int batchSize, SerialFormat serialFormat = SerialFormat.Binary)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(Stream stream, SerialFormat serialFormat = SerialFormat.Binary)
        {
            throw new NotImplementedException();
        }
        public object Deserialize(ref object block, SerialFormat serialFormat = SerialFormat.Binary)
        {
            throw new NotImplementedException();
        }

        public object[] GetMessage()
        {
            return new[] { (IFigures)this };
        }

        public object GetHeader()
        {
            return this;
        }       

        public int ItemsCount => Count;      

        #endregion

    }
}