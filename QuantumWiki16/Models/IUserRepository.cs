using System.Linq;

namespace QuantumWiki16.Models
{
    public interface IUserRepository
    {
        // C r e a t e
        
        public User AddUser(User u);
        
        // R e a d

        public IQueryable<User> GetAllUsers();
        public User GetUserById(int id);
        public IQueryable<User> GetUserByName(string keyword);
        public User GetUserByEmail(string email);
        public bool Login(User u);
        public void LoginAsGuest();
        public int GetUserBySessionId();
        public bool IsMember();

        // U p d a t e

        public User UpdateUser(User u);

        // D e l e t e

        public bool DeleteUser(int id);
        public void Logout();


    }
}
