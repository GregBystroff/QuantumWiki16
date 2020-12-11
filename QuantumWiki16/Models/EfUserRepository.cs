using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QuantumWiki16.Models
{
    public class EfUserRepository
        : IUserRepository
    {
        //  F i e l d s   &   P r o p e r t i e s 

        private AppDbContext _context;
        private ISession _session;

        //   C o n s t r u c t o r s

        public EfUserRepository(AppDbContext context, IHttpContextAccessor sessionContext)
        {
            _context = context;
            _session = sessionContext.HttpContext.Session;  // currently created session
        }

        // C r e a t e

        public User AddUser(User user)
        {
            try
            {
                user.Password = encrypt(user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                user.Id = -1;  // failed to add, that user object gets a bad Id
            }
            user.Password = ""; // get rid of pw immediately either way
            return user;

        }

        // R e a d

        public IQueryable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public IQueryable<User> GetUserByKeyword(string keyword)
        {
            return _context.Users.Where(u => u.Name.Contains(keyword));
        }  // end get User By keyword

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }  // end get user by email (new key)

        public bool Login(User u)
        {
            User knownUser = GetUserByEmail(u.Email);

            if (knownUser == null)
            {
                return false;
            }

            u.Password = encrypt(u.Password);

            if (knownUser.Password == u.Password)
            {
                _session.SetInt32("userId", knownUser.Id);
                _session.SetString("userEmail", knownUser.Email);
                //if (userIfExists.IsAdmin == true)
                //{
                //    _session.SetInt32("userAdmin", 1);
                //}
                //else
                //{
                //    _session.SetInt32("userAdmin", 0);
                //}
                return true;
            }

            return false;
        }  // end Login

        private string encrypt(string password)  // lowercase local method because its private. Not in Interface.
        {
            SHA256 myHashingVar = SHA256.Create();
            byte[] passwordByteArray = Encoding.ASCII.GetBytes(password);
            //passwordByteArray[0] += 1;
            //passwordByteArray[1] += 2;
            //passwordByteArray[2] += 3;
            //passwordByteArray[3] += 4;
            byte[] hashedPasswordByteArray = myHashingVar.ComputeHash(passwordByteArray);
            string hashedPassword = "";
            foreach (byte b in hashedPasswordByteArray)
            {
                hashedPassword += b.ToString("x2");
            }
            return hashedPassword;
        }  // end Encrypt


        // U p d a t e

        public User UpdateUser(User user)
        {
            User userToUpdate = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            if (userToUpdate != null)
            {
                userToUpdate.Name = user.Name;
                userToUpdate.Password = user.Password;
                userToUpdate.Member = user.Member;
                _context.SaveChanges();
            }
            return userToUpdate;
        }

        // D e l e t e

        public bool DeleteUser(int id)
        {
            User userToDelete = _context.Users.Find(id);
            if (userToDelete == null)
            {
                return false;
            }
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
            return true;
        }
    }
}
