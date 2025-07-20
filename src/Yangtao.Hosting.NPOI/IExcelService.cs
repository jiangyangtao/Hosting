using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;

namespace Yangtao.Hosting.NPOI
{
    /// <summary>
    /// Excel 服务
    /// </summary>
    public interface IExcelService
    {
        /// <summary>
        /// 获取 Excel 列集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ExcelColumns GetExcelColumns<T>();

        /// <summary>
        /// 读取到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="formFiles"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headRowStartIndex"></param>
        /// <param name="headRowCount"></param>
        /// <param name="cellHandle"></param>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        IEnumerable<T> ReadToList<T>(IFormFileCollection formFiles, int sheetIndex = 0, int headRowStartIndex = 0, int headRowCount = 1,
            Func<ExcelColumn, ICell, int, int, ExcelCellHandleResult>? cellHandle = null,
            Func<T, int, ExcelHandleResult>? rowHandle = null) where T : class, new();

        /// <summary>
        /// 读取到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headRowStartIndex"></param>
        /// <param name="headRowCount"></param>
        /// <param name="cellHandle"></param>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        IEnumerable<T> ReadToList<T>(Stream stream, int sheetIndex = 0, int headRowStartIndex = 0, int headRowCount = 1,
            Func<ExcelColumn, ICell, int, int, ExcelCellHandleResult>? cellHandle = null,
            Func<T, int, ExcelHandleResult>? rowHandle = null) where T : class, new();

        /// <summary>
        /// 读取到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headRowStartIndex"></param>
        /// <param name="headRowCount"></param>
        /// <param name="cellHandle"></param>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        IEnumerable<T> ReadToList<T>(string filePath, int sheetIndex = 0, int headRowStartIndex = 0, int headRowCount = 1,
            Func<ExcelColumn, ICell, int, int, ExcelCellHandleResult>? cellHandle = null,
            Func<T, int, ExcelHandleResult>? rowHandle = null) where T : class, new();

        /// <summary>
        /// 读取到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headRowStartIndex"></param>
        /// <param name="headRowCount"></param>
        /// <param name="cellHandle"></param>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> ReadToListAsync<T>(string filePath, int sheetIndex = 0, int headRowStartIndex = 0, int headRowCount = 1,
            Func<ExcelColumn, ICell, int, int, ExcelCellHandleResult>? cellHandle = null,
            Func<T, int, ExcelHandleResult>? rowHandle = null) where T : class, new();

        /// <summary>
        /// 读取到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workbook"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="headRowStartIndex"></param>
        /// <param name="headRowCount"></param>
        /// <param name="cellHandle"></param>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        IEnumerable<T> ReadToList<T>(IWorkbook workbook, int sheetIndex = 0, int headRowStartIndex = 0, int headRowCount = 1,
           Func<ExcelColumn, ICell, int, int, ExcelCellHandleResult>? cellHandle = null,
           Func<T, int, ExcelHandleResult>? rowHandle = null) where T : class, new();

        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="formFiles"></param>
        /// <returns></returns>
        IWorkbook ReadExcel(IFormFileCollection formFiles);

        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        IWorkbook ReadExcel(Stream stream);

        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        IWorkbook ReadExcel(string filePath);

        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task<IWorkbook> ReadExcelAsync(string filePath);

        /// <summary>
        /// 导出模板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ExcelExportResult ExportTemplate<T>() where T : class, new();
    }
}
