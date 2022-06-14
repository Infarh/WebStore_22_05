using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities;

[Index(nameof(Name), IsUnique = false)]
public class Product : NamedEntity, IOrderedEntity
{
    public int Order { get; set; }

    public int SectionId { get; set; }

    [Required]
    [ForeignKey(nameof(SectionId))]
    public Section Section { get; set; } = null!;

    public int? BrandId { get; set; }

    [ForeignKey(nameof(BrandId))]
    public Brand? Brand { get; set; }

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
}
