using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NerdStoreEnterprise.WebApp.Mvc.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult<IViewComponentResult>(View());
        }
    }
}