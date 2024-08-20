namespace Yangtao.Hosting.GrpcCore.Abstractions
{
    /// <summary>
    /// 签名验证提供者
    /// </summary>
    public interface ISignAuthenticationProvider
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value">明文</param>
        /// <returns>密文</returns>
        string Encrypt(string value);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value">密文</param>
        /// <returns>明文</returns>
        string Decrypt(string value);
    }
}
