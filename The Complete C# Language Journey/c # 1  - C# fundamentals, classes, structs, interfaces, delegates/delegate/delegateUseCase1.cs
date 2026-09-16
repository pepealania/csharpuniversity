public class Product
{
    public string Name;
    public double Price;
    public int Stock;

    public Product(string name, double price, int stock)
    {
        this.Name = name;
        this.Price = price;
        this.Stock = stock;
    }
}

// The delegate argument definition: 
// "Give me any product, and I will tell you true or false if it matches my rule."
public delegate bool ProductFilterRule(Product p);

public class InventoryManager
{
    // Note how the delegate is passed directly as a method argument
    public static void PrintMatchingProducts(Product[] inventory, ProductFilterRule rule)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            // Execute the injected delegate argument
            if (rule(inventory[i]) == true)
            {
                System.Console.WriteLine("Match found: " + inventory[i].Name);
            }
        }
    }
}

public class Program
{
    // Rule 1: Concrete target method for cheap items
    public static bool IsCheap(Product p)
    {
        return p.Price < 20.00;
    }

    // Rule 2: Concrete target method for out-of-stock items
    public static bool IsOutOfStock(Product p)
    {
        return p.Stock == 0;
    }

    public static void Main(string[] args)
    {
        // Set up the mock database array
        Product[] catalog = new Product[3];
        catalog[0] = new Product("Coffee Mug", 12.50, 100);
        catalog[1] = new Product("Leather Jacket", 250.00, 5);
        catalog[2] = new Product("Batteries", 8.00, 0);

        // Scenario A: Marketing wants a list of cheap items
        System.Console.WriteLine("--- CHEAP ITEMS ---");
        ProductFilterRule cheapRule = new ProductFilterRule(Program.IsCheap);
        InventoryManager.PrintMatchingProducts(catalog, cheapRule);

        // Scenario B: Operations wants a list of items to reorder
        System.Console.WriteLine("\n--- OUT OF STOCK ITEMS ---");
        ProductFilterRule stockRule = new ProductFilterRule(Program.IsOutOfStock);
        InventoryManager.PrintMatchingProducts(catalog, stockRule);
    }
}

