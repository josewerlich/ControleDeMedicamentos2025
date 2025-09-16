using Serilog;
using Serilog.Events;

namespace ControleDeMedicamentos.WebApp.DependencyInjection;

public static class SerilogConfig
{
    public static void AddSerilogConfig(this IServiceCollection services, ILoggingBuilder logging, IConfiguration configuration)
    {
        // Pegar o caminho do AppData
        var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var caminhoArquivoLogs = Path.Combine(caminhoAppData, "ControleDeMedicamentos", "erro.log");

        // Variáveis de Ambiente
        var licenseKey = "C9E83D51B82741F31A7CB9C31EC8116ED57E79CEDD277B14061595C28F1B6B16";

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(caminhoArquivoLogs, LogEventLevel.Error)
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.newrelic.com/log/v1",
                applicationName: "controle-de-medicamentos",
                licenseKey: licenseKey
            )
            .CreateLogger();

        logging.ClearProviders();

        // Injeta o serviço do serilog na aplicação
        services.AddSerilog();
    }

}
