using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace register.ViewModels.Questions
{
    public class Question
    {
        [Required(ErrorMessage ="سوال را وارد کنید")]
        [Display(Name ="سوال")]
        public string Quaere { get; set; }


    }
}
