namespace SapConnectivitySample.DTO;

public class ProductDataCenter
{
    public string? Id { get; set; }
    public List<DataCenterEvent> Events { get; set; } = new();
    public bool IsProspective { get; set; }
}