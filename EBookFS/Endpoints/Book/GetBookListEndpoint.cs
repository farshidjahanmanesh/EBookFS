using EBookFS.Models.Contracts;
using FastEndpoints;
using Mapster;
using static EBookFS.Endpoints.Book.GetBookListEndpoint;

namespace EBookFS.Endpoints.Book
{
    public class GetBookListEndpoint : EndpointWithoutRequest<List<BookResponseDto>>
    {
        private readonly IBookRepository bookRepository;

        public class BookResponseDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }
        }
        public override void Configure()
        {
            Get("/book/list");
            AllowAnonymous();
            ResponseCache(60);
        }
        public GetBookListEndpoint(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public override async Task<List<BookResponseDto>> ExecuteAsync(CancellationToken ct)
        {
            return (await bookRepository.GetAll()).Adapt<List<BookResponseDto>>();
        }
    }

}
