﻿using HealthMedScheduler.Application.Features.Medicos.Queries.ObterTodosMedicos;
using HealthMedScheduler.Application.MappingProfiles;
using HealthMedScheduler.Application.UnitTests.Mocks;
using HealthMedScheduler.Domain.Interfaces;
using AutoMapper;
using Moq;
using Shouldly;

namespace HealthMedScheduler.Application.UnitTests.Features.Medicos.Queries
{
    public class ObterMedicoPorIdQueryHandlerTests
    {
      /*  private readonly Mock<IMedicoRepository> _medicoRepository;
        private IMapper _mapper;

        public ObterMedicoPorIdQueryHandlerTests()
        {
            _medicoRepository = MockMedicoRepository.ObterTodosMockMedicoRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MedicoProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

        }

        [Fact]
        public async Task ObterMedicosQueryHandler_DeveRetornarListaDeMedicos()
        {
            //Arrange   
            var handler = new ObterMedicoPorIdQueryHandler(_mapper, _medicoRepository.Object);

            //Act
            var result = await handler.Handle(new ObterMedicoPorIdQuery(Guid.Parse("11111111-1111-1111-1111-111111111111")), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<MedicoPorIdDTO>();
            //result.Count.ShouldBe(3);
        }*/
    }
}
