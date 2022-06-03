namespace WebStore.ViewModels;

public class CartViewModel
{
    public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; } = null!;

    public int ItemsCount => Items.Sum(item => item.Quantity);

    public decimal TotalPrice => Items.Sum(item => item.Quantity * item.Product.Price);
}
