using System.Threading.Channels;
using Yangtao.Hosting.Logger;
using Yangtao.Hosting.Logger.Options;


namespace Test.CustomLogger.Application
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
            builder.Services.AddCustomLogger(options =>
            {
                options.EnableChannelLogger = true;
                options.RealtimeLoggerOptions = new RealtimeLoggerOptions
                {
                    BufferCapacity = 5000,
                    CentralChannelFullMode = BoundedChannelFullMode.DropOldest,
                    CentralChannelCapacity = 100000,
                    ChannelFullMode = BoundedChannelFullMode.DropOldest,
                    MinLevel = LogLevel.Information,
                    LoggerResponseType = LoggerResponseType.Text,
                    Format = "{Level}: {Timestamp:yyyy-MM-dd HH:mm:ss.fff} {NewLine} {Category} {NewLine} {Message} {NewLine}"
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
