using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TestContext ctx = new TestContext())
            {
                var strs = ctx.Set<User>().Select(d=>String.Join("Users","@","Version = 1")).ToList();
            }
        }
    }
}
