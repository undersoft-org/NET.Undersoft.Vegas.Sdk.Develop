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

        NodeCatalog Map { get; }

        #endregion

        #region Methods

        void Truncate();

        IDeck<Link> GetLinks(IList<LinkMember> members);

        Link GetLink(LinkMember member);

        IDeck<IDeck<BranchDeck>> GetMaps(IList<LinkMember> members);

        IDeck<BranchDeck> GetMap(LinkMember member);

        NodeCatalog Build();

        NodeCatalog Update();

        #endregion
    }

    [Serializable]
    public class Linkmap
    {
        #region Fields

        private Links links;
        private NodeCatalog map;

        #endregion

        public Linkmap()
        {
            links = new Links();
            map = new NodeCatalog(links, PRIMES_ARRAY.Get(9));
        }

        #region Properties

        public Links Links { get => links; }

        public NodeCatalog Map { get => map; }

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

        public IDeck<IDeck<BranchDeck>> GetMaps(IList<LinkMember> members)
        {
            throw new NotImplementedException();
        }

        public IDeck<BranchDeck> GetMap(LinkMember member)
        {
            throw new NotImplementedException();
        }
         
        public NodeCatalog Build()
        {
            throw new NotImplementedException();
        }

        public NodeCatalog Update()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
