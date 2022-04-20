namespace SapConnectivitySample.DTO;

public class Product
{
    public string Name { get; set; } = string.Empty;
    public List<ProductDataCenter> DataCenters { get; set; } = new();
}