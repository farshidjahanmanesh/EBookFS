using AutoFixture;
using EBookFS.Endpoints.Book;
using EBookFS.Models;
using EBookFS.Models.Contracts;
using FastEndpoints;
using Moq;

namespace EBookFS.tests.UnitTests.EndPoints
{
    public class TestAddBookEndpoint
    {
        [Fact]
        public async Task POST_OnSuccess_ReturnStatusCode200()
        {
            var fixture = new Fixture();
            var book = fixture.Create<EBookFS.Endpoints.Book.AddBookEndpoint.BookRequestDto>();
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(c => c.Add(It.IsAny<Book>()));

            var loginEndpoint = Factory.Create<AddBookEndpoint>(bookRepositoryMock.Object);
            await loginEndpoint.HandleAsync(book, default);

            bookRepositoryMock.Verify(s => s.Add(It.IsAny<Book>()), Times.Once());
        }
    }
}
