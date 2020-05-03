using Leave_management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_management.ViewModels
{
    public class LeaveRequestViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Requesting Employee")]
        public EmployeeViewModel RequestingEmployee { get; set; }

        [Display(Name = "Employee Name")]
        public string RequestingEmployeeId { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name ="Leave Type")]
        public LeaveTypeViewModel LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; } //? can be put in with a null state, waiting
        public EmployeeViewModel ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
        [Display(Name = "Request Comments")]
        public bool Cancelled { get; set; }
        [Display(Name = "Employee Comments")]
        [MaxLength(300)]
        public string RequestComments { get; set; }
    }

    public class AdminLeaveRequestViewVM
    {
        [Display(Name = "Total Number of Requests")]
        public int TotalRequests { get; set; }

        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }

        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }

        [Display(Name = "Rejected Requests")]
        public int RejectedRequests { get; set; }

        public List<LeaveRequestViewModel> LeaveRequests { get; set; }
      
    }
    public class CreateLeaveRequestVM
    {

        [Display(Name = "Start Date")]
        [Required]    
        public  string StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required]
        public string EndDate{ get; set; }

        public IEnumerable<SelectListItem> LeaveTypes { get; set; }

        [Display(Name = "Leave Type")]
        public int LeaveTypeId { get; set; }
    }
    public class EmployeeLeaveRequestViewVM
    {
        public List<LeaveAllocationViewModel> LeaveAllocations { get; set; }
        public List<LeaveRequestViewModel> LeaveRequests { get; set; }
    }
                
}
