using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Helper
{
    public abstract class FunctionConvert : IStoreModelConvention<EntityContainer>
    {
        private string mNamespacep;

        public FunctionConvert(string @namespace = "dbo")
        {
            this.mNamespacep = @namespace;
        }

        public void Apply(EntityContainer item, DbModel model)
        {
            List<EdmFunction> fucntions = this.CreateEdmFunctions(model);
            fucntions.ForEach(function => model.StoreModel.AddItem(function));
        }

        protected abstract Type GetFunctionType();

        private List<EdmFunction> CreateEdmFunctions(DbModel model)
        {
            Type type = this.GetFunctionType();
            MethodInfo[] methods = type.GetMethods();
            List<EdmFunction> edmFunctions = new List<EdmFunction>();
            foreach (var method in methods)
            {
                DbFunctionAttribute dbFunction = method.GetCustomAttributes(false).OfType<DbFunctionAttribute>().FirstOrDefault();
                if(dbFunction != null)
                {
                    List<FunctionParameter> functionParameters = this.CreateFunctionParameters(method,model);
                    FunctionParameter returnParameter = this.CreateReturnParameter(method,model);
                    var functionPayload = new EdmFunctionPayload
                    {
                        Parameters = functionParameters.ToArray(),
                        ReturnParameters = new List<FunctionParameter>() { returnParameter }.ToArray(),
                        IsComposable = true,
                        Schema = this.mNamespacep,
                        IsBuiltIn = false
                    };
                    edmFunctions.Add(EdmFunction.Create(dbFunction.FunctionName, constant.Namespace, DataSpace.SSpace, functionPayload, null));
                }
            }
            return edmFunctions;
        }

        private List<FunctionParameter> CreateFunctionParameters(MethodInfo method, DbModel model)
        {
            ParameterInfo[] parameters = method.GetParameters();
            List<FunctionParameter> functionParameters = new List<FunctionParameter>();
            foreach (var parameter in parameters)
            {
                string parameterName = parameter.Name;
                PrimitiveType primitiveType = PrimitiveType.GetEdmPrimitiveTypes().FirstOrDefault(t => t.ClrEquivalentType == parameter.ParameterType);
                EdmType edmType = model.ProviderManifest.GetStoreType(TypeUsage.CreateDefaultTypeUsage(primitiveType)).EdmType;
                ParameterMode parameterMode = !parameter.IsIn ? ParameterMode.In : ParameterMode.Out;
                FunctionParameter functionParameter = FunctionParameter.Create(parameterName, edmType, parameterMode); 
                functionParameters.Add(functionParameter);
            }
            return functionParameters;
        }

        private FunctionParameter CreateReturnParameter(MethodInfo method,DbModel model)
        {
            Type parameterType = method.ReturnType;
            string parameterName = "return";
            PrimitiveType primitiveType = PrimitiveType.GetEdmPrimitiveTypes().FirstOrDefault(t => t.ClrEquivalentType == parameterType);
            EdmType edmType = model.ProviderManifest.GetStoreType(TypeUsage.CreateDefaultTypeUsage(primitiveType)).EdmType;
            ParameterMode parameterMode = ParameterMode.ReturnValue;
            FunctionParameter functionParameter = FunctionParameter.Create(parameterName, edmType, parameterMode);
            return functionParameter;
        }
    }
}
