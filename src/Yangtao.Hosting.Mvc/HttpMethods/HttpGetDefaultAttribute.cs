﻿using Microsoft.AspNetCore.Mvc;

namespace Yangtao.Hosting.Mvc.HttpMethods
{
    public class HttpGetDefaultAttribute : HttpGetAttribute
    {
        public HttpGetDefaultAttribute() : base("/api/v{v:ApiVersion}/[controller]/") { }

        public HttpGetDefaultAttribute(string template) : base("/api/v{v:ApiVersion}/[controller]/" + template)
        {
        }
    }
}
