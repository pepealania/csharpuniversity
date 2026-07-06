using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console; 

namespace CSharpSevenDemo
{
    public class Product
    {
        public string Name { get; set; } = "Unknown Item";
        public double Price { get; set; } = 0.00;
        public int Stock { get; set; } = 0;
        public string DisplaySummary => $"{Name} (${Price})"; 
    }

    public class InventoryManager
    {
        // UPGRADE 1: ValueTuple Return Type. 
        // This method can now naturally return multiple variables (a string summary AND an integer count) 
        // cleanly without needing a custom wrapper class or out parameters.
        public static async Task<(string ExecutionSummary, int TotalProcessed)> PrintMatchingProductsAsync(
            List<Product> catalog, 
            Func<Product, bool> rule, 
            string header = "MATCHING PRODUCTS")
        {
            WriteLine($"--- {header} ---");
            int matchCount = 0;

            // UPGRADE 2: Local Function.
            // A helper method declared directly inside another method. 
            // It is completely invisible to the outside class, keeping the class scope perfectly clean.
            bool IsValidCatalog() => catalog?.Count > 0;

            if (IsValidCatalog())
            {
                foreach (var p in catalog)
                {
                    await Task.Delay(50); 

                    if (rule(p))
                    {
                        WriteLine($"Found: {p.DisplaySummary}");
                        matchCount++;
                    }
                }
            }

            // Returning an on-the-fly named tuple structure
            return ($"Completed evaluation for {header}", matchCount);
        }
    }

    public class Program
    {
        // UPGRADE 3: Async Main (C# 7.1+).
        // No more legacy 'task.Wait()' or '.GetAwaiter().GetResult()'. 
        // The runtime now natively accepts an asynchronous entry point task.
        public static async Task Main(string[] args)
        {
            var catalog = new List<Product>
            {
                new Product { Name = "Coffee Mug", Price = 12.50, Stock = 100 },
                new Product { Name = "Leather Jacket", Price = 250.00, Stock = 5 },
                new Product { Name = "Batteries", Price = 8.00, Stock = 0 }
            };

            WriteLine("Launching async tasks...");
            
            // UPGRADE 4: Tuple Deconstruction.
            // We instantly break apart the multiple variables returned by the method into direct, local variables.
            (string summary, int count) = await InventoryManager.PrintMatchingProductsAsync(catalog, p => p.Price < 20.00);
            WriteLine($"Result -> {summary}. Count: {count}");

            // ==========================================
            // DEMO: Pattern Matching & Out Variables
            // ==========================================
            WriteLine("\n--- Pattern Matching & Out Variables Demo ---");

            object mysteryData = catalog[1]; // Grabbing the Leather Jacket as a loose object type

            // UPGRADE 5: Pattern Matching with the 'is' expression.
            // It simultaneously checks the type AND extracts it into a typed variable ('matchedProduct') in one step.
            // UPGRADE 6: Case Guards ('when'). We can append an extra logical filter straight into the type check.
            if (mysteryData is Product matchedProduct && matchedProduct.Price > 100.00)
            {
                WriteLine($"Pattern Match Success: {matchedProduct.Name} is a high-expense item.");
            }

            // UPGRADE 7: Inline Out Variables.
            // In C# 1.0 - 6.0, you had to declare a variable on a separate line before passing it as 'out'. 
            // Now, you can declare it inline directly inside the argument block.
            string inputPriceString = "250.00";
            if (double.TryParse(inputPriceString, out double parsedPrice))
            {
                // 'parsedPrice' is immediately scoped and usable right here
                WriteLine($"Inline Out Variable Success! Parsed value: ${parsedPrice}");
            }
        }
    }
}
