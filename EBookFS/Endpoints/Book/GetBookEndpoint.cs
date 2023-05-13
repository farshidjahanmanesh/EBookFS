using EBookFS.Models.Contracts;
using FastEndpoints;
using Mapster;

namespace EBookFS.Endpoints.Book
{
    public class GetBookEndpoint : Endpoint<GetBookEndpoint.BookRequestDto, GetBookEndpoint.BookResponseDto>
    {
        private readonly IBookRepository bookRepository;
        public class BookRequestDto
        {
            public int Id { get; set; }
        }
        public class BookResponseDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }
        }
        public override void Configure()
        {
            Get("/book/Get");
            AllowAnonymous();
        }
        public GetBookEndpoint(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public override async Task<BookResponseDto> ExecuteAsync(BookRequestDto req, CancellationToken ct)
        {

            return (await bookRepository.Get(req.Id)).Adapt<BookResponseDto>();
        }
    }

}
