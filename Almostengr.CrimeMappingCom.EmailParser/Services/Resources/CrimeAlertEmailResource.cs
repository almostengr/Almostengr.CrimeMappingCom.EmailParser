namespace Almostengr.CrimeMappingCom.EmailParser.Services.Resources;

public sealed class CrimeAlertEmailResource
{
    public string ZipCode { get; set; }
    public DateTime Published { get; set; }
    public string MapLink { get; set; }
    public List<CrimeIncidentResource> Incidents { get; set; } = new();
}