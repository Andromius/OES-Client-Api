using Domain.Entities.Users;
using Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using Persistence;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace OESAppApi.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly OESAppApiDbContext _context;
        private readonly ITokenService _tokenService;

        public LoginModel(OESAppApiDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [BindProperty]
        public CredentialsModel Credentials { get; set; } = new();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User? user = await _context.User.SingleOrDefaultAsync(u => u.Username == Credentials.Username);
            if (user is null)
            {
                return Page();
            }

            if (user.Role != UserRole.Admin || !PasswordService.CompareHash(Credentials.Password, user.Password))
                return Page();

            string token = _tokenService.GenerateToken(user.Id, user.Role, DateTime.UtcNow);
            await HttpContext.SignInAsync("Cookies", await _tokenService.GetPrincipal(token), new AuthenticationProperties() { IsPersistent = true });
            return LocalRedirect("~/");
        }
        public class CredentialsModel
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
            public CredentialsModel() { }
        }
    }
}
