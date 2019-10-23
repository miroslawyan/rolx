// -----------------------------------------------------------------------
// <copyright file="MapperProfile.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AutoMapper;
using RolXServer.Common.Util;

namespace RolXServer.WorkRecord.WebApi.Detail
{
    /// <summary>
    /// AutoMapper profile for the WorkRecord.WebApi package.
    /// </summary>
    public sealed class MapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapperProfile"/> class.
        /// </summary>
        public MapperProfile()
        {
            this.CreateMap<Domain.Model.Record, Resource.Record>()
                .ForMember(
                    dest => dest.NominalWorkTimeHours,
                    opt => opt.MapFrom(src => src.NominalWorkTime.TotalHours))
                .ForMember(
                    dest => dest.Date,
                    opt => opt.MapFrom(src => src.Date.ToIsoDate()));
        }
    }
}
