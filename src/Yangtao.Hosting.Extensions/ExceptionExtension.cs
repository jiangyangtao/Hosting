using System;

namespace Papaya.Hosting.Extensions
{
    public static class ExceptionExtension
    {
        public static Exception GetInnerException(this Exception exception)
        {
            if (exception.InnerException != null) return exception.InnerException.GetInnerException();

            return exception;
        }
    }
}
