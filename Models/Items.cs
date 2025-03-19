using System.Text.Json.Serialization;

namespace InverntorySys.Models;

public class Items
{
    public int ItemId { get; set; }
    public string Item { get; set; } = string.Empty;
    public string RackNo { get; set; } = string.Empty;
    public string ShelfNo { get; set; } = string.Empty;
    public string BinNo { get; set; } = string.Empty;
    public string PartNo { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Description { get; set; } = string.Empty;
    public string History { get; set; } = string.Empty;
}
