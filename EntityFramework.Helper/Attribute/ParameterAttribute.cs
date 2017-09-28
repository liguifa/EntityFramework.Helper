using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public class ParameterAttribute : Attribute
    {
        private string mName;

        private SqlDbType mType;

        public ParameterAttribute(string name, SqlDbType type)
        {
            this.mName = name;
            this.mType = type;
        }
    }
}
