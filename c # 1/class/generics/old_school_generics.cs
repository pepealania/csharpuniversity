
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
