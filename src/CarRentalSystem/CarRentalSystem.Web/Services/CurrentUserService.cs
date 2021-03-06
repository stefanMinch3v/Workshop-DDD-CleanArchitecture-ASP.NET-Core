﻿namespace CarRentalSystem.Web.Services
{
    using Application.Contracts;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("No authenticated user for the current request.");
            }

            this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
