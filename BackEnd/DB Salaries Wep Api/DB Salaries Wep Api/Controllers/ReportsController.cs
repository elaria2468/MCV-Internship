using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyApi.Data;


namespace CompanyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ReportsController(AppDbContext context) { _context = context; }

        // 1) ثاني أعلى مرتب
        [HttpGet("SecondHighestSalary")]
        public async Task<ActionResult<decimal?>> GetSecondHighestSalary()
        {
            var result = await _context.Employees
                .OrderByDescending(e => e.Salary)
                .Skip(1)
                .Select(e => (decimal?)e.Salary)
                .FirstOrDefaultAsync();

            return result ?? 0;
        }

        // 2) ترتيب الموظفين حسب المرتب داخل كل قسم
        [HttpGet("EmployeesBySalary")]
        public async Task<ActionResult> GetEmployeesBySalary()
        {
            var result = await _context.Employees
                .Include(e => e.Department)
                .OrderByDescending(e => e.Salary)
                .Select(e => new {
                    Department = e.Department.DepartmentName,
                    Employee = e.Name,
                    e.Salary
                })
                .ToListAsync();

            return Ok(result);
        }

        // 3) الأقسام مرتبة حسب إجمالي المرتبات
        [HttpGet("TopDepartments")]
        public async Task<ActionResult> GetTopDepartments()
        {
            var result = await _context.Departments
                .Select(d => new {
                    d.DepartmentName,
                    TotalSalaries = d.Employees.Sum(e => e.Salary),
                    Manager = d.Manager != null ? d.Manager.Name : null
                })
                .OrderByDescending(x => x.TotalSalaries)
                .ToListAsync();

            return Ok(result);
        }
    }
}
