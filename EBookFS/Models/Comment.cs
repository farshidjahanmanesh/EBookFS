namespace EBookFS.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
