// -----------------------------------------------------------------------
// <copyright file="PhaseExtensionTests.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using FluentAssertions;
using NUnit.Framework;
using RolXServer.Projects.DataAccess;

namespace RolXServer.Projects.Domain.Detail
{
    public sealed class PhaseExtensionTests
    {
        private Project project = null!;

        [SetUp]
        public void SetUp()
        {
            this.project = new Project
            {
                Number = "P1234",
                Name = "Foo",
                Phases = new List<Phase>
                {
                    new Phase
                    {
                        Number = 42,
                        Name = "Bar",
                    },
                },
            };

            foreach (var phase in this.project.Phases)
            {
                phase.Project = this.project;
            }
        }

        [Test]
        public void Sanitize_FullName()
        {
            var phase = this.project.Phases[0];
            phase.Sanitize();

            phase.FullName.Should().Be("P1234.042 - Foo - Bar");
        }

        [Test]
        public void Sanitize_Budget()
        {
            var phase = this.project.Phases[0];
            phase.Budget = TimeSpan.FromMinutes(0.5);
            phase.Sanitize();

            phase.Budget.Should().BeNull();
        }
    }
}
