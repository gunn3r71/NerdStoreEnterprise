using System;
using System.Net;

namespace NerdStoreEnterprise.WebApp.Mvc.Exceptions
{
    public class CustomHttpResponseException : Exception
    {
        public CustomHttpResponseException()
        {
        }

        public CustomHttpResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }


        public CustomHttpResponseException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}