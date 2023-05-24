namespace EBookFS.Models.Contracts
{
    public interface IBookRepository
    {
        Task<Book> Get(int id);
        Task<List<Book>> GetAll();
        Task Add(Book book);
        void Remove(Book book);
        Task<bool> IsExist(int id);
    }
}
