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
            return $@"declare @whereSQL varchar(max);
                      declare @returnValue varchar(max);
                      if @where != ''
                        set @whereSQL = ' where ' + @where
                      else
                        set @whereSQL = ''
                      declare @sql varchar(max)
                      set @sql = 'select @returnValue = stuff((select ''' + @separator + ''' + Name from ' + @table + @whereSQL + ' for xml path('''')),1,1,'''')'
                      EXEC sp_executesql @sql
                      return @returnValue";
        }

        public Dictionary<string, SqlDbType> GetParameters()
        {
            Dictionary<string, SqlDbType> dics = new Dictionary<string, SqlDbType>();
            dics.Add("table", SqlDbType.VarChar);
            dics.Add("separator", SqlDbType.VarChar);
            dics.Add("where", SqlDbType.VarChar);
            return dics;
        }

        public KeyValuePair<SqlDbType, int> GetReturn()
        {
            return new KeyValuePair<SqlDbType, int>(SqlDbType.VarChar, 0);
        }
    }
}
