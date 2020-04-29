﻿using AutoMapper;
using Leave_management.Data;
using Leave_management.Models;
using Leave_management.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_management.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestViewModel>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationViewModel>().ReverseMap();
            CreateMap<LeaveAllocation, EditLeaveAllocationViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
