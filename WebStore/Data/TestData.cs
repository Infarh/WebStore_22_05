using WebStore.Models;

namespace WebStore.Data;

public static class TestData
{
    public static ICollection<Employee> Employees { get; } = new List<Employee>
    {
        new() { Id = 1, LastName = "Иванов", FirstName = "Иван", Patronymic = "Иванович", Age = 23 },
        new() { Id = 2, LastName = "Петров", FirstName = "Пётр", Patronymic = "Петрович", Age = 27 },
        new() { Id = 3, LastName = "Сидоров", FirstName = "Сидор", Patronymic = "Сидорович", Age = 18 },
    };
}
