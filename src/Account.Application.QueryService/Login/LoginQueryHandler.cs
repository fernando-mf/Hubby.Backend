using System;
using System.Threading;
using System.Threading.Tasks;
using Account.Application.QueryService.Account;
using Core.Application.Exceptions;
using Core.Infrastructure.Security;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Account.Application.QueryService.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryModel, LoginQueryResponse>
    {
        private readonly IPasswordComputer _passwordComputer;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMongoContext _context;

        public LoginQueryHandler(IMongoContext context, IPasswordComputer passwordComputer, ITokenHandler tokenHandler)
        {
            _passwordComputer = passwordComputer;
            _tokenHandler = tokenHandler;
            _context = context;
        }

        public async Task<LoginQueryResponse> Handle(LoginQueryModel request, CancellationToken cancellationToken)
        {
            var view = await _context.GetCollection<AccountReadModel>().Find(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (view == null) throw new ItemNotFoundException($"Account with username {request.Id} not found");

            var isAuthorized = _passwordComputer.Compare(request.Password, view.Password);
            if (!isAuthorized) throw new UnauthorizedAccessException("Password incorrect.");
            
            var token = _tokenHandler.Create(request.Id);
            return new LoginQueryResponse(view.Id, view.BuddyId, token);
        }
    }
}