using NPOI.SS.UserModel;
using Yangtao.Hosting.NPOI.Extensions;

namespace Yangtao.Hosting.NPOI
{
    public class ExcelExportResult
    {
        internal ExcelExportResult(IWorkbook workBook)
        {
            WorkBook = workBook;
            ContentType = workBook.GetContentType();
        }

        public IWorkbook WorkBook { get; }


        public string ContentType { get; }


        public MemoryStream GetStream()
        {
            var stream = new MemoryStream();
            WorkBook.Write(stream);

            return stream;
        }
    }
}
