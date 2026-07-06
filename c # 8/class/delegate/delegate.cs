using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;

namespace CSharpEightDemo
{
    // UPGRADE 1: Nullable Reference Types are enabled. 
    // The compiler will now warn you if a string or object could potentially be null.
    #nullable enable

    public class Product
    {
        // Using the '!' (null-forgiving operator) to tell the compiler Name will be initialized
        public string Name { get; set; } = default!;
        public double Price { get; set; }
        public int Stock { get; set; }
        public string DisplaySummary => $"{Name} (${Price})";
    }

    public class InventoryManager
    {
        // UPGRADE 2: Async Streams using 'IAsyncEnumerable<T>'.
        // Instead of waiting for a whole List to process and returning all at once, 
        // this method can stream matched items out to the caller one-by-one as they are found.
        public static async IAsyncEnumerable<Product> StreamMatchingProductsAsync(
            List<Product>? catalog, // '?' marks that the list itself is allowed to be null
            Func<Product, bool> rule)
        {
            if (catalog is null) yield break; // C# 8 cleaner null checking syntax

            foreach (var p in catalog)
            {
                await Task.Delay(50); // Simulating database or API delay

                if (rule(p))
                {
                    // Yielding back control immediately to the caller for every single match
                    yield return p; 
                }
            }
        }

        // UPGRADE 3: Switch Expressions.
        // A pure functional rewrite of the classic statement. It returns a value directly, 
        // uses lambda arrows, and enforces compile-time exhaustiveness.
        public static string GetStockStatusDescription(Product p) => p.Stock switch
        {
            0 => "Completely out of stock. Reorder immediately.",
            long qty when qty < 10 => $"Critical alert: Only {qty} items remaining!",
            _ => "Stock levels are healthy." // '_' acts as the functional fallback (default) discard pattern
        };
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var catalog = new List<Product>
            {
                new Product { Name = "Coffee Mug", Price = 12.50, Stock = 100 },
                new Product { Name = "Leather Jacket", Price = 250.00, Stock = 5 },
                new Product { Name = "Batteries", Price = 8.00, Stock = 0 }
            };

            WriteLine("--- STREAMING CHEAP ITEMS ---");

            // UPGRADE 4: 'await foreach' loop.
            // This pulls items from the IAsyncEnumerable stream in real-time as they are yielded. 
            // The loop doesn't block the thread while waiting for the next index to become available.
            await foreach (var item in InventoryManager.StreamMatchingProductsAsync(catalog, p => p.Price < 20.00))
            {
                WriteLine($"Stream Received -> Found: {item.DisplaySummary}");
            }


            WriteLine("\n--- SWITCH EXPRESSIONS & INDICES DEMO ---");

            // UPGRADE 5: Indices and Ranges.
            // '^1' means "1st item from the end" (the last item). 
            // This replaces the old 'catalog[catalog.Count - 1]' syntax.
            Product lastItem = catalog[^1]; 
            
            // Evaluating the product through our C# 8 functional switch expression
            string statusMessage = InventoryManager.GetStockStatusDescription(lastItem);
            WriteLine($"Product: {lastItem.Name} | Status: {statusMessage}");


            // ==========================================
            // DEMO 6: Nullable Reference Type Safeguards
            // ==========================================
            // Product? regularProduct = null;
            // WriteLine(regularProduct.Name); 
            // ^ UNCOMMENTING ABOVE WILL GENERATE A COMPILER WARNING IN C# 8!
            // The compiler intercepts potential NullReferenceExceptions at build time.
        }
    }
}
