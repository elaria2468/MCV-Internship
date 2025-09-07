// Models/Employee.cs
using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Models

{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
