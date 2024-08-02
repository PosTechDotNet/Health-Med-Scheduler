using AutoMapper;
using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Application.Features.Pacientes.Commands.AdicionarPaciente;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Application.UnitTests.Mocks;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.Pacientes.Commands
{
    [Collection(nameof(PacienteTestsAutoMockerCollection))]
    public class AdicionarPacienteCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPacienteRepository> _mockPacienteRepo;
        private readonly AdicionarPacienteCommandHandler _pacienteHandler;
        private readonly PacienteTestsAutoMockerFixture _pacientefixture;
        private readonly AutoMocker _mocker;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;

        public AdicionarPacienteCommandHandlerTests(PacienteTestsAutoMockerFixture pacientefixture)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<PacienteProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(mapperConfig.CreateMapper());
            //_pacienteHandler = _mocker.CreateInstance<AdicionarPacienteCommandHandler>();
            _mockPacienteRepo = new Mock<IPacienteRepository>();
            _pacientefixture = pacientefixture;
            _userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _pacienteHandler = new AdicionarPacienteCommandHandler(_mapper, _mockPacienteRepo.Object, _userManagerMock.Object);
        }

        [Fact(DisplayName = "Adicionar paciente novo com Sucesso")]
        [Trait("Categoria", "Paciente - Paciente Command Handler")]
        public async Task AdicionarPaciente_NovoPaciente_DeveExecutarComSucesso()
        {
            //Arrange
            var pacienteCommand = _pacientefixture.GerarPacienteCommandValido();
            var validator = new AdicionarPacienteCommandValidator();

            _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), pacienteCommand.Password)).ReturnsAsync(IdentityResult.Success);
            _mockPacienteRepo.Setup(r => r.ObterPacientePorCPF(pacienteCommand.Cpf)).ReturnsAsync((Paciente)null);
            _mockPacienteRepo.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);

            //Act
            var result = await _pacienteHandler.Handle(pacienteCommand, CancellationToken.None);
            var validationResult = await validator.ValidateAsync(pacienteCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            _mockPacienteRepo.Verify(r => r.Adicionar(It.IsAny<Paciente>()), Times.Once);
            _mockPacienteRepo.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar paciente novo com cpf existente com falha")]
        [Trait("Categoria", "Paciente - Paciente Command Handler")]
        public async Task AdicionarPaciente_CPFPacienteExistente_DeveExecutarComFalha()
        {
            // Arrange
            var pacienteOriginal = _pacientefixture.GerarPacienteValido();
            var pacienteNovo = _pacientefixture.GerarPacienteCommandValido();
            _mockPacienteRepo.Setup(r => r.Adicionar(It.IsAny<Paciente>())).Returns(Task.CompletedTask);
            _mockPacienteRepo.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _mockPacienteRepo.Setup(r => r.ObterPacientePorCPF(pacienteOriginal.Cpf)).ReturnsAsync(pacienteOriginal);

            //Act
            pacienteNovo.Cpf = pacienteOriginal.Cpf;

            //Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _pacienteHandler.Handle(pacienteNovo, CancellationToken.None));
            _mockPacienteRepo.Verify(r => r.Adicionar(It.IsAny<Paciente>()), Times.Never);
            _mockPacienteRepo.Verify(r => r.UnitOfWork.Commit(), Times.Never);

        }

        [Fact(DisplayName = "Adicionar paciente novo com Falha")]
        [Trait("Categoria", "Paciente - Paciente Command Handler")]
        public async Task AdicionarPaciente_NovoPaciente_DeveExecutarComFalha()
        {
            //Arrange
            var pacienteCommand = _pacientefixture.GerarPacienteCommandInValido();
            var validator = new AdicionarPacienteCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(pacienteCommand);

            //Acert
            Assert.False(validationResult.IsValid);
        }
    }
}
