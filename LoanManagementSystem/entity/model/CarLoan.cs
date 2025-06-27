
using LoanManagementSystem.entity.model;

namespace LoanManagementSystem.entity.model
{
    public class CarLoan : Loan
    {
        public string CarModel { get; set; }
        public int CarValue { get; set; }

        public CarLoan() {}

        public CarLoan(int loanId, Customer customer, double principal, double rate, int term, string status,
                       string carModel, int carValue)
            : base(loanId, customer, principal, rate, term, "CarLoan", status)
        {
            CarModel = carModel;
            CarValue = carValue;
        }

        public override string ToString()
        {
            return base.ToString() + $", CarModel: {CarModel}, CarValue: {CarValue}";
        }
    }
}
