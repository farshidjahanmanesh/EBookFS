using AutoFixture;
using Castle.Core.Resource;
using EBookFS.Endpoints.Book;
using EBookFS.Models;
using EBookFS.Models.Contracts;
using EBookFS.Models.Repositories;
using EBookFS.tests.UnitTests.Helpers;
using FastEndpoints;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace EBookFS.tests.UnitTests.Repositories
{
    public class TestBookRepository
    {
        [Fact]
        public async Task Add_OnSuccess_ReturnOk()
        {
            var fixture = new Fixture();
            fixture.Inject<List<Comment>>(new List<Comment>());

            var book = fixture.Create<Book>();

            var bookMock = new Mock<DbSet<Book>>();
            var bookContextMock = new Mock<BookDBContext>();

            bookContextMock.Setup(x => x.Books.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .Callback((Book model, CancellationToken token) =>
                {
                }).Returns((Book model, CancellationToken token) => ValueTask.FromResult((EntityEntry<Book>)null))
                ;

            var repository = new BookRepository(bookContextMock.Object);
            await repository.Add(book);

        }

        [Fact]
        public async Task Add_OnBookNull_ThrowException()
        {
            var bookMock = new Mock<DbSet<Book>>();
            var bookContextMock = new Mock<BookDBContext>();

            bookContextMock.Setup(x => x.Books.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                .Callback((Book model, CancellationToken token) =>
                {
                }).Returns((Book model, CancellationToken token) => ValueTask.FromResult((EntityEntry<Book>)null))
                ;

            var repository = new BookRepository(bookContextMock.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Add(null));
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnOk()
        {
            var context = ContextFactory.DBContextFactoryHelper();
            context.Books.Add(new Book()
            {
                Id = 1,
                Author = "",
                Description = "",
                Title = ""
            });
            context.SaveChanges();

            var repository = new BookRepository(context);
            var result = await repository.Get(1);
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

        [Fact]
        public async Task Get_OnNotExist_ReturnNULL()
        {
            var context = ContextFactory.DBContextFactoryHelper();

            var repository = new BookRepository(context);
            var result = await repository.Get(2);
            result.Should().BeNull();
        }


    }
}
