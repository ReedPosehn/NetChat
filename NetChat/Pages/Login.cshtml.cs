using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetChat.Pages
{
    public class LoginModel : PageModel
    {
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
                if (isValidLogin(Username, Password))
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

        // Validate that they have passed something in..
        private bool isValidLogin(string username, string password)
        {
            return !String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password);
        }
    }
}
