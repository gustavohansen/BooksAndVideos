namespace BooksAndVideos.App.Entities
{
    public class OrderLine : Entity
    {
        public Product Product { get; set; }

        public int Count { get; set; }
    }
}