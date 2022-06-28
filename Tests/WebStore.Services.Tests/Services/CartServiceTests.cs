using Moq;

using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

using Assert = Xunit.Assert;

namespace WebStore.Services.Tests.Services;

[TestClass]
public class CartServiceTests
{
    private Cart _Cart;
    private Mock<IProductData> _ProductDataMock;

    [TestInitialize]
    public void Initialize()
    {
        _Cart = new Cart
        {
            Items = new List<CartItem>
            {
                new() { ProductId = 1, Quantity = 1 },
                new() { ProductId = 2, Quantity = 3 },
            }
        };

        _ProductDataMock = new Mock<IProductData>();
        _ProductDataMock
           .Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
           .Returns(new[]
            {
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Price = 1.1m,
                    Order = 1,
                    ImageUrl = "img_1.png",
                    Brand = new Brand { Id = 1, Name = "Brand 1", Order = 1},
                    SectionId = 1,
                    Section = new Section{ Id = 1, Name = "Section 1", Order = 1 },
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 2",
                    Price = 2.2m,
                    Order = 2,
                    ImageUrl = "img_2.png",
                    Brand = new Brand { Id = 2, Name = "Brand 2", Order = 2},
                    SectionId = 2,
                    Section = new Section{ Id = 2, Name = "Section 2", Order = 2 },
                },
                new Product
                {
                    Id = 3,
                    Name = "Product 3",
                    Price = 3.3m,
                    Order = 3,
                    ImageUrl = "img_3.png",
                    Brand = new Brand { Id = 3, Name = "Brand 3", Order = 3},
                    SectionId = 3,
                    Section = new Section{ Id = 3, Name = "Section 3", Order = 3 },
                },
            });

    }


    [TestMethod, Description("Тест модели корзины")]
    public void Cart_Class_ItemsCount_returns_Correct_Quantity()
    {

    }
}
