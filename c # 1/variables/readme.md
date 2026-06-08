
```csharp
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
```

# Application Runtime Memory

<img width="641" height="541" alt="image" src="https://github.com/user-attachments/assets/47608c53-4aeb-4d41-bf6b-9fbee29d224e" />

IL CODE
```
.method private hidebysig static void Main() cil managed
{
    .entrypoint
    // Code size       49 (0x31)
    .maxstack  2
    
    // 1. Declare the 3 slots on the Stack Frame (Locals)
    .locals init (
        [0] int32 age,
        [1] float64 salary,
        [2] string name
    )

    // 2. Initialize the variables on the Stack
    ldc.i4.s   25              // Load the integer 25 onto the evaluation stack
    stloc.0                    // Pop it and store it in slot [0] (age)
    
    ldc.r8     50000.5         // Load the double 50000.50 onto the evaluation stack
    stloc.1                    // Pop it and store it in slot [1] (salary)
    
    ldstr      "John"          // Load the string literal "John" from metadata
    stloc.2                    // Pop it and store it in slot [2] (name)

    // 3. Print 'name' to the Console
    ldloc.2                    // Load value from slot [2] (name)
    call       void [mscorlib]System.Console::WriteLine(string)

    // 4. Print 'age' to the Console
    ldloc.0                    // Load value from slot [0] (age)
    call       void [mscorlib]System.Console::WriteLine(int32)

    // 5. Print 'salary' to the Console
    ldloc.1                    // Load value from slot [1] (salary)
    call       void [mscorlib]System.Console::WriteLine(float64)

    ret                        // Return and close the Main method
}
```






