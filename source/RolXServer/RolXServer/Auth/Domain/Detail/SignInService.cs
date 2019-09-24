﻿// -----------------------------------------------------------------------
// <copyright file="SignInService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RolXServer.Auth.DataAccess.Entity;
using RolXServer.Auth.Domain.Model;
using RolXServer.Common.DataAccess;

namespace RolXServer.Auth.Domain.Detail
{
    /// <summary>
    /// Service for signing users in.
    /// </summary>
    public sealed class SignInService : ISignInService
    {
        private readonly ILogger logger;
        private readonly IRepository<User> userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignInService" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="userRepository">The user repository.</param>
        public SignInService(ILogger<SignInService> logger, IRepository<User> userRepository)
        {
            this.logger = logger;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Authenticates with the specified google identifier token.
        /// </summary>
        /// <param name="googleIdToken">The google identifier token.</param>
        /// <returns>
        /// The authenticated user or <c>null</c> if authentication failed.
        /// </returns>
        public async Task<AuthenticatedUser?> Authenticate(string googleIdToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleIdToken);
                if (payload is null)
                {
                    return null;
                }

                if (payload.HostedDomain != "m-f.ch")
                {
                    this.logger.LogWarning("Sign-in from foreign domain refused: {0}", payload.HostedDomain);
                }

                var user = await this.EnsureUser(payload);

                // TODO: use mapping
                return new AuthenticatedUser
                {
                    Id = user.Id,
                    GoogleId = user.GoogleId,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    Email = payload.Email,
                    AvatarUrl = payload.Picture,
                };
            }
            catch (InvalidJwtException e)
            {
                this.logger.LogWarning(e, "While validating googleIdToken");
                return null;
            }
        }

        private async Task<User> EnsureUser(GoogleJsonWebSignature.Payload payload)
        {
            var user = await this.userRepository.Entities.SingleOrDefaultAsync(u => u.GoogleId == payload.Subject);
            if (user is null)
            {
                this.logger.LogInformation("Adding yet unknown user {0}", payload.Name);

                user = new User { GoogleId = payload.Subject };
                this.userRepository.Entities.Add(user);
            }

            user.FirstName = payload.GivenName;
            user.LastName = payload.FamilyName;
            user.Email = payload.Email;
            user.AvatarUrl = payload.Picture;

            await this.userRepository.SaveChanges();

            return user;
        }
    }
}
