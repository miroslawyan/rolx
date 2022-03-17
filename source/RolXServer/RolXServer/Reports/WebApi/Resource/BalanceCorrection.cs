// -----------------------------------------------------------------------
// <copyright file="BalanceCorrection.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace RolXServer.Reports.WebApi.Resource;

public sealed record BalanceCorrection(string Date, TimeSpan Overtime, TimeSpan Vacation);
