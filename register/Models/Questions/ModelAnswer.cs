using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace register.Models.Questions
{
    public class ModelAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public int PersonId { get; set; }
    }
}
