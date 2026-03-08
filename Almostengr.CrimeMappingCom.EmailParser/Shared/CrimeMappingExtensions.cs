using Almostengr.CrimeMappingCom.EmailParser.Services;
using Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;
using MailKit.Net.Imap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Almostengr.CrimeMappingCom.EmailParser.Shared;

public static class CrimeMappingExtensions
{
    public static void AddCrimeMappingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CrimeMappingSettings>(configuration.GetSection(nameof(CrimeMappingSettings)));

        services.AddTransient<IImapClient, ImapClient>();
        services.AddTransient<ICrimeBlockParser, CrimeBlockParser>();
        services.AddTransient<ICrimeEmailParser, CrimeEmailParser>();
        services.AddTransient<IImapEmailReader, ImapEmailReader>();
        services.AddTransient<IJsonCrimeWriter, JsonCrimeWriter>();
    }
}