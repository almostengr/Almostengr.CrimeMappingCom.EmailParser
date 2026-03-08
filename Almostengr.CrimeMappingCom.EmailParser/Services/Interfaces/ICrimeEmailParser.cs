using Almostengr.CrimeMappingCom.EmailParser.Services.Resources;

namespace Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;

public interface ICrimeEmailParser
{
    CrimeAlertEmailResource Parse(string emailBody);
}