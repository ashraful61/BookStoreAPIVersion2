
using BookSrote.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookSrote.API.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {

            var user = new ApplicationUser()
            {
                FirstName = signUpModel.Firstname,
                LastName = signUpModel.Lastname,
                Email = signUpModel.Email,
                UserName = signUpModel.Email,
            };

           return await _userManager.CreateAsync(user, signUpModel.Password);
        }

        public async Task<string> SignInAsync(SignInModel signInModel)
        {

            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password,false, false);
            if(!result.Succeeded)
            {
                return null;
            }

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, signInModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secretKey = _configuration["JWT:Secret"];

            var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var token = new JwtSecurityToken(
              issuer: _configuration["JWT:Issuer"],
              audience: _configuration["JWT:Audience"],
              claims: authClaims,
              expires: DateTime.UtcNow.AddDays(7), // Token expiration time
              signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256Signature)
          );

           var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
