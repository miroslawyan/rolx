// -----------------------------------------------------------------------
// <copyright file="InMemory.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

using Microsoft.EntityFrameworkCore;

namespace RolXServer
{
    /// <summary>
    /// Provides an in-memory database context for testing.
    /// </summary>
    internal static class InMemory
    {
        public static Func<RolXContext> ContextFactory()
        {
            var id = Guid.NewGuid().ToString();

            return () =>
            {
                var options = new DbContextOptionsBuilder<RolXContext>()
                    .UseInMemoryDatabase(id)
                    .EnableSensitiveDataLogging()
                    .Options;

                return new RolXContext(options);
            };
        }

        public static Func<RolXContext> ContextFactory(params object[] seedData)
        {
            var factory = ContextFactory();

            using (var context = factory())
            {
                foreach (var data in seedData)
                {
                    context.Add(data);
                }

                context.SaveChanges();
            }

            return factory;
        }
    }
}
