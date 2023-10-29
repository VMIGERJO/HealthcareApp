using BL.Managers.Interfaces;
using BL.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using EFDal.Repositories.Interfaces;
using EFDal.Repositories;

namespace HealthCareWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnExit(ExitEventArgs e)
        {
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
            base.OnExit(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // base.OnStartup(e);

            // Create a service collection and register your dependencies.
            var services = new ServiceCollection();

            // Register your services and dependencies.
            // var connectionString = Settings.Default.ConnectionString;

            // services.AddDbContext<HealthcareDbContext>(opt => opt.UseSqlServer(connectionString)
            //                                        , ServiceLifetime.Transient);
            services.AddTransient<IPatientManager, PatientManager>();
            services.AddTransient<IDoctorManager, DoctorManager>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<MainWindow>();
            // services.AddTransient<DoctorSearchWindow>();
            // services.AddTransient<PatientSearchWindow>();

            // Build the service provider.
            _serviceProvider = services.BuildServiceProvider();

            // Create and show your main window.
            var mainWindow = new MainWindow();
            // mainWindow.DataContext = _serviceProvider.GetRequiredService<MainViewModel>(); // Example usage.
            mainWindow.Show();

        }
    }
}
