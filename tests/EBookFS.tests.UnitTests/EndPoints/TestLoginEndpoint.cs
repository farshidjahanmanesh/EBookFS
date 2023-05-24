using EBookFS.Endpoints.Accounting;
using EBookFS.Models.Contracts;
using FastEndpoints;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EBookFS.Endpoints.Accounting.LoginEndpoint;

namespace EBookFS.tests.UnitTests.EndPoints
{
    public class TestLoginEndpoint
    {
        [Fact]
        public async Task POST_OnSuccess_ReturnStatusCode200()
        {
            var userManagerMock = new Mock<IUserManagerRepository>();
            userManagerMock.Setup(c => c.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            var loginEndpoint = Factory.Create<LoginEndpoint>(userManagerMock.Object);
            await loginEndpoint.HandleAsync(new LoginEndpoint.LoginRequestDto()
            {
                UserName = "test",
                Password = "test"
            }, default);
            var result = loginEndpoint.Response;

            result.Should().BeOfType<LoginResponseDto>();
            userManagerMock.Verify(s => s.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }


        [Fact]
        public async Task POST_OnWrongUserNameOrPassword_ReturnStatusCode401()
        {
            var userManagerMock = new Mock<IUserManagerRepository>();
            userManagerMock.Setup(c => c.Login("admin", "admin")).Returns(true);
            var loginEndpoint = Factory.Create<LoginEndpoint>(userManagerMock.Object);


            var result = await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await loginEndpoint.HandleAsync(new LoginEndpoint.LoginRequestDto()
            {
                UserName = "test",
                Password = "test"
            }, default));

            userManagerMock.Verify(s => s.Login("test", "test"), Times.Once());
        }
    }
}
