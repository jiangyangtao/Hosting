using Microsoft.AspNetCore.StaticFiles;

namespace Yangtao.Hosting.Common
{
    public class ContentTypeProvider
    {
        private static FileExtensionContentTypeProvider? _fileExtensionContentTypeProvider { set; get; } = null;

        public static FileExtensionContentTypeProvider FileExtensionContentTypeProvider
        {
            get
            {
                _fileExtensionContentTypeProvider ??= new();

                return _fileExtensionContentTypeProvider;
            }
        }


        public static IDictionary<string, string> Mappings => FileExtensionContentTypeProvider.Mappings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subpath">extension</param>
        /// <returns>ContentType or empty</returns>
        public static string Get(string extension)
        {
            var existContntType = FileExtensionContentTypeProvider.TryGetContentType(extension, out string? contentType);
            if (existContntType == false) return string.Empty;
            if (string.IsNullOrEmpty(contentType)) return string.Empty;

            return contentType;
        }
    }
}
