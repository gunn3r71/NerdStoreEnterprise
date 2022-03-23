using System.Threading.Tasks;
using NerdStoreEnterprise.WebApp.Mvc.Models.AuthenticationResponse;
using NerdStoreEnterprise.WebApp.Mvc.Models.Users;

namespace NerdStoreEnterprise.WebApp.Mvc.Services
{
    public interface IAuthenticationService
    {
        Task<TokenViewModel> Login(UserLoginViewModel login);

        Task<TokenViewModel> Register(UserRegisterViewModel register);
    }
}