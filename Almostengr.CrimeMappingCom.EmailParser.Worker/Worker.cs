using Almostengr.Common.DomainServices.Results;
using Almostengr.CrimeMappingCom.EmailParser.Domain;
using Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;
using Almostengr.CrimeMappingCom.EmailParser.Services.Resources;
using MimeKit;

namespace Almostengr.CrimeMappingCom.EmailParser.Worker;

public sealed class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ICrimeEmailParser _crimeEmailParser;
    private readonly IImapEmailReader _imapEmailReader;
    private readonly IJsonCrimeWriter _jsonCrimeWriter;
    private readonly IHostApplicationLifetime _lifetime;

    public Worker(
        ILogger<Worker> logger,
        ICrimeEmailParser crimeEmailParser,
        IImapEmailReader imapEmailReader,
        IJsonCrimeWriter jsonCrimeWriter,
        IHostApplicationLifetime lifetime
        )
    {
        _logger = logger;
        _crimeEmailParser = crimeEmailParser;
        _imapEmailReader = imapEmailReader;
        _jsonCrimeWriter = jsonCrimeWriter;
        _lifetime = lifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("CrimeMapping ingestion started");

        List<(MimeMessage, MailKit.UniqueId)> emails = await _imapEmailReader.GetUnreadAsync();
        List<MailKit.UniqueId> processedMessageIds = new();
        foreach (var (email, uid) in emails)
        {
            try
            {
                var alert = _crimeEmailParser.Parse(email.TextBody);
                foreach (var crime in alert.Incidents)
                {
                    // var result = await WriteToDatabaseAsync(crime, stoppingToken);
                    var result = await WriteToFileAsync(crime, stoppingToken);
                    if (result.Failed)
                    {
                        continue;
                    }
                }
                processedMessageIds.Add(uid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                continue;
            }
        }

        await _imapEmailReader.MarkReadAsync(processedMessageIds);

        _logger.LogInformation("CrimeMapping ingestion finished");
        _lifetime.StopApplication();
    }

    private async Task<Result<CrimeIncident>> WriteToDatabaseAsync(CrimeIncidentResource resource, CancellationToken cancellationToken)
    {
        Result<CrimeIncident> crimeResult = CrimeIncident.Create(
            Guid.Empty,
            resource.Category,
            resource.Description,
            resource.CaseNumber,
            resource.Address,
            resource.OccurredAt,
            resource.Agency);

        if (crimeResult.Failed)
        {
            _logger.LogError(string.Join(".", crimeResult.Errors));
        }

        // dbContext.CrimeIncidents.AddAsync(crimeResult);

        return crimeResult;
    }

    private async Task<Result<CrimeIncident>> WriteToFileAsync(CrimeIncidentResource resource, CancellationToken cancellationToken)
    {
        _jsonCrimeWriter.Write(resource);
        return Result<CrimeIncident>.Success(null);
    }
}
