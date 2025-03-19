using InverntorySys.Models;
using System.Text.Json;

namespace InverntorySys.Data;

public class ItemService
{
    private readonly string _filePath = "Data/db.json";

    public async Task<List<Items>> GetAllAsync()
    {
        if (!File.Exists(_filePath)) return new List<Items>();
        var json = await File.ReadAllTextAsync(_filePath);
        return JsonSerializer.Deserialize<List<Items>>(json) ?? new List<Items>();
    }

    public async Task AddAsync(Items item, string restockedBy = "")
    {
        var items = await GetAllAsync();
        item.ItemId = items.Count > 0 ? items.Max(i => i.ItemId) + 1 : 1;
        item.History = "Restocked by " + restockedBy + " on " + DateTime.Now.Date.ToString("yyyy-MM-dd");
        items.Add(item);
        var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task<List<Items>> SearchAsync(string query)
    {
        var items = await GetAllAsync();
        return items.Where(s =>
            s.Item.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            s.RackNo.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            s.ShelfNo.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            s.BinNo.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            s.PartNo.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            s.Make.Contains(query, StringComparison.OrdinalIgnoreCase) 
        ).ToList();
    }

    public async Task<bool> UpdateQuantityAsync(int itemId, int quantity, string lastIssuedBy = "")
    {
        var items = await GetAllAsync();
        var item = items.FirstOrDefault(i => i.ItemId == itemId);

        if (item != null)
        {
            item.Quantity = quantity;
            item.History = "Last issued by "+ lastIssuedBy +" on " + DateTime.Now.Date.ToString("yyyy-MM-dd");

            // Save updated items list back to the file
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
            return true; // Success
        }

        return false; // Item not found
    }
}
