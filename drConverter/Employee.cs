using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace drConverter
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
    }
}
