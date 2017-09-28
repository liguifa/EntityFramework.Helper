using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public static class EFFunctionInitialize
    {
        public static void Initialize(DbContext ctx, FunctionInitialize function)
        {
            function.CreateFunction(ctx);
        }
    }
}
