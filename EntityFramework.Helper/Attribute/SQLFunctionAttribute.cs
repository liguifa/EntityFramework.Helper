using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public class SQLFunctionAttribute:Attribute
    {
        public Type SQLFunction { get; set; }

        public SQLFunctionAttribute(Type type)
        {
            this.SQLFunction = type;
        }
    }
}
