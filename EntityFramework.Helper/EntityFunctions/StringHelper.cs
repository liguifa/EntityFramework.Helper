﻿using EntityFramework.Helper.SQLFunctions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public static class String
    {
        [DbFunction(constant.Namespace, constant.Join)]
        [SQLFunction(typeof(StringJoinFunction))]
        public static string Join(string table, string separator, string where)
        {
            throw new NotImplementedException();
        }
    }
}
