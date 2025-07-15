namespace Yangtao.Hosting.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ToBuffer(this Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
    }
}
