using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper.SQLFunctions
{
    public class JsonGetValueHelper : ISQLFunction
    {
        public string GetFucntionName()
        {
            return constant.GetValue;
        }

        public string GetFunctionBody()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, SqlDbType> GetParameters()
        {
            Dictionary<string, SqlDbType> dics = new Dictionary<string, SqlDbType>();
            dics.Add("json", SqlDbType.VarChar);
            dics.Add("name", SqlDbType.VarChar);
            return dics;
        }

        public KeyValuePair<SqlDbType, int> GetReturn()
        {
            return new KeyValuePair<SqlDbType, int>(SqlDbType.VarChar, 0);
        }
    }
}
