using Microsoft.AspNetCore.Mvc;

using WebStore.Models;

namespace WebStore.Controllers;

public class EmployeesController : Controller
{
    private static readonly List<Employee> __Employees = new()
    {
        new Employee { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 23 },
        new Employee { Id = 2, LastName = "Петров", FirstName = "Пётр", Patronymic = "Петрович", Age = 27 },
        new Employee { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18 },
    };

    public IActionResult Index()
    {
        return View(__Employees);
    }

    public IActionResult Details(int Id)
    {
        var employee = __Employees.FirstOrDefault(x => x.Id == Id);
        if (employee is null)
            return NotFound();

        return View(employee);
    }
}
