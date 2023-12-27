
using System.Text.RegularExpressions;

namespace ProniaOnion202.Application.Abstractions.Repository.Common.Extensions
{
    public static class RegisterValidator
    {

        public static bool IsDigit(this string name)
        {
            bool result = false;
            foreach (var item in name)
            {
                result = char.IsDigit(item);
            }
            return result;
        }
        public static bool IsSymbol(this string name)
        {
            bool result = false;
            foreach (var item in name)
            {
                result = char.IsSymbol(item);
            }
            return result;
        }
    }
}


