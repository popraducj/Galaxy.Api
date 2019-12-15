using System;
using System.Security.Claims;
using GraphQL.Authorization.AspNetCore;

namespace Galaxy.Api.Presentation.Authorization
{
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
               
        public Guid UserId { get; set; }
    }
}