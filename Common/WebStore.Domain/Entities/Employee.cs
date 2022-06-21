using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities;

/// <summary>Сотрудник</summary>
[Index(nameof(LastName), nameof(FirstName), nameof(Patronymic), nameof(Age), IsUnique = true)]
public class Employee : Entity
{
    /// <summary>Фамилия</summary>
    [Required]
    public string LastName { get; set; } = null!;

    /// <summary>Имя</summary>
    [Required]
    public string FirstName { get; set; } = null!;

    /// <summary>Отчество</summary>
    public string? Patronymic { get; set; }

    /// <summary>Возраст</summary>
    public int Age { get; set; }

    public override string ToString() => $"(id:{Id}){LastName} {FirstName} {Patronymic} - age:{Age}";
}
