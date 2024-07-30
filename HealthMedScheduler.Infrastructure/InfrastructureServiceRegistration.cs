﻿using HealthMedScheduler.Domain.Interfaces;
using HealthMedScheduler.Infrastructure.Data.Context;
using HealthMedScheduler.Infrastructure.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMedScheduler.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            //Repository
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IEspecialidadeMedicoRepository, EspecialidadeMedicoRepository>();
            services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

            //Services
            /* services.AddScoped<IEspecialidadeService, EspecialidadeService>();
             services.AddScoped<IAgendamentoService, AgendamentoService>();
             services.AddScoped<IPacienteService, PacienteService>();
             services.AddScoped<IMedicoService, MedicoService>();
             services.AddScoped<IEspecialidadeMedicoService, EspecialidadeMedicoService>();*/

            return services;
        }        
    }
}
