using AFMSSediDll;
using log4net;

namespace AFMSSSCService
{
    public class Program
    {
        private const string PROCESS_NAME = "AFMSSSCService";
        private static readonly ILog Log = LogManager.GetLogger(PROCESS_NAME);

        public static void Main(string[] args)
        {
            AFMSLog.Initialize(Environment.UserInteractive, PROCESS_NAME);
            AFMSLogBanner.WriteStartup(PROCESS_NAME, "AFMS SSC SERVICE");

            string programPath = Environment.ProcessPath ?? throw new InvalidOperationException("현재 프로그램 경로를 확인할 수 없습니다.");
            ServiceInstallResult installResult = WindowsServiceManager.EnsureInstalled(programPath, PROCESS_NAME);
            Log.Info(installResult.Status);
            Log.Info(installResult.Message);

            FBProvider.Instance.Initialize(FBProvider.SetFBConnStrBuilder());

            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<WorkerSSC>();

            var host = builder.Build();
            host.Run();
        }
    }
}
