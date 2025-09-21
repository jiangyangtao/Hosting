using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.NPOI.Extensions;

namespace Yangtao.Hosting.NPOI
{
    public abstract class ExcelExportBase<TResult> where TResult : class
    {
        protected int DataRowIndex;

        public readonly XSSFWorkbook Workbook;
        public readonly ISheet Sheet;
        public readonly IRow HeadRow;
        public readonly ICellStyle DrawBorderStyle;
        public readonly string SheetName;
        public readonly string Author;

        protected ExcelExportBase(string sheetName, string author = "")
        {
            SheetName = sheetName;
            Workbook = new XSSFWorkbook();
            Author = author;
            if (author.NotNullAndEmpty()) Workbook.SetAuthor(author);

            DrawBorderStyle = Workbook.CreateDrawBorderStyle();
            Sheet = Workbook.CreateSheet(sheetName);

            HeadRow = Sheet.CreateRow(0);
            DataRowIndex = 1;

            CreateColumns();

        }

        public abstract string[] Columns { get; }

        public abstract string FileName { get; }

        public virtual string ContentType => Workbook.WorkbookType.ContentType;

        protected virtual void AutiResizeColumn()
        {
            for (int i = 0; i < HeadRow.Cells.Count; i++)
            {
                Sheet.AutoSizeColumn(i);
            }
        }

        protected virtual void CreateColumns()
        {
            for (int i = 0; i < Columns.Length; i++)
            {
                HeadRow.CreateStyleCell(HeadRow.Cells.Count, DrawBorderStyle).SetBlodFont().SetCellValue(Columns[i]);
            }
        }

        protected virtual void FillContent(IEnumerable<TResult> list)
        {
            foreach (var item in list)
            {
                SetRowContent(item);
            }
        }

        protected abstract void SetRowContent(TResult result);


        protected IRow GetDataRow()
        {
            var row = Sheet.CreateRow(DataRowIndex);

            DataRowIndex += 1;
            return row;
        }

        public MemoryStream GetStream()
        {
            var memoryStream = new MemoryStream();
            Workbook.Write(memoryStream);

            return memoryStream;
        }
    }
}
