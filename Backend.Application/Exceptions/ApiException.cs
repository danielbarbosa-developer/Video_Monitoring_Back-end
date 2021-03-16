using System;

namespace Backend.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException()
        {
        }
        public ApiException(string message) : base(message)
        {
        }
        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}