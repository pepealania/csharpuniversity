public interface IProductFilterRule
{
    // The polymorphic method contract
    bool IsMatch(Product p);
}

// Rule 1 Class: Cheap Items
public class CheapFilter : IProductFilterRule
{
    public bool IsMatch(Product p)
    {
        return p.Price < 20.00;
    }
}

// Rule 2 Class: Out of Stock Items
public class OutOfStockFilter : IProductFilterRule
{
    public bool IsMatch(Product p)
    {
        return p.Stock == 0;
    }
}

public class InventoryManager
{
    // Accepts the interface object as a method argument
    public static void PrintMatchingProducts(Product[] catalog, IProductFilterRule rule)
    {
        for (int i = 0; i < catalog.Length; i++)
        {
            // Execute the polymorphic rule check
            if (rule.IsMatch(catalog[i]) == true)
            {
                System.Console.WriteLine("Match found: " + catalog[i].Name);
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Set up the mock database array
        Product[] catalog = new Product[3];
        catalog[0] = new Product("Coffee Mug", 12.50, 100);
        catalog[1] = new Product("Leather Jacket", 250.00, 5);
        catalog[2] = new Product("Batteries", 8.00, 0);

        // Scenario A: Marketing wants a list of cheap items
        System.Console.WriteLine("--- CHEAP ITEMS ---");
        // Instantiate the specific class implementing the interface
        IProductFilterRule cheapFilterObj = new CheapFilter();
        InventoryManager.PrintMatchingProducts(catalog, cheapFilterObj);

        // Scenario B: Operations wants a list of items to reorder
        System.Console.WriteLine("\n--- OUT OF STOCK ITEMS ---");
        // Instantiate the other class implementing the interface
        IProductFilterRule stockFilterObj = new OutOfStockFilter();
        InventoryManager.PrintMatchingProducts(catalog, stockFilterObj);
    }
}
