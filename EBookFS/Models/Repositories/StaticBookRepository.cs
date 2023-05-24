using EBookFS.Models.Contracts;

namespace EBookFS.Models.Repositories
{
    public class StaticBookRepository : IBookRepository
    {

        private static List<Book> _books;
        static StaticBookRepository()
        {
            _books = new List<Book>();
        }
        public Task Add(Book book)
        {
            book.Id = _books.OrderBy(c => c.Id).LastOrDefault()?.Id + 1 ?? 1;
            _books.Add(book);
            return Task.FromResult(book);
        }

        public Task<Book> Get(int id)
        {
            return Task.FromResult(_books.FirstOrDefault(c => c.Id == id));
        }

        public Task<List<Book>> GetAll()
        {
            return Task.FromResult(_books.ToList());
        }

        public Task<bool> IsExist(int id)
        {
            return Task.FromResult(_books.Any(c => c.Id == id));
        }

        public void Remove(Book book)
        {
            _books.Remove(book);
        }
    }
}