namespace Yangtao.Hosting.Common
{
    public static class StaticContentType
    {
        public static class Office
        {
            public const string xls = "application/vnd.ms-excel";
            public const string xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            public const string doc = "application/msword";
            public const string docx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            public const string ppt = "application/vnd.ms-powerpoint";
            public const string pptx = "application/vnd.openxmlformats-officedocument.presentationml.presentation";


            public static bool IsExcel(string contentType)
            {
                if (contentType == xls) return true;
                if (contentType == xlsx) return true;

                return false;
            }

            public static bool IsWord(string contentType)
            {
                if (contentType == doc) return true;
                if (contentType == docx) return true;

                return false;
            }

            public static bool IsPowerPoint(string contentType)
            {
                if (contentType == ppt) return true;
                if (contentType == pptx) return true;

                return false;
            }
        }
    }
}
