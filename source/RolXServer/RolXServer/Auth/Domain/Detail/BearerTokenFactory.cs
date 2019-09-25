// -----------------------------------------------------------------------
// <copyright file="BearerTokenFactory.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RolXServer.Auth.DataAccess;

namespace RolXServer.Auth.Domain.Detail
{
    /// <summary>
    /// Produces JWT bearer tokens.
    /// </summary>
    public static class BearerTokenFactory
    {
        // TODO: get this from Configuration.GetSection("AppSettings:Secret")
        private static readonly string Secret = "This is quite non-secret.";

        private static byte[] Key => Encoding.ASCII.GetBytes(Secret);

        /// <summary>
        /// Produces a bearer token for the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The bearer token.</returns>
        public static string ProduceFor(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(ProduceClaimsFor(user)),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = ProduceSigningCredentials(),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Configures the specified JWT bearer options.
        /// </summary>
        /// <param name="options">The options.</param>
        public static void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        }

        private static IEnumerable<Claim> ProduceClaimsFor(User user)
        {
            yield return new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
            yield return new Claim(ClaimTypes.Role, user.Role.ToString());
        }

        private static SigningCredentials ProduceSigningCredentials()
        {
            return new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
