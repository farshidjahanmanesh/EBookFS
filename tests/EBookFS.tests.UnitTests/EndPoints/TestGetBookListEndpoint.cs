using EBookFS.Endpoints.Book;
using EBookFS.Models;
using EBookFS.Models.Contracts;
using FastEndpoints;
using FluentAssertions;
using Moq;

namespace EBookFS.tests.UnitTests.EndPoints
{
    public class TestGetBookListEndpoint
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(c => c.GetAll()).ReturnsAsync(new List<Book>());

            var loginEndpoint = Factory.Create<GetBookListEndpoint>(bookRepositoryMock.Object);
            var result = await loginEndpoint.ExecuteAsync(default);

            result.Should().BeOfType<List<GetBookListEndpoint.BookResponseDto>>();
            result.Should().NotBeNull();
            bookRepositoryMock.Verify(s => s.GetAll(), Times.Once());
        }
    }
}
