/*************************************************
   Copyright (c) 2021 Undersoft

   System.Instant.Linkmap.cs
   
   @project: Undersoft.Vegas.Sdk
   @stage: Development
   @author: Dariusz Hanc
   @date: (29.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Instant.Linking
{
    using System.Collections.Generic;
    using System.Multemic;
    using System.Uniques;

    public interface ILinkmap
    {
        #region Properties

        Links Links { get; }

        LinkBranches Map { get; }

        #endregion

        #region Methods

        void Truncate();

        IDeck<Link> GetLinks(IList<LinkMember> members);

        Link GetLink(LinkMember member);

        IDeck<IDeck<LinkBranch>> GetMaps(IList<LinkMember> members);

        IDeck<LinkBranch> GetMap(LinkMember member);

        LinkBranches Build();

        LinkBranches Update();

        #endregion
    }

    [Serializable]
    public class Linkmap
    {
        #region Fields

        private Links links;
        private LinkBranches map;

        #endregion

        public Linkmap()
        {
            links = new Links();
            map = new LinkBranches(links, PRIMES_ARRAY.Get(9));
        }

        #region Properties

        public Links Links { get => links; }

        public LinkBranches Map { get => map; }

        #endregion

        #region Methods

        public void Truncate()
        {
            Map.Flush();
        }

        public IDeck<Link> GetLinks(IList<LinkMember> members)
        {
            throw new NotImplementedException();
        }

        public Link GetLink(LinkMember member)
        {
            throw new NotImplementedException();
        }

        public IDeck<IDeck<LinkBranch>> GetMaps(IList<LinkMember> members)
        {
            throw new NotImplementedException();
        }

        public IDeck<LinkBranch> GetMap(LinkMember member)
        {
            throw new NotImplementedException();
        }
         
        public LinkBranches Build()
        {
            throw new NotImplementedException();
        }

        public LinkBranches Update()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
