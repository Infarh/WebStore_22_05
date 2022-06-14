using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Domain.ViewModels;

public class EmployeeViewModel : IValidatableObject
{
    [HiddenInput(DisplayValue = false)]
    [Display(Name = "Идентификатор")]
    public int Id { get; set; }

    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Фамилия обязательна")]
    [StringLength(10, MinimumLength = 2, ErrorMessage = "Длина строки от 2 до 10 символов")]
    [RegularExpression("([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат имени. Либо все русские буквы, либо все латинские. Первая - заглавная.")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(10, MinimumLength = 2, ErrorMessage = "Длина строки от 2 до 10 символов")]
    public string Name { get; set; } = null!;

    [Display(Name = "Отчество")]
    [StringLength(10, ErrorMessage = "Длина строки до 10 символов")]
    public string? Patronymic { get; set; }

    [Display(Name = "Возраст")]
    [Range(18, 80, ErrorMessage = "Возраст должен быть в диапазоне от 18 до 80 лет")]
    public int Age { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext Context)
    {
        if (LastName == "Zxc" && Name == "Zxc" && Patronymic == "Zxc")
            return new[]
            {
                new ValidationResult("Везде Zxc", new []
                {
                    nameof(LastName), 
                    nameof(Name), 
                    nameof(Patronymic)
                })
            };

        return new[]
        {
            ValidationResult.Success!, 
        };
    }
}
