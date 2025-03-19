using InverntorySys.Data;
using InverntorySys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InverntorySys.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ItemService _itemService;

        public List<Items> items { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; } = string.Empty;

        public CreateModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task OnGetAsync()
        {
            //if (HttpContext.Session.GetString("User") == null)
            //{
            //    return RedirectToPage("/Index"); // Redirect to login if not authenticated
            //}

            items = string.IsNullOrWhiteSpace(SearchQuery)
                ? await _itemService.GetAllAsync()
                : await _itemService.SearchAsync(SearchQuery);
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id, int quantity)
        {
            var user = HttpContext.Session.GetString("User");

            var result = await _itemService.UpdateQuantityAsync(id, quantity, user);
            if (result)
            {
                TempData["SuccessMessage"] = "Stock updated successfully.";
                if (quantity == 0)
                    TempData["SuccessMessage"] = "Item was Depleted";
                return RedirectToPage(); // Reload the page after update
            }

            TempData["ErrorMessage"] = "Stock update failed !!.";
            return RedirectToPage();
        }


    }
}
