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
                user.Password = encrypt(user.Password);  // this is where it should fail and drop out before adding the user
                _context.Users.Add(user);  // method in base class of appDbContext
                _context.SaveChanges();  // same
            }
            catch (Exception ex)
            {
                user.Id = -1;  // failed to add, that user object gets a bad Id - will fail to update. Must be deleted and re-entered.
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

        public IQueryable<User> GetUserByName(string keyword)
        {
            return _context.Users.Where(u => u.Name.Contains(keyword));
        }  // end get User By keyword

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }  // end get user by email (new key)

        public bool Login(User u)
        {
            User knownUser = GetUserByEmail(u.Email);

            if (knownUser == null)  // no user by that email
            {
                return false;  // failed to log in
            }
            // otherwise, have valid user and continue with login
            u.Password = encrypt(u.Password);

            if (knownUser.Password == u.Password)
            {
                _session.SetInt32("userId", knownUser.Id);
                _session.SetString("userEmail", knownUser.Email);
                _session.SetString("member", knownUser.Member.ToString());
                return true;
            }
            return false;  // password mismatch
        }  // end Login

        public void LoginAsGuest()
        {
            _session.SetInt32("userId", 0);
            _session.SetString("userEmail", "guest@quantumwiki.com");
            _session.SetString("member", "false");
        }  // end Login as Guest

        public bool IsMember()
        {
            int id = _session.GetInt32("userId").Value;  // session will return some int
            User u;
            if (id > 0) // all but guests // did not get a null user
            {
                    u = _context.Users.Find(GetUserBySessionId());  // did they select membership?
                    return u.Member;
                }
              // null and guests get false
            return false;
        }  // is member // now accounts for guests

        public int GetUserBySessionId()
        {
            int? currentUserId = _session.GetInt32("userId");
            if (currentUserId != null)
            {
                return currentUserId.Value;
            }
            return -1;
        }  // end Get user by Session

        private string encrypt(string password)  // lowercase local method because its private. Not in Interface.
        {
            SHA256 myHashingVar = SHA256.Create();
            byte[] passwordByteArray = Encoding.ASCII.GetBytes(password);
            passwordByteArray[0] += 1;
            passwordByteArray[2] += 1;
            passwordByteArray[4] += 1;
            byte[] hashedPasswordByteArray = myHashingVar.ComputeHash(passwordByteArray);
            string hashedPassword = "";
            foreach (byte b in hashedPasswordByteArray)
            {
                hashedPassword += b.ToString("x2");
            }
            return hashedPassword;
        }  // end Encrypt

        public int GetLoggedInUserId()
        {
            int? userId = _session.GetInt32("userId");
            if (userId == null)
            {
                return -1;
            }
            else
            {
                return userId.Value; // Value is an int
            }
        }  // end Get Logged in user Id


        // U p d a t e

        public User UpdateUser(User user)
        {
            User userToUpdate = _context.Users.SingleOrDefault(u => u.Email == user.Email);
            if (userToUpdate != null)
            {
                //                userToUpdate.Id = user.Id;  // key. this will never be necessary. Just written for understanding.
                try
                {  // first try to encrypt unknown pw. This is most likely to fail.
                    user.Password = encrypt(user.Password);  // encrypt before storing. If fails, no other work done.
                    userToUpdate.Name = user.Name;
                    userToUpdate.Password = user.Password;
                    userToUpdate.Member = user.Member;
                    _context.SaveChanges();
                }
                catch
                {
                    user.Id = -1;  // set temp user object id to -1 and return it intead. PW likely failed.
                    return user;
                }
            }
            user.Password = ""; // get rid of pw immediately
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
        }  // end Delete user

        public void Logout()
        {
            _session.Remove("userEmail");
            _session.Remove("userId");
            _session.Remove("member");
        }  // end Logout
    }
}
