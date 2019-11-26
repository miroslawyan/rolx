// -----------------------------------------------------------------------
// <copyright file="Info.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Setup.WebApi.Resource
{
    /// <summary>
    /// The setup information.
    /// </summary>
    public sealed class Info
    {
        /// <summary>
        /// Gets or sets the pattern for project numbers.
        /// </summary>
        public string ProjectNumberPattern { get; set; } = string.Empty;
    }
}
