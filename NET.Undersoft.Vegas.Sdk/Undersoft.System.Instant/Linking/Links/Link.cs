/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Link.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Linking
{
    using System.Uniques;

    [JsonObject]
    [Serializable]
    public class Link : IUnique
    {
        #region Fields

        [NonSerialized] public Links Links;
        private Ussc systemSerialCode;

        #endregion

        #region Constructors

        public Link()
        {
            Name = "FiguresLink#" + DateTime.Now.ToBinary().ToString();
            Origin = new LinkMember();
            Origin.Site = LinkSite.Origin;
            Target = new LinkMember();
            Target.Site = LinkSite.Target;
            SetUniqueKey(Name.UniqueKey64());
        }
        public Link(IFigures origin, IFigures target)
        {
            Name = origin.Type.Name + "_" + target.Type.Name;
            Origin = new LinkMember(origin, this, LinkSite.Origin);
            Target = new LinkMember(target, this, LinkSite.Target);
            SetUniqueKey(Name.UniqueKey64());
            origin.Links.Put(this);
            target.Links.Put(this);
        }

        #endregion

        #region Properties

        public IUnique Empty => Ussn.Empty;

        public long UniqueKey { get => systemSerialCode.UniqueKey; set => systemSerialCode.UniqueKey = value; }

        public string Name { get; set; }

        public LinkMember Origin { get; set; }

        public IRubrics OriginKeys
        {
            get
            {
                return Origin.KeyRubrics;
            }
            set
            {
                Origin.KeyRubrics = value;
            }
        }

        public string OriginName
        {
            get { return Origin.Name; }
            set
            {
                if (Links != null)
                {
                    var links = Links[Name];
                    if (links != null)
                    {
                        IFigures figures = links.Origin.Figures;
                        Origin.Figures = figures;
                        Origin.Name = figures.Type.Name;
                        Origin.Rubrics = figures.Rubrics;
                        Origin.KeyRubrics = new MemberRubrics();
                    }
                    Target.Name = value;
                }
            }
        }

        public IRubrics OriginRubrics
        {
            get
            {
                return Origin.Rubrics;
            }
            set
            {
                Origin.Rubrics = value;
            }
        }

        public uint UniqueSeed { get => systemSerialCode.UniqueSeed; set => systemSerialCode.UniqueSeed = value; }

        public Ussc SystemSerialCode
        {
            get => systemSerialCode;
            set
            {
                systemSerialCode.UniqueKey = value.UniqueKey;
                systemSerialCode.UniqueSeed = value.UniqueSeed;
            }
        }

        public LinkMember Target { get; set; }

        public IRubrics TargetKeys
        {
            get
            {
                return Target.KeyRubrics;
            }
            set
            {
                Target.KeyRubrics = value;
            }
        }

        public string TargetName
        {
            get { return Target.Name; }
            set
            {
                if (Links != null)
                {
                    var links = Links[Name];
                    if (links != null)
                    {
                        IFigures figures = links.Target.Figures;
                        Target.Figures = figures;
                        Target.Name = figures.Type.Name;
                        Target.Rubrics = figures.Rubrics;
                        Target.KeyRubrics = new MemberRubrics();
                    }
                }
                Target.Name = value;
            }
        }

        public IRubrics TargetRubrics
        {
            get
            {
                return Target.KeyRubrics;
            }
            set
            {
                Target.KeyRubrics = value;
            }
        }

        #endregion

        #region Methods

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

        public long GetUniqueKey()
        {
            return systemSerialCode.UniqueKey;
        }

        public uint GetUniqueSeed()
        {
            return systemSerialCode.GetUniqueSeed();
        }

        public byte[] GetUniqueBytes()
        {
            return systemSerialCode.GetUniqueBytes();
        }

        public void SetUniqueKey(long value)
        {
            systemSerialCode.SetUniqueKey(value);
        }

        public void SetUniqueSeed(uint seed)
        {
            systemSerialCode.SetUniqueSeed(seed);
        }

        #endregion
    }
}
