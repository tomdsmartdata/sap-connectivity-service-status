// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;
using SapConnectivitySample.DTO;


Console.WriteLine("SAP Connectivity Service Outage Reporter");
Console.WriteLine($"Time displayed in {TimeZoneInfo.Local}");

var client = new HttpClient();

const string productsJsonUrl = "https://www.sap.com/bin/sapdxc/proxy.ctcsl.products.json";
const string trustCenterUrl = "https://www.sap.com/about/trust-center/cloud-service-status.html";
const string dataCenterName = "USA: N. Virginia";
const string productName = "SAP Connectivity Service";

// Retrieve the list of products and datacenters dimensional data
Console.WriteLine($"Retrieving products from: {productsJsonUrl}");
var productsResponse = await client.GetAsync(productsJsonUrl);
var products = await productsResponse.Content.ReadFromJsonAsync<ProductsResponse>();

// Get the solution id which is used to grab the status json
var solutionId = products?.Solutions.FirstOrDefault(s =>
    s.Products.Any(p => p.Name.Equals(productName, StringComparison.InvariantCultureIgnoreCase)))!.Id;
if (solutionId == null)
{
    Console.WriteLine($"Unable to find status url for product: {productName}");
    return 1;
}
string statusesJsonUrl = $"https://www.sap.com/bin/sapdxc/proxy.ctcsl.{solutionId}.json";

// Retrieve the service events
Console.WriteLine($"Retrieving statuses from: {statusesJsonUrl}");
var statusResponse = await client.GetAsync(statusesJsonUrl);
var statuses = (await statusResponse.Content.ReadFromJsonAsync<IEnumerable<Product>>())?.ToList();

// Get the dimensional data for our data center
var dataCenter =
    products?.DataCenters.FirstOrDefault(d => 
        d.Name.Equals(dataCenterName, StringComparison.InvariantCultureIgnoreCase));

// Get the statuses for the SAP Connectivity Service
var connectivityServiceStatus = statuses?.FirstOrDefault(s => 
        s.Name.Equals(productName, StringComparison.InvariantCultureIgnoreCase));

// Get any events for the service specific to our datacenter
var dataCenterEvents = connectivityServiceStatus?.DataCenters.FirstOrDefault(d =>
    d.Id == dataCenter?.Id)?.Events;

// Provide some buffer space
Console.WriteLine($"{Environment.NewLine}");

// If there were any events, display the Begin -> End times
if (dataCenterEvents != null && dataCenterEvents.Any())
{
    Console.WriteLine($"{productName} in {dataCenterName}({dataCenter?.Id}) had an event: ");
    foreach (var dataCenterEvent in dataCenterEvents)
    {
        Console.WriteLine($"\tFrom {dataCenterEvent.Begin.ToLocalTime()} to {dataCenterEvent.End.ToLocalTime()}");
    }
    Console.WriteLine($"For more details, go to {trustCenterUrl} and search for '{productName}'");
}
else
{
    Console.WriteLine($"There are no events for {productName} in {dataCenterName}");
}
return 0;