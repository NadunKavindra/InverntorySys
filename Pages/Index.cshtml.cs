using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace InverntorySys.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        //private readonly IWebHostEnvironment _env;
        private readonly string _filePath = "Data/users.json";
        public IndexModel()
        {
        }

        public void OnGet()
        {
            HttpContext.Session.Clear(); // Remove session
        }

        public IActionResult OnPost()
        {
            var users = LoadUsersFromJson();

            // Check if credentials match any user in the list
            var validUser = users.FirstOrDefault(u => u.Username == Username && u.Password == Password);


            if (validUser != null) // Replace with real authentication logic
            {
                HttpContext.Session.SetString("User", Username); // Store user session
                return RedirectToPage("Privacy"); // Redirect to another page on success
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page(); // Stay on the page and show error
            }
        }

        private List<User> LoadUsersFromJson()
        {
            //string filePath = Path.Combine(_env.WebRootPath, "users.json"); // Ensure correct path
            if (!System.IO.File.Exists(_filePath))
            {
                return new List<User>(); // Return empty list if file is missing
            }

            string json = System.IO.File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }
    }
}


public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
}