namespace WebStore.Domain.Entities;

public class Cart
{
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

    public int ItemsCount => Items.Sum(item => item.Quantity);

    public void Add(int ProductId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == ProductId);
        if (item is null)
        {
            item = new() { ProductId = ProductId };
            Items.Add(item);
        }
        else
            item.Quantity++;
    }

    public void Decrement(int ProductId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == ProductId);
        if(item is null) return;

        item.Quantity--;
        if (item.Quantity <= 0)
            Items.Remove(item);
    }

    public void Remove(int ProductId)
    {
        var item = Items.FirstOrDefault(i => i.ProductId == ProductId);
        if (item is null) return;

        Items.Remove(item);
    }

    public void Clear() => Items.Clear();
}

public class CartItem
{
    public int ProductId { get; set; }

    public int Quantity { get; set; } = 1;
}
