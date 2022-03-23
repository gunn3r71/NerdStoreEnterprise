using System;
using System.Net;
using System.Net.Http;
using NerdStoreEnterprise.WebApp.Mvc.Exceptions;

namespace NerdStoreEnterprise.WebApp.Mvc.Services
{
    public abstract class Service
    {
        protected bool IsSuccess(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.NotFound:
                case HttpStatusCode.InternalServerError:
                    throw new CustomHttpResponseException(response.StatusCode);

                case HttpStatusCode.BadRequest:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}
