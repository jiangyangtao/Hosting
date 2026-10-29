using System.Text;
using Yangtao.Hosting.Extensions;
using Yangtao.Hosting.Logger.Abstractions;

namespace Yangtao.Hosting.Logger
{
    internal class LoggerFormatter
    {
        private readonly Segment[] _textSegments;
        private readonly Segment[] _titleSegments;

        public LoggerFormatter(string textTemplate)
        {
            _textSegments = Parse(textTemplate);
        }

        public LoggerFormatter(string textTemplate, string titleTemplate) : this(textTemplate)
        {
            _titleSegments = Parse(titleTemplate);
        }

        private readonly record struct Segment(string? Literal, string? Field);

        private static Segment[] Parse(string template)
        {
            var segments = new List<Segment>();
            var pos = 0;
            while (pos < template.Length)
            {
                var start = template.IndexOf('{', pos);
                if (start < 0)
                {
                    segments.Add(new Segment(template[pos..], null));
                    break;
                }
                var end = template.IndexOf('}', start + 1);
                if (end < 0) // 未闭合，当字面量
                {
                    segments.Add(new Segment(template[pos..], null));
                    break;
                }
                if (start > pos)
                    segments.Add(new Segment(template[pos..start], null));
                segments.Add(new Segment(null, template[(start + 1)..end]));
                pos = end + 1;
            }
            return segments.ToArray();
        }

        public string TextFormat(ILogInfo info, Exception? exception) => Format(_textSegments, info, exception);

        public string TitleFormat(ILogInfo log) => Format(_titleSegments, log);

        private static string Format(Segment[] segments, ILogInfo info, Exception? exception = null)
        {
            if (segments.IsNullOrEmpty()) return string.Empty;

            var stringBuilder = new StringBuilder(256);
            foreach (var seg in segments)
            {
                if (seg.Literal is not null)
                {
                    stringBuilder.Append(seg.Literal);
                    continue;
                }
                switch (seg.Field)
                {
                    case "Timestamp:HH:mm:ss": stringBuilder.Append(info.Timestamp.ToString("HH:mm:ss")); break;
                    case "Timestamp:yyyy-MM-dd HH:mm:ss.fff": stringBuilder.Append(info.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff")); break;
                    case "Level": stringBuilder.Append(info.Level); break;
                    case "Category": stringBuilder.Append(info.Category); break;
                    case "Message": stringBuilder.Append(info.Message); break; // 正文原样拼接，不会被再解析
                    case "Exception": if (exception != null) stringBuilder.Append(exception); break;
                    case "NewLine": stringBuilder.Append('\n'); break;
                    default: stringBuilder.Append('{').Append(seg.Field).Append('}'); break; // 未知占位符原样保留
                }
            }
            return stringBuilder.ToString();
        }


        //public string TextFormat(ILogInfo log, Exception? exception)
        //{
        //    var stringBuilder = new StringBuilder(_textTemplate
        //            .Replace("{Timestamp:HH:mm:ss}", log.Timestamp.ToString("HH:mm:ss"))
        //            .Replace("{Timestamp:yyyy-MM-dd HH:mm:ss.fff}", log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"))
        //            .Replace("{Level}", log.Level.ToString())
        //            .Replace("{Category}", log.Category)
        //            .Replace("{Message}", log.Message)
        //            .Replace("{NewLing}", Environment.NewLine));

        //    if (exception != null)
        //    {
        //        stringBuilder.Append(Environment.NewLine);
        //        stringBuilder.Append(exception);
        //    }

        //    return stringBuilder.ToString();
        //}

        //public string TitleFormat(ILogInfo log)
        //{
        //    if (_titleTemplate.IsNullOrEmpty()) return string.Empty;

        //    var stringBuilder = new StringBuilder(_textTemplate
        //                .Replace("{Timestamp:HH:mm:ss}", log.Timestamp.ToString("HH:mm:ss"))
        //                .Replace("{Timestamp:yyyy-MM-dd HH:mm:ss.fff}", log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"))
        //                .Replace("{Level}", log.Level.ToString())
        //                .Replace("{Category}", log.Category)
        //                .Replace("{Message}", log.Message)
        //                .Replace("{NewLing}", Environment.NewLine));

        //    return stringBuilder.ToString();
        //}
    }
}
