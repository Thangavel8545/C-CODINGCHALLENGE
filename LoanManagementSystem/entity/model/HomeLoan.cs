
using LoanManagementSystem.entity.model;

namespace LoanManagementSystem.entity.model
{
    public class HomeLoan : Loan
    {
        public string PropertyAddress { get; set; }
        public int PropertyValue { get; set; }

        public HomeLoan() {}

        public HomeLoan(int loanId, Customer customer, double principal, double rate, int term, string status,
                        string propertyAddress, int propertyValue)
            : base(loanId, customer, principal, rate, term, "HomeLoan", status)
        {
            PropertyAddress = propertyAddress;
            PropertyValue = propertyValue;
        }

        public override string ToString()
        {
            return base.ToString() + $", PropertyAddress: {PropertyAddress}, PropertyValue: {PropertyValue}";
        }
    }
}
