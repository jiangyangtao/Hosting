using System.Text;
using Yangtao.Hosting.Extensions;

namespace Yangtao.Hosting.Common
{
    public class PasswordHandler : IDisposable
    {
        /// <summary>
        /// 明文密码
        /// </summary>
        private readonly string PlaintextPassword;

        private PasswordHandler(string plaintextPassword)
        {
            PlaintextPassword = plaintextPassword;
        }

        private PasswordHandler(string plaintextPassword, string salt) : this(plaintextPassword)
        {
            Salt = salt;
        }

        private PasswordHandler(string plaintextPassword, string salt, string encryptedPassword) : this(plaintextPassword, salt)
        {
            EncryptedPassword = encryptedPassword;
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        public string EncryptedPassword { private set; get; }

        /// <summary>
        /// 盐
        /// </summary>
        private string Salt { set; get; }

        /// <summary>
        /// 密码盐
        /// </summary>
        public string PasswordSalt
        {
            get
            {
                if (Salt.IsNullOrEmpty()) Salt = Guid.NewGuid().ToString("N");

                return Salt;
            }
        }

        public static PasswordHandler CreateHandler(string plaintextPassword) => new(plaintextPassword);

        public static PasswordHandler CreateHandler(string plaintextPassword, string salt) => new(plaintextPassword, salt);

        public static PasswordHandler CreateHandler(string plaintextPassword, string salt, string encryptedPassword) => new(plaintextPassword, salt, encryptedPassword);

        public string Encrypt() => BuildSaltPassword(PlaintextPassword);

        /// <summary>
        /// 加密新密码
        /// </summary>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public string EncryptNewPassword(string newPassword) => BuildSaltPassword(newPassword);

        /// <summary>
        /// 密码比对
        /// </summary>
        /// <returns>True 则比对成功，false 则比对失败</returns>
        public bool PasswordComparison()
        {
            var slatPassword = BuildSaltPassword(PlaintextPassword);
            return slatPassword == EncryptedPassword;
        }

        private string BuildSaltPassword(string password)
        {
            var strBuilder = new StringBuilder(PasswordSalt[..16]);
            strBuilder.Append(password);
            strBuilder.Append(PasswordSalt[16..]);

            var slatPassword = strBuilder.ToString();
            var slatPasswordBytes = Encoding.UTF8.GetBytes(slatPassword);
            var sha512 = System.Security.Cryptography.SHA512.Create();
            var hashBytes = sha512.ComputeHash(slatPasswordBytes);

            return Convert.ToBase64String(hashBytes);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
