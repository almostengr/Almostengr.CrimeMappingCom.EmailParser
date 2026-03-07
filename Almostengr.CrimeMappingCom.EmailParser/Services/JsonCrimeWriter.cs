using System.Text.Json;
using Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;
using Almostengr.CrimeMappingCom.EmailParser.Services.Resources;
using Almostengr.CrimeMappingCom.EmailParser.Shared;
using Microsoft.Extensions.Options;

namespace Almostengr.CrimeMappingCom.EmailParser.Services;

public class JsonCrimeWriter : IJsonCrimeWriter
{
    private readonly CrimeMappingSettings _settings;

    public JsonCrimeWriter(
        IOptions<CrimeMappingSettings> options
    )
    {
        _settings = options.Value;
    }

    public void Write(CrimeIncidentResource incident)
    {
        ArgumentNullException.ThrowIfNull(incident);
        ArgumentException.ThrowIfNullOrWhiteSpace(_settings.OutputDirectory, nameof(_settings.OutputDirectory));

        var path = Path.Combine(_settings.OutputDirectory, $"{incident.OccurredAt:yyyymmddhhMM}.json");

        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

        var json = JsonSerializer.Serialize(
            incident,
            new JsonSerializerOptions { WriteIndented = true });

        File.AppendAllText(path, json + Environment.NewLine);
    }
}
