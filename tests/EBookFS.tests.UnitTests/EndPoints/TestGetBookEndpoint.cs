using EBookFS.Endpoints.Book;
using EBookFS.Models;
using EBookFS.Models.Contracts;
using FastEndpoints;
using FluentAssertions;
using Moq;

namespace EBookFS.tests.UnitTests.EndPoints
{
    public class TestGetBookEndpoint
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(c => c.Get(It.IsAny<int>())).ReturnsAsync(new Book());
            bookRepositoryMock.Setup(c => c.IsExist(It.IsAny<int>())).ReturnsAsync(true);

            var loginEndpoint = Factory.Create<GetBookEndpoint>(bookRepositoryMock.Object);
            var result = await loginEndpoint.ExecuteAsync(new GetBookEndpoint.BookRequestDto()
            {
                Id = 1
            }, default);

            result.Should().BeOfType<GetBookEndpoint.BookResponseDto>();
            result.Should().NotBeNull();
            bookRepositoryMock.Verify(s => s.Get(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task Get_OnWrongId_ReturnStatusCode400()
        {
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(c => c.Get(2)).ReturnsAsync(new Book());
            bookRepositoryMock.Setup(c => c.IsExist(5)).ReturnsAsync(true);

            var loginEndpoint = Factory.Create<GetBookEndpoint>(bookRepositoryMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => loginEndpoint.ExecuteAsync(new GetBookEndpoint.BookRequestDto()
            {
                Id = 1
            }, default));
            bookRepositoryMock.Verify(s => s.IsExist(1), Times.Once());
        }
    }
}
