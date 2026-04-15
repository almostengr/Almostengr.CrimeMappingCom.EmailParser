using Almostengr.Common.DomainServices.Resources;

namespace Almostengr.CrimeMappingCom.EmailParser.Services.Resources;

public sealed class CrimeIncidentResource : Resource
{
    public string Category { get; set; }
    public string Description { get; set; }
    public string CaseNumber { get; set; }
    public string Address { get; set; }
    public DateTime OccurredAt { get; set; }
    public string Agency { get; set; }
}