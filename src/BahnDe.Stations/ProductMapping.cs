namespace BahnDe.Stations;

internal class ProductMapping
{
    private static readonly Dictionary<string, Product> TransportCategories =
    new()
    {
        {"ICE", Product.HighSpeedTrain},
        {"EC_IC", Product.FastTrain},
        {"IR", Product.UnpartneredFastTrains},
        {"REGIONAL", Product.RegionalTrain},
        {"SBAHN", Product.SuburbanTrain},
        {"BUS", Product.Bus},
        {"SCHIFF", Product.Boat},
        {"UBAHN", Product.UndergroundTrain},
        {"TRAM", Product.Tram},
        {"ANRUFPFLICHTIG", Product.CallService}
    };

    internal static IReadOnlyList<Product> MapProducts(IEnumerable<string> products)
    {
        return products.Select(MapProduct).ToList();
    }

    internal static Product MapProduct(string product)
    {
        if (TransportCategories.TryGetValue(product, out var result))
        {
            return result;
        }

        throw new InvalidOperationException($"Product {product} is not supported");
    }
}