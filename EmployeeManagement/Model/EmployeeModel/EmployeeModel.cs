using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Model
{
    public class EmployeeModel
    {
        public string EmployeeName { get; set; }

        public string JobDiscription { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        public int DepartmentId { get; set; }
    }
}
