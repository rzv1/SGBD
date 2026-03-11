using System;
using System.Collections.Generic;
using Npgsql;

namespace L01.Models;

public class PaymentRepo(DatabaseManager manager)
{
    private readonly DatabaseManager _manager = manager;

    public IEnumerable<Payment> FindAll(long customerId)
    {
        var payments = new List<Payment>();
        using var command = new NpgsqlCommand("SELECT * FROM payment where id_customer = @id", _manager.GetConnection());
        command.Parameters.AddWithValue("id", customerId);

        using var reader = command.ExecuteReader();
        while (reader.Read())
            payments.Add(GetPayment(reader));
        return payments;
    }

    private Payment GetPayment(NpgsqlDataReader reader)
    {
        return new Payment(reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetInt64(4))
        {
            Id = reader.GetInt64(0)
        };
    }

    public void Save(Payment payment)
    {
        using var command =
            new NpgsqlCommand(
                "INSERT INTO payment (amount, type, bank_name, id_customer) VALUES(@amount, @type, @bank_name, @id_customer)",
                _manager.GetConnection());
        command.Parameters.AddWithValue("amount", payment.Amount);
        command.Parameters.AddWithValue("type", payment.Type);
        command.Parameters.AddWithValue("bank_name", string.IsNullOrEmpty(payment.BankName) ? DBNull.Value : payment.BankName);
        command.Parameters.AddWithValue("id_customer", payment.Customer);
        command.ExecuteNonQuery();
    }

    public void Update(Payment payment)
    {
        using var command =
            new NpgsqlCommand(
                "UPDATE payment SET amount = @amount, type = @type, bank_name = @bank_name where id = @id",
                _manager.GetConnection());
        command.Parameters.AddWithValue("amount", payment.Amount);
        command.Parameters.AddWithValue("type", payment.Type);
        
        //Desi bank name poate fi null la crearea obiectului, la update are obligatoriu o valoare si specificam asta
        //cu operatorul !
        command.Parameters.AddWithValue("bank_name", payment.BankName!);
        command.Parameters.AddWithValue("id", payment.Id);
        command.ExecuteNonQuery();
    }

    public void Delete(long paymentId)
    {
        using var command = new NpgsqlCommand("DELETE FROM payment where id = @id", _manager.GetConnection());
        command.Parameters.AddWithValue("id", paymentId);
        command.ExecuteNonQuery();
    }
}