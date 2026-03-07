using Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;
using Almostengr.CrimeMappingCom.EmailParser.Services.Resources;
using Almostengr.CrimeMappingCom.EmailParser.Shared;

namespace Almostengr.CrimeMappingCom.EmailParser.Services;

public class CrimeEmailParser : ICrimeEmailParser
{    
    private readonly CrimeBlockParser _blockParser = new();
    private readonly CrimeMappingSettings _settings;

    public CrimeEmailParser(CrimeMappingSettings settings)
    {
        _settings = settings;   
    }

    public CrimeAlertEmailResource Parse(string emailBody)
    {
        var alert = new CrimeAlertEmailResource();

        var sections = emailBody.Split(_settings.Separator);

        foreach (var section in sections)
        {
            if (IsCrimeBlock(section))
            {
                var incident = _blockParser.Parse(section);
                alert.Incidents.Add(incident);
            }
        }

        return alert;
    }

    private bool IsCrimeBlock(string section)
    {
        return section.Contains("BLOCK");
    }
}
