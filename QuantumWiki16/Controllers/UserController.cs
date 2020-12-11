using Microsoft.AspNetCore.Mvc;
using QuantumWiki16.Models;
using System.Linq;

namespace QuantumWiki16.Controllers
{
    public class UserController : Controller
    {
        //   F i e l d s   &   P r o p e r t i e s

        IUserRepository _repository;

        //   C o n s t r u c t o r s

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        //   M e t h o d s

        public IActionResult Index()
        {
            IQueryable<User> userList = _repository.GetAllUsers();
            return View(userList);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User u)
        {
            if (_repository.Login(u))
            {
                return RedirectToAction("Index", "Tutorial");
            }
            return View(u);
        }  // after mon

        [HttpGet]
        public IActionResult Create()
        {
            return View();  // if I don't offer the title of a view, it goes to the Create view.
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _repository.AddUser(user);
                //                System.Threading.Thread.Sleep(200); // sleep for 2 seconds
                //return RedirectToAction("Detail", new { userId = user.Email });
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(user);
            }

        }

        [HttpGet]
        public IActionResult Detail(int userId)
        {
            User u = _repository.GetUserById(userId);
            return View(u);
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string keyword)
        {
            IQueryable<User> someUsers = _repository.GetUserByKeyword(keyword);
            return View("Index", someUsers);
        }
    }
}
