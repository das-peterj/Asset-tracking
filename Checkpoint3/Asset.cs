// Declares the list and puts in sample data for easier testing




class Asset
{
    public string Type { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Office { get; set; }

    public int PriceInUSD { get; set; }
    public DateTime PurchaseDate { get; set; }

    public Asset(string type, string brand, string model, string office, DateTime purchaseDate, int priceInUSD)
    {
        Type = type;
        Brand = brand;
        Model = model;
        Office = office;
        PurchaseDate = purchaseDate;
        PriceInUSD = priceInUSD;
    }
}