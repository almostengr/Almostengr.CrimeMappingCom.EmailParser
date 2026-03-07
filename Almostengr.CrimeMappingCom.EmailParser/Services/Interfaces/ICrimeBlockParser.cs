using Almostengr.CrimeMappingCom.EmailParser.Services.Resources;

namespace Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;

public interface ICrimeBlockParser
{
    CrimeIncidentResource Parse(string block);
}