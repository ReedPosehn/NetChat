using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetChat.Models;
using System.Threading.Tasks;

namespace NetChat.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
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
            if (ModelState.IsValid)
            {
                // Handle Login requests
                if (await IsValidLogin(Username, Password))
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
                // Login attempt failed - password incorrect
                ModelState.AddModelError(string.Empty, "Invalid login credentials."); // Add error message
            }

            // If validation fails, stay on the same page and show errors
            return Page();
        }

        // Validate that the username and password are correct
        private async Task<bool> IsValidLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            // Find the user by username
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null) {
                ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Add error message
                return false;
            }
            // Verify the password against the stored hash
            return VerifyPasswordHash(password, user.PasswordHash);
        }

        // Using BCrypt for password encryption
        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash); // Use BCrypt for verifying
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login");
        }
    }
}
