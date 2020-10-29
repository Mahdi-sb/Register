using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using register.Models.DBcontext;
using register.Models.Questions;

namespace register.Controllers
{
    [Authorize(Roles = "User")]
    public class AnswerController : Controller
    {
        private readonly AppDBcontext _context;
        public AnswerController(AppDBcontext context)
        {
            _context = context;
        }
        /// <summary>
        /// show questions to user
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowQuestion()
        {
            var useId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var show = _context.Questions.ToList();
            ViewData["Rate"] = _context.Answers.Where(x => x.UserId == useId).ToList();
            return View(show);
        }

        /// <summary>
        /// save rating 
        /// </summary>
        /// <param name="rate"></param>
        /// <param name="qusId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RateQuestion(int rate, int qusId)
        {
            var useId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new ModelAnswer()
            {
                QuestionId = qusId,
                Rate = rate,
                UserId = useId
            };
            _context.Answers.Add(model);

            var find = _context.Questions.Find(qusId);
            find.View = find.View + 1;
            _context.Questions.Update(find);
            _context.SaveChanges();

            ViewData["Rate"] = _context.Answers.Find(qusId);
            return RedirectToAction("ShowQuestion");
        }


        
    }
}
