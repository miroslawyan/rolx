// -----------------------------------------------------------------------
// <copyright file="ExportData.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Reports.Domain.Model;

public sealed record ExportData(
    DateOnly Date,
    int ProjectNumber,
    string CustomerName,
    string ProjectName,
    int SubprojectNumber,
    string SubprojectName,
    int ActivityNumber,
    string ActivityName,
    string BillabilityName,
    string UserName,
    TimeSpan Duration,
    string Comment);
