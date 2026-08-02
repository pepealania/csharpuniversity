public delegate int MathOp(int x);

// The compiler generates a full class under the hood
public sealed class MathOp : System.MulticastDelegate
{
    // 1. The constructor that binds the method target
    public MathOp(object target, IntPtr method);

    // 2. The synchronous execution method
    public virtual int Invoke(int x);

    // 3. The asynchronous startup method
    public virtual IAsyncResult BeginInvoke(int x, AsyncCallback callback, object state);

    // 4. The asynchronous completion method
    public virtual int EndInvoke(IAsyncResult result);
}
