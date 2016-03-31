using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamerauLevenshteinCLR
{
    //Code via: https://gist.github.com/449595/cb33c2d0369551d1aa5b6ff5e6a802e21ba4ad5c

    public class Functions
    {
        [SqlFunction(DataAccess=DataAccessKind.None)]
        public static int DlEditDistance(string original, string modified)
        {
            //Modified by Jeff Rosenberg
            if (original == null) original = "";
            if (modified == null) modified = "";

            original = original.ToLowerInvariant();
            modified = modified.ToLowerInvariant();

            int len_orig = original.Length;
            int len_diff = modified.Length;

            var matrix = new int[len_orig + 1, len_diff + 1];
            for (int i = 0; i <= len_orig; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= len_diff; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= len_orig; i++)
            {
                for (int j = 1; j <= len_diff; j++)
                {
                    int cost = modified[j - 1] == original[i - 1] ? 0 : 1;
                    var vals = new int[] {
				matrix[i - 1, j] + 1,
				matrix[i, j - 1] + 1,
				matrix[i - 1, j - 1] + cost
			};
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && original[i - 1] == modified[j - 2] && original[i - 2] == modified[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[len_orig, len_diff];
        }
    }
}
