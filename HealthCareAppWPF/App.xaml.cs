using HealthCareAppWPF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System;
using HealthCareAppWPF.Properties;
using BL.Managers.Interfaces;
using EFDal.Repositories.Interfaces;
using EFDal.Repositories;
using BL.Managers;
using EFDal.Data;
using System.Windows.Controls;
using HealthCareAppWPF.UserControls;

namespace HealthCareAppWPF
{

    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }


        protected override void OnExit(ExitEventArgs e)
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
            base.OnExit(e);
        }

        public void Application_Startup(object sender, StartupEventArgs e)
        {
            // base.OnStartup(e);

            // Create a service collection and register your dependencies.
            var services = new ServiceCollection();

            // Register your services and dependencies.
            var connectionString = Settings.Default.ConnectionString;

            services.AddDbContext<HealthcareDbContext>(opt => opt.UseSqlServer(connectionString)
                                                    , ServiceLifetime.Transient);
            services.AddTransient<IPatientManager, PatientManager>();
            services.AddTransient<IMedicationManager, MedicationManager>();
            services.AddTransient<IDoctorManager, DoctorManager>();
            services.AddTransient<IPrescriptionManager, PrescriptionManager>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPrescriptionRepository, PrescriptionRepository>();
            services.AddTransient<IMedicationRepository, MedicationRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<MainWindow>();
            services.AddTransient<LandingControl>();
            services.AddTransient<DoctorLandingControl>();
            services.AddTransient<PatientSearchControl>();
            services.AddTransient<PatientLandingControl>();
            services.AddTransient<DoctorSearchControl>();
            services.AddTransient<PastPrescriptionsControl>();
            services.AddTransient<HealthAgencyDashboardControl>();

            // Build the service provider.
            ServiceProvider = services.BuildServiceProvider();

            // Create and show your main window.
            var mainWindow = ServiceProvider.GetService<MainWindow>();
            var loginControl = ServiceProvider.GetService<LandingControl>();
            mainWindow.NavigateToView(loginControl);
            mainWindow.Show();
        }
    }
}


