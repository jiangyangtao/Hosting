using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Yangtao.Hosting.Core.HttpErrorResult;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.NPOI.Abstractions;
using Yangtao.Hosting.NPOI.Attributes;
using Yangtao.Hosting.NPOI.Extensions;

namespace Yangtao.Hosting.NPOI
{
    internal class ExcelService : IExcelService
    {
        public ExcelService()
        {
        }

        public ExcelColumns GetExcelColumns<T>()
        {
            var type = typeof(T);
            var properties = type.GetProperties().Where(a => a.HasAttribute<ExcelColumnAttribute>()).ToArray();
            if (properties.IsNullOrEmpty()) throw new HttpErrorResult(400, "Not found ExcelColumnAttribute the filed.");

            return new ExcelColumns(properties);
        }

        public ExcelExportResult ExportTemplate<T>() where T : class, new()
        {
            var excelColumns = GetExcelColumns<T>().OrderBy(a => a.SortIndex);
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet();
            var drawBorderStyle = workbook.CreateSolidBorderStyle();

            var headRow = sheet.CreateRow(0);
            foreach (var item in excelColumns)
            {
                var cell = headRow.GetOrCreateCell(headRow.Cells.Count, drawBorderStyle).SetBlodFont();
                cell.SetCellValue(item.Name);
            }

            return new(workbook);
        }

        public IWorkbook ReadExcel(IFormFileCollection formFiles)
        {
            if (formFiles.IsNullOrEmpty()) throw new HttpErrorResult(400, "未检测到上传的文件");

            var formFile = formFiles[0] ?? throw new HttpErrorResult(400, "未检测到上传的文件");
            var isExcel = formFile.FileName.IsExcel();
            if (isExcel == false) throw new HttpErrorResult(400, "请上传 Excel 文件");

            return ReadExcel(formFile.OpenReadStream());
        }

        public IWorkbook ReadExcel(Stream stream)
        {
            var workbook = WorkbookFactory.Create(stream);
            return workbook ?? throw new HttpErrorResult(400, "Excel 读取失败，请上传正确的 Excel");
        }

        public IWorkbook ReadExcel(string filePath)
        {
            var exist = File.Exists(filePath);
            if (exist) throw new HttpErrorResult(400, "未找到文件");

            var buffer = File.ReadAllBytes(filePath);
            if (buffer.IsNullOrEmpty()) throw new HttpErrorResult(400, "无可用的流");

            var stream = new MemoryStream(buffer);
            return ReadExcel(stream);
        }

        public async Task<IWorkbook> ReadExcelAsync(string filePath)
        {
            var exist = File.Exists(filePath);
            if (exist) throw new HttpErrorResult(400, "未找到文件");

            var buffer = await File.ReadAllBytesAsync(filePath);
            if (buffer.IsNullOrEmpty()) throw new HttpErrorResult(400, "无效的文件");

            var stream = new MemoryStream(buffer);
            return ReadExcel(stream);
        }

        public IEnumerable<T> ReadToList<T>(
            IFormFileCollection formFiles, int sheetIndex = 0, int headRowStartIndex = 0, int headRowCount = 1,
            Func<ExcelColumn, ICell, int, int, ExcelCellHandleResult>? cellHandle = null,
            Func<T, int, ExcelHandleResult>? rowHandle = null) where T : class, new()
        {
            var workbook = ReadExcel(formFiles);
            return ReadToList(workbook, sheetIndex, headRowStartIndex, headRowCount, cellHandle, rowHandle);
        }

        public IEnumerable<T> ReadToList<T>(Stream stream, int sheetIndex = 0, int headRowStartIndex = 0, int headRowCount = 1,
            Func<ExcelColumn, ICell, int, int, ExcelCellHandleResult>? cellHandle = null,
            Func<T, int, ExcelHandleResult>? rowHandle = null) where T : class, new()
        {
            var workbook = ReadExcel(stream);
            return ReadToList(workbook, sheetIndex, headRowStartIndex, headRowCount, cellHandle, rowHandle);
        }

        public IEnumerable<T> ReadToList<T>(string filePath, int sheetIndex = 0, int headRowStartIndex = 0, int headRowCount = 1,
            Func<ExcelColumn, ICell, int, int, ExcelCellHandleResult>? cellHandle = null,
            Func<T, int, ExcelHandleResult>? rowHandle = null) where T : class, new()
        {
            var workbook = ReadExcel(filePath);
            return ReadToList(workbook, sheetIndex, headRowStartIndex, headRowCount, cellHandle, rowHandle);
        }


        public async Task<IEnumerable<T>> ReadToListAsync<T>(string filePath, int sheetIndex = 0, int headRowStartIndex = 0, int headRowCount = 1,
            Func<ExcelColumn, ICell, int, int, ExcelCellHandleResult>? cellHandle = null,
            Func<T, int, ExcelHandleResult>? rowHandle = null) where T : class, new()
        {
            var workbook = await ReadExcelAsync(filePath);
            return ReadToList(workbook, sheetIndex, headRowStartIndex, headRowCount, cellHandle, rowHandle);
        }


        public IEnumerable<T> ReadToList<T>(IWorkbook workbook, int sheetIndex = 0, int headRowStartIndex = 0, int headRowCount = 1,
           Func<ExcelColumn, ICell, int, int, ExcelCellHandleResult>? cellHandle = null,
           Func<T, int, ExcelHandleResult>? rowHandle = null) where T : class, new()
        {
            var sheet = workbook.GetSheetAt(sheetIndex) ?? throw new HttpErrorResult(400, "没有可使用的工作簿");
            var excelColumns = GetExcelColumns<T>();
            var excelColumnNames = sheet.GetColumns();
            var notExistColumns = excelColumns.GetNotExistColumn(excelColumnNames);
            if (notExistColumns.NotNullAndEmpty()) throw new HttpErrorResult(400, $"缺少 {string.Join("、", notExistColumns)} 列");

            var headerRow = sheet.GetRow(headRowStartIndex);
            var cellCount = headerRow.LastCellNum;
            var rowCount = sheet.LastRowNum;
            var rowStartIndex = sheet.FirstRowNum + headRowCount;

            var list = new List<T>();
            for (var rowIndex = rowStartIndex; rowIndex <= rowCount; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);
                if (row == null) continue;

                var isBlank = row.IsAllBlank();
                if (isBlank) continue;

                var item = new T();
                for (var columnIndex = row.FirstCellNum; columnIndex < cellCount; columnIndex++)
                {
                    var columnName = excelColumnNames[columnIndex];
                    var excelColumn = excelColumns.GetExcelColumn(columnName);
                    if (excelColumn == null) continue;

                    var cell = row.GetCell(columnIndex);
                    var cellValue = excelColumn.GetCellValue(cell);
                    var breakValidate = false;
                    if (cellHandle != null)
                    {
                        var handleCellResult = cellHandle(excelColumn, cell, rowIndex, columnIndex);
                        if (handleCellResult.Result == false) throw new HttpErrorResult(400, handleCellResult.ErrorMessage);

                        cellValue = handleCellResult.Value;
                        breakValidate = handleCellResult.BreakValidate;
                    }

                    var stringValue = cellValue?.ToString();
                    if (cell == null || cellValue == null || stringValue.IsNullOrEmpty())
                    {
                        if (excelColumn.Required) throw new HttpErrorResult(400, $"第 {rowIndex + 1} 行的 {columnName} 不能为空");
                        continue;
                    }

                    if (breakValidate == false && stringValue.NotNullAndEmpty())
                    {
                        stringValue = stringValue.Trim();
                        var r = excelColumn.ValidateCellValue(stringValue, out cellValue);
                        if (r == false) throw new HttpErrorResult(400, $"第 {rowIndex + 1} 行的 {columnName} 不是正确的格式");
                    }

                    excelColumn.Property.SetValue(item, cellValue);
                }

                if (rowHandle != null)
                {
                    var r = rowHandle(item, rowIndex + 1);
                    if (r.Result == false) throw new HttpErrorResult(400, r.ErrorMessage);
                }

                list.Add(item);
            }

            return list;
        }
    }
}
