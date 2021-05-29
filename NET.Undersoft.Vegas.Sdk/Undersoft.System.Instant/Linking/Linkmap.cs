using System.Uniques;
using System.Multemic;
using System.Multemic.Basedeck;
using System.Collections.Generic;
using System.Linq;


namespace System.Instant.Linking
{
    public interface ILinkmap
    {
        Links Links { get; }

        LinkBranches Map { get; }

        IDeck<LinkBranch> GetMap(IList<LinkMember> members);
        IDeck<LinkBranch> GetMap(LinkMember member);

        IDeck<Link> GetLink(IList<LinkMember> members);
        IDeck<Link> GetLink(LinkMember member);

        LinkBranches Update();

        void Clear();
    }

    [JsonObject]
    [Serializable]
    public static class Linkmap
    {
        #region NonSerialized
        [NonSerialized] private static Links links = new Links();
        [NonSerialized] private static LinkBranches map = new LinkBranches(links, PRIMES_ARRAY.Get(9));
        #endregion

        public static Links Links { get => links; }

        public static LinkBranches Map { get => map; }

        public static void Clear()
        {
            throw new NotImplementedException();
        }

        public static IDeck<Link> GetLink(IList<LinkMember> members)
        {
            throw new NotImplementedException();
        }

        public static IDeck<Link> GetLink(LinkMember member)
        {
            throw new NotImplementedException();
        }

        public static IDeck<LinkBranch> GetMap(IList<LinkMember> members)
        {
            throw new NotImplementedException();
        }

        public static IDeck<LinkBranch> GetMap(LinkMember member)
        {
            throw new NotImplementedException();
        }

        public static LinkBranches Update()
        {
            throw new NotImplementedException();
        }
    }
}
