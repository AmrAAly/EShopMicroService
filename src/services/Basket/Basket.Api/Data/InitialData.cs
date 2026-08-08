public class InitialData
{
    public static IEnumerable<ShoppingCart> Baskets =>
        new List<ShoppingCart>
        {
            new ShoppingCart("swn")
            {
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem
                    {
                        ProductId  = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                        ProductName = "IPhone X",
                        Quantity   = 2,
                        Color      = "White",
                        Price      = 500
                    },
                    new ShoppingCartItem
                    {
                        ProductId  = new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                        ProductName = "Samsung 10",
                        Quantity   = 1,
                        Color      = "Gray",
                        Price      = 300
                    }
                }
            }
        };
}
