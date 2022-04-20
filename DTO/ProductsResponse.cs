namespace SapConnectivitySample.DTO;

public class ProductsResponse
{
    private List<DataCenter> _dataCenters = new();

    public List<DataCenter> DataCenters
    {
        get => _dataCenters;
        set => _dataCenters = value;
    }

    public DateTime LastUpdated { get; set; }
    public List<string> PortfolioCategories { get; set; } = new();
    public List<Solution> Solutions { get; set; } = new();
}