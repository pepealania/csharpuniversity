// UPGRADE 1: Top-Level Statements. 
// In C# 9.0, you can delete 'namespace', 'class Program', and 'static void Main()'.
// The compiler automatically wraps these loose lines of code into the program's main entry point.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;

#nullable enable

// Initialize our data collection using C# 9 optimizations
var catalog = new List<Product>
{
    // UPGRADE 2: Target-Typed 'new()'.
    // Since the compiler already knows this is a List of 'Product', you can omit the word 'Product' 
    // and use an empty 'new()' constructor expression.
    new() { Name = "Coffee Mug", Price = 12.50, Stock = 100 },
    new() { Name = "Leather Jacket", Price = 250.00, Stock = 5 },
    new() { Name = "Batteries", Price = 8.00, Stock = 0 }
};

WriteLine("--- STREAMING CHEAP ITEMS ---");

await foreach (var item in InventoryManager.StreamMatchingProductsAsync(catalog, p => p.Price < 20.00))
{
    // Records automatically generate clean string overrides, printing properties cleanly instead of the type name.
    WriteLine($"Stream Received -> Found: {item}"); 
}

WriteLine("\n--- EXTENDED PATTERN MATCHING DEMO ---");
Product targetItem = catalog[^1];

// Evaluating our record using C# 9's relational logical patterns
string statusMessage = InventoryManager.GetStockStatusDescription(targetItem);
WriteLine($"Product: {targetItem.Name} | Status: {statusMessage}");


// ============================================================================
// --- Domain Models & Services (Declared at the bottom of top-level files) ---
// ============================================================================

// UPGRADE 3: Record Types & Init-Only Properties.
// Changing 'class' to 'record' gives us automatic value-based equality.
// Using 'init' instead of 'set' means these properties can be written to during construction, 
// but are 100% frozen (immutable) for the rest of the application runtime.
public record Product
{
    public string Name { get; init; } = default!;
    public double Price { get; init; }
    public int Stock { get; init; }
}

public static class InventoryManager
{
    public static async IAsyncEnumerable<Product> StreamMatchingProductsAsync(
        List<Product>? catalog, 
        Func<Product, bool> rule)
    {
        if (catalog is null) yield break;

        foreach (var p in catalog)
        {
            await Task.Delay(50);
            if (rule(p)) yield return p;
        }
    }

    // UPGRADE 4: Relational and Logical Pattern Matching.
    // Instead of using 'when' guards or variable comparisons, C# 9 allows you to write 
    // inequality operators (<, >, <=, >=) and logical keywords (and, or, not) straight inside switch tables.
    public static string GetStockStatusDescription(Product p) => p.Stock switch
    {
        0 => "Completely out of stock. Reorder immediately.",
        > 0 and < 10 => "Critical alert: Stock levels are running dangerously low!",
        _ => "Stock levels are healthy."
    };
}
