using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public abstract class FunctionInitialize
    {
        protected abstract Type GetFunctionType();

        public virtual void CreateFunction(DbContext ctx)
        {
            Type type = this.GetFunctionType();
            MethodInfo[] methods = type.GetMethods();
            foreach (var method in methods)
            {
                SQLFunctionAttribute sqlFunction = method.GetCustomAttributes(false).OfType<SQLFunctionAttribute>().FirstOrDefault();
                if (sqlFunction != null)
                {
                    Type sqlFunctionType = sqlFunction.SQLFunction;
                    string sql = this.GetSQLFunctionSQL(sqlFunctionType);
                    ctx.Database.ExecuteSqlCommand(sql);
                }
            }
        }

        private string GetSQLFunctionSQL(Type type)
        {
            ISQLFunction sqlFunction = Activator.CreateInstance(type) as ISQLFunction;
            string name = sqlFunction.GetFucntionName();
            Dictionary<string, SqlDbType> parameters = sqlFunction.GetParameters();
            KeyValuePair<SqlDbType, int> returnValue = sqlFunction.GetReturn();
            string sql = sqlFunction.GetFunctionBody();
            StringBuilder function = new StringBuilder();
            function.AppendLine($"Create function {name}");
            function.AppendLine("(");
            function.AppendLine(string.Join(",", parameters.Select(d => $"@{d.Key} {d.Value.ToString()}")));
            function.AppendLine(")");
            function.AppendLine($"returns {this.GetSQLReturn(returnValue)}");
            function.AppendLine("as");
            function.AppendLine("begin");
            function.AppendLine(sql);
            function.AppendLine("end");
            return function.ToString();
        }

        private string GetSQLReturn(KeyValuePair<SqlDbType, int> returnValue)
        {
            if (returnValue.Key == SqlDbType.VarChar)
            {
                string size = returnValue.Value <= 0 ? "max" : returnValue.Value.ToString();
                return $"{returnValue.Key.ToString()}({size})";
            }
            return returnValue.Key.ToString();
        }
    }
}
