using System.Text.RegularExpressions;

namespace NerdStoreEnterprise.BuildingBlocks.Core.DomainObjects
{
    public class Email : IValueObject
    {
        public const int MaxLength = 254;
        public const int MinLenght = 5;

        protected Email()
        {
        }
        
        public Email(string address) : this()
        {
            Address = IsValid(address) ? address : throw new DomainException("The email provided is invalid.");
        }

        public string Address { get; private set; }

        public static bool IsValid(string email)
        {
            if (email is {Length: > MaxLength} or {Length: < MinLenght})
                return false;
            
            var emailRegex = new Regex(@"/^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$/i");

            return emailRegex.IsMatch(email);
        }
    }
}