using System.Collections.Generic;
using System.Linq;

public class Items
{
    internal object thimbnail;

    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Barcode { get; set; }
    public string BarcodeImage { get; internal set; }
}

public static class ItemStorage
{
    private static List<Items> items = new List<Items>();

    public static void AddItem(Items item)
    {
        items.Add(item);
    }

    public static List<Items> GetAllItems()
    {
        return items;
    }

    public static Items GetItemByBarcode(string barcode)
    {
        return items.FirstOrDefault(item => item.Barcode == barcode);
    }
}