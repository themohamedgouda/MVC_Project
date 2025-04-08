using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class DepartmentController(IDepartmentServices departmentservices) : Controller
    {

        public IActionResult Index()
        
        {
            var departments = departmentservices.GetAllDepartments();
            return View(departments);
        }
    }
}
