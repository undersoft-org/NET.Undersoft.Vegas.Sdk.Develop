/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.LinkMember.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Linking
{
    using System.Extract;
    using System.Uniques;

    [JsonObject]
    [Serializable]
    public class LinkMember : IUnique
    {
        #region Fields

        public int BranchesCount = 0;

        #endregion

        #region Constructors

        public LinkMember()
        {
            KeyRubrics = new MemberRubrics();
        }
        public LinkMember(IFigures figures, Link link, LinkSite site) : this()
        {
            Figures = figures;
            Name = figures.Type.Name;
            Site = site;
            Rubrics = figures.Rubrics;
            KeyRubrics = new MemberRubrics();
            Link = link;
            byte[] keybytes = new long[] { figures.UniqueKey, link.UniqueKey }.GetBytes();
            SetUniqueKey(keybytes.UniqueKey64());
            SetUniqueSeed((uint)keybytes.UniqueKey32());
        }

        #endregion

        #region Properties

        public IUnique Empty => Ussc.Empty;

        public IFigures Figures { get; set; }

        public long UniqueKey { get => SystemSerialCode.UniqueKey; set => SystemSerialCode.SetUniqueKey(value); }

        public IRubrics KeyRubrics { get; set; }

        public Link Link { get; set; }

        public string Name { get; set; }

        public IRubrics Rubrics { get; set; }

        public uint UniqueSeed { get => SystemSerialCode.UniqueSeed; set => SystemSerialCode.SetUniqueSeed(value); }

        public LinkSite Site { get; set; }

        public Ussc SystemSerialCode { get; set; }

        #endregion

        #region Methods

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

        public uint GetUniqueSeed()
        {
            return SystemSerialCode.GetUniqueSeed();
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

        #endregion
    }
}
