using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yangtao.Hosting.Abstractions;
using Yangtao.Hosting.NPOI.Attributes;

namespace Yangtao.Hosting.NPOI
{
    /// <summary>
    /// Excel 列
    /// </summary>
    public class ExcelColumn
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 是否必须
        /// </summary>
        public bool Required { get; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// 排序索引
        /// </summary>
        public int SortIndex { get; } = -1;

        /// <summary>
        /// 校验类型
        /// </summary>
        public Type? ValidateType { get; }

        /// <summary>
        /// 是否为日期时间格式
        /// </summary>
        public bool IsDateTimeFormat
        {
            get
            {
                if (ValidateType != null) return BasicTypes.IsDateTimeType(ValidateType);

                return BasicTypes.IsDateTimeType(Property.PropertyType);
            }
        }

        /// <summary>
        /// 是否为数字
        /// </summary>
        public bool IsNumeric
        {
            get
            {
                if (ValidateType != null) return BasicTypes.IsNumeric(ValidateType);

                return BasicTypes.IsNumeric(Property.PropertyType);
            }
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public object? GetCellValue(ICell cell)
        {
            if (cell == null) return null;

            switch (cell.CellType)
            {
                case CellType.Blank: return string.Empty;
                case CellType.Boolean: return cell.BooleanCellValue.ToString();
                case CellType.Error: return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                    {
                        if (cell.CellStyle.DataFormat == 0) return cell.NumericCellValue;
                        if (IsNumeric) return cell.NumericCellValue;
                        if (IsDateTimeFormat) return cell.DateCellValue;

                        return cell.NumericCellValue;
                    }
                case CellType.Unknown: default: return cell.ToString();
                case CellType.String: return cell.StringCellValue.Trim();
                case CellType.Formula:
                    try
                    {
                        var e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parseValue"></param>
        /// <returns></returns>
        public bool ValidateCellValue(string value, out object? parseValue)
        {
            parseValue = value;
            if (BasicTypes.IsStringType(Property.PropertyType))
            {
                var emailAttr = Property.PropertyType.GetCustomAttribute<EmailAddressAttribute>();
                if (emailAttr != null) return emailAttr.IsValid(value);
                return true;
            }

            if (BasicTypes.IsShortType(Property.PropertyType))
            {
                var r = short.TryParse(value, out short _value);
                parseValue = _value;
                return r;
            }

            if (BasicTypes.IsIntType(Property.PropertyType))
            {
                var r = int.TryParse(value, out int _value);
                parseValue = _value;
                return r;
            }

            if (BasicTypes.IsLongType(Property.PropertyType))
            {
                var r = long.TryParse(value, out long _value);
                parseValue = _value;
                return r;
            }

            if (BasicTypes.IsFloatType(Property.PropertyType))
            {
                var r = float.TryParse(value, out float _value);
                parseValue = _value;
                return r;
            }

            if (BasicTypes.IsDoubleType(Property.PropertyType))
            {
                var r = double.TryParse(value, out double _value);
                parseValue = _value;
                return r;
            }

            if (BasicTypes.IsDecimalType(Property.PropertyType))
            {
                var r = decimal.TryParse(value, out decimal _value);
                parseValue = _value;
                return r;
            }

            if (BasicTypes.IsDateTimeType(Property.PropertyType))
            {
                var r = DateTime.TryParse(value, out DateTime _value);
                parseValue = _value;
                return r;
            }

            if (BasicTypes.IsBooleanType(Property.PropertyType))
            {
                var r = bool.TryParse(value, out bool _value);
                parseValue = _value;
                return r;
            }

            if (BasicTypes.IsGuidType(Property.PropertyType))
            {
                var r = Guid.TryParse(value, out Guid _value);
                parseValue = _value;
                return r;
            }

            if (Property.PropertyType.IsEnum)
            {
                var r = Enum.TryParse(Property.PropertyType, value, out object? _value);
                parseValue = _value;
                return r;
            }

            var underlyingType = Nullable.GetUnderlyingType(Property.PropertyType);
            if (underlyingType != null && underlyingType.IsEnum)
            {
                var r = Enum.TryParse(underlyingType, value, out object? _value);
                parseValue = _value;
                return r;
            }

            return true;
        }


        public ExcelColumn(PropertyInfo property)
        {
            Property = property;
            var attr = property.GetCustomAttribute<ExcelColumnAttribute>() ?? throw new ArgumentNullException(nameof(property));
            Name = attr.Name;
            Required = attr.Required;
            ValidateType = attr.ValidateType;
        }
    }

    /// <summary>
    /// Excel 列集合
    /// </summary>
    public class ExcelColumns : IEnumerable<ExcelColumn>
    {
        private readonly List<ExcelColumn> Columns;

        public ExcelColumns() { }

        public ExcelColumns(PropertyInfo[] propertyInfos)
        {
            Columns = propertyInfos.Select(a => new ExcelColumn(a)).ToList();
        }

        /// <summary>
        /// 获取不存在的列
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public IEnumerable<string> GetNotExistColumn(IEnumerable<string> columns)
        {
            var missColumns = new List<string>();
            foreach (var item in Columns)
            {
                if (item.Required == false) continue;

                var exist = columns.Any(a => a == item.Name);
                if (exist == false) missColumns.Add(item.Name);
            }

            return missColumns;
        }

        /// <summary>
        /// 获取列
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public ExcelColumn? GetExcelColumn(string columnName) => Columns.FirstOrDefault(a => a.Name == columnName);

        public IEnumerator<ExcelColumn> GetEnumerator() => Columns.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
