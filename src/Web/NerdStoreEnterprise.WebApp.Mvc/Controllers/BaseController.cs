using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NerdStoreEnterprise.WebApp.Mvc.Models.Errors;

namespace NerdStoreEnterprise.WebApp.Mvc.Controllers
{
    public abstract class BaseController : Controller
    {
        protected bool HasErrors(ErrorViewModel error)
        {
            if (error is null || !error.Errors.Messages.Any()) return false;

            error.Errors.Messages.ForEach(x => ModelState.AddModelError(string.Empty, x));

            return true;
        }
    }
}
