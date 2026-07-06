using System;
using System.Collections.Generic;

namespace CSharpFourDemo
{
    // --- Domain Models ---
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }

    // Derived class to demonstrate C# 4.0 Generic Co-variance
    public class PremiumProduct : Product
    {
        public string LuxuryCertificateId { get; set; }
    }

    // --- Core Processing System ---
    public class InventoryManager
    {
        // C# 4.0 UPGRADE: Optional Parameters ('header' and 'showCount' have default values)
        public static void PrintMatchingProducts(
            List<Product> catalog, 
            Func<Product, bool> rule, 
            string header = "MATCHING PRODUCTS", 
            bool showCount = false)
        {
            Console.WriteLine("--- " + header + " ---");
            int matchCount = 0;

            foreach (var p in catalog)
            {
                if (rule(p))
                {
                    Console.WriteLine("Found: " + p.Name);
                    matchCount++;
                }
            }

            // Utilizing the optional logic toggle
            if (showCount)
            {
                Console.WriteLine("Total Items Listed: " + matchCount);
            }
        }
    }

    // --- Main Entry Point ---
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initializing data using C# 3.0 collection initializers to prepare for C# 4.0 execution
            var catalog = new List<Product>
            {
                new Product { Name = "Coffee Mug", Price = 12.50, Stock = 100 },
                new Product { Name = "Leather Jacket", Price = 250.00, Stock = 5 },
                new Product { Name = "Batteries", Price = 8.00, Stock = 0 }
            };

            // ==========================================
            // DEMO 1: Optional Parameters
            // ==========================================
            // We omit the last two parameters; they fall back to "MATCHING PRODUCTS" and 'false'
            InventoryManager.PrintMatchingProducts(catalog, p => p.Price < 20.00);


            // ==========================================
            // DEMO 2: Named Arguments
            // ==========================================
            Console.WriteLine();
            // We explicitly name 'showCount' to toggle it, while cleanly skipping 'header' entirely
            InventoryManager.PrintMatchingProducts(
                catalog, 
                p => p.Stock == 0, 
                showCount: true
            );

            Console.WriteLine();
            // We can pass parameters completely out of order by declaring their target names explicitly
            InventoryManager.PrintMatchingProducts(
                header: "HIGH EXPENSE INVESTIGATION",
                rule: p => p.Price > 100.00,
                catalog: catalog
            );


            // ==========================================
            // DEMO 3: The 'dynamic' Keyword
            // ==========================================
            Console.WriteLine("\n--- Dynamic Typing Demo ---");
            
            // The compiler completely turns off type validation for this variable until runtime
            dynamic mysteryProduct = new Product { Name = "Imported Caviar", Price = 150.00, Stock = 12 };

            // This compiles perfectly in C# 4.0, resolving the property checks on execution.
            Console.WriteLine("Dynamic access resolved: " + mysteryProduct.Name + " ($" + mysteryProduct.Price + ")");


            // ==========================================
            // DEMO 4: Generic Delegate Co-variance
            // ==========================================
            Console.WriteLine("\n--- Generic Co-variance Demo ---");

            // A factory delegate generating a specific child type (PremiumProduct)
            Func<PremiumProduct> premiumFactory = () => new PremiumProduct 
            { 
                Name = "Gold Rolex Watch", 
                Price = 8500.00, 
                LuxuryCertificateId = "CERT-992" 
            };

            // UPGRADE: Valid in C# 4.0 because Func<out TResult> is now marked as COVARIANT.
            // A delegate returning a child class can be safely assigned to a delegate expecting the base class.
            Func<Product> generalProductFactory = premiumFactory;

            Product genericOutput = generalProductFactory();
            Console.WriteLine("Co-variant assignment created: " + genericOutput.Name);
        }
    }
}
