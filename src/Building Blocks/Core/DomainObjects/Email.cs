using System.Text.RegularExpressions;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects
{
    public class Email : IValueObject
    {
        public const int MaxLength = 254;
        public const int MinLenght = 5;

        private Email()
        {
        }

        public Email(string address) : this()
        {
            Address = IsValid(address) ? address : throw new DomainException("The email provided is invalid.");
        }

        public string Address { get; }

        public static bool IsValid(string email)
        {
            if (email is {Length: > MaxLength} or {Length: < MinLenght})
                return false;

            var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            return emailRegex.IsMatch(email);
        }
    }
}