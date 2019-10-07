// -----------------------------------------------------------------------
// <copyright file="MapperProfile.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;

namespace RolXServer.Auth.Domain.Detail
{
    /// <summary>
    /// AutoMapper profile for the Auth.Domain package.
    /// </summary>
    public sealed class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {
            this.CreateMap<DataAccess.User, Model.AuthenticatedUser>();
        }
    }
}
