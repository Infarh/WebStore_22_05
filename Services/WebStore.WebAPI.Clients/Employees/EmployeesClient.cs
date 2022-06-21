using System.Net;
using System.Net.Http.Json;

using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.WebAPI.Clients.Base;

namespace WebStore.WebAPI.Clients.Employees;

public class EmployeesClient : BaseClient, IEmployeesData
{
    public EmployeesClient(HttpClient Client)
        : base(Client, "api/employees")
    {
    }

    public int GetCount()
    {
        var result = Get<int>($"{Address}/count");
        return result;
    }

    public IEnumerable<Employee> GetAll()
    {
        var result = Get<IEnumerable<Employee>>(Address);
        return result ?? Enumerable.Empty<Employee>();
    }

    public IEnumerable<Employee> Get(int Skip, int Take)
    {
        var result = Get<IEnumerable<Employee>>($"{Address}/[{Skip}:{Take}]");
        return result ?? Enumerable.Empty<Employee>();
    }

    public Employee? GetById(int id)
    {
        var result = Get<Employee>($"{Address}/{id}");
        return result;
    }

    public int Add(Employee employee)
    {
        var response = Post(Address, employee);
        var added_employee = response.Content.ReadFromJsonAsync<Employee>().Result;
        if (added_employee is null)
            throw new InvalidOperationException("Не удалось добавить сотрудника");

        var id = added_employee.Id;
        employee.Id = id;
        return id;
    }

    public bool Edit(Employee employee)
    {
        var response = Put(Address, employee);

        //return response.StatusCode == HttpStatusCode.OK;
        //return response.IsSuccessStatusCode;

        var result = response
           .Content
           .ReadFromJsonAsync<bool>()
           .Result;

        return result;
    }

    public bool Delete(int Id)
    {
        var response = Delete($"{Address}/{Id}");
        var success = response.IsSuccessStatusCode;
        return success;
    }
}
