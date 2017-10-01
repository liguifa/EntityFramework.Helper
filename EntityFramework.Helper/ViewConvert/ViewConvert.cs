using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public class ViewConvert : IStoreModelConvention<EntityContainer>
    {
        private Type mType;

        public ViewConvert(Type type)
        {
            this.mType = type;
        }

        public void Apply(EntityContainer item, DbModel model)
        {
            EntityType entityType = this.GetEntityType(model);
            EntitySet entitySet = this.GetEntitySet(entityType);
            item.AddEntitySetBase(entitySet);
            model.StoreModel.AddItem(entityType);
        }

        private EntitySet GetEntitySet(EntityType entityType)
        {
            string name = this.mType.Name;
            string tableName = name;
            string schema = "dbo";
            TableAttribute tableConfig = this.mType.GetCustomAttributes(false).OfType<TableAttribute>().FirstOrDefault();
            if (tableConfig != null)
            {
                tableName = tableConfig.Name;
                schema = string.IsNullOrEmpty(tableConfig.Schema) ? schema : tableConfig.Schema;
            }
            EntitySet entitySet = EntitySet.Create(name, schema, tableName, string.Empty, entityType, null);
            return entitySet;
        }

        private EntityType GetEntityType(DbModel model)
        {
            string name = this.mType.Name;
            List<EdmMember> members = this.GetEdmMembers(model);
            return EntityType.Create(name, constant.Namespace, DataSpace.SSpace, new List<string>() { members.First().Name }, members, null);
        }

        public List<EdmMember> GetEdmMembers(DbModel model)
        {
            List<PropertyInfo> properties = this.mType.GetProperties().ToList();
            List<EdmMember> members = new List<EdmMember>();
            foreach (PropertyInfo property in properties)
            {
                PrimitiveType primitiveType = PrimitiveType.GetEdmPrimitiveTypes().FirstOrDefault(t => t.ClrEquivalentType == property.PropertyType);
                EdmType edmType = model.ProviderManifest.GetStoreType(TypeUsage.CreateDefaultTypeUsage(primitiveType)).EdmType;
                TypeUsage typeUsage = TypeUsage.CreateDefaultTypeUsage(edmType);
                EdmProperty edmProperty = EdmProperty.Create(property.Name, typeUsage);
                edmProperty.Nullable = false;
                members.Add(edmProperty);
            }
            return members;
        }
    }
}
