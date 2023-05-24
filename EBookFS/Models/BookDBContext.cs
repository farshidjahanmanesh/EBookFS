using Microsoft.EntityFrameworkCore;

namespace EBookFS.Models
{
    public class BookDBContext : DbContext
    {
        public BookDBContext()
        {

        }
        public BookDBContext(DbContextOptions<BookDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
    }
}
