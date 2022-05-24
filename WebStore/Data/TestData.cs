using WebStore.Domain.Entities;
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

    /// <summary>Секции</summary>
    public static IEnumerable<Section> Sections { get; } = new Section[]
    {
          new() { Id = 01, Name = "Спорт", Order = 0 },
          new() { Id = 02, Name = "Nike", Order = 0, ParentId = 1 },
          new() { Id = 03, Name = "Under Armour", Order = 1, ParentId = 1 },
          new() { Id = 04, Name = "Adidas", Order = 2, ParentId = 1 },
          new() { Id = 05, Name = "Puma", Order = 3, ParentId = 1 },
          new() { Id = 06, Name = "ASICS", Order = 4, ParentId = 1 },
          new() { Id = 07, Name = "Для мужчин", Order = 1 },
          new() { Id = 08, Name = "Fendi", Order = 0, ParentId = 7 },
          new() { Id = 09, Name = "Guess", Order = 1, ParentId = 7 },
          new() { Id = 10, Name = "Valentino", Order = 2, ParentId = 7 },
          new() { Id = 11, Name = "Диор", Order = 3, ParentId = 7 },
          new() { Id = 12, Name = "Версачи", Order = 4, ParentId = 7 },
          new() { Id = 13, Name = "Армани", Order = 5, ParentId = 7 },
          new() { Id = 14, Name = "Prada", Order = 6, ParentId = 7 },
          new() { Id = 15, Name = "Дольче и Габбана", Order = 7, ParentId = 7 },
          new() { Id = 16, Name = "Шанель", Order = 8, ParentId = 7 },
          new() { Id = 17, Name = "Гуччи", Order = 9, ParentId = 7 },
          new() { Id = 18, Name = "Для женщин", Order = 2 },
          new() { Id = 19, Name = "Fendi", Order = 0, ParentId = 18 },
          new() { Id = 20, Name = "Guess", Order = 1, ParentId = 18 },
          new() { Id = 21, Name = "Valentino", Order = 2, ParentId = 18 },
          new() { Id = 22, Name = "Dior", Order = 3, ParentId = 18 },
          new() { Id = 23, Name = "Versace", Order = 4, ParentId = 18 },
          new() { Id = 24, Name = "Для детей", Order = 3 },
          new() { Id = 25, Name = "Мода", Order = 4 },
          new() { Id = 26, Name = "Для дома", Order = 5 },
          new() { Id = 27, Name = "Интерьер", Order = 6 },
          new() { Id = 28, Name = "Одежда", Order = 7 },
          new() { Id = 29, Name = "Сумки", Order = 8 },
          new() { Id = 30, Name = "Обувь", Order = 9 },
    };

    /// <summary>Бренды</summary>
    public static IEnumerable<Brand> Brands { get; } = new Brand[]
    {
        new() { Id = 1, Name = "Acne", Order = 0 },
        new() { Id = 2, Name = "Grune Erde", Order = 1 },
        new() { Id = 3, Name = "Albiro", Order = 2 },
        new() { Id = 4, Name = "Ronhill", Order = 3 },
        new() { Id = 5, Name = "Oddmolly", Order = 4 },
        new() { Id = 6, Name = "Boudestijn", Order = 5 },
        new() { Id = 7, Name = "Rosch creative culture", Order = 6 },
    };
}
