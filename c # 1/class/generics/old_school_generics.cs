
using System;
using System.Collections;

public class Employee
{
    private string name;
    public string Name { get { return name; } set { name = value; } }
}

public class EmployeeCollection : CollectionBase
{
    // Type-safe Entry Point
    public void Add(Employee emp)
    {
        this.InnerList.Add(emp);
    }

    // Type-safe Indexer
    public Employee this[int index]
    {
        get { return (Employee)this.InnerList[index]; }
        set { this.InnerList[index] = value; }
    }

    // INTERCEPT ENGINE: Intercepts before an item hits the array
    protected override void OnInsert(int index, object value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("Cannot insert a null Employee.");
        }

        Employee emp = (Employee)value; // Cast the raw object
        if (emp.Name == null || emp.Name.Length == 0)
        {
            throw new InvalidOperationException("Employee must have a valid name.");
        }
    }

    // INTERCEPT ENGINE: Intercepts when an item is replaced via indexer
    protected override void OnSet(int index, object oldValue, object newValue)
    {
        if (newValue == null)
        {
            throw new ArgumentNullException("Cannot replace an employee with null.");
        }
    }
}


using System;
using System.Collections;

// C# 1.0 Event Infrastructure
public delegate void CollectionChangedHandler(object sender, EventArgs e);

public class Invoice
{
    private int id;
    public int Id { get { return id; } set { id = value; } }
}

public class ObservableInvoiceCollection : CollectionBase
{
    // Public events exposed to the architecture
    public event CollectionChangedHandler ItemAdded;
    public event CollectionChangedHandler ItemRemoved;

    public void Add(Invoice inv)
    {
        this.InnerList.Add(inv);
    }

    public void Remove(Invoice inv)
    {
        this.InnerList.Remove(inv);
    }

    public Invoice this[int index]
    {
        get { return (Invoice)this.InnerList[index]; }
    }

    // TRIGGER HOOK: Fires immediately AFTER data is safely safely in the list
    protected override void OnInsertComplete(int index, object value)
    {
        if (ItemAdded != null)
        {
            ItemAdded(this, EventArgs.Empty);
        }
    }

    // TRIGGER HOOK: Fires immediately AFTER data is deleted from the list
    protected override void OnRemoveComplete(int index, object value)
    {
        if (ItemRemoved != null)
        {
            ItemRemoved(this, EventArgs.Empty);
        }
    }
}


using System;
using System.Collections;

public class ConfigurationSetting
{
    private string key;
    private string val;
    public string Key { get { return key; } set { key = value; } }
    public string Value { get { return val; } set { val = value; } }
}

public class ReadOnlyConfigCollection : CollectionBase
{
    // Internal-only loader used by your system initialization logic
    internal void InternalLoad(ConfigurationSetting setting)
    {
        this.InnerList.Add(setting);
    }

    // Expose only a Getter indexer (No 'set' definition allowed)
    public ConfigurationSetting this[int index]
    {
        get { return (ConfigurationSetting)this.InnerList[index]; }
    }

    // SHIELD: Overriding validation to block any outside runtime manipulations
    protected override void OnClear()
    {
        throw new NotSupportedException("This configuration collection is read-only.");
    }

    protected override void OnRemove(int index, object value)
    {
        throw new NotSupportedException("Cannot remove elements from a read-only collection.");
    }
}

public class Program
{
    public static void Main()
    {
        EmployeeCollection staff = new EmployeeCollection();
        
        Employee emp1 = new Employee();
        emp1.Name = "Alice";
        
        staff.Add(emp1); // Clean, type-safe execution!

        try
        {
            // This crashes during execution because OnInsert catches the empty name!
            Employee emp2 = new Employee();
            staff.Add(emp2); 
        }
        catch(Exception ex)
        {
            Console.WriteLine("Blocked: " + ex.Message);
        }
    }
}
