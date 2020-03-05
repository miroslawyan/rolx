// -----------------------------------------------------------------------
// <copyright file="RolXClaimTypes.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Auth
{
    /// <summary>
    /// The (non-standard) claim types used in RolX.
    /// </summary>
    public static class RolXClaimTypes
    {
        /// <summary>
        /// A claim that specifies an entry date.
        /// </summary>
        public const string EntryDate = "rolx://EntryDate";

        /// <summary>
        /// A claim that specifies a left date.
        /// </summary>
        public const string LeftDate = "rolx://LeftDate";
    }
}
