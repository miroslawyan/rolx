// -----------------------------------------------------------------------
// <copyright file="ApprovalMapper.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using RolXServer.Users.WebApi.Mapping;

namespace RolXServer.Auth.WebApi.Mapping
{
    /// <summary>
    /// Maps approvals from / to resource.
    /// </summary>
    public static class ApprovalMapper
    {
        /// <summary>
        /// Converts the specified domain to resource.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The resource.</returns>
        public static Resource.Approval ToResource(this Domain.Model.Approval domain)
        {
            return new Resource.Approval
            {
                User = domain.User.ToResource(),
                BearerToken = domain.BearerToken,
                Expires = domain.Expires,
            };
        }
    }
}
