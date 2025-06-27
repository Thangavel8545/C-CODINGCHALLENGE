
using LoanManagementSystem.dao;
using LoanManagementSystem.entity.model;
using System;

namespace LoanManagementSystem.main
{
    class MainModule
    {
        static void Main(string[] args)
        {
            ILoanRepository repo = new LoanRepositoryImpl();
            while (true)
            {
                Console.WriteLine("\n--- Loan Management System ---");
                Console.WriteLine("1. Apply Loan");
                Console.WriteLine("2. View All Loans");
                Console.WriteLine("3. View Loan by ID");
                Console.WriteLine("4. Calculate EMI");
                Console.WriteLine("5. Loan Repayment");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Customer customer = new Customer(1, "John", "john@mail.com", "1234567890", "NY", 700);
                        Loan loan = new Loan(101, customer, 500000, 8, 24, "HomeLoan", "Pending");
                        repo.ApplyLoan(loan);
                        break;
                    case 2:
                        var allLoans = repo.GetAllLoan();
                        allLoans.ForEach(l => Console.WriteLine(l));
                        break;
                    case 3:
                        Console.Write("Enter Loan ID: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(repo.GetLoanById(id));
                        break;
                    case 4:
                        Console.Write("Enter Loan ID: ");
                        int emiId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine($"EMI: {repo.CalculateEMI(emiId)}");
                        break;
                    case 5:
                        Console.Write("Enter Loan ID: ");
                        int repayId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter amount to repay: ");
                        double amt = Convert.ToDouble(Console.ReadLine());
                        repo.LoanRepayment(repayId, amt);
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid Choice.");
                        break;
                }
            }
        }
    }
}
