
using System;
using System.Collections.Generic;
using System.Linq; // UPGRADE: Gives us functional querying capabilities

public class Program
{
    public static void Main(string[] args)
    {
        // UPGRADE 1: Implicit typing ('var') avoids repeating type names.
        // UPGRADE 2: Collection & Object Initializers remove 'new Product[3]' arrays 
        //            and manual index assignment constructors.
        var catalog = new List<Product>
        {
            new Product { Name = "Coffee Mug", Price = 12.50, Stock = 100 },
            new Product { Name = "Leather Jacket", Price = 250.00, Stock = 5 },
            new Product { Name = "Batteries", Price = 8.00, Stock = 0 }
        };

        // Scenario A: Marketing wants a list of cheap items
        Console.WriteLine("--- CHEAP ITEMS ---");
        
        // UPGRADE 3: Lambda Expressions ('=>') replace the heavy C# 2.0 'delegate(Product p) { return ... }' syntax.
        // UPGRADE 4: Built-in Generic Delegates ('Func<T, TResult>') remove the need for 'ProductFilterRule'.
        Func<Product, bool> cheapRule = p => p.Price < 20.00;
        InventoryManager.PrintMatchingProducts(catalog, cheapRule);


        // Scenario B: Operations wants a list of items to reorder
        Console.WriteLine("\n--- OUT OF STOCK ITEMS ---");
        
        // UPGRADE 5: Passing the lambda inline directly as a compact argument.
        InventoryManager.PrintMatchingProducts(catalog, p => p.Stock == 0);
    }
}
