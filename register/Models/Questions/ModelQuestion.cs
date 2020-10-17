using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace register.Models.Questions
{
    public class ModelQuestion
    {
        public int Id { get; set; }
        public string Quaere { get; set; }
        public bool Status { get; set; }
        public int View { get; set; }


    }
}
