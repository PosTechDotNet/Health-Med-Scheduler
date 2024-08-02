using HealthMedScheduler.Application.ViewModel.Auth;
using MediatR;

namespace HealthMedScheduler.Application.Features.Auth.Commands
{
    public class GerarJwtTokenCommand : IRequest<UsuarioRespostaLoginViewModel>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
