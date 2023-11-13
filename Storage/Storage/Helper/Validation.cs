using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Helper
{
    internal class Validation
    {
        public const string NOTALLOWED = @"><@{}[]#&()/|!^*-+$%~\";
        public const string EMAIL = @"><{}[]#&()!^/|*+$%~\";
        public const string NO = @"><@{}[]#&()/|!^*+$%~\";
    }
}
