using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using Avalonia.Utilities;
using L01.Models;

namespace L01.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly CustomerRepo? _customerRepo;
    private readonly PaymentRepo? _paymentRepo;

    public MainWindowViewModel()
    {
        Customers.Add(new Customer("John", "Labu") {Id = 1});
    }
    public MainWindowViewModel(CustomerRepo cRepo, PaymentRepo pRepo)
    {
        _customerRepo = cRepo;
        _paymentRepo = pRepo;
        RefreshCustomers();
    }
    
    private Customer? _selectedCustomer;
    private Payment? _selectedPayment;
    public ObservableCollection<Customer> Customers { get; } = new();
    public ObservableCollection<Payment> Payments { get; } = new();

    public Customer? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            SetProperty(ref _selectedCustomer, value);
            LoadPayments();
        }
    }

    public Payment? SelectedPayment
    {
        get => _selectedPayment;
        set => _selectedPayment = value;
    }

    private string? _errorMessage;

    public string? ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }
    private void ClearError() => ErrorMessage = null;
    public string NewAmount { get; set; } = ""; 
    public string NewType { get; set; } = "";
    public string NewBank { get; set; } = "";

    private void LoadPayments()
    {
        if (SelectedCustomer == null) return;

        try
        {
            var data = _paymentRepo!.FindAll(SelectedCustomer.Id);
            Payments.Clear();
            foreach (var p in data)
            {
                Payments.Add(p);
            }
        }
        catch (Exception e)
        {
            ErrorMessage = $"Payments load error: {e.Message}";
        }
    }

    public void RefreshCustomers()
    {
        try
        {
            Customers.Clear();
            var data = _customerRepo!.FindAll();
            foreach (var c in data)
            {
                Customers.Add(c);
            }
        }
        catch (Exception e)
        {
            ErrorMessage = $"Refresh error: {e.Message}";
        }
    }

    public void AddPayment()
    {
        try
        {
            decimal parsed = 0;
            decimal.TryParse(NewAmount, out parsed);
            if (parsed == 0)
                ErrorMessage = $"Invalid amount!";
            else if (string.IsNullOrEmpty(NewType))
                ErrorMessage = $"Invalid type (empty)!";
            else if (SelectedCustomer == null)
                ErrorMessage = $"Customer not selected!";
            else
            {
                _paymentRepo!.Save(new Payment(parsed, NewType, NewBank, SelectedCustomer!.Id));
                LoadPayments();
                ErrorMessage = $"Operation successful";
            }
        }
        catch (Exception e)
        {
            ErrorMessage = $"Add payment error: {e.Message}";
        }
    }

    public void UpdatePayment()
    {
        try
        {
            if (SelectedPayment == null)
            {
                ErrorMessage = $"Select a payment first";
                return;
            }

            _paymentRepo?.Update(SelectedPayment);
            LoadPayments();
            ErrorMessage = "Operation successful";
        }
        catch (Exception e)
        {
            ErrorMessage = $"Update error: {e.Message}";
        }
    }

    public void DeletePayment()
    {
        try
        {
            if (SelectedPayment == null)
                ErrorMessage = $"Select a payment first";
            else
            {
                _paymentRepo!.Delete(SelectedPayment.Id);
                LoadPayments();
                ErrorMessage = "Operation successful";
            }
        }
        catch (Exception e)
        {
            ErrorMessage = $"Delete error: {e.Message}";
        }
    }
}