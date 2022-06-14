using Microsoft.Extensions.Logging;
using WebStore.DAL.Context;
using WebStore.DAL.Migrations;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.InSQL;

public class SqlEmployeesData : IEmployeesData
{
    private readonly WebStoreDB _db;
    private readonly ILogger<SqlEmployeesData> _Logger;

    public SqlEmployeesData(WebStoreDB db, ILogger<SqlEmployeesData> Logger)
    {
        _db = db;
        _Logger = Logger;
    }

    public int GetCount() => _db.Employees.Count();

    public IEnumerable<Employee> GetAll() => _db.Employees;

    public IEnumerable<Employee> Get(int Skip, int Take)
    {
        if (Take == 0) return Enumerable.Empty<Employee>();

        IQueryable<Employee> query = _db.Employees;

        if(Skip > 0)
            query = query.Skip(Skip);

        return query.Take(Take);
    }

    //public Employee? GetById(int id) => _db.Employees.FirstOrDefault(e => e.Id == id);
    //public Employee? GetById(int id) => _db.Employees.SingleOrDefault(e => e.Id == id);
    public Employee? GetById(int id) => _db.Employees.Find(id);

    public int Add(Employee employee)
    {
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        _db.Employees.Add(employee);

        _db.SaveChanges();

        _Logger.LogInformation("Сотрудник {0} добавлен", employee);

        return employee.Id;
    }

    public bool Edit(Employee employee)
    {
        if (employee is null) throw new ArgumentNullException(nameof(employee));

        var db_employee = GetById(employee.Id);
        if (db_employee is null)
        {
            _Logger.LogWarning("При попытке редактирования сотрудника {0} - запись не найдена", employee);
            return false;
        }

        db_employee.Id = employee.Id;
        db_employee.LastName = employee.LastName;
        db_employee.FirstName = employee.FirstName;
        db_employee.Patronymic = employee.Patronymic;
        db_employee.Age = employee.Age;

        _db.SaveChanges();

        _Logger.LogInformation("Сотрудник {0} отредактирован", employee);

        return true;

    }

    public bool Delete(int Id)
    {
        //var employee = GetById(Id);
        var employee = _db.Employees
           .Select(e => new Employee { Id = e.Id })
           .FirstOrDefault(e => e.Id == Id);
        if (employee is null)
        {
            //_Logger.LogWarning($"При попытке удаления сотрудника с id:{Id} - запись не найдена");
            _Logger.LogWarning("При попытке удаления сотрудника с id:{0} - запись не найдена", Id);

            return false;
        }

        _db.Remove(employee);

        _db.SaveChanges();

        return true;
    }
}
