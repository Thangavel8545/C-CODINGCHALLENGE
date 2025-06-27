
using LoanManagementSystem.entity.model;
using LoanManagementSystem.exception;
using LoanManagementSystem.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LoanManagementSystem.dao
{
    public class LoanRepositoryImpl : ILoanRepository
    {
        private string _connFile = "appsettings.properties";

        public void ApplyLoan(Loan loan)
        {
            Console.WriteLine("Confirm apply for loan? (Yes/No): ");
            if (Console.ReadLine().ToLower() != "yes")
                return;

            using (SqlConnection conn = DBConnUtil.GetConnection(_connFile))
            {
                conn.Open();
                string query = "INSERT INTO Loans (LoanId, CustomerId, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) VALUES (@LoanId, @CustomerId, @Principal, @Rate, @Term, @Type, @Status)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LoanId", loan.LoanId);
                cmd.Parameters.AddWithValue("@CustomerId", loan.Customer.CustomerId);
                cmd.Parameters.AddWithValue("@Principal", loan.PrincipalAmount);
                cmd.Parameters.AddWithValue("@Rate", loan.InterestRate);
                cmd.Parameters.AddWithValue("@Term", loan.LoanTerm);
                cmd.Parameters.AddWithValue("@Type", loan.LoanType);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.ExecuteNonQuery();
            }
        }

        public double CalculateInterest(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            return CalculateInterest(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public double CalculateInterest(double principal, double rate, int term)
        {
            return (principal * rate * term) / 12;
        }

        public void LoanStatus(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            string status = loan.Customer.CreditScore > 650 ? "Approved" : "Rejected";

            using (SqlConnection conn = DBConnUtil.GetConnection(_connFile))
            {
                conn.Open();
                string query = "UPDATE Loans SET LoanStatus=@Status WHERE LoanId=@LoanId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@LoanId", loanId);
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine($"Loan status updated to {status}");
        }

        public double CalculateEMI(int loanId)
        {
            Loan loan = GetLoanById(loanId);
            return CalculateEMI(loan.PrincipalAmount, loan.InterestRate, loan.LoanTerm);
        }

        public double CalculateEMI(double principal, double rate, int term)
        {
            double monthlyRate = rate / 12 / 100;
            return (principal * monthlyRate * Math.Pow(1 + monthlyRate, term)) / (Math.Pow(1 + monthlyRate, term) - 1);
        }

        public void LoanRepayment(int loanId, double amount)
        {
            double emi = CalculateEMI(loanId);
            if (amount < emi)
            {
                Console.WriteLine("Payment rejected. Amount is less than single EMI.");
                return;
            }
            int noOfEMIs = (int)(amount / emi);
            Console.WriteLine($"Payment accepted. Number of EMIs paid: {noOfEMIs}");
        }

        public List<Loan> GetAllLoan()
        {
            List<Loan> loans = new List<Loan>();
            using (SqlConnection conn = DBConnUtil.GetConnection(_connFile))
            {
                conn.Open();
                string query = "SELECT * FROM Loans";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Loan loan = new Loan
                    {
                        LoanId = (int)reader["LoanId"],
                        PrincipalAmount = (double)reader["PrincipalAmount"],
                        InterestRate = (double)reader["InterestRate"],
                        LoanTerm = (int)reader["LoanTerm"],
                        LoanType = reader["LoanType"].ToString(),
                        LoanStatus = reader["LoanStatus"].ToString(),
                        Customer = new Customer { CustomerId = (int)reader["CustomerId"] }
                    };
                    loans.Add(loan);
                }
            }
            return loans;
        }

        public Loan GetLoanById(int loanId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(_connFile))
            {
                conn.Open();
                string query = "SELECT * FROM Loans WHERE LoanId = @LoanId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LoanId", loanId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Loan
                    {
                        LoanId = (int)reader["LoanId"],
                        PrincipalAmount = (double)reader["PrincipalAmount"],
                        InterestRate = (double)reader["InterestRate"],
                        LoanTerm = (int)reader["LoanTerm"],
                        LoanType = reader["LoanType"].ToString(),
                        LoanStatus = reader["LoanStatus"].ToString(),
                        Customer = new Customer { CustomerId = (int)reader["CustomerId"] }
                    };
                }
            }
            throw new InvalidLoanException("Loan not found with ID: " + loanId);
        }
    }
}
