// Declares the list and puts in sample data for easier testing
List<Asset> assets = new List<Asset>()
            {
                new Asset("Phone", "iPhone", "8", "Spain", Convert.ToDateTime("2019-11-05"), 970),
                new Asset("Computer", "HP", "Elitebook", "Spain", Convert.ToDateTime("2022-05-01"), 1423),
                new Asset("Phone", "iPhone", "11", "Spain", Convert.ToDateTime("2022-04-25"), 990),
                new Asset("Phone", "iPhone", "X", "Sweden", Convert.ToDateTime("2019-08-05"), 1245),
                new Asset("Phone", "Motorola", "Razr", "Sweden", Convert.ToDateTime("2019-09-06"), 970),
                new Asset("Computer", "HP", "Elitebook", "Sweden", Convert.ToDateTime("2019-10-07"), 588),
                new Asset("Computer", "Asus", "W234", "USA", Convert.ToDateTime("2019-07-21"), 1200),
                new Asset("Computer", "Lenovo", "Yoga 730", "USA", Convert.ToDateTime("2019-09-28"), 835),
                new Asset("Computer", "Lenovo", "Yoga 530", "USA", Convert.ToDateTime("2019-11-21"), 1030),
                new Asset("Phone", "OnePlus", "8 Pro", "Sweden", Convert.ToDateTime("2020-05-11"), 999),
                new Asset("Phone", "Xiamio", "8TZ Pro", "USAn", Convert.ToDateTime("2020-04-19"), 989),
                new Asset("Computer", "Asus", "Magni", "Sweden", Convert.ToDateTime("2020-06-22"), 1999)
            };

do
{
    bool quitProgram = false;
    do
    {
        string type = "";
        string brand = "";
        string model = "";
        string office = "";
        int priceInUSD = 0;
        DateTime purchaseDate = DateTime.Now;
        Console.SetWindowSize(175, 40);

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Write what type of asset it is. | Type \"S\" to show the list\" | Type \"Q\" to quit the input screen");
        string input = Console.ReadLine();

        if (input.ToLower().Equals("s"))
        {
            Console.Clear();
            break;
        }
        else if (input.ToLower().Equals("q"))
        {
            quitProgram = true;
            Console.Clear();
            break;
        }
        else
        {
            type = input;
        }

        Console.Write("Write the brand name: ");
        brand = Console.ReadLine();

        Console.Write("Write the model name: ");
        model = Console.ReadLine();

        Console.Write("Write where the office is located(Sweden/Spain/USA): ");
        office = Console.ReadLine();

        Console.Write("Write the cost of the asset in USD: ");
        priceInUSD = Int32.Parse(Console.ReadLine());

        do
        {
            Console.Write("Write the purchase date in the following format yyyy-MM-dd: ");
            string line = Console.ReadLine();

            // Checks if user's input of purchase date is correct. If not, prompts user to retry again.
            DateTime dt;
            if (DateTime.TryParseExact(line, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                purchaseDate = dt;
                break;
            }
            else
            {
                Console.WriteLine("Invalid date, please retry");
            }
        } while (true);

        Asset asset = new Asset(type, brand, model, office, purchaseDate, priceInUSD);
        assets.Add(asset);

        Console.WriteLine("Asset successfully added!");
        Console.WriteLine();
    } while (true);

    // Sorts list by Office and then by sorts it by Purchase date
    List<Asset> sortedByType = assets.OrderBy(o => o.Office).ThenBy(o => o.PurchaseDate).ToList();
    DateTime today = DateTime.Today;

    Console.WriteLine("Type".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Office".PadRight(20) + "Purchase Date".PadRight(20) + "Price in USD".PadRight(20) + "Currency".PadRight(20) + "Local Price");
    Console.WriteLine("---".PadRight(20) + "---".PadRight(20) + "---".PadRight(20) + "---".PadRight(20) + "------".PadRight(20) + "----".PadRight(20) + "----".PadRight(20) + "----");
    foreach (Asset asset in sortedByType)
    {

        // 3 years = 1095 days
        // items *RED* if purchase date is less than 3 months away from 3 years
        // Items *Yellow* if date less than 6 months away from 3 year
        // 1095 days - 90(3 months) = 1005 days
        // 1095 days - 180(6 months = 915 days

        DateTime assetPurchaseDate = asset.PurchaseDate;
        // Gets how many days between the purchase date of the asset and todays date
        TimeSpan isAssetExpiring = today - assetPurchaseDate;
        int totalDays = (int)isAssetExpiring.TotalDays;

        double localPrice = calculateLocalPrice(asset.Office, asset.PriceInUSD);
        string localCurrency = checkLocalCurrency(asset.Office);

        // If totaldays exceeds 1005 days, that means the asset is less than 3 months away from reaching 3 years or already has exceeded the 3 years mark.
        if (totalDays > 1005)
        {
            Console.Write(asset.Type.PadRight(20) + asset.Brand.PadRight(20) + asset.Model.PadRight(20) + asset.Office.PadRight(20));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(asset.PurchaseDate.ToShortDateString().PadRight(20));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(asset.PriceInUSD.ToString().PadRight(20) + localCurrency.PadRight(20) + localPrice);
        }
        // If totaldays exceeds 915 days but is less than 1005 days, that means the asset is between 6 months and 3 months from reaching 3 years old.
        else if (totalDays > 915 && totalDays < 1005)
        {
            Console.Write(asset.Type.PadRight(20) + asset.Brand.PadRight(20) + asset.Model.PadRight(20) + asset.Office.PadRight(20));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(asset.PurchaseDate.ToShortDateString().PadRight(20));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(asset.PriceInUSD.ToString().PadRight(20) + localCurrency.PadRight(20) + localPrice);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(asset.Type.PadRight(20) + asset.Brand.PadRight(20) + asset.Model.PadRight(20) + asset.Office.PadRight(20) + asset.PurchaseDate.ToShortDateString().PadRight(20) + asset.PriceInUSD.ToString().PadRight(20) + localCurrency.PadRight(20) + localPrice);
        }
        Console.WriteLine("");
    }
    // Merely to make console viewing easier.
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("");
    if (quitProgram)
    {
        break;
    }
} while (true);


double calculateLocalPrice(string input, int value)
{
    if (input.ToLower().Equals("sweden"))
    {
        return Math.Round(value * 10.37, 2);
    }
    else if (input.ToLower().Equals("spain"))
    {
        return Math.Round(value * 0.96, 2);
    }
    else
    {
        return value;
    }
}

string checkLocalCurrency(string input)
{
    if (input.ToLower().Equals("sweden"))
    {
        return "SEK";
    }
    else if (input.ToLower().Equals("spain"))
    {
        return "EUR";
    }
    else
    {
        return "USD";
    }
}


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