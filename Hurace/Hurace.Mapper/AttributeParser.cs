using System;
using System.Data;
using System.Linq;
using Hurace.Core.Mapper.Attributes;

namespace Hurace.Core.Mapper
{
    internal class AttributeParser
    {
        /// <summary>
        ///     Returns the table name of a type. If the Table is not found the type name will be used.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The table name of the type.</returns>
        internal static string GetTableName(Type type)
        {
            var tableName = nameof(type);
            var tableAttributes = (Table[]) type.GetCustomAttributes(typeof(Table), false);

            if (tableAttributes.Length > 0) tableName = tableAttributes[0].Name;

            return tableName;
        }

        /// <summary>
        ///     Returns the name of the property with a key attribute. If no or multiple keys are defined an exception is thrown.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The property name with the key attribute</returns>
        public static string GetKey(Type type)
        {
            var keyProperties = type.GetProperties().Where(property => Attribute.IsDefined(property, typeof(Key)))
                .ToArray();

            if (keyProperties.Length > 1)
                throw new DataException("The mapper only supports an entity with a single key!");
            if (keyProperties.Length == 0)
                throw new DataException("The mapper only supports an entity with a key!");

            return keyProperties[0].Name;
        }
    }
}