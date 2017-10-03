
using System.Text.RegularExpressions;

namespace EStoreRESTApi.Models
{
    public class Validator
    {
        private const string emailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				              [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				              [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                            + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
        internal static bool emailIsValid(string email)
        {
            if (email != null) return Regex.IsMatch(email, emailPattern);
            else return false;
        }

    }
}