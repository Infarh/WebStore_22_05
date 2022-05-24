namespace WebStore.Models;

public class Employee
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int Age { get; set; }

    public override string ToString() => $"(id:{Id}){LastName} {FirstName} {Patronymic} - age:{Age}";
}
