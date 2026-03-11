using System.Runtime.InteropServices.ComTypes;

namespace L01.Models;

public class Payment(decimal amount, string type, string bankName, long customer) : Entity<long>
{
    public decimal Amount { get; set; } = amount;
    public string Type { get; set; } = type;
    public string? BankName { get; set; } = bankName;
    public long Customer { get; } = customer;
}