﻿using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Enums;
using HealthMedScheduler.Domain.Interfaces;
using HealthMedScheduler.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HealthMedScheduler.Infrastructure.Data.Repository
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(MeuDbContext db) : base(db)
        {
        }

        public async Task<Agendamento> ObterAgendamentoPorId(Guid id)
        {
            return await Db.Agendamentos
               .Include(m => m.Paciente)
                .Include(e => e.EspecialidadeMedico)
                   .ThenInclude(e => e.Especialidade)
                  .ThenInclude(m => m.EspecialidadeMedicos)
                      .ThenInclude(p => p.Medico).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Agendamento>> ObterAgendamentosPorIStatus(int status)
        {
            StatusAgendamento statusDesejado = (StatusAgendamento)status;

            return await Db.Agendamentos
                .Include(m => m.Paciente)
                 .Include(e => e.EspecialidadeMedico)
                    .ThenInclude(e => e.Especialidade)
                   .ThenInclude(m => m.EspecialidadeMedicos)
                       .ThenInclude(p => p.Medico)
                .Where(a => a.StatusAgendamento == statusDesejado || status == 0).ToListAsync();
        }

        public async Task<IEnumerable<Agendamento>> ObterTodosAgendamentos()
        {
            return await Db.Agendamentos
               .Include(m => m.Paciente)
                .Include(e => e.EspecialidadeMedico)
                   .ThenInclude(e => e.Especialidade)
                  .ThenInclude(m => m.EspecialidadeMedicos)
                      .ThenInclude(p => p.Medico).ToListAsync();
        }
    }
}
