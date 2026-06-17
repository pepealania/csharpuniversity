```
using System;

class Program
{
    static void Main()
    {
        string name = "John";
        name = "Peter";
        Console.WriteLine(name);
    }
}
```
<img width="449" height="460" alt="image" src="https://github.com/user-attachments/assets/12d3d9f2-2ebd-4be4-b8f0-a68ab4aad42b" />

```
.method private hidebysig static void Main() cil managed
{
    .entrypoint
    // Code size       19 (0x13)
    .maxstack  1
    .locals init (string V_0) // This is your local variable 'name' at index [0]

    // 1. string name = "John";
    IL_0000:  ldstr      "John"       // Load the string literal "John" onto the evaluation stack
    IL_0005:  stloc.0                 // Pop the value off the stack and store it in local variable 0

    // 2. name = "Peter";
    IL_0006:  ldstr      "Peter"      // Load the string literal "Peter" onto the evaluation stack
    IL_000b:  stloc.0                 // Pop "Peter" and overwrite local variable 0 (reassignment)

    // 3. Console.WriteLine(name);
    IL_000c:  ldloc.0                 // Load the current value of local variable 0 ("Peter") onto the stack
    IL_000d:  call       void [System.Console]System.Console::WriteLine(string) 
                                      // Call WriteLine, which consumes the string from the stack
    // 4. End of method
    IL_0012:  ret                     // Return from the static Main method
}

```
