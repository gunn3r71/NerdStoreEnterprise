using System.Linq;

namespace NerdStoreEnterprise.BuildingBlocks.Core.Shared.Tools
{
    public static class StringExtensions
    {
        public static string OnlyNumbers(this string str) =>
            new(str.Where(char.IsDigit).ToArray());
    }
}