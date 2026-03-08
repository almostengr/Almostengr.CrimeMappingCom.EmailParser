using Almostengr.CrimeMappingCom.EmailParser.Services.Interfaces;

namespace Almostengr.CrimeMappingCom.EmailParser.Worker;

public class Worker : BackgroundService
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
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("CrimeMapping ingestion started");

            var emails = await _imapEmailReader.GetUnreadAsync();
            foreach (var email in emails)
            {
                var alert = _crimeEmailParser.Parse(email.TextBody);
                foreach (var crime in alert.Incidents)
                {
                    _jsonCrimeWriter.Write(crime);
                }
            }

            _logger.LogInformation("CrimeMapping ingestion finished");
            _lifetime.StopApplication();
        }
    }
}
