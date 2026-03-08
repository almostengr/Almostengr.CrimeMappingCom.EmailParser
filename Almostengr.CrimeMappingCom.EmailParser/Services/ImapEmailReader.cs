using Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;
using Almostengr.CrimeMappingCom.EmailParser.Shared;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Almostengr.CrimeMappingCom.EmailParser.Services;

public sealed class ImapEmailReader : IImapEmailReader
{
    private readonly IImapClient _imapClient;
    private readonly CrimeMappingSettings _settings;

    public ImapEmailReader(
        IImapClient imapClient,
        IOptions<CrimeMappingSettings> options
    )
    {
        _imapClient = imapClient;
        _settings = options.Value;
    }

    public async Task<List<(MimeMessage, UniqueId)>> GetUnreadAsync()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(_settings.Hostname, nameof(_settings.Hostname));
        ArgumentException.ThrowIfNullOrWhiteSpace(_settings.Username, nameof(_settings.Username));
        ArgumentException.ThrowIfNullOrWhiteSpace(_settings.Password, nameof(_settings.Password));

        await ConnectClientAsync();

        var inbox = _imapClient.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadWrite);

        var results = new List<(MimeMessage, UniqueId)>();

        foreach (UniqueId uid in await inbox.SearchAsync(SearchQuery.NotSeen))
        {
            var message = await inbox.GetMessageAsync(uid);
            if (message.Subject.Contains("crimemapping.com", StringComparison.CurrentCultureIgnoreCase))
            {
                results.Add((message, uid));
            }
        }

        return results;
    }

    public async Task MarkReadAsync(List<UniqueId> uniqueIds)
    {
        if (uniqueIds == null || !uniqueIds.Any())
        {
            return;
        }

        await ConnectClientAsync();

        IMailFolder inbox = _imapClient.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadWrite);

        await inbox.AddFlagsAsync(uniqueIds, MessageFlags.Seen, true);

        await inbox.CloseAsync();
    }

    private async Task ConnectClientAsync()
    {
        if (!_imapClient.IsConnected)
        {
            await _imapClient.ConnectAsync(_settings.Hostname, _settings.PortNumber, true);
            await _imapClient.AuthenticateAsync(_settings.Username, _settings.Password);
        }
    }
}
