using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SystemManagement.Shared.Attributes;
using SystemManagement.Shared.Utilities;

namespace SystemManagement.Api.Utilities
{
    /// <summary>
    /// Contains extension methods of related to SqlParameter
    /// </summary>
    public static class SqlParameterExtensions
    {
        /// <summary>
        /// Convert input to list of SqlParameters
        /// </summary>
        /// <param name="input">Source of conversion</param>
        /// <typeparam name="TInput">Must be a input class</typeparam>
        /// <returns></returns>
        public static List<SqlParameter> ToSqlParameters<TInput>(this TInput input) where TInput : class, new()
        {
            var result = new List<SqlParameter>();

            var properties = typeof(TInput).GetProperties();

            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;

                var attributes = property.GetCustomAttributes(false);

                var isPropertyIgnored = attributes.Any(attribute => attribute is IgnoreInStoredProcedureParametersAttribute);

                if (isPropertyIgnored)
                {
                    continue;
                }

                var isPropertyArrayType = propertyType.IsArray;
                var isPropertyListType = propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>);
                var needsToBeConvertedToDataTable = isPropertyArrayType || isPropertyListType;

                var storedProcedureParameterAttribute = (StoredProcedureParameterAttribute)attributes.SingleOrDefault(attribute => attribute is StoredProcedureParameterAttribute);

                SqlParameter sqlParameter;

                if (storedProcedureParameterAttribute != null)
                {
                    SqlDbType sqlDbType;
                    object value;

                    if (storedProcedureParameterAttribute.SqlDbType == default)
                    {
                        sqlDbType = needsToBeConvertedToDataTable ? SqlDbType.Structured : propertyType.ToSqlDbType();

                        value =
                            needsToBeConvertedToDataTable
                                ? property.GetValue(input).ToDataTable()
                                : property.GetValue(input);
                    }
                    else
                    {
                        sqlDbType = needsToBeConvertedToDataTable ? SqlDbType.Structured : storedProcedureParameterAttribute.SqlDbType;

                        value = sqlDbType == SqlDbType.Structured && needsToBeConvertedToDataTable
                            ? property.GetValue(input).ToDataTableWithEnumerableType(propertyType)
                            : property.GetValue(input);
                    }

                    var parameterName =
                        string.IsNullOrWhiteSpace(storedProcedureParameterAttribute.ParameterName)
                            ? $"@{property.Name}"
                            : storedProcedureParameterAttribute.ParameterName;

                    sqlParameter = new SqlParameter
                    {

                        XmlSchemaCollectionDatabase = storedProcedureParameterAttribute.XmlSchemaCollectionDatabase,
                        XmlSchemaCollectionOwningSchema = storedProcedureParameterAttribute.XmlSchemaCollectionOwningSchema,
                        XmlSchemaCollectionName = storedProcedureParameterAttribute.XmlSchemaCollectionName,
                        ParameterName = parameterName,
                        Precision = storedProcedureParameterAttribute.Precision,
                        Scale = storedProcedureParameterAttribute.Scale,
                        Value = value,
                        Direction = storedProcedureParameterAttribute.Direction,
                        Size = storedProcedureParameterAttribute.Size,
                        SourceColumn = storedProcedureParameterAttribute.SourceColumn,
                        SourceVersion = storedProcedureParameterAttribute.SourceVersion,
                        SourceColumnNullMapping = storedProcedureParameterAttribute.SourceColumnNullMapping,
                        IsNullable = storedProcedureParameterAttribute.IsNullable,
                        SqlDbType = sqlDbType
                    };
                }
                else
                {
                    var convertedToSqlDbType =
                        needsToBeConvertedToDataTable
                            ? SqlDbType.Structured
                            : propertyType.ToSqlDbType();

                    var value =
                        convertedToSqlDbType == SqlDbType.Structured
                            ? property.GetValue(input).ToDataTableWithEnumerableType(propertyType)
                            : property.GetValue(input);

                    var isNullableType = Nullable.GetUnderlyingType(propertyType) != null;

                    sqlParameter = new SqlParameter
                    {
                        Value = value,
                        ParameterName = $"@{property.Name}",
                        SqlDbType = convertedToSqlDbType,
                        IsNullable = !needsToBeConvertedToDataTable && isNullableType
                    };
                }

                result.Add(sqlParameter);
            }
            return result;
        }

        private static SqlDbType ToSqlDbType(this Type dataType)
        {

            var isNullableType = Nullable.GetUnderlyingType(dataType) != null;

            if (isNullableType)
            {
                dataType = dataType.GenericTypeArguments[0];
            }

            if (dataType.BaseType == typeof(Enum))
            {
                return SqlDbType.Int;
            }

            var sqlParameter = new SqlParameter();
            var typeConverter = System.ComponentModel.TypeDescriptor.GetConverter(sqlParameter.DbType);
            if (typeConverter.CanConvertFrom(dataType))
            {
                sqlParameter.DbType = (DbType)typeConverter.ConvertFrom(dataType.Name);
            }
            else
            {
                try
                {
                    sqlParameter.DbType = (DbType)typeConverter.ConvertFrom(dataType.Name);
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException(
                        $"Could not convert '{dataType.FullName}' to 'System.Data.SqlDbType', original exception message: {exception.Message}");
                }
            }
            return sqlParameter.SqlDbType;
        }
    }
}