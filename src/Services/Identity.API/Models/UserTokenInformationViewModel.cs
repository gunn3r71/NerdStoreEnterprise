using System.Collections.Generic;

namespace NerdStoreEnterprise.Services.Identity.API.Models
{
    public class UserTokenInformationViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaimViewModel> Claims { get; set; }
    }
}