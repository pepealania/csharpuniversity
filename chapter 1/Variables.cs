using System;

class Program
{
    static void Main()
    {
        int age = 25;
        double salary = 50000.50;
        string name = "John";

        Console.WriteLine(name);
        Console.WriteLine(age);
        Console.WriteLine(salary);
    }
}

Memory diagram:

 ┌────────────────────────────────────────────────────────────────────────┐
 │                      APPLICATION RUNTIME MEMORY                        │
 └────────────────────────────────────────────────────────────────────────┘
                                     │
      ┌──────────────────────────────┴──────────────────────────────┐
      ▼                                                             ▼
┌────────────────────────────────┐             ┌────────────────────────────────┐
│           THE STACK            │             │           THE HEAP             │
├────────────────────────────────┤             ├────────────────────────────────┤
│  [Main Method Stack Frame]     │             │  [Normal Managed Heap]         │
│                                │             │                                │
│  age    = 25                   │             │  ┌──────────────────────────┐  │
│  (Value Type - 4 bytes)        │             │  │ "John"                   │  │
│                                │             │  │                          │  │
│  salary = 50000.50             │             │  │ (The actual string text  │  │
│  (Value Type - 8 bytes)        │             │  │  lives safely here)      │  │
│                                │             │  └──────────────────────────┘  │
│  name   = [Memory Address] ────┼────────────►│                ▲               │
│  (Reference Pointer)           │             │                │               │
│                                │             │                │               │
└────────────────────────────────┘             └────────────────┼───────────────┘
                                                                │
                                                                │
                                             (Cleaned up later by the GC)
