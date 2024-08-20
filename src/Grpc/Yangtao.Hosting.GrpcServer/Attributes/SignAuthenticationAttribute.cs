namespace Yangtao.Hosting.GrpcServer.Attributes
{
    /// <summary>
    /// 签名验证 <see cref="Attribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SignAuthenticationAttribute : Attribute
    {
        /// <summary>
        /// 构建 <see cref="SignAuthenticationAttribute"/>
        /// </summary>
        public SignAuthenticationAttribute()
        {
        }

    }
}
