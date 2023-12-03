using HealthCareAppWPF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System;
using HealthCareAppWPF.Properties;
using BL.Managers.Interfaces;
using DAL.Repositories.Interfaces;
using DAL.Repositories;
using BL.Managers;
using DAL.Data;
using DAL;
using System.Windows.Controls;
using HealthCareAppWPF.UserControls;
using AutoMapper;
using BL.MappingProfiles;
using DAL.Repositories.EFRepositories;
using DAL.Repositories.DapperRepositories;
using Dapper;

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

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PatientMappingProfile>();
                cfg.AddProfile<DoctorMappingProfile>();
                cfg.AddProfile<PrescriptionMappingProfile>();
                cfg.AddProfile<MedicationMappingProfile>();
            });

            IMapper mapper = new Mapper(mapperConfig);


            // Create a service collection and register your dependencies.
            var services = new ServiceCollection();

            // Register your services and dependencies.
            var connectionString = Settings.Default.ConnectionString;

            // Register Dapper
            services.AddSingleton<DbConnectionFactory>(provider => new DbConnectionFactory(connectionString));

            // Register EF
            services.AddDbContext<HealthcareDbContext>(opt => opt.UseSqlServer(connectionString)
                                                    , ServiceLifetime.Transient);

            // Register other Services
            services.AddTransient<IPatientManager, PatientManager>();
            services.AddTransient<IMedicationManager, MedicationManager>();
            services.AddTransient<IDoctorManager, DoctorManager>();
            services.AddTransient<IPrescriptionManager, PrescriptionManager>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IPrescriptionRepository, PrescriptionRepository>();
            services.AddTransient<IMedicationRepository, MedicationRepository>();
            services.AddTransient<IDoctorRepository, DapperDoctorRepository>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(DapperGenericRepository<>));
            services.AddSingleton<MainWindow>();
            services.AddTransient<LandingControl>();
            services.AddTransient<DoctorLandingControl>();
            services.AddTransient<PatientSearchControl>();
            services.AddTransient<PatientLandingControl>();
            services.AddTransient<DoctorSearchControl>();
            services.AddTransient<PastPrescriptionsControl>();
            services.AddTransient<HealthAgencyDashboardControl>();
            services.AddSingleton(mapper);

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


