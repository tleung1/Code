using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelperFunctions
{
    public class Functions
    {
        [SqlFunction(DataAccess = DataAccessKind.None)]
        public static int LastIndexOf(string textToFind, string textToSearch)
        {
            int result = 0;
            try
            {
                result = textToSearch.LastIndexOf(textToFind) + 1; //SQL Server uses a 1-based index, C# uses a 0-based index
            }
            catch (NullReferenceException)
            {
                result = 0;
            }

            return result;
        }

        [SqlFunction(DataAccess = DataAccessKind.None)]
        public static string RemoveSymbols(string text)
        {
            if (text == null)
            {
                return text;
            }

            var re = new Regex(@"[^\w\d ]", RegexOptions.IgnoreCase);
            return re.Replace(text, "");
        }

        [SqlFunction(DataAccess = DataAccessKind.None)]
        public static bool Regex(string text, string regex)
        {
            if (text == null || regex == null)
            {
                return false;
            }

            var re = new Regex(regex, RegexOptions.IgnoreCase);
            return re.IsMatch(text);
        }
    }
}
