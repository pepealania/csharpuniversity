
```
using System;

public interface IShape 
{
    void Draw();
}

public class Circle : IShape 
{
    public void Draw() { Console.WriteLine("Circle drawn"); }
}

public static class GraphicUtils 
{
    public static void ClearScreen() { Console.WriteLine("Screen cleared"); }
}

class Program 
{
    static void Main() 
    {
        IShape shape = new Circle();
        
        // 1. Interface method call
        shape.Draw(); 

        // 2. Static method call
        GraphicUtils.ClearScreen(); 
    }
}

```

<img width="513" height="693" alt="image" src="https://github.com/user-attachments/assets/46802d9a-c55c-4795-9b94-10e15ba875e1" />
