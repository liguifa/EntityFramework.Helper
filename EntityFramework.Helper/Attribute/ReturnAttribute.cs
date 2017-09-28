using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public class ReturnAttribute : Attribute
    {
        private SqlDbType mType;

        private int mLength;

        public ReturnAttribute(SqlDbType type, int length = 0)
        {
            this.mType = type;
            this.mLength = length;
        }
    }
}
