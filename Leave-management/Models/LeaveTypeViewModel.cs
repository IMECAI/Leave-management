using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_management.Models
{
    public class LeaveTypeViewModel
    {
        //view model is a display only to the user; however i can set the view up how i like
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Date Created")]//Puts a space between Date Created on the view page
        public DateTime? DateCreated { get; set; }
    }
}
 
