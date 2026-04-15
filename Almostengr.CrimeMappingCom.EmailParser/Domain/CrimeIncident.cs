using System.ComponentModel.DataAnnotations;
using Almostengr.Common.Domain;
using Almostengr.Common.DomainServices.Results;

namespace Almostengr.CrimeMappingCom.EmailParser.Domain;

public class CrimeIncident : Entity
{
    private CrimeIncident(
        Guid publicId,
        string category,
        string description,
        string caseNumber,
        string address,
        DateTime occurredAt,
        string agency,
        string createdBy = "SYSTEM"
    ) : base(publicId, createdBy)
    {
        Category = category;
        Description = description;
        CaseNumber = caseNumber;
        Address = address;
        OccurredAt = occurredAt;
        Agency = agency;
        CreatedDate = DateTime.UtcNow;
    }

    [StringLength(150)]
    public string Category { get; private set; }

    public string Description { get; private set; }

    [StringLength(150)]
    public string CaseNumber { get; private set; }

    [StringLength(150)]
    public string Address { get; private set; }

    public DateTime OccurredAt { get; private set; }

    [StringLength(150)]
    public string Agency { get; private set; }

    public static Result<CrimeIncident> Create(
        Guid publicId,
        string category,
        string description,
        string caseNumber,
        string address,
        DateTime occurredAt,
        string agency)
    {
        Result<CrimeIncident> result = Result<CrimeIncident>.Create();
        if (occurredAt > DateTime.UtcNow)
        {
            result.AddError("Time occurred cannot be in the future.");
        }

        if (result.Succeeded)
        {
            CrimeIncident crimeIncident = new(publicId, category, description, caseNumber, address, occurredAt, agency);
            result.SetValue(crimeIncident);
        }
        return result;
    }
}