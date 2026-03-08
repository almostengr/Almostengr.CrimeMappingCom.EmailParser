using Almostengr.CrimeMappingCom.EmailParser.Shared;
using Almostengr.CrimeMappingCom.EmailParser.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddCrimeMappingServices(builder.Configuration);

var host = builder.Build();
host.Run();
