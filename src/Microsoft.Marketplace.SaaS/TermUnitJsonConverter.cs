// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for license information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.Marketplace.SaaS.Models
{
    /// <summary> Serializes a <see cref="TermUnit"/> using its marketplace term value. </summary>
    public sealed class TermUnitJsonConverter : JsonConverter<TermUnit>
    {
        /// <inheritdoc />
        public override TermUnit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var inputValue = reader.GetString();

            return new TermUnit(inputValue);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, TermUnit value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
