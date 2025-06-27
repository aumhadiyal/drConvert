using System.Data;
using System.Reflection;

namespace drConverter
{
    internal static class DataRowConverter
    {
        public static T Convert<T>(DataRow dataRow)
            where T : new()
        {
            T modelObject = new();

            List<string> listOfColumns =
            [
                .. dataRow.Table.Columns.Cast<DataColumn>().Select(col => col.ColumnName),
            ];

            Dictionary<string, PropertyInfo> propertyMap = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

            foreach (string column in listOfColumns)
            {
                object value = dataRow[column];
                if (
                    propertyMap.TryGetValue(column, out PropertyInfo? propertyInfo)
                    && propertyInfo != null
                )
                {
                    if (value != DBNull.Value)
                    {
                        Type targetType = propertyInfo.PropertyType;
                        Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
                        object convertedValue;

                        if (underlyingType.IsEnum)
                            convertedValue = Enum.Parse(
                                underlyingType,
                                System.Convert.ToString(value) ?? string.Empty
                            );
                        else if (underlyingType == typeof(Guid))
                            convertedValue = Guid.Parse(
                                System.Convert.ToString(value) ?? string.Empty
                            );
                        else
                            convertedValue = System.Convert.ChangeType(value, underlyingType);

                        propertyInfo.SetValue(modelObject, convertedValue);
                    }
                }
            }
            return modelObject;
        }

        public static List<T> ConvertList<T>(DataTable dataTable)
            where T : new()
        {
            List<T> modelObjectList = new List<T>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                modelObjectList.Add(Convert<T>(dataRow));
            }
            return modelObjectList;
        }
    }
}
