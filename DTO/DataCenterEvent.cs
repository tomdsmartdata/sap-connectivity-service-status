namespace SapConnectivitySample.DTO;

public class DataCenterEvent
{
    public string? AffectedTenants { get; set; }
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public string? Status { get; set; }
    public string? Type { get; set; }
}