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

        const int LINES_EXPECTED = 6;
        const int LAST_INCIDENT_LINES_EXPECTED = 8;

        if (lines.Length != LINES_EXPECTED &&
            (lines.Length != LAST_INCIDENT_LINES_EXPECTED && !lines.Contains("Sex Offender State Website")))
        {
            throw new FormatException($"Unexpected crime block format. Found: {lines.Length}");
        }

        var occurredAt = lines[4].Replace("@", "").Trim();

        return new CrimeIncidentResource
        {
            Category = lines[0],
            Description = lines[1],
            CaseNumber = lines[2],
            Address = lines[3],
            OccurredAt = DateTime.Parse(occurredAt),
            Agency = lines[5]
        };
    }
}
