namespace Yangtao.Hosting.Extensions
{
    public static class BufferExtensions
    {
        public static Stream ToStream(this byte[] buffer) => new MemoryStream(buffer);
    }
}
