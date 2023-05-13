using EBookFS.Models.Contracts;

namespace EBookFS.Models.Repositories
{
    public class StaticUserManagerRepository : IUserManagerRepository
    {
        public bool Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
                return true;
            return false;
        }
    }
}
