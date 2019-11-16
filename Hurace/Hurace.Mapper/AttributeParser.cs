using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
        /// <returns>The property info of the key attribute</returns>
        public static PropertyInfo GetKey(Type type)
        {
            var keyProperties = type.GetProperties().Where(property => Attribute.IsDefined(property, typeof(Key)))
                .ToArray();

            if (keyProperties.Length > 1)
                throw new DataException("The mapper only supports an entity with a single key!");
            if (keyProperties.Length == 0)
                throw new DataException("The mapper only supports an entity with a key!");

            return keyProperties[0];
        }

        /// <summary>
        ///     Checks if a property has the ForeignKey attribute.
        /// </summary>
        /// <param name="propertyInfo">The property info</param>
        /// <returns>Tur if the property has the ForeignKey attribute. Otherwise false.</returns>
        public static bool HasForeignKey(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributesData()
                       .Count(attribute => attribute.AttributeType == typeof(ForeignKey)) == 1;
        }

        /// <summary>
        ///     Returns the value of the Key attribute of a property info. If no attribute is found true is returned.
        /// </summary>
        /// <param name="propertyInfo">The property info</param>
        /// <returns>True if the key is generated. Otherwise false.</returns>
        public static bool GetKeyIsGenerated(PropertyInfo propertyInfo)
        {
            var isGenerated = true;

            var keyAttributes = propertyInfo.GetCustomAttributesData()
                .Where(attribute => attribute.AttributeType == typeof(Key)).ToArray();
            if (keyAttributes.Length == 1)
                isGenerated = (bool) keyAttributes[0].ConstructorArguments[0].Value;

            return isGenerated;
        }

        /// <summary>
        ///     Returns all properties of a certain type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The property infos of the type</returns>
        internal static IEnumerable<PropertyInfo> GetAllProperties(Type type)
        {
            return type.GetProperties().AsEnumerable();
        }

        /// <summary>
        ///     Returns the value of the Column or ForeignKey attribute of a property info. If no attribute is found the property
        ///     name is used.
        /// </summary>
        /// <param name="propertyInfo">The property info</param>
        /// <returns>The column name if defined. Otherwise the property name</returns>
        public static string GetColumnName(PropertyInfo propertyInfo)
        {
            var name = propertyInfo.Name;

            var columnAttributes = propertyInfo.GetCustomAttributesData()
                .Where(attribute => attribute.AttributeType == typeof(Column)).ToArray();
            if (columnAttributes.Length == 1)
                name = (string) columnAttributes[0].ConstructorArguments[0].Value;

            var foreignKeyAttributes = propertyInfo.GetCustomAttributesData()
                .Where(attribute => attribute.AttributeType == typeof(ForeignKey)).ToArray();
            if (foreignKeyAttributes.Length == 1)
                name = (string) foreignKeyAttributes[0].ConstructorArguments[0].Value;

            return name;
        }

        /// <summary>
        ///     Returns all collection properties of a type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The collection properties</returns>
        public static IEnumerable<PropertyInfo> GetAllCollections(Type type)
        {
            return GetAllProperties(type).Where(pi =>
                pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>));
        }
    }
}