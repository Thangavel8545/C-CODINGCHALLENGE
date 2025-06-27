
namespace LoanManagementSystem.entity.model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CreditScore { get; set; }

        public Customer() {}

        public Customer(int id, string name, string email, string phone, string address, int creditScore)
        {
            CustomerId = id;
            Name = name;
            Email = email;
            PhoneNumber = phone;
            Address = address;
            CreditScore = creditScore;
        }

        public override string ToString()
        {
            return $"CustomerId: {CustomerId}, Name: {Name}, Email: {Email}, Phone: {PhoneNumber}, Address: {Address}, CreditScore: {CreditScore}";
        }
    }
}
