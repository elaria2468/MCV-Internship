// Models/Department.cs
using System.Collections.Generic;

namespace CompanyApi.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        // Self relation: Manager هو موظف
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
