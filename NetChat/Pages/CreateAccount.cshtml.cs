using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetChat.Models;

namespace NetChat.Pages
{
    public class CreateAccountModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateAccountModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Handle Login requests
            if (await IsUsernameTaken(Username))
            {
                // Hashing the input password for storage
                var hashedPass = BCrypt.Net.BCrypt.HashPassword(Password);
                if(await CreateAccount(Username, hashedPass))
                {
					var claims = new[]
					{
						new Claim(ClaimTypes.Name, Username)
					};

					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    
					return RedirectToPage("/Index"); // Redirect to the home page after successful login
				}
            }

            // If validation fails, stay on the same page and show errors
            return Page();
        }

        private async Task<bool> IsUsernameTaken(string username)
        {
            var usr = await _context.Users.FirstOrDefaultAsync(u => u.Username == Username);
            if (usr != null)
            {
                ModelState.AddModelError("Username", "An account with that username already exists.");
                return false;
            }
            return true;
        }

        private async Task<bool> CreateAccount(string username, string pass)
        {
            var newUsr = new User
            {
                Username = Username,
                PasswordHash = pass
			};
            _context.Users.Add(newUsr);
            await _context.SaveChangesAsync();
            return true;
		}
    }
}
