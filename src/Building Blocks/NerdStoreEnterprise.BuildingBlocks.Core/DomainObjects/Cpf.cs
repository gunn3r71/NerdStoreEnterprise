using NerdStoreEnterprise.BuildingBlocks.Core.Shared.Tools;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.DomainObjects
{
    public class Cpf : IValueObject
    {
        public const int Length = 11;

        protected Cpf()
        {
        }
        
        public Cpf(string number) : this()
        {
            
            Number = IsValid(number) ? number : throw new DomainException("Invalid CPF.");
        }
        
        public string Number { get; private set; }

        public static bool IsValid(string cpf)
        {
            cpf = cpf.OnlyNumbers();
            
            var firstMultiplier = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var secondMultiplier = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length is not Length) return false;

            var tempCpf = cpf[..9];
            
            var sum = 0;

            for(var i=0; i<9; i++) sum += int.Parse(tempCpf[i].ToString()) * firstMultiplier[i];

            var rest = sum % 11;

            rest = rest < 2 ? 0 : 11 - rest;

            var digit = rest.ToString();

            tempCpf += digit;

            sum = 0;

            for(var i=0; i<10; i++) sum += int.Parse(tempCpf[i].ToString()) * secondMultiplier[i];

            rest = sum % 11;

            if (rest < 2) rest = 0;

            else

                rest = 11 - rest;

            digit += rest.ToString();

            return cpf.EndsWith(digit);
        }
    }
}