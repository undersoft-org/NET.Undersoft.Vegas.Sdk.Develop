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

        public Links Links { get; set; }
        private Ussc serialcode;

        #endregion

        #region Constructors

        public Link()
        {
            Name = Unique.NewKey.ToString() + "_Link";
            UniqueKey = Name.UniqueKey64();
            Origin = new LinkMember(this, LinkSite.Origin);
            Target = new LinkMember(this, LinkSite.Target);
        }
        public Link(IFigures origin, IFigures target)
        {
            Name = origin.Type.Name + "_" + target.Type.Name;
            UniqueKey = Name.UniqueKey64();
            Origin = new LinkMember(origin, this, LinkSite.Origin);
            Target = new LinkMember(target, this, LinkSite.Target);            
            origin.Links.Put(this);
            target.Links.Put(this);
        }
        public Link(IFigures origin, IFigures target, IRubrics keyRubrics) : this(origin, target)
        {
            foreach(var rubric in keyRubrics)
            {
                var originRubric = origin.Rubrics[rubric];
                var targetRubric = target.Rubrics[rubric];
                if (originRubric != null && targetRubric != null)
                {
                    OriginKeys.Add(originRubric);
                    TargetKeys.Add(targetRubric);
                }
                else
                    throw new IndexOutOfRangeException("Rubric not found");
            }
        }
        public Link(IFigures origin, IFigures target, string[] keyRubricNames) : this(origin, target)
        {
            foreach (var name in keyRubricNames)
            {
                var originRubric = origin.Rubrics[name];
                var targetRubric = target.Rubrics[name];
                if (originRubric != null && targetRubric != null)
                {
                    OriginKeys.Add(originRubric);
                    TargetKeys.Add(targetRubric);
                }
                else
                    throw new IndexOutOfRangeException("Rubric not found");
            }
        }

        #endregion

        #region Properties

        public IUnique Empty => Ussn.Empty;

        public long UniqueKey { get => serialcode.UniqueKey; set => serialcode.UniqueKey = value; }

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
                Origin.KeyRubrics.Renew(value);
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
                        Origin.KeyRubrics.Clear();
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

        public uint UniqueSeed { get => serialcode.UniqueSeed; set => serialcode.UniqueSeed = value; }

        public Ussc SerialCode
        {
            get => serialcode;
            set => serialcode = value;            
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
                Target.KeyRubrics.Renew(value);
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
                        Target.KeyRubrics.Clear();
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

        #endregion
    }
}
