using EBookFS.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EBookFS.Models.Repositories
{
    public class BookRepository : IBookRepository
    {

        private BookDBContext _context;
        public BookRepository(BookDBContext context)
        {
            _context = context;
        }
        public async Task Add(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book is null");
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> Get(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.Books.AnyAsync(c => c.Id == id);
        }

        public void Remove(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}