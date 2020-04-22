using AutoMapper;
using Leave_management.Data;
using Leave_management.Models;
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
            CreateMap<LeaveType, LeaveHistoryViewModel>().ReverseMap();
            CreateMap<LeaveType, LeaveAllocationViewModel>().ReverseMap();
            CreateMap<LeaveType, EmployeeViewModel>().ReverseMap();
            

        }
    }
}
