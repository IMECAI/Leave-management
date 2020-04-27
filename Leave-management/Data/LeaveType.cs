using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leave_management.Data
{
    public class LeaveType
    {
        [Key] // This is the primary key generated for this table
        public int Id { get; set; }

        [Required] // The property can't be null 
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public DateTime DateCreated { get; set; }
    }
    
}
