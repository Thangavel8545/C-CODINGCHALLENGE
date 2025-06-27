
using LoanManagementSystem.entity.model;
using System.Collections.Generic;

namespace LoanManagementSystem.dao
{
    public interface ILoanRepository
    {
        void ApplyLoan(Loan loan);
        double CalculateInterest(int loanId);
        double CalculateInterest(double principal, double rate, int term);
        void LoanStatus(int loanId);
        double CalculateEMI(int loanId);
        double CalculateEMI(double principal, double rate, int term);
        void LoanRepayment(int loanId, double amount);
        List<Loan> GetAllLoan();
        Loan GetLoanById(int loanId);
    }
}
