using EBookFS.Models.Contracts;
using FastEndpoints;
using Mapster;
using static EBookFS.Endpoints.Book.AddBookEndpoint;

namespace EBookFS.Endpoints.Book
{
    public class AddBookEndpoint : Endpoint<BookRequestDto>
    {
        private readonly IBookRepository bookRepository;

        public class BookRequestDto
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }
        }
        public override void Configure()
        {
            Post("/book/Add");
        }
        public AddBookEndpoint(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public override async Task HandleAsync(BookRequestDto req, CancellationToken ct)
        {
            await bookRepository.Add(req.Adapt<EBookFS.Models.Book>());
        }
    }

}
