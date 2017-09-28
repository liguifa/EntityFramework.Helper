using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public interface ISQLFunction
    {
        string GetFunctionBody();

        Dictionary<string,SqlDbType> GetParameters();

        KeyValuePair<SqlDbType, int> GetReturn();

        string GetFucntionName();
    }
}
