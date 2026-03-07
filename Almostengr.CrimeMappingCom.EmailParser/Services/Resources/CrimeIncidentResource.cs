namespace Almostengr.CrimeMappingCom.EmailParser.Services.Resources;

public class CrimeIncidentResource
{
    public string Category { get; set; }

    public string Description { get; set; }

    public string CaseNumber { get; set; }

    public string Address { get; set; }

    public DateTime OccurredAt { get; set; }

    public string Agency { get; set; }

    public string SourceZip { get; set; }

    public string MapLink { get; set; }
}
