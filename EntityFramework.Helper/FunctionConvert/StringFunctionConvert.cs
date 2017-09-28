using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public class StringFunctionConvert: FunctionConvert
    {
        public StringFunctionConvert(string @namespace = "dbo") : base(@namespace)
        {

        }

        protected override Type GetFunctionType()
        {
            return typeof(String);
        }
    }
}
