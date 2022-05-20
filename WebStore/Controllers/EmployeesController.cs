using Microsoft.AspNetCore.Mvc;

using WebStore.Services.Interfaces;

namespace WebStore.Controllers;

//[Route("Staff/{action=Index}/{Id?}")]
public class EmployeesController : Controller
{
    private readonly IEmployeesData _Employees;

    public EmployeesController(IEmployeesData Employees) => _Employees = Employees;

    public IActionResult Index()
    {
        var employees = _Employees.GetAll();
        return View(employees);
    }

    //[Route("Staff/Info/{Id}")]
    //[Route("[controller]/Info/{Id}")]
    public IActionResult Details(int Id)
    {
        var employee = _Employees.GetById(Id);
        if (employee is null)
            return NotFound();

        return View(employee);
    }

    public IActionResult Create() => View();

    public IActionResult Edit(int Id) => View();

    public IActionResult Delete(int Id) => View();
}
