// -----------------------------------------------------------------------
// <copyright file="BearerTokenFactory.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using RolXServer.Common.Util;
using RolXServer.Users.DataAccess;

namespace RolXServer.Auth.Domain.Detail;

/// <summary>
/// Produces JWT bearer tokens.
/// </summary>
internal sealed class BearerTokenFactory
{
    private readonly Settings settings;
    private readonly SymmetricSecurityKey key;

    /// <summary>
    /// Initializes a new instance of the <see cref="BearerTokenFactory"/> class.
    /// </summary>
    /// <param name="settingsAccessor">The settings accessor.</param>
    public BearerTokenFactory(IOptions<Settings> settingsAccessor)
        : this(settingsAccessor.Value)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BearerTokenFactory" /> class.
    /// </summary>
    /// <param name="settings">The settings.</param>
    public BearerTokenFactory(Settings settings)
    {
        this.settings = settings;
        this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.settings.Secret));
    }

    /// <summary>
    /// Configures the specified JWT bearer options.
    /// </summary>
    /// <param name="options">The options.</param>
    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = this.key,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    }

    /// <summary>
    /// Produces a bearer token for the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The bearer token.</returns>
    public BearerToken ProduceFor(User user)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(ProduceClaimsFor(user)),
            Expires = DateTime.UtcNow + this.settings.TokenExpiration,
            SigningCredentials = new SigningCredentials(this.key, SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new BearerToken
        {
            Token = tokenHandler.WriteToken(token),
            Expires = tokenDescriptor.Expires.Value,
        };
    }

    private static IEnumerable<Claim> ProduceClaimsFor(User user)
    {
        yield return new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
        yield return new Claim(ClaimTypes.Role, user.Role.ToString());

        if (user.EntryDate.HasValue)
        {
            yield return new Claim(RolXClaimTypes.EntryDate, user.EntryDate.Value.ToIsoDate());
        }

        if (user.LeftDate.HasValue)
        {
            yield return new Claim(RolXClaimTypes.LeftDate, user.LeftDate.Value.ToIsoDate());
        }
    }
}
