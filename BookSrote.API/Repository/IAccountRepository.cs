using BookSrote.API.Models;
using Microsoft.AspNetCore.Identity;

namespace BookSrote.API.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> SignInAsync(SignInModel signInModel);
    }
}