using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.NPOI.Extensions;

namespace Yangtao.Hosting.NPOI
{
    /// <summary>
    /// Excel 导出基类
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ExcelExportBase<TResult> where TResult : class
    {
        /// <summary>
        /// 数据行索引
        /// </summary>
        protected int DataRowIndex;

        /// <summary>
        /// XSSFWorkbook
        /// </summary>
        public readonly XSSFWorkbook Workbook;

        /// <summary>
        /// 工作表
        /// </summary>
        public readonly ISheet Sheet;

        /// <summary>
        /// 表头行
        /// </summary>
        public readonly IRow HeadRow;

        /// <summary>
        /// 表头单元格格式
        /// </summary>
        public readonly ICellStyle HeadCellStyle;

        /// <summary>
        /// 实线边框样式
        /// </summary>
        public readonly ICellStyle SolidBorderStyle;

        /// <summary>
        /// 工作表名
        /// </summary>
        public readonly string SheetName;

        /// <summary>
        /// 作者
        /// </summary>
        public readonly string Author;

        /// <summary>
        /// 构建 <see cref="ExcelExportBase{TResult}"/>
        /// </summary>
        /// <param name="sheetName">工作表名</param>
        /// <param name="author">作者</param>
        protected ExcelExportBase(string sheetName, string author = "")
        {
            SheetName = sheetName;
            Workbook = new XSSFWorkbook();
            Author = author;
            if (author.NotNullAndEmpty()) Workbook.SetAuthor(author);

            HeadCellStyle = Workbook.CreateCellStyle(cellStyle =>
            {
                cellStyle.BorderTop = BorderStyle.Thin;
                cellStyle.BorderRight = BorderStyle.Thin;
                cellStyle.BorderBottom = BorderStyle.Thin;
                cellStyle.BorderLeft = BorderStyle.Thin;

                var font = Workbook.CreateFont();
                font.IsBold = true;
                cellStyle.SetFont(font);
            });

            SolidBorderStyle = Workbook.CreateDrawBorderStyle();
            Sheet = Workbook.CreateSheet(sheetName);

            HeadRow = Sheet.CreateRow(0);
            DataRowIndex = 1;
        }

        /// <summary>
        /// 列名集合
        /// </summary>
        public abstract string[] Columns { get; }

        /// <summary>
        /// 文件名
        /// </summary>
        public abstract string FileName { get; }

        /// <summary>
        /// 内容类型
        /// </summary>
        public virtual string ContentType => Workbook.WorkbookType.ContentType;

        /// <summary>
        /// 自动调整列宽
        /// </summary>
        protected virtual void AutoResizeColumn()
        {
            for (int i = 0; i < HeadRow.Cells.Count; i++)
            {
                Sheet.AutoSizeColumn(i);
            }
        }

        /// <summary>
        /// 创建表头
        /// </summary>
        protected virtual void CreateColumns()
        {
            for (int i = 0; i < Columns.Length; i++)
            {
                HeadRow.GetOrCreateCell(HeadRow.Cells.Count, HeadCellStyle).SetBlodFont().SetCellValue(Columns[i]);
            }
        }

        /// <summary>
        /// 填充内容
        /// </summary>
        /// <param name="list">要填充的数据</param>
        protected virtual void FillContent(IEnumerable<TResult> list)
        {
            foreach (var item in list)
            {
                SetRowContent(item);
            }
        }

        /// <summary>
        /// 设置数据行内容
        /// </summary>
        /// <param name="result">要设置的内容</param>
        protected abstract void SetRowContent(TResult result);

        /// <summary>
        /// 获取数据行
        /// </summary>
        /// <returns></returns>
        protected virtual IRow GetDataRow()
        {
            var row = Sheet.CreateRow(DataRowIndex);
            for (int i = 0; i < HeadRow.Cells.Count; i++)
            {
                row.GetOrCreateCell(i, SolidBorderStyle);
            }
            DataRowIndex += 1;
            return row;
        }

        /// <summary>
        /// 获取流
        /// </summary>
        /// <returns></returns>
        public virtual MemoryStream GetStream()
        {
            var memoryStream = new MemoryStream();
            Workbook.Write(memoryStream);

            return memoryStream;
        }
    }
}
