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
        /// <summary>
        /// edit question in question database 
        /// </summary>
        /// <param name="model"></param>
        private void Update_In_QuestionDatabase(ModelQuestion model)
        {
            _context.Questions.Update(model);
            _context.SaveChanges();
        }

        /// <summary>
        /// add question to question database
        /// </summary>
        /// <param name="question"></param>
        /// <param name="status"></param>
        /// <param name="view"></param>
        private void Add_To_QuestionDatabase(string question , bool status , int view )
        {
            var ques = new ModelQuestion()
            {
                Quaere = question,
                Status = status,
                View = view
            };
            _context.Questions.Add(ques);
            _context.SaveChanges();
        }
        /// <summary>
        /// remove question from database
        /// </summary>
        /// <param name="id"></param>
        private void Remove_From_QuestionDatbase(int id)
        {
            var delete = _context.Questions.Find(id);
            _context.Questions.Remove(delete);
            _context.SaveChanges();
        }

        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View();
        }
        /// <summary>
        /// add question 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
                Add_To_QuestionDatabase(input.Quaere, true, 0);
                return RedirectToAction("ViewQuestions");
            }
            return View();
        }

        /// <summary>
        /// show questions to Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ViewQuestions()
        {
            var model = _context.Questions.ToList();
            return View(model);
        }
        /// <summary>
        /// Delete question 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeleteQuestion(int id)
        {
            Remove_From_QuestionDatbase(id);
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

        /// <summary>
        /// Edit Question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            Update_In_QuestionDatabase(ques);
            return RedirectToAction("ViewQuestions");

        }

        /// <summary>
        /// Active question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ActiveQuestion(int id)
        {
            var status = _context.Questions.Find(id);
            status.Status = true;
            Update_In_QuestionDatabase(status);
            return RedirectToAction("ViewQuestions");

        }
        /// <summary>
        /// Deactivate Question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeactivateQuestion(int id)
        {
            var stu = _context.Questions.Find(id);
            stu.Status = false;
            Update_In_QuestionDatabase(stu);
            return RedirectToAction("ViewQuestions");
        }

        /// <summary>
        /// Show Deactivate Questions to Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ViewDeactivateQuestion()
        {
            var model = _context.Questions.ToList();
            return View(model);
        }

        /// <summary>
        /// Show Result of Answers To Admin
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowResult(string username)
        {
            var usr = _context.Users.Select(x => x.UserName).Distinct().ToList();
            usr.Remove("Admin");
            ViewData["user"] = usr;
            var question1 = _context.Questions.ToList();
            ////Inner join 3 table => output: question  username  rating
            var result = (from question in _context.Questions
                          join answer in _context.Answers on question.Id equals answer.QuestionId
                          join userName in _context.Users on answer.UserId equals userName.Id
                          select new
                          {
                              Queare = question.Quaere,
                              UsrName = userName.UserName,
                              Rating = answer.Rate
                          }).ToList();

            var res = new List<ShowResult>();
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

