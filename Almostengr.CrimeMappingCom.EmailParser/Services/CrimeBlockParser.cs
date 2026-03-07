using Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;
using Almostengr.CrimeMappingCom.EmailParser.Services.Resources;

namespace Almostengr.CrimeMappingCom.EmailParser.Services;

public class CrimeBlockParser : ICrimeBlockParser
{
    public CrimeIncidentResource Parse(string block)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(block);

        var lines = block
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray();

        if (lines.Length != 6)
        {
            throw new FormatException("Unexpected crime block format.");
        }

        return new CrimeIncidentResource
        {
            Category = lines[0],
            Description = lines[1],
            CaseNumber = lines[2],
            Address = lines[3],
            OccurredAt = DateTime.Parse(lines[4]),
            Agency = lines[5]
        };
    }
}
