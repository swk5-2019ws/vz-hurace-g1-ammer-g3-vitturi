using System;
using System.Data.Common;
using System.Linq;

namespace Hurace.Core.Mapper
{
    public static class DbCommandExtensions
    {
        public static void AddParam(this DbCommand command, string name, object value)
        {
            _ = command ?? throw new ArgumentNullException($"{nameof(command)} must not be null!");
            var dbParam = command.CreateParameter();
            dbParam.ParameterName = name;
            dbParam.Value = value;
            command.Parameters.Add(dbParam);
        }

        public static void AddParams(this DbCommand command, object param)
        {
            _ = command ?? throw new ArgumentNullException($"{nameof(command)} must not be null!");
            _ = param ?? throw new ArgumentNullException($"{nameof(param)} must not be null!");

            var properties = AttributeParser.GetAllProperties(param.GetType()).ToList();
            foreach (var propertyInfo in properties)
            {
                object value = null;
                if (AttributeParser.HasForeignKey(propertyInfo))
                    value = AttributeParser.GetKey(propertyInfo.PropertyType)
                        .GetValue(propertyInfo.GetValue(param));
                else if (propertyInfo.PropertyType.IsEnum) value = propertyInfo.GetValue(param).ToString();

                command.AddParam(propertyInfo.Name, value ?? propertyInfo.GetValue(param));
            }
        }
    }
}