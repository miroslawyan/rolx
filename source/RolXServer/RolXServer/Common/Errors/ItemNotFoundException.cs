// -----------------------------------------------------------------------
// <copyright file="ItemNotFoundException.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace RolXServer.Common.Errors
{
    /// <summary>
    /// Is thrown when a non-existing item is requested.
    /// </summary>
    public sealed class ItemNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotFoundException"/> class.
        /// </summary>
        public ItemNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ItemNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ItemNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
