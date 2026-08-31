using AFMSSediDll;

namespace AFMSSSCService
{
    public class Program
    {
        private const string PROCESS_NAME = "AFMSSSCService";

        public static void Main(string[] args)
        {
            AFMSLog.Initialize(Environment.UserInteractive, PROCESS_NAME);
            AFMSLogBanner.WriteStartup(PROCESS_NAME, "AFMS DISCHARGE SERVICE");

            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}
