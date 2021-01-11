using System.Collections.Generic;

namespace BooksAndVideos.App.Entities
{
    public class Customer : Entity
    {
        public IList<MembershipType> Memberships { get; set; }

        public Customer() => Memberships = new List<MembershipType>();
    }
}