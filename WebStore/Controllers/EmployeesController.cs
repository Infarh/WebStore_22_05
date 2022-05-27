using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Mapping;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

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

    public IActionResult Create() => View("Edit", new EmployeeViewModel());

    public IActionResult Edit(int? Id)
    {
        if (Id is null)
            return View(new EmployeeViewModel());

        var employee = _Employees.GetById((int)Id);
        if (employee is null)
            return NotFound();

        var view_model = employee.ToView();
        //var view_model = new EmployeeViewModel
        //{
        //    Id = employee.Id,
        //    LastName = employee.LastName,
        //    Name = employee.FirstName,
        //    Patronymic = employee.Patronymic,
        //    Age = employee.Age,
        //};

        return View(view_model);
    }

    [HttpPost]
    public IActionResult Edit(EmployeeViewModel Model)
    {
        if(Model.LastName == "Qwe" && Model.Name == "Qwe" && Model.Patronymic == "Qwe")
            ModelState.AddModelError("", "Qwe - плохой выбор");

        if(Model.Name == "Asd")
            ModelState.AddModelError("Name", "Asd - неважное имя");

        if (!ModelState.IsValid)
            return View(Model);

        var employee = Model.FromView();
        //var employee = new Employee
        //{
        //    Id = Model.Id,
        //    LastName = Model.LastName,
        //    FirstName = Model.Name,
        //    Patronymic = Model.Patronymic,
        //    Age = Model.Age,
        //};

        if (Model.Id == 0)
        {
            var new_employee_id = _Employees.Add(employee);
            return RedirectToAction(nameof(Details), new { Id = new_employee_id });
        }

        _Employees.Edit(employee);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int Id)
    {
        //_Employees.Delete(Id);                    // Так жить нельзя!
        //return RedirectToAction(nameof(Index));   // Так жить нельзя!

        var employee = _Employees.GetById(Id);
        if (employee is null)
            return NotFound();

        var view_model = new EmployeeViewModel
        {
            Id = employee.Id,
            LastName = employee.LastName,
            Name = employee.FirstName,
            Patronymic = employee.Patronymic,
            Age = employee.Age,
        };

        return View(view_model);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int Id)
    {
        if (!_Employees.Delete(Id))
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}
