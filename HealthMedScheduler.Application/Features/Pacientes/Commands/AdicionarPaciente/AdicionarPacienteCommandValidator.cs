﻿using FluentValidation;

namespace HealthMedScheduler.Application.Features.Pacientes.Commands.AdicionarPaciente
{
    public class AdicionarPacienteCommandValidator : AbstractValidator<AdicionarPacienteCommand>
    {
        public AdicionarPacienteCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MinimumLength(2).WithMessage("O campo {PropertyName} precisa ter mais que {MinLength} caracteres");

            RuleFor(x => x.Idade)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MinimumLength(11).WithMessage("O campo {PropertyName} precisa ter {MinLength} caracteres")
                .Must(ValidarCpf).WithMessage("O campo {PropertyName} é inválido");

            RuleFor(x => x.Cep).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MinimumLength(2).WithMessage("O campo {PropertyName} precisa ter mais que {MinLength} caracteres");


            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MinimumLength(2).WithMessage("O campo {PropertyName} precisa ter mais que {MinLength} caracteres");

            RuleFor(x => x.Telefone).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
        }

        private bool ValidarCpf(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
