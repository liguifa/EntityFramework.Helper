using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public class StringFunctionInitialize:FunctionInitialize
    {
        protected override Type GetFunctionType()
        {
            return typeof(String);
        }
    }
}
