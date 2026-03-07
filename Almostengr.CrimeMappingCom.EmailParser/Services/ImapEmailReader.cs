using Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;
using Almostengr.CrimeMappingCom.EmailParser.Shared;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Almostengr.CrimeMappingCom.EmailParser.Services;

public class ImapEmailReader : IImapEmailReader
{
    private readonly CrimeMappingSettings _settings;

    public ImapEmailReader(
        IOptions<CrimeMappingSettings> options
    )
    {
        _settings = options.Value;
    }

    public async Task<List<MimeMessage>> GetUnreadAsync()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(_settings.Hostname, nameof(_settings.Hostname)); 
        ArgumentException.ThrowIfNullOrWhiteSpace(_settings.Username, nameof(_settings.Username));
        ArgumentException.ThrowIfNullOrWhiteSpace(_settings.Password, nameof(_settings.Password));
        
        using var client = new ImapClient();

        await client.ConnectAsync(_settings.Hostname, _settings.PortNumber, true);
        await client.AuthenticateAsync(_settings.Username, _settings.Password);

        var inbox = client.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadWrite);

        var results = new List<MimeMessage>();

        foreach (var uid in await inbox.SearchAsync(SearchQuery.NotSeen))
        {
            var message = await inbox.GetMessageAsync(uid);
            results.Add(message);

            await inbox.AddFlagsAsync(uid, MessageFlags.Seen, true);
        }

        return results;
    }
}
