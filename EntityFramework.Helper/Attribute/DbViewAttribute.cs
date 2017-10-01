using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public class DbViewAttribute:Attribute
    {
        public string Namespace { get; set; }

        public string Name { get; set; }

        public DbViewAttribute(string name,string @namespace = "dbo")
        {
            this.Name = name;
            this.Namespace = @namespace;
        }
    }
}
