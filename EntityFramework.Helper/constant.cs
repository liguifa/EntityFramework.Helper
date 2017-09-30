using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    internal static class constant
    {
        public const string Namespace = "CodeFirstDatabaseSchema";

        public const string Prefix = "EFHelper_";

        #region string
        public const string StringPrefix = "String_";

        public const string Join = Prefix + StringPrefix + "Join";
        #endregion

        #region MyRegion
        public const string JsonPrefix = "Json_";

        public const string GetValue = Prefix + JsonPrefix + "GetValue";
        #endregion
    }
}
