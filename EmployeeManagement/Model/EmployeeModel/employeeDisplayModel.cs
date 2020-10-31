using System;

namespace EmployeeManagement.Model
{
    public class employeeDisplayModel
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string JobDiscription { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        public int DepartmentId { get; set; }

        public string departmentName { get; set; }

        public string Location { get; set; }
    }
}
