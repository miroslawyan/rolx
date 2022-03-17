// -----------------------------------------------------------------------
// <copyright file="TimeSpanJsonSecondsConverter.cs" company="Christian Ewald">
// Copyright (c) Christian Ewald. All rights reserved.
// Licensed under the MIT license.
// See LICENSE.md in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;

namespace RolXServer.Common.Util;

/// <summary>
/// Converts <see cref="TimeSpan"/> values from/to JSON numbers in seconds.
/// </summary>
public sealed class TimeSpanJsonSecondsConverter : JsonConverter<TimeSpan>
{
    /// <inheritdoc/>
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => TimeSpan.FromSeconds(reader.GetInt64());

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        => writer.WriteNumberValue(Math.Round(value.TotalSeconds));
}
