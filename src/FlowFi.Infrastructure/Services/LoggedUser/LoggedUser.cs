using FlowFi.Domain.Entities;
using FlowFi.Domain.Security.Tokens;
using FlowFi.Domain.Services.LoggedUser;
using FlowFi.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FlowFi.Infrastructure.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly FlowFiDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(FlowFiDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.Id == Guid.Parse(identifier));
    }
}
