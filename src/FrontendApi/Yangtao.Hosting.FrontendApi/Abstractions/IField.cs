using Yangtao.Hosting.FrontendApi.Enums;

namespace Yangtao.Hosting.FrontendApi.Abstractions
{
    internal interface IField
    {
        public string? Name { get; }

        public string? Text { get; }

        public string? Format { get; }

        public FieldType FieldType { get; }

        public bool IsKey { get; }
    }

}
