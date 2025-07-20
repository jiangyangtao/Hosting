namespace Yangtao.Hosting.NPOI
{
    /// <summary>
    /// Excel 处理结果
    /// </summary>
    public class ExcelHandleResult
    {
        internal ExcelHandleResult(bool result, string message)
        {
            Result = result;
            ErrorMessage = message;
        }

        public bool Result { get; }

        public string? ErrorMessage { get; }

        public static ExcelHandleResult Error(string errorMessage) => new(false, errorMessage);
    }

    /// <summary>
    /// Excel 单元格处理结果
    /// </summary>
    public class ExcelCellHandleResult : ExcelHandleResult
    {
        private ExcelCellHandleResult(object? value, bool breakValidate, bool result, string message) : base(result, message)
        {
            Value = value;
            BreakValidate = breakValidate;
        }

        public object? Value { get; }

        /// <summary>
        /// 跳过验证
        /// </summary>
        public bool BreakValidate { get; } = false;

        public static ExcelCellHandleResult Successed(object value, bool breakValidate = false) => new(value, breakValidate, true, string.Empty);
    }

    /// <summary>
    /// Excel 行处理结果
    /// </summary>
    public class ExcelRowHandleResult : ExcelHandleResult
    {
        private ExcelRowHandleResult(bool result, string message) : base(result, message)
        {

        }

        public static ExcelRowHandleResult Successed() => new(true, string.Empty);
    }
}
