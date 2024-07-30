﻿using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Enums;
using HealthMedScheduler.Domain.Interfaces;
using HealthMedScheduler.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthMedScheduler.Infrastructure.Data.Repository
{
    public class EspecialidadeMedicoRepository : Repository<EspecialidadeMedico>, IEspecialidadeMedicoRepository
    {
        public EspecialidadeMedicoRepository(MeuDbContext context) : base(context) { }

        public async Task<EspecialidadeMedico> ObterPorIdEspecialidadeIDMedico(Guid idMedico, Guid idEspecialidade)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.MedicoId == idMedico && e.EspecialidadeId == idEspecialidade);
        }

        public async Task<bool> VerificarAgendaLivreMedico(Guid idMedido, DateTime dataAtendimento)
        {
            var agenda = await DbSet.FirstOrDefaultAsync(e => e.MedicoId == idMedido && e.Agendamentos.Any(a => a.DataHoraAtendimento.Equals(dataAtendimento) && a.StatusAgendamento == StatusAgendamento.Ativo));

            return agenda == null;
        }
    }
}
