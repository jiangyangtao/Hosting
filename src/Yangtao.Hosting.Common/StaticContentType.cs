namespace Yangtao.Hosting.Common
{
    public static class StaticContentType
    {
        public static class Office
        {
            public const string Xls = "application/vnd.ms-excel";
            public const string Xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            public const string Doc = "application/msword";
            public const string Docx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            public const string Ppt = "application/vnd.ms-powerpoint";
            public const string Pptx = "application/vnd.openxmlformats-officedocument.presentationml.presentation";


            public static bool IsExcel(string contentType)
            {
                if (contentType == Xls) return true;
                if (contentType == Xlsx) return true;

                return false;
            }

            public static bool IsWord(string contentType)
            {
                if (contentType == Doc) return true;
                if (contentType == Docx) return true;

                return false;
            }

            public static bool IsPowerPoint(string contentType)
            {
                if (contentType == Ppt) return true;
                if (contentType == Pptx) return true;

                return false;
            }
        }


        public static class Image
        {
            public const string Gif = "image/gif";
            public const string Jpeg = "image/jpeg";
            public const string Png = "image/png";
            public const string Tiff = "image/tiff";
            public const string Ico = "image/x-icon";
            public const string Bmp = "image/bmp";
            public const string Svg = "image/svg+xml";
            public const string Webp = "image/webp";

            public static bool IsImage(string contentType)
            {
                if (contentType == Gif) return true;
                if (contentType == Jpeg) return true;
                if (contentType == Png) return true;
                if (contentType == Tiff) return true;
                if (contentType == Ico) return true;
                if (contentType == Bmp) return true;
                if (contentType == Svg) return true;
                if (contentType == Webp) return true;

                return false;
            }
        }
    }
}
