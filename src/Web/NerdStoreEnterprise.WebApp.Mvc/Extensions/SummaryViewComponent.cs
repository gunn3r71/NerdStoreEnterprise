using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NerdStoreEnterprise.WebApp.Mvc.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
