using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bataille
{
    public static class PuissanceCarte
    {
        public static Dictionary<int, string> Standard = new Dictionary<int, string>()
        {
            {0, "Deux" },
            {1,"Trois" },
            {2,"Quatre" },
            {3,"Cinq" },
            {4,"Six" },
            {5,"Sept" },
            {6,"Huit" },
            {7,"Neuf" },
            {8,"Dix" },
            {9,"Valet" },
            {10,"Dame" },
            {11,"Roi" },
            {12,"As" }
        };

        public static Dictionary<int, string> BelotteNormal = new Dictionary<int, string>()
        {
            {0,"Sept" },
            {1,"Huit" },
            {2,"Neuf" },
            {3,"Valet" },
            {4,"Dame" },
            {5,"Roi" },
            {6,"Dix" },
            {7,"As" }
        };

        public static Dictionary<int, string> BelotteAtout = new Dictionary<int, string>()
        {
            {8,"Sept" },
            {9,"Huit" },
            {10,"Dame" },
            {11,"Roi" },
            {12,"Dix" },
            {13,"As" },
            {14,"Neuf" },
            {15,"Valet" }
        };
    }
}
