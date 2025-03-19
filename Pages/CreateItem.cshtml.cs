using InverntorySys.Data;
using InverntorySys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InverntorySys.Pages
{
    public class CreateItemModel : PageModel
    {
        private readonly ItemService _itemService;

        [BindProperty]
        public Items items { get; set; } = new();
        public CreateItemModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("User") == null)
            {
                return RedirectToPage("/Index"); // Redirect to login if not authenticated
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = HttpContext.Session.GetString("User");
            await _itemService.AddAsync(items, user);
            return RedirectToPage("Privacy");
        }
    }
}
