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
        // UPGRADE: Implicit method group conversion (No 'new' keyword required!)
        ProductFilterRule cheapRule = Program.IsCheap;
        InventoryManager.PrintMatchingProducts(catalog, cheapRule);

        // Scenario B: Operations wants a list of items to reorder
        System.Console.WriteLine("\n--- OUT OF STOCK ITEMS ---");
        // UPGRADE: You can even pass the method name straight into the argument directly
        InventoryManager.PrintMatchingProducts(catalog, Program.IsOutOfStock);
    }
}
