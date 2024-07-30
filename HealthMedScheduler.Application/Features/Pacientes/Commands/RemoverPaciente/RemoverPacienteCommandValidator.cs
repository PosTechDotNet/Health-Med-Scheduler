﻿using FluentValidation;

namespace HealthMedScheduler.Application.Features.Pacientes.Commands.RemoverPaciente
{
    public class RemoverPacienteCommandValidator : AbstractValidator<RemoverPacienteCommand>
    {
        public RemoverPacienteCommandValidator()
        {
            RuleFor(c => c.IdPaciente)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

        }
    }
}
