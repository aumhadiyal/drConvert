using System.Data;
using System.Reflection;

namespace drConverter
{
    internal static class DataRowConverter
    {
        public static T Convert<T>(DataRow dataRow)
            where T : new()
        {
            T ModelObject = new T();

            List<string> ListOfColumns = dataRow
                .Table.Columns.Cast<DataColumn>()
                .Select(col => col.ColumnName)
                .ToList();

            var propertyMap = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

            foreach (var column in ListOfColumns)
            {
                var value = dataRow[column];
                if (propertyMap.TryGetValue(column, out var propertyInfo))
                {
                    if (value != DBNull.Value)
                    {
                        Type targetType = propertyInfo.PropertyType;
                        Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
                        object convertedValue;

                        if (underlyingType.IsEnum)
                            convertedValue = Enum.Parse(underlyingType, value.ToString());
                        else if (underlyingType == typeof(Guid))
                            convertedValue = Guid.Parse(value.ToString());
                        else
                            convertedValue = System.Convert.ChangeType(value, underlyingType);

                        propertyInfo.SetValue(ModelObject, convertedValue);
                    }
                }
            }
            return ModelObject;
        }
    }
}
