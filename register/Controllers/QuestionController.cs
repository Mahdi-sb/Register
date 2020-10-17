using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using register.Models.DBcontext;
using register.Models.Questions;
using register.ViewModels.Questions;

namespace register.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {

        private  readonly AppDBcontext _context;
        public QuestionController(AppDBcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddQuestion(Question input)
        {
            if (ModelState.IsValid)
            {
                if (_context.Questions.Where(x => x.Quaere == input.Quaere).Count() > 0)
                {
                    ViewData["ErrorMessage"]=" این سوال موجود است";
                    return View();

                }
                var ques = new ModelQuestion()
                {
                    Quaere = input.Quaere,
                    Status = true,
                    View = 0
                };
                 _context.Questions.Add(ques);
                _context.SaveChanges();
                return RedirectToAction("ViewQuestions");
            }

            return View();

        }

        [HttpGet]
        public IActionResult ViewQuestions()
        {
            var model = _context.Questions.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteQuestion(int id)
        {
            var delete = _context.Questions.Find(id);
            _context.Questions.Remove(delete);
            _context.SaveChanges();
            return RedirectToAction("ViewQuestions");
        }

        [HttpGet]
        public IActionResult EditQuestion(int id)
        {

            var ques = _context.Questions.Find(id);
            var model = new Edit()
            {
                Id = ques.Id,
                Question = ques.Quaere,
            };

            return View(model);

        }
        [HttpPost]
        public IActionResult EditQuestion(Edit edit)
        {
            if (string.IsNullOrEmpty(edit.Question))
            {
                ModelState.AddModelError("", "فیلد را پر کنید");
                return View();
            }
            var ques = _context.Questions.Find(edit.Id);
            ques.Quaere = edit.Question;
            _context.Questions.Update(ques);
            _context.SaveChanges();
            return RedirectToAction("ViewQuestions");



        }

        [HttpGet]
        public IActionResult ActiveQuestion(int id)
        {
            var status = _context.Questions.Find(id);
            status.Status = true;
            _context.Questions.Update(status);
            _context.SaveChanges();
            return RedirectToAction("ViewQuestions");

        }

        [HttpGet]
        public IActionResult DeactivateQuestion(int id)
        {
            var stu = _context.Questions.Find(id);
            stu.Status = false;
            _context.Questions.Update(stu);
            _context.SaveChanges();
            return RedirectToAction("ViewQuestions");
        }

        [HttpGet]
        public IActionResult ViewDeactivateQuestion()
        {
            var model = _context.Questions.ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult ShowResult(string username)
        {
            var usr = _context.Users.Select(x => x.UserName).Distinct().ToList();
            usr.Remove("Admin");
            ViewData["user"] = usr;
            var question1 = _context.Questions.ToList();
            var result = (from question in _context.Questions
                          join answer in _context.Answers on question.Id equals answer.QuestionId
                          join userName in _context.Users on answer.UserId equals userName.Id
                          select new
                          {
                              Queare = question.Quaere,
                              UsrName = userName.UserName,
                              Rating = answer.Rate
                          }).ToList();

            var res = new List< ShowResult>();
            if (string.IsNullOrEmpty(username))
            {
                ViewData["checkFilter"] = false;
                foreach (var item in result)
                {
                    res.Add(new ShowResult() { Queare = item.Queare, Rating = item.Rating, UsrName = item.UsrName });
                    
                }

            }

            else
            {
                ViewData["checkFilter"] = true;
                foreach (var item in result)
                {
                    if (username == item.UsrName)
                    {
                        res.Add(new ShowResult() { Queare = item.Queare, Rating = item.Rating, UsrName = item.UsrName });
                    }

                }
            }
                
            ViewData["result"] = res;
            
             
            return View(question1);
        }

    }
}

