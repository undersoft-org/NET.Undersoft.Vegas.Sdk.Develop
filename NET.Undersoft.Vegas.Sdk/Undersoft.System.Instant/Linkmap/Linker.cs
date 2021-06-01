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
    using System.Sets;
    using System.Uniques;

    public interface ILinker
    {
        #region Properties

        Links Links { get; }

        #endregion

        #region Methods

        void Clear();

        IDeck<Link> GetLinks(IList<LinkMember> members);

        Link GetLink(LinkMember member);

        IDeck<IDeck<BranchDeck>> GetMaps(IList<LinkMember> members);

        IDeck<BranchDeck> GetMap(LinkMember member);

        NodeCatalog Build();

        NodeCatalog Update();

        #endregion
    }

    [Serializable]
    public class Linker : ILinker
    {
        #region Fields

        private static NodeCatalog map = new NodeCatalog(new Links(), PRIMES_ARRAY.Get(9));
        
        private Links links;

        #endregion

        public Linker()
        {
            links = new Links();
        }

        #region Properties

        public static NodeCatalog Map { get => map; }

        public Links Links { get => links; } 

        #endregion

        #region Methods

        public void Clear()
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
