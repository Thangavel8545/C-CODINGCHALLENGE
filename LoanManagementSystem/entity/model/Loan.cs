
using LoanManagementSystem.entity.model;

namespace LoanManagementSystem.entity.model
{
    public class Loan
    {
        public int LoanId { get; set; }
        public Customer Customer { get; set; }
        public double PrincipalAmount { get; set; }
        public double InterestRate { get; set; }
        public int LoanTerm { get; set; } // in months
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }

        public Loan() {}

        public Loan(int loanId, Customer customer, double principal, double rate, int term, string type, string status)
        {
            LoanId = loanId;
            Customer = customer;
            PrincipalAmount = principal;
            InterestRate = rate;
            LoanTerm = term;
            LoanType = type;
            LoanStatus = status;
        }

        public override string ToString()
        {
            return $"LoanId: {LoanId}, CustomerId: {Customer.CustomerId}, Principal: {PrincipalAmount}, InterestRate: {InterestRate}, Term: {LoanTerm}, Type: {LoanType}, Status: {LoanStatus}";
        }
    }
}
