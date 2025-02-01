
using Serilog;
using System.Security.Cryptography.X509Certificates;

namespace MadStickWebAppTester
{
    public class Program
    {
        public static int Main(string[] args)
        {

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            Log.Logger = new LoggerConfiguration()
                    .WriteTo.Async(a => a.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"))
                    .WriteTo.Async(a => a.File("Logs/log.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}", shared: true))
                    .WriteTo.Seq("http://localhost:5341")
                    .CreateLogger();

            try
            {
                Log.Information("|---||| Starting host... |||---|");
                Log.Information("|---||| Current working directory: {directory} |||---|", Directory.GetCurrentDirectory());

                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception e)
            {
                Log.Fatal(e, "|---||| Host terminated unexpectedly. |||---|");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }


        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                //.ConfigureAppConfiguration(AddAppConfiguration)
                // .ConfigureAppConfiguration(config =>
                // {
                //     config.Sources.Clear();
                //     //config.AddJsonFile("appsettings.Development.json");
                //     config.AddJsonFile("appsettings.json");
                // })
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(opts =>
                        {

                            opts.ConfigureHttpsDefaults(cOpts =>
                            {
                                string certificateName = "agurkufabrikas";
                                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                                store.Open(OpenFlags.ReadOnly);
                                X509Certificate2 certificate = null;
                                foreach (var cert in store.Certificates)
                                {
                                    if (cert.Subject.Contains(certificateName))
                                        certificate = cert;
                                }
                                if (certificate == null)
                                {
                                    throw new InvalidOperationException($"Server certificate: '{certificateName}' wasn't found");
                                }
                                cOpts.ServerCertificate = certificate;
                                Log.Information("Certificate: {certificate} was found and selected", certificate);
                                store.Close();
                            });

                        });
                    //TODO: how this fixed the issue with the footer being bloated only when environment is set(from the launchSetting) to Production??? I don't know, its like magic.
                    webBuilder.UseStaticWebAssets();
                });


        }

        private static void AddAppConfiguration(HostBuilderContext context, IConfigurationBuilder config)
        {
            //config.Sources.Clear();
            //config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //config.AddEnvironmentVariables();
        }
    }


}