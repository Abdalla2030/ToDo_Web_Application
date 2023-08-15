using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo_Web_App.Models
{
    public class ToDo
    {
        public int ID { get; set; }

        [Required(ErrorMessage="Please Enter a Description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter a Due Date.")]
        public DateTime? dueDate { get; set; }

        [Required(ErrorMessage = "Please Select a Category")]
        public string categoryID { get; set; } = string.Empty;
        [ValidateNever]
        public Category category { get; set; } = null!;

        [Required(ErrorMessage = "Please Select a Status")]
        public string statusID { get; set; } = string.Empty;
        [ValidateNever]
        public Status status { get; set; } = null!;

        public bool overDate => statusID == "open" && dueDate < DateTime.Today; 




    }
}
