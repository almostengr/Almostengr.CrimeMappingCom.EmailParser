using MimeKit;

namespace Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;

public interface IImapEmailReader
{
    Task<List<MimeMessage>> GetUnreadAsync();
}