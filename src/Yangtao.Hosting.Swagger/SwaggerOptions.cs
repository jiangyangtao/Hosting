using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yangtao.Hosting.Swagger
{
    public class SwaggerOptions
    {
        public string ProjectName { set; get; }

        public string ProjectXmlFileName { set; get; }

        public OpenApiInfo OpenApiInfo { set; get; }

        public SwaggerGenOptions SwaggerGenOptions { set; get; }
    }
}
