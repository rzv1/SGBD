namespace L01.Models;

public class Customer(string name, string phone) : Entity<long>
{
    public string Name { get; } = name;
    public string Phone { get; } = phone;
}