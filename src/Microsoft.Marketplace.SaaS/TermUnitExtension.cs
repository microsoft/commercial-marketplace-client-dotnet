// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.Marketplace.SaaS.Models
{
    /// <summary> Provides display formatting for marketplace term units. </summary>
    public static class TermUnitExtension
    {
        private static readonly Regex YearsMonthsPattern = new Regex(
            @"^P(?:(?<years>\d+)Y)?(?:(?<months>\d+)M)?$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary> Formats year/month durations for display and returns unrecognized values unchanged. </summary>
        /// <param name="termUnit">The marketplace term unit.</param>
        /// <returns>The formatted duration or the original term value.</returns>
        public static string ToDisplayString(this TermUnit termUnit)
        {
            var raw = termUnit.ToString() ?? string.Empty;
            var match = YearsMonthsPattern.Match(raw);
            if (!match.Success || (!match.Groups["years"].Success && !match.Groups["months"].Success))
            {
                return raw;
            }

            var years = match.Groups["years"].Success ? int.Parse(match.Groups["years"].Value) : 0;
            var months = match.Groups["months"].Success ? int.Parse(match.Groups["months"].Value) : 0;

            var parts = new List<string>();
            if (years > 0)
            {
                parts.Add($"{years} year{(years == 1 ? string.Empty : "s")}");
            }

            if (months > 0)
            {
                parts.Add($"{months} month{(months == 1 ? string.Empty : "s")}");
            }

            return parts.Count > 0 ? string.Join(" ", parts) : raw;
        }
    }
}
