namespace Yangtao.Hosting.Extensions
{
    public static class ArrayExtension
    {
        public static Stream ToStream(this byte[] buffer) => new MemoryStream(buffer);

        public static MemoryStream ToMemoryStream(this byte[] buffer) => new(buffer);
    }
}
