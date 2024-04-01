using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotTotalCommanderLib.Infrastructure.Directories;
using NotTotalCommanderLib.Infrastructure.DirectoryContent;
using NotTotalCommanderLib.Infrastructure.Drives;
using NotTotalCommanderLib.Infrastructure.FileContent;
using NotTotalCommanderLib.Infrastructure.Files;
using NotTotalCommanderLib.Infrastructure.Routing;

namespace NotTotalCommaderDesktop
{
    public partial class App
    {
        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<NewDirectoryWindow>();
                    services.AddSingleton<DeleteDirectoryWindow>();
                    services.AddScoped<DirectoryContentService>();
                    services.AddScoped<DirectoryService>();
                    services.AddScoped<DriveService>();
                    services.AddScoped<FileService>();
                    services.AddScoped<PathService>();
                    services.AddScoped<RouteService>();
                    services.AddScoped<FileContentService>();
                    services.AddScoped<DirectoryValidator>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            host.Start();

            MainWindow = host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();


            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            host.StopAsync();
            host.Dispose();

            base.OnExit(e);
        }
    }
}