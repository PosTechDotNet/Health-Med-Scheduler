﻿using FluentValidation;

namespace HealthMedScheduler.Application.Features.Medicos.Commands.RemoverMedico
{
    public class RemoverMedicoCommandValidator : AbstractValidator<RemoverMedicoCommand>
    {
        public RemoverMedicoCommandValidator()
        {
            RuleFor(c => c.IdMedico)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

        }
    }
}
