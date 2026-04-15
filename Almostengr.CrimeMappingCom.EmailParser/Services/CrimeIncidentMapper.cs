using System.Reflection.Metadata.Ecma335;
using Almostengr.Common.DomainServices.Interfaces;
using Almostengr.CrimeMappingCom.EmailParser.Domain;
using Almostengr.CrimeMappingCom.EmailParser.Services.Resources;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Almostengr.CrimeMappingCom.EmailParser.Services;

public sealed class CrimeIncidentMapper : IMapper<CrimeIncident, CrimeIncidentResource>
{
    public CrimeIncidentResource ToResource(CrimeIncident entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new CrimeIncidentResource
        {
            PublicId = entity.PublicId,
            Category = entity.Category,
            Description = entity.Description,
            CaseNumber = entity.CaseNumber,
            Address = entity.Address,
            OccurredAt = entity.OccurredAt,
            Agency = entity.Agency,
            CreatedDate = entity.CreatedDate,
        };
    }
}