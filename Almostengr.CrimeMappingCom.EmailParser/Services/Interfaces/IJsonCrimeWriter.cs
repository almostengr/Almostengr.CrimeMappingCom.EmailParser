using Almostengr.CrimeMappingCom.EmailParser.Services.Resources;

namespace Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;

public interface IJsonCrimeWriter
{
    void Write(CrimeIncidentResource incident);
}