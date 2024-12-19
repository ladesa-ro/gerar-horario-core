using GerarHorarioService;
using GerarHorarioService.Workers;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddHostedService<ListenWorker>();

var host = builder.Build();
host.Run();