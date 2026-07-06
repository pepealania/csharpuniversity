using System;
using System.Collections.Generic;
using System.Threading.Tasks;
// UPGRADE 1: Using static directive allows us to call Console methods directly without typing 'Console.'
using static System.Console; 

namespace CSharpSixDemo
{
    public class Product
    {
        // UPGRADE 2: Auto-Property Initializers let us assign default values directly to properties
        // without needing a formal constructor block.
        public string Name { get; set; } = "Unknown Item";
        public double Price { get; set; } = 0.00;
        public int Stock { get; set; } = 0;

        // UPGRADE 3: Expression-Bodied Members use '=>' to replace simple multi-line methods/properties
        public string DisplaySummary => $"{Name} (${Price})"; 
    }

    public class InventoryManager
    {
        public static async Task PrintMatchingProductsAsync(
            List<Product> catalog, 
            Func<Product, bool> rule, 
            string header = "MATCHING PRODUCTS", 
            bool showCount = false)
        {
            // UPGRADE 4: String Interpolation using the '$' prefix replaces ugly 'Console.WriteLine("--- " + header + " ---")' 
            // or 'String.Format()' calls with direct, type-safe inline variable embedding.
            WriteLine($"--- {header} ---");
            int matchCount = 0;

            // UPGRADE 5: Null-Conditional Operator ('?.') safely checks if the catalog list is null 
            // before running the loop. If catalog is null, it skips the block instead of throwing a NullReferenceException.
            if (catalog?.Count > 0)
            {
                foreach (var p in catalog)
                {
                    await Task.Delay(100); 

                    if (rule(p))
                    {
                        // Using string interpolation and direct static WriteLine call
                        WriteLine($"Found: {p.DisplaySummary}");
                        matchCount++;
                    }
                }
            }

            if (showCount)
            {
                WriteLine($"Total Items Listed: {matchCount}");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var catalog = new List<Product>
            {
                new Product { Name = "Coffee Mug", Price = 12.50, Stock = 100 },
                new Product { Name = "Leather Jacket", Price = 250.00, Stock = 5 },
                new Product { Name = "Batteries", Price = 8.00, Stock = 0 }
            };

            WriteLine("Launching async tasks...");
            Task taskA = InventoryManager.PrintMatchingProductsAsync(catalog, p => p.Price < 20.00);
            taskA.Wait();

            // ==========================================
            // DEMO: Null-Conditional Operator & Nameof
            // ==========================================
            WriteLine("\n--- Null-Safety and Metadata Demo ---");
            
            List<Product> emptyCatalog = null;
            
            // C# 6.0 Upgrade: The logic will gracefully execute without breaking, evaluating to a safe null string check
            WriteLine($"Is empty catalog null? {emptyCatalog == null}");
            
            // UPGRADE 6: The 'nameof' operator extracts the string name of a variable, method, or class at compile-time.
            // If you rename the variable 'catalog' later, the refactoring engine updates this string automatically!
            WriteLine($"The variable name used above was: {nameof(catalog)}");
        }
    }
}
