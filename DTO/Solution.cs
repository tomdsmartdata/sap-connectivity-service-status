namespace SapConnectivitySample.DTO;

public class Solution
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PortfolioCategory { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new();
}