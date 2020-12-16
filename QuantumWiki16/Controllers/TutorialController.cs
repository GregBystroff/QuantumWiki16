using Microsoft.AspNetCore.Mvc;
using QuantumWiki16.Models;
using System.Linq;

namespace QuantumWiki16.Controllers
{
    public class TutorialController : Controller
    {
        //   F i e l d s   &   P r o p e r t i e s

        ITutorialRepository _tutorialRepository;
        IUserRepository _userRepository;

        //   C o n s t r u c t o r s

        public TutorialController(ITutorialRepository repository, IUserRepository userRepository)
        {
            _tutorialRepository = repository;
            _userRepository = userRepository;
        }
        //   M e t h o d s

        //   c r e a t e

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }  // end Add tutorial get

        [HttpPost]
        public IActionResult Add(Tutorial tut)
        {
            if (ModelState.IsValid)
            {
                _tutorialRepository.AddTutorial(tut);
                return RedirectToAction("Index", "Tutorial");
            }
            else
            {
                return View(tut);
            }
        }  // end Add tutorial post

        //   r e a d

        public IActionResult Index()  // list of tutorials
        {
            IQueryable<Tutorial> tutorialList;
            tutorialList = _tutorialRepository.GetAllTutorials();
            return View(tutorialList);
        }  // end Index

        [HttpGet]
        public IActionResult SearchBySubject()
        {
            return View();
        }  // end get SearchSubject

        [HttpPost]
        public IActionResult SearchBySubject(string keyword)
        {
            IQueryable<Tutorial> results = _tutorialRepository.GetTutorialBySubject(keyword);
            return View("Index", results);
        }  // end Search Subject

        [HttpGet]
        public IActionResult SearchByTitle()
        {
            return View();
        }  // end get SearchByTitle

        [HttpPost]
        public IActionResult SearchByTitle(string keyword)
        {
            IQueryable<Tutorial> results = _tutorialRepository.GetTutorialByTitle(keyword);
            return View("Index", results);
        }  // end Search by title

        [HttpGet]
        public IActionResult SearchById()
        {
            return View();
        }  // end get SearchById

        [HttpPost]
        public IActionResult SearchById(int id)
        {
            Tutorial result = _tutorialRepository.GetTutorialById(id);
            return View("Detail", result);
        }  // end Search by Id

        //   u p d a t e

        [HttpGet]
        public IActionResult Edit(int tutid)
        {
            Tutorial tutToUpdate = _tutorialRepository.GetTutorialById(tutid);
            return View(tutToUpdate);
        } // end get to Edit

        [HttpPost]
        public IActionResult Edit(Tutorial t)
        {
            _tutorialRepository.UpdateTutorial(t);
            return RedirectToAction("Index", "Tutorial");
        }  // end Edit Post

        //   d e l e t e

        [HttpGet]
        public IActionResult Delete(int tutid)
        {
            // User loggedInUser = _userRepository.GetUserById(_userRepository.GetUserBySessionId()); // Why get the logged in user?
            Tutorial tutToDelete = _tutorialRepository.GetTutorialById(tutid);
            if (tutToDelete != null)
            {
                _tutorialRepository.DeleteTutorial(tutid);
                return RedirectToAction("Index", "Tutorial");
            }
            ViewBag.ErrorMessage = "Attempt to delete a null tutorial";
            return View();

        } // end Edit get

        //[HttpPost]
        //public IActionResult Delete(Tutorial t)
        //{
        //    _tutorialRepository.DeleteTutorial(t.TutId);
        //    return RedirectToAction("Index", "Tutorial");
        //}  // end Edit Post


    }
}
