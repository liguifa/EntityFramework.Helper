using EntityFramework.Helper.SQLFunctions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public static class Json
    {
        [DbFunction(constant.Namespace,constant.GetValue)]
        [SQLFunction(typeof(JsonGetValueHelper))]
        public static string GetValue(string json,string name)
        {
            throw new NotImplementedException();
        }
    }
}
