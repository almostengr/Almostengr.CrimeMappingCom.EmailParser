using MimeKit;

namespace Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;

public interface IImapEmailReader
{
    Task<List<(MimeMessage, MailKit.UniqueId)>> GetUnreadAsync();
    Task MarkReadAsync(List<MailKit.UniqueId> uniqueIds);
}