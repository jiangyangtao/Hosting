using RSAExtensions;
using System.Security.Cryptography;
using Test.SignatureValidation.Server.GrpcServices;
using Yangtao.Hosting.SignatureValidation.Core.Enums;
using Yangtao.Hosting.SignatureValidation.Server;

namespace Test.SignatureValidation.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddHcmaShaSignatureValidation(options =>
            //{
            //    options.HmacShaAlgorithmType = HashAlgorithmType.HmacSha256;
            //    options.HmacShaSignatureFormatType = HmacShaSignatureFormatType.Base64;
            //    options.SecretKey = "3d37adf4f8a593811d8035c9a355bb25";
            //});

            //builder.Services.AddRsaSignatureValidation(options =>
            //{
            //    options.HashAlgorithmType = HashAlgorithmType.SHA256;
            //    options.RSAKeyType = RSAKeyType.Pkcs8;
            //    options.RSASignaturePaddingMode = RSASignaturePaddingMode.Pkcs1;
            //    options.PrivateKey = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCuVES3RLyJa572ZWPCNsAQsvO0SHfPq9svIYvaF1Vp2ascTW/f7vExS8ufYdj7ATaEk182/oCLlK4RlaVWD3ifS0SopwGze02ySohylTrzia3gg2C0uX9zTigSSETMV9yO/pBUy6U8sAoTLXdGeWydbChv6yMSOicf4dwswHAHxdb0TKHAW4DJKOkCvodVDyUwlV5XUvjlbRw74/ERoW+8Cg7ARGmywFXcpjd0yLZRJbq3xZy5jSpwvDjB2/6oI/0/SnMxsX3xolBSRmgvzQZa2r/ofIP33hlEP2j93ULdGqFA+zeYwHISqK7ZfskOL3b8amEpn25BgwfHtqfw+CB9AgMBAAECggEAYcAGt+zm+vdJr0ey5Ffm4nY1iMWJyPzIbmkVDjk/P5c6Byi0rpBA1i92AezPyg8oDrbEEQYr9hhscpfCmbsbG9cMrLTYk1d0faClWfqrj5uHz/ha5XuOJ42PkpDLYvlxRw0eyS1XfewH+jDoTLARY0QeeIMUq0fIYGw5FS7iorgzi6oaCe+LmVMBIPmmxYrryGgBTJkAo0YstGedED7Ly/ZemaBld0Caw/DH+DJUmAu27J8NmUVJ+GXFevgi4ks2VI9henoTDq8i5d/jGB7ewXooKOaXo/RSFECcY/n3i9kj9LQuhgnRfcEs+5ddID7wp4V1o407l8aSIQlG06UWUQKBgQDNldXmM+r7Y33U+prCbIbajfyxKvC9lWE3frTI1kJfSAYX6JAGpaspYkjAa4D4JWNa3jhxETpjgPkXibrp1S0kgSq5pVSW1DlU4bwPE7gUyqArUFB808i1qnKZKY2T/twxiOZh5kvMAdHgUm+k0+0+3A0h+XYnwEwUTR8RmwvVqwKBgQDZFD8hD+z1Ia+v0+aFzHXHlqvkSXyx/WC+djVqqgasuzl51pGzY8H7NUMMlVgJ5IhzBNcxkA9uetcuCD6+wjPW/R2mB48pUWRKLixhBf+ph1x7Ny5WQtjoJx1sESAJVauke8Bvht8nXU+rBYm+mt4l6cLWEpNy4yyu7r1Rav1qdwKBgEFGxyEN8RmbEJQ0cjkzjmoM1WRHtyLrMHAXej2e0npAezbSMBD8P2mnfGQkflHMRUzP7GnyR3Davby8ja27c1b48GD73uz5O6748eZzo2puoAbAvJ21/S+5jCOXrw2DydSsnEIPhpejKEmqwyo55EWmR0E3XU3n4AA+MJQ9f7d7AoGAWJ/PdiwOTbXHCD22q4FhxG5bFwR5iCEt0hRoknd/6h01xew358kedPS1vvlpzAAlRPAA+xcZdb1pBD4Rf8fBalBEPnhvlirIixB2xdWxHwFIXHWW0VjRPVZayLflIGp9fNWZJu8lQ+jwkZ3dMpf/gfMBvGwxDoJ4x6JoYEkR1NkCgYEArmpIhjcYyukSC3RnryR1TuixcDeTZVIdY8OZnfYxJhMHY4VTsNCutYBY3mdZ0cedJWmaqj5+SQRYhN98zRH+Qzz82xu1EhQDDlkIzimYXUUS02ebv6wPdAufFUoXM1L2k0xT3muD/Rwdjc+/SvA/+zL0TJSb3jJ+xmiiDI5zYR0=";
            //});
            builder.Services.AddEncryptionValidation(options =>
            {
                options.RSAEncryptionPaddingType = RSAEncryptionPaddingType.Pkcs1;
                options.RSAKeyType = RSAKeyType.Pkcs8;
                options.PrivateKey = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCuVES3RLyJa572ZWPCNsAQsvO0SHfPq9svIYvaF1Vp2ascTW/f7vExS8ufYdj7ATaEk182/oCLlK4RlaVWD3ifS0SopwGze02ySohylTrzia3gg2C0uX9zTigSSETMV9yO/pBUy6U8sAoTLXdGeWydbChv6yMSOicf4dwswHAHxdb0TKHAW4DJKOkCvodVDyUwlV5XUvjlbRw74/ERoW+8Cg7ARGmywFXcpjd0yLZRJbq3xZy5jSpwvDjB2/6oI/0/SnMxsX3xolBSRmgvzQZa2r/ofIP33hlEP2j93ULdGqFA+zeYwHISqK7ZfskOL3b8amEpn25BgwfHtqfw+CB9AgMBAAECggEAYcAGt+zm+vdJr0ey5Ffm4nY1iMWJyPzIbmkVDjk/P5c6Byi0rpBA1i92AezPyg8oDrbEEQYr9hhscpfCmbsbG9cMrLTYk1d0faClWfqrj5uHz/ha5XuOJ42PkpDLYvlxRw0eyS1XfewH+jDoTLARY0QeeIMUq0fIYGw5FS7iorgzi6oaCe+LmVMBIPmmxYrryGgBTJkAo0YstGedED7Ly/ZemaBld0Caw/DH+DJUmAu27J8NmUVJ+GXFevgi4ks2VI9henoTDq8i5d/jGB7ewXooKOaXo/RSFECcY/n3i9kj9LQuhgnRfcEs+5ddID7wp4V1o407l8aSIQlG06UWUQKBgQDNldXmM+r7Y33U+prCbIbajfyxKvC9lWE3frTI1kJfSAYX6JAGpaspYkjAa4D4JWNa3jhxETpjgPkXibrp1S0kgSq5pVSW1DlU4bwPE7gUyqArUFB808i1qnKZKY2T/twxiOZh5kvMAdHgUm+k0+0+3A0h+XYnwEwUTR8RmwvVqwKBgQDZFD8hD+z1Ia+v0+aFzHXHlqvkSXyx/WC+djVqqgasuzl51pGzY8H7NUMMlVgJ5IhzBNcxkA9uetcuCD6+wjPW/R2mB48pUWRKLixhBf+ph1x7Ny5WQtjoJx1sESAJVauke8Bvht8nXU+rBYm+mt4l6cLWEpNy4yyu7r1Rav1qdwKBgEFGxyEN8RmbEJQ0cjkzjmoM1WRHtyLrMHAXej2e0npAezbSMBD8P2mnfGQkflHMRUzP7GnyR3Davby8ja27c1b48GD73uz5O6748eZzo2puoAbAvJ21/S+5jCOXrw2DydSsnEIPhpejKEmqwyo55EWmR0E3XU3n4AA+MJQ9f7d7AoGAWJ/PdiwOTbXHCD22q4FhxG5bFwR5iCEt0hRoknd/6h01xew358kedPS1vvlpzAAlRPAA+xcZdb1pBD4Rf8fBalBEPnhvlirIixB2xdWxHwFIXHWW0VjRPVZayLflIGp9fNWZJu8lQ+jwkZ3dMpf/gfMBvGwxDoJ4x6JoYEkR1NkCgYEArmpIhjcYyukSC3RnryR1TuixcDeTZVIdY8OZnfYxJhMHY4VTsNCutYBY3mdZ0cedJWmaqj5+SQRYhN98zRH+Qzz82xu1EhQDDlkIzimYXUUS02ebv6wPdAufFUoXM1L2k0xT3muD/Rwdjc+/SvA/+zL0TJSb3jJ+xmiiDI5zYR0=";

            });
            builder.Services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.AddServerSignatureValidationGrpcInterceptor();
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseServerSignatureValidation();
            app.MapControllers();
            app.MapGrpcService<UserGrpcService>();
            app.Run();
        }
    }
}