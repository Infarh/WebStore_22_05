using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Employees;

public class EmployeesClient : BaseClient, IEmployeesData
{
    public EmployeesClient(HttpClient Client, string Address /* Сперциально оставлена ошибка! */) 
        : base(Client, "api/employees")
    {
    }

    public int GetCount() { throw new NotImplementedException(); }

    public IEnumerable<Employee> GetAll() { throw new NotImplementedException(); }

    public IEnumerable<Employee> Get(int Skip, int Take) { throw new NotImplementedException(); }

    public Employee? GetById(int id) { throw new NotImplementedException(); }

    public int Add(Employee employee) { throw new NotImplementedException(); }

    public bool Edit(Employee employee) { throw new NotImplementedException(); }

    public bool Delete(int Id) { throw new NotImplementedException(); }
}
