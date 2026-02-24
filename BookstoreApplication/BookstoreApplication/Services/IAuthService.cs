using BookstoreApplication.DTOs;

namespace BookstoreApplication.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto dto);
        Task LoginAsync(LoginDto dto);
    }
}
