using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper.SQLFunctions
{
    internal class StringJoinFunction: ISQLFunction
    {
        public string GetFucntionName()
        {
            return constant.Join;
        }

        public string GetFunctionBody()
        {
            StringBuilder function = new StringBuilder();
            function.AppendLine("declare @returnValue varchar(200)");
            function.AppendLine("set @returnValue = 'Hello World'");
            function.AppendLine("return @returnValue");
            return function.ToString();
        }

        public Dictionary<string, SqlDbType> GetParameters()
        {
            Dictionary<string, SqlDbType> dics = new Dictionary<string, SqlDbType>();
            dics.Add("Id", SqlDbType.UniqueIdentifier);
            return dics;
        }

        public KeyValuePair<SqlDbType, int> GetReturn()
        {
            return new KeyValuePair<SqlDbType, int>(SqlDbType.VarChar, 200);
        }
    }
}
