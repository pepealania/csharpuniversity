using System;
using System.Collections.Generic;
using System.Threading.Tasks; // C# 5.0 UPGRADE: Required for Task-based Asynchronous Pattern (TAP)

namespace CSharpFiveDemo
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }

    public class InventoryManager
    {
        // C# 5.0 UPGRADE: Method marked with 'async' and returns a 'Task'.
        // This allows background asynchronous processing using 'await'.
        public static async Task PrintMatchingProductsAsync(
            List<Product> catalog, 
            Func<Product, bool> rule, 
            string header = "MATCHING PRODUCTS", 
            bool showCount = false)
        {
            Console.WriteLine("--- " + header + " ---");
            int matchCount = 0;

            foreach (var p in catalog)
            {
                // Simulating a real-world asynchronous operation (e.g., database fetch or network lag)
                // without freezing the main application execution thread.
                await Task.Delay(500); 

                if (rule(p))
                {
                    Console.WriteLine("Found: " + p.Name);
                    matchCount++;
                }
            }

            if (showCount)
            {
                Console.WriteLine("Total Items Listed: " + matchCount);
            }
        }
    }

    public class Program
    {
        // C# 5.0 Note: Main cannot be marked async yet in this version (that came in C# 7.1).
        // We use .Wait() or .GetAwaiter().GetResult() to block the console from closing prematurely.
        public static void Main(string[] args)
        {
            var catalog = new List<Product>
            {
                new Product { Name = "Coffee Mug", Price = 12.50, Stock = 100 },
                new Product { Name = "Leather Jacket", Price = 250.00, Stock = 5 },
                new Product { Name = "Batteries", Price = 8.00, Stock = 0 }
            };

            // ==========================================
            // DEMO 1: Asynchronous Execution (Async / Await)
            // ==========================================
            Console.WriteLine("Launching async tasks...");
            
            // Kick off the asynchronous process
            Task taskA = InventoryManager.PrintMatchingProductsAsync(catalog, p => p.Price < 20.00);
            
            // Block and wait for completion since we are inside a synchronous Main method
            taskA.Wait();


            // ==========================================
            // DEMO 2: The C# 5.0 Foreach Variable Capture Fix
            // ==========================================
            Console.WriteLine("\n--- Foreach Closure Capture Bug Fix ---");
            
            var delegateList = new List<Action>();

            // Crucial C# 5.0 Upgrade: The loop variable 'item' is now structurally scoped INSIDE the loop iteration block.
            foreach (var item in catalog)
            {
                // In C# 3.0 and 4.0, this lambda captured a reference to the global loop variable.
                // Running this demo in C# 4.0 would print "Batteries" 3 times!
                // In C# 5.0, it prints each product name sequentially as expected.
                delegateList.Add(() => Console.WriteLine("Captured Item: " + item.Name));
            }

            // Execute the stored actions
            foreach (var action in delegateList)
            {
                action();
            }
        }
    }
}
