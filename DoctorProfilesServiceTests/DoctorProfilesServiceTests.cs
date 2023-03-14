using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using ProfilesApi.Contracts.Requests.DoctorProfiles;
using ProfilesApi.DataAccess.Models;
using ProfilesApi.DataAccess.Repositories.Interfaces.Base;
using ProfilesApi.Mappers;
using ProfilesApi.Services.Implementations;
using ProfilesApi.Services.Interfaces;
using Assert = Xunit.Assert;

namespace DoctorProfilesServiceTests;

public class DoctorProfilesServiceTests
{
    private readonly IDoctorProfilesService _service;
    private readonly IMapper _mapper;
    private readonly Mock<IDoctorProfileRepository> _doctorMock = new Mock<IDoctorProfileRepository>();
    private readonly Mock<IAccountRepository> _accountMock = new Mock<IAccountRepository>();

    public DoctorProfilesServiceTests()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DoctorProfilesMapper());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        _mapper = mapper;
        _service = new DoctorProfilesService(_mapper,_accountMock.Object,_doctorMock.Object,null,null,null);
    }

    public EditDoctorProfileRequest GetDoctorProfileEntity()
        => new ()
        {
            Id = It.IsAny<Guid>(),
            Address = "ul Mira",
            CareerStartYear = 2019,
            DateOfBirth = Convert.ToDateTime("2005-02-28 22:00:00.0000000"),
            FirstName = "Alena",
            LastName = "Vorobey",
            OfficeId = It.IsAny<Guid>(),
            SpecializationId = It.IsAny<Guid>(),
            SpecializationName = "Teeth"
        };

    [Fact]
    public async Task UpdateAsync_ShouldReturnException_WhenDoctorProfileNotExists()
    {
        //Arrange
        _doctorMock.Setup(d => d.GetByIdAsync(It.IsAny<Guid>(), false))
            .ReturnsAsync(() => null);
        //Act
        _service.UpdateAsync(GetDoctorProfileEntity(), It.IsAny<Guid>());
        //Assert
        var ex = await Assert.ThrowsAsync<BadHttpRequestException>(()=>_service.UpdateAsync(GetDoctorProfileEntity(),It.IsAny<Guid>()));
        Assert.Equal("Doctor not found",ex.Message);
    }
    
    [Fact]
    public async Task UpdateAsync_ShouldReturnException_WhenAccountProfileNotExists()
    {
        //Arrange
        _doctorMock.Setup(d => d.GetByIdAsync(It.IsAny<Guid>(), false))
            .ReturnsAsync(new Doctor());
        _accountMock.Setup(d => d.GetByIdAsync(It.IsAny<Guid>(), true))
           .ReturnsAsync(() => null);
        //Act
        _service.UpdateAsync(GetDoctorProfileEntity(), It.IsAny<Guid>());
        //Assert
        var ex = await Assert.ThrowsAsync<BadHttpRequestException>(()=>_service.UpdateAsync(GetDoctorProfileEntity(),new Guid("8adc24d1-f541-4af8-b0f5-08db1b3b30dd")));
        Assert.Equal("Account not found",ex.Message );
    }
    
    [Fact]
    public async Task UpdateAsync_ShouldReturnDoctorProfileResponse_WhenDoctorExists()
    {
        //Arrange
        _doctorMock.Setup(d => d.GetByIdAsync(It.IsAny<Guid>(), false))
            .ReturnsAsync(new Doctor());
        _accountMock.Setup(d => d.GetByIdAsync(It.IsAny<Guid>(), true))
            .ReturnsAsync(new Account());
        //Act
        var response =  await _service.UpdateAsync(GetDoctorProfileEntity(), It.IsAny<Guid>());
        //Assert
        Assert.Equal(response.Address,GetDoctorProfileEntity().Address);
        Assert.Equal(response.CareerStartYear,GetDoctorProfileEntity().CareerStartYear);
        _doctorMock.Verify(d => d.GetByIdAsync(It.IsAny<Guid>(), false), Times.Once);
    }
    
}