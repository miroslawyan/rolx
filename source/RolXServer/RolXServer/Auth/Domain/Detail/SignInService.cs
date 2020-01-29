// -----------------------------------------------------------------------
// <copyright file="SignInService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;

using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RolXServer.Auth.DataAccess;
using RolXServer.Auth.Domain.Mapping;
using RolXServer.Auth.Domain.Model;

namespace RolXServer.Auth.Domain.Detail
{
    /// <summary>
    /// Service for signing users in.
    /// </summary>
    public sealed class SignInService : ISignInService
    {
        private readonly RolXContext dbContext;
        private readonly BearerTokenFactory bearerTokenFactory;
        private readonly Settings settings;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignInService" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="bearerTokenFactory">The bearer token factory.</param>
        /// <param name="settingsAccessor">The settings accessor.</param>
        /// <param name="logger">The logger.</param>
        public SignInService(
            RolXContext dbContext,
            BearerTokenFactory bearerTokenFactory,
            IOptions<Settings> settingsAccessor,
            ILogger<SignInService> logger)
        {
            this.dbContext = dbContext;
            this.bearerTokenFactory = bearerTokenFactory;
            this.settings = settingsAccessor.Value;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the sign-in information.
        /// </summary>
        /// <returns>
        /// The sign-in information.
        /// </returns>
        public Task<Info> GetInfo()
        {
            var info = new Info
            {
                GoogleClientId = this.settings.GoogleClientId,
            };

            return Task.FromResult(info);
        }

        /// <summary>
        /// Authenticates with the specified google identifier token.
        /// </summary>
        /// <param name="signInData">The sign in data.</param>
        /// <returns>
        /// The authenticated user or <c>null</c> if authentication failed.
        /// </returns>
        public async Task<AuthenticatedUser?> Authenticate(SignInData signInData)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(signInData.GoogleIdToken);
                if (payload is null)
                {
                    return null;
                }

                if (!this.IsAllowedDomain(payload.HostedDomain))
                {
                    this.logger.LogWarning("Sign-in from foreign domain refused: {0}", payload.HostedDomain);
                    return null;
                }

                return this.Authenticate(await this.EnsureUser(payload));
            }
            catch (InvalidJwtException e)
            {
                this.logger.LogWarning(e, "While validating googleIdToken");
                return null;
            }
        }

        /// <summary>
        /// Extends the authentication for user with the specified identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// The authenticated user.
        /// </returns>
        public async Task<AuthenticatedUser?> Extend(Guid userId)
        {
            var user = await this.dbContext.Users.FindAsync(userId);
            if (user is null)
            {
                this.logger.LogWarning("Unknown user tries to extend authentication: {0}", userId);
                return null;
            }

            return this.Authenticate(user);
        }

        private bool IsAllowedDomain(string domain)
        {
            return this.settings.GoogleHostedDomainWhitelist.Length == 0
                || this.settings.GoogleHostedDomainWhitelist.Any(d => d == domain);
        }

        private async Task<User> EnsureUser(GoogleJsonWebSignature.Payload payload)
        {
            var user = await this.dbContext.Users.SingleOrDefaultAsync(u => u.GoogleId == payload.Subject);
            if (user is null)
            {
                this.logger.LogInformation("Adding yet unknown user {0}", payload.Name);

                user = new User
                {
                    GoogleId = payload.Subject,
                    Role = Role.User,
                };

                this.dbContext.Users.Add(user);
            }

            user.FirstName = payload.GivenName;
            user.LastName = payload.FamilyName;
            user.Email = payload.Email;
            user.AvatarUrl = payload.Picture ?? string.Empty;

            await this.dbContext.SaveChangesAsync();

            return user;
        }

        private AuthenticatedUser Authenticate(User user)
        {
            return user.ToDomain(this.bearerTokenFactory.ProduceFor(user));
        }
    }
}
