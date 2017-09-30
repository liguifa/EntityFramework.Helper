using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper.Test
{
    public class TestContext : DbContext
    {
        public TestContext():base("test")
        {
            Database.SetInitializer(new MyContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Conventions.Add(new StringFunctionConvert());
        } 
    }

    public class MyContextInitializer : CreateDatabaseIfNotExists<TestContext>
    {
        protected override void Seed(TestContext context)
        {
            EFFunctionInitialize.Initialize(context, new StringFunctionInitialize());
            for (int i = 0; i < 100; i++)
            {
                context.Set<User>().Add(new User() { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), Version = 1 });
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }

    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Version { get; set; }
    }

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {

        }
    }
}
