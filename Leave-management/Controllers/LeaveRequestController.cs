using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Leave_management.Contracts;
using Leave_management.Data;
using Leave_management.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Leave_management.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        //dependencies
        private readonly ILeaveRequestRepository    _leaveRequestRepo;
        private readonly ILeaveTypeRepository       _leaveTypeRepo;
        private readonly ILeaveAllocationRepository _leaveAllocationRepo;
        private readonly IMapper                    _mapper;
        private readonly UserManager<Employee>      _userManager;
        

        //Controller
        public LeaveRequestController(
            ILeaveRequestRepository leaveRequestRepo,
            ILeaveTypeRepository    leaveTypeRepo,
            ILeaveAllocationRepository leaveAllocationRepo,
            IMapper                 mapper,
            UserManager<Employee>   userManager
        )
        {
            //Initialize
            _leaveRequestRepo    = leaveRequestRepo;
            _leaveTypeRepo       = leaveTypeRepo;
            _leaveAllocationRepo = leaveAllocationRepo;
            _mapper              = mapper;
            _userManager         = userManager;
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveRequest
        public ActionResult Index()
        {
            //Retrieving all the leave request in the Database
            var leaveRequests = _leaveRequestRepo.FindAll();
            var leaveRequestModel = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequests);
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests    = leaveRequestModel.Count,
                ApprovedRequests = leaveRequestModel.Count(q => q.Approved == true ),
                PendingRequests  = leaveRequestModel.Count(q => q.Approved == null ),
                RejectedRequests = leaveRequestModel.Count(q => q.Approved == false),
                LeaveRequests    = leaveRequestModel
            };
            return View(model);
        }

        // GET: LeaveRequest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveRequest/Create
        public ActionResult Create()
        {
            //Retrieving leavetype list from the database
            var leaveTypes     = _leaveTypeRepo.FindAll();
            var leaveTypeItems = leaveTypes.Select(q => new SelectListItem
            {
                Text  = q.Name,
                Value = q.Id.ToString()
            });
            //pass the data into the form
            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypeItems
            };
            //right-click view: add view
            return View(model);
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLeaveRequestVM model)
        {    
            try
            {
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate   = Convert.ToDateTime(model.EndDate);

                //Retrieve leavetype list from the database
                var leaveTypes     = _leaveTypeRepo.FindAll();
                var leaveTypeItems = leaveTypes.Select(q => new SelectListItem         
                {
                    Text  = q.Name,
                    Value = q.Id.ToString()
                });
                model.LeaveTypes = leaveTypeItems;

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (DateTime.Compare(startDate, endDate) > 1) // validation to compare the startDate with EndDate, eliminates user putting garbage into the system
                {
                    ModelState.AddModelError("", "Error...Start date can't be further than End date");
                    return View(model);
                }
               
                var employee      = _userManager.GetUserAsync(User).Result; // retrieve the employee 
                var allocation    = _leaveAllocationRepo.GetLeaveAllocationsByEmployeeAndType(employee.Id, model.LeaveTypeId); //retrieve the allocations that the employee has 
                int daysRequested = (int)(endDate - startDate).TotalDays;

                if (daysRequested > allocation.NumberofDays)
                {
                    ModelState.AddModelError("", "Error...Not enough days to process request");
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestViewModel
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate            = startDate,
                    EndDate              = endDate,
                    DateRequested        = DateTime.Now,
                    DateActioned         = DateTime.Now,
                    LeaveTypeId          = model.LeaveTypeId
                };

                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);
                var isSuccess    = _leaveRequestRepo.Create (leaveRequest);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Submission Error...Contact Your Admininstrator");
                    return View(model);
                }
                return RedirectToAction(nameof(Index),"Home");
            }        
            catch
            {               
                ModelState.AddModelError("", "Error...");
                return View(model);
            }
        }

        // GET: LeaveRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveRequest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}