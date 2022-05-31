using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InMemory;

public class InMemoryEmployeesData : IEmployeesData
{
    private readonly ILogger<InMemoryEmployeesData> _Logger;
    private readonly ICollection<Employee> _Employees;
    private int _LastFreeId;

    public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> Logger)
    {
        _Employees = TestData.Employees;
        _Logger = Logger;

        if (_Employees.Count > 0)
            _LastFreeId = _Employees.Max(e => e.Id) + 1;
        else
            _LastFreeId = 1;
    }

    public int GetCount() => _Employees.Count;

    public IEnumerable<Employee> GetAll()
    {
        return _Employees;
    }

    public IEnumerable<Employee> Get(int Skip, int Take) //=> _Employees.Skip(Skip).Take(Take);
    {
        IEnumerable<Employee> query = _Employees;

        if (Take == 0) return Enumerable.Empty<Employee>();

        if (Skip > 0)
        {
            if(Skip > _Employees.Count) return Enumerable.Empty<Employee>();

            query = query.Skip(Skip);
        }

        return query.Take(Take);
    }

    public Employee? GetById(int id)
    {
        var employee = _Employees.FirstOrDefault(e => e.Id == id);
        return employee;
    }

    public int Add(Employee employee)
    {
        if (employee is null)
            throw new ArgumentNullException(nameof(employee));

        // это нужно делать только для хранения данных в памяти! Если используется БД, то этого делать не надо!
        if (_Employees.Contains(employee))
            return employee.Id;

        employee.Id = _LastFreeId;  // этого тоже делать не надо, если работаем с БД!!!
        _LastFreeId++;              // этого тоже делать не надо, если работаем с БД!!!

        _Employees.Add(employee);

        // Если работа с БД, то не забыть вызвать SaveChanges() тут!

        _Logger.LogInformation("Сотрудник {0} добавлен", employee);

        return employee.Id;
    }

    public bool Edit(Employee employee)
    {
        if (employee is null) throw new ArgumentNullException(nameof(employee));

        // это нужно делать только для хранения данных в памяти! Если используется БД, то этого делать не надо!
        if (_Employees.Contains(employee))
            return true;

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

        // Если работа с БД, то не забыть вызвать SaveChanges() тут!

        _Logger.LogInformation("Сотрудник {0} отредактирован", employee);

        return true;
    }

    public bool Delete(int Id)
    {
        var employee = GetById(Id);
        if (employee is null)
        {
            //_Logger.LogWarning($"При попытке удаления сотрудника с id:{Id} - запись не найдена");
            _Logger.LogWarning("При попытке удаления сотрудника с id:{0} - запись не найдена", Id);

            return false;
        }

        _Employees.Remove(employee);

        _Logger.LogInformation("Сотрудник {0} удалён", employee);

        return true;
    }
}
