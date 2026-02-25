using System.Security.Claims;
using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto dto);
        Task<string> LoginAsync(LoginDto data);
        Task<ProfileDto> GetProfileAsync(ClaimsPrincipal userPrincipal);
    }
}
