using FastEndpoints.Security;
using FastEndpoints;
using static EBookFS.Endpoints.Accounting.LoginEndpoint;
using EBookFS.Models.Contracts;
using System.Security.Claims;

namespace EBookFS.Endpoints.Accounting
{
    public class LoginEndpoint : Endpoint<LoginRequestDto>
    {
        private readonly IUserManagerRepository userManagerRepository;

        public class LoginRequestDto
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public class LoginResponseDto
        {
            public string UserName { get; set; }
            public string Token { get; set; }
        }
        public override void Configure()
        {
            Post("/accounting/login");
            AllowAnonymous();
        }
        public LoginEndpoint(IUserManagerRepository userManagerRepository)
        {
            this.userManagerRepository = userManagerRepository;
        }
        public override async Task HandleAsync(LoginRequestDto req, CancellationToken ct)
        {
            if (userManagerRepository.Login(req.UserName, req.Password))
            {
                var jwtToken = JWTBearer.CreateToken(
                    signingKey: "Refah_Yourself_Do_It_Again_To_Going_to_Pardis",
                    expireAt: DateTime.UtcNow.AddDays(1),
                    priviledges: u =>
                    {
                        u.Claims.Add(new Claim(type: "username", value: req.UserName));
                    });

                await SendAsync(new LoginResponseDto()
                {
                    UserName = req.UserName,
                    Token = jwtToken
                });
            }
            else
            {
                throw new UnauthorizedAccessException("auth failed");
            }
        }
    }
}
