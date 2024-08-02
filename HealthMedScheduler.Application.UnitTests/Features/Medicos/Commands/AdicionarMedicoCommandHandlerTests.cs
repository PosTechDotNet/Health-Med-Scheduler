using AutoMapper;
using HealthMedScheduler.Application.Exceptions;
using HealthMedScheduler.Application.Features.Medicos.Commands.AdicionarMedico;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Application.UnitTests.Mocks;
using HealthMedScheduler.Domain.Entity;
using HealthMedScheduler.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using Moq.AutoMock;

namespace HealthMedScheduler.Application.UnitTests.Features.Medicos.Commands
{
    [Collection(nameof(MedicoTestsAutoMockerCollection))]
    public class AdicionarMedicoCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly AdicionarMedicoCommandHandler _medicoHandler;
        private readonly MedicoTestsAutoMockerFixture _medicofixture;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly AutoMocker _mocker;
        private readonly Mock<IMedicoRepository> _medicoRepositoryMock;

        public AdicionarMedicoCommandHandlerTests(MedicoTestsAutoMockerFixture medicofixture)
        {

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MedicoProfile>();
            });
            _mocker = new AutoMocker();
            _mapper = mapperConfig.CreateMapper();
            _mocker.Use(mapperConfig.CreateMapper());
            _medicofixture = medicofixture;
            _medicoRepositoryMock = new Mock<IMedicoRepository>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _medicoHandler = new AdicionarMedicoCommandHandler(_mapper, _medicoRepositoryMock.Object, _userManagerMock.Object);

        }

        [Fact(DisplayName = "Adicionar médico novo com Sucesso")]
        [Trait("Categoria", "Medico - Medico Command Handler")]
        public async Task AdicionarMedico_NovoMedico_DeveExecutarComSucesso()
        {
            //Arrange
            var medicoCommand = _medicofixture.GerarMedicoCommandValido();
            var validator = new AdicionarMedicoCommandValidator();
            _userManagerMock.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), medicoCommand.Password)).ReturnsAsync(IdentityResult.Success);
            _medicoRepositoryMock.Setup(r => r.ObterMedicoPorCPF(medicoCommand.Cpf)).ReturnsAsync((Medico)null);
            _medicoRepositoryMock.Setup(r => r.ObterMedicoPorCRM(medicoCommand.Crm)).ReturnsAsync((Medico)null);
            _medicoRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);


            //Act
            var result = await _medicoHandler.Handle(medicoCommand, CancellationToken.None);

            var validationResult = await validator.ValidateAsync(medicoCommand);

            //Assert
            Assert.True(validationResult.IsValid);
            _medicoRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Once);
            _medicoRepositoryMock.Verify(r => r.Adicionar(It.IsAny<Medico>()), Times.Once);

        }

        [Fact(DisplayName = "Adicionar médico novo com cpf existente deve retornar Falha")]
        [Trait("Categoria", "Medico - Medico Command Handler")]
        public async Task AdicionarMedico_CPFMedicoExistente_DeveExecutarComFalha()
        {
            // Arrange
            var medicoOriginal = _medicofixture.GerarMedicoValido();
            var medicoNovo = _medicofixture.GerarMedicoCommandValido();
            _medicoRepositoryMock.Setup(r => r.Adicionar(It.IsAny<Medico>())).Returns(Task.CompletedTask);
            _medicoRepositoryMock.Setup(r => r.UnitOfWork.Commit()).ReturnsAsync(true);
            _medicoRepositoryMock.Setup(r => r.ObterMedicoPorCPF(medicoOriginal.Cpf)).ReturnsAsync(medicoOriginal);

            //Act
            medicoNovo.Cpf = medicoOriginal.Cpf;

            //Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _medicoHandler.Handle(medicoNovo, CancellationToken.None));
            _medicoRepositoryMock.Verify(r => r.Adicionar(It.IsAny<Medico>()), Times.Never);
            _medicoRepositoryMock.Verify(r => r.UnitOfWork.Commit(), Times.Never);

        }

        [Fact(DisplayName = "Adicionar médico novo com Falha")]
        [Trait("Categoria", "Medico - Medico Command Handler")]
        public async Task AdicionarMedico_NovoMedico_DeveExecutarComFalha()
        {
            //Arrange
            var medicoCommand = _medicofixture.GerarMedicoCommandInValido();
            var validator = new AdicionarMedicoCommandValidator();

            //Act
            var validationResult = await validator.ValidateAsync(medicoCommand);

            //Acert
            Assert.False(validationResult.IsValid);
        }
    }
}
