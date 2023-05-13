namespace EBookFS.Models.Contracts
{
    public interface IUserManagerRepository
    {
        bool Login(string username, string password);
    }
}
