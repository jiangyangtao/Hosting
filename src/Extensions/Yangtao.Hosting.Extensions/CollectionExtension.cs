using System.Data;

namespace Yangtao.Hosting.Extensions
{
    public static class CollectionExtension
    {
        public static string[] GetColumnNames(this DataColumnCollection dataColumnCollection)
        {
            if (dataColumnCollection == null) return Array.Empty<string>();
            if (dataColumnCollection.Count == 0) return Array.Empty<string>();

            var columnNames = new string[dataColumnCollection.Count];
            for (int i = 0; i < dataColumnCollection.Count; i++)
            {
                columnNames[i] = dataColumnCollection[i].ColumnName;
            }

            return columnNames;
        }
    }
}
