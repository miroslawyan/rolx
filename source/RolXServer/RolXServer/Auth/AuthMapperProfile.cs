// -----------------------------------------------------------------------
// <copyright file="AuthMapperProfile.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;

namespace RolXServer.Auth
{
    /// <summary>
    /// AutoMapper profile for the Auth package.
    /// </summary>
    public sealed class AuthMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthMapperProfile"/> class.
        /// </summary>
        public AuthMapperProfile()
        {
            this.CreateMap<DataAccess.User, Domain.Model.AuthenticatedUser>();
        }
    }
}
