int? databaseAge = null; // Perfectly legal now!

if (databaseAge.HasValue)
{
    Console.WriteLine("User age is: " + databaseAge.Value);
}
else
{
    Console.WriteLine("Age data is missing.");
}
