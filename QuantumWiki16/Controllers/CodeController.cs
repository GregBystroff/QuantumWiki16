using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuantumWiki16.Models;

namespace QuantumWiki16.Controllers
{
    public class CodeController 
        : Controller
    {
        //   F i e l d s   &   P r o p e r t i e s

        ICodeRepository _codeRepository;
        IUserRepository _userRepository;

        //   C o n s t r u c t o r s

        public CodeController(ICodeRepository repository, IUserRepository userRepository)
        {
            _codeRepository = repository;
            _userRepository = userRepository;
        }
        //   M e t h o d s

        //   c r e a t e

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }  // end Add code segment get

        [HttpPost]
        public IActionResult Add(Code code)
        {
            if (ModelState.IsValid)
            {
                _codeRepository.AddCode(code);
                return RedirectToAction("Detail", code);
            }
            else
            {
                return View(code);
            }
        }  // end Add code post

        //   r e a d

        public IActionResult Index()  // list of code segments
        {
            IQueryable<Code> codeList;
            codeList = _codeRepository.GetAllCode();
            return View(codeList);
        }  // end Index all code

        [HttpGet]
        public IActionResult SearchByDescription()
        {
            return View();
        }  // end get SearchByDescription

        [HttpPost]
        public IActionResult SearchByDescription(string keyword)
        {
            IQueryable<Code> results = _codeRepository.GetCodeByDescription(keyword);
            return View("Index", results);
        }  // end Search by description

        [HttpGet]
        public IActionResult SearchByTitle()
        {
            return View();
        }  // end get SearchByTitle

        [HttpPost]
        public IActionResult SearchByTitle(string keyword)
        {
            IQueryable<Code> results = _codeRepository.GetCodeByTitle(keyword);
            return View("Index", results);
        }  // end Search by title

        [HttpGet]
        public IActionResult SearchByCodeId()
        {
            return View();
        }  // end get SearchByCodeId

        [HttpPost]
        public IActionResult SearchByCodeId(int id)
        {
            Code result = _codeRepository.GetCodeById(id);
            return View("Detail", result);
        }  // end post Search by code id

        //   u p d a t e

        [HttpGet]
        public IActionResult Edit(int codeId)
        {
            Code codeToUpdate = _codeRepository.GetCodeById(codeId);
            return View(codeToUpdate);
        } // end get Edit

        [HttpPost]
        public IActionResult Edit(Code c)
        {
            _codeRepository.UpdateCode(c);
            return RedirectToAction("Index", "Code");
        }  // end post Edit

        //   d e l e t e

        [HttpGet]
        public IActionResult Delete(int codeId)
        {
            // User loggedInUser = _userRepository.GetUserById(_userRepository.GetUserBySessionId()); // Why get the logged in user?
            Code codeToDelete = _codeRepository.GetCodeById(codeId);
            if (codeToDelete != null)
            {
                _codeRepository.DeleteCode(codeId);
                return RedirectToAction("Index", "Code");
            }
            ViewBag.ErrorMessage = "Attempt to delete a null code segment";
            return View();

        } // end get Delete
    }
}
