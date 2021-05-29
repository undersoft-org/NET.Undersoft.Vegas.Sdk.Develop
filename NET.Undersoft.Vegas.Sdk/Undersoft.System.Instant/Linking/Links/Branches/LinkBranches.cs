using System.Uniques;
using System.Multemic;

namespace System.Instant.Linking
{
    [Serializable]
    public class LinkBranches : SharedMultiAlbum<LinkBranch>
    {
        public LinkBranches(int capacity = 17) : base(capacity)
        {
        }

        public LinkBranches(Links links, int capacity = 17) : base(capacity)
        {
            Links = links;
        }

        public override ICard<LinkBranch>[] EmptyCardList(int size)
        {
            return new BranchCard[size];
        }

        public override ICard<LinkBranch> EmptyCard()
        {
            return new BranchCard();
        }

        public override ICard<LinkBranch> NewCard(long key, LinkBranch value)
        {
            return new BranchCard(key, value);
        }

        public override ICard<LinkBranch> NewCard(object key, LinkBranch value)
        {
            return new BranchCard(key, value);
        }

        public override ICard<LinkBranch> NewCard(ICard<LinkBranch> card)
        {
            return new BranchCard(card);
        }

        public override ICard<LinkBranch> NewCard(LinkBranch card)
        {
            return new BranchCard(card);
        }

        public override ICard<LinkBranch>[] EmptyCardTable(int size)
        {
            return new BranchCard[size];
        }

        public Links Links { get; set; }

    }

}
