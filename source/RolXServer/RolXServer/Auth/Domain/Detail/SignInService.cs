// -----------------------------------------------------------------------
// <copyright file="SignInService.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Google.Apis.Auth;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using RolXServer.Auth.Domain.Model;
using RolXServer.Users;
using RolXServer.Users.DataAccess;

namespace RolXServer.Auth.Domain.Detail;

/// <summary>
/// Service for signing users in.
/// </summary>
internal sealed class SignInService : ISignInService
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
            InstallationId = this.settings.InstallationId,
        };

        return Task.FromResult(info);
    }

    /// <summary>
    /// Authenticates with the specified google identifier token.
    /// </summary>
    /// <param name="signInData">The sign in data.</param>
    /// <returns>
    /// The approval or <c>null</c> if authentication failed.
    /// </returns>
    public async Task<Approval?> Authenticate(SignInData signInData)
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
    /// The approval or <c>null</c> if authentication failed.
    /// </returns>
    public async Task<Approval?> Extend(Guid userId)
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
        var user = await this.dbContext.Users.SingleOrDefaultAsync(u => u.GoogleId == payload.Subject || u.GoogleId == payload.Email);
        if (user is null)
        {
            this.logger.LogInformation("Adding yet unknown user {0}", payload.Name);

            var isFirstUser = !(await this.dbContext.Users.AnyAsync());

            user = new User
            {
                Role = isFirstUser ? Role.Administrator : Role.User,
                EntryDate = isFirstUser ? DateTime.Today : (DateTime?)null,
            };

            this.dbContext.Users.Add(user);
        }

        user.GoogleId = payload.Subject;
        user.FirstName = payload.GivenName;
        user.LastName = payload.FamilyName;
        user.Email = payload.Email;
        user.AvatarUrl = payload.Picture ?? string.Empty;

        await this.dbContext.SaveChangesAsync();

        return user;
    }

    private Approval Authenticate(User user)
    {
        var bearerToken = this.bearerTokenFactory.ProduceFor(user);

        return new Approval
        {
            User = user,
            BearerToken = bearerToken.Token,
            Expires = bearerToken.Expires,
        };
    }
}
