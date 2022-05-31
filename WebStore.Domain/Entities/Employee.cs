using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities;

public class Employee : Entity
{
    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int Age { get; set; }

    public override string ToString() => $"(id:{Id}){LastName} {FirstName} {Patronymic} - age:{Age}";
}
