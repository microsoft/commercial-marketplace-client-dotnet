// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Text.Json;
using Microsoft.Marketplace.SaaS.Models;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Microsoft.Marketplace.Tests
{
    [TestFixture]
    public class TermUnitTests
    {
        [Test]
        public void KnownTermUnitRoundTrips()
        {
            TermUnit termUnit = "PAY";

            ClassicAssert.AreEqual(TermUnit.PAY, termUnit);
            ClassicAssert.AreEqual("PAY", termUnit.ToString());
        }

        [Test]
        public void CustomTermUnitRoundTrips()
        {
            var termUnit = new TermUnit("P18M");

            ClassicAssert.AreEqual("P18M", termUnit.ToString());
        }

        [Test]
        public void SubscriptionTermPreservesCustomDuration()
        {
            using var document = JsonDocument.Parse(
                "{\"termUnit\":\"P18M\",\"startDate\":\"2026-01-01T00:00:00Z\",\"endDate\":\"2027-07-01T00:00:00Z\"}");

            var term = SubscriptionTerm.DeserializeSubscriptionTerm(document.RootElement);

            ClassicAssert.AreEqual(new TermUnit("P18M"), term.TermUnit);
        }

        [Test]
        public void TermUnitEqualityIsCaseInsensitive()
        {
            ClassicAssert.AreEqual(new TermUnit("pay"), TermUnit.PAY);
        }

        [TestCase("P18M", "18 months")]
        [TestCase("P1Y6M", "1 year 6 months")]
        [TestCase("P4Y", "4 years")]
        [TestCase("P1Y", "1 year")]
        [TestCase("P1M", "1 month")]
        [TestCase("P2Y3M", "2 years 3 months")]
        public void ToDisplayStringFormatsYearMonthDuration(string value, string expected)
        {
            ClassicAssert.AreEqual(expected, new TermUnit(value).ToDisplayString());
        }

        [Test]
        public void ToDisplayStringFallsBackToRawValue()
        {
            ClassicAssert.AreEqual("PAY", TermUnit.PAY.ToDisplayString());
        }

        [Test]
        public void SystemTextJsonDeserializesRawValue()
        {
            var termUnit = JsonSerializer.Deserialize<TermUnit>("\"PAY\"");

            ClassicAssert.AreEqual(TermUnit.PAY, termUnit);
        }

        [Test]
        public void SystemTextJsonSerializesRawValue()
        {
            var json = JsonSerializer.Serialize(TermUnit.PAY);

            ClassicAssert.AreEqual("\"PAY\"", json);
        }

        [Test]
        public void ConstructorThrowsArgumentNullExceptionForNullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new TermUnit(null));
        }

        [Test]
        public void ConstructorThrowsArgumentExceptionForWhitespaceValue()
        {
            Assert.Throws<ArgumentException>(() => new TermUnit("   "));
        }

        [Test]
        public void SystemTextJsonThrowsForNullToken()
        {
            Assert.Throws<ArgumentNullException>(() => JsonSerializer.Deserialize<TermUnit>("null"));
        }

        [Test]
        public void SystemTextJsonThrowsForEmptyStringToken()
        {
            Assert.Throws<ArgumentException>(() => JsonSerializer.Deserialize<TermUnit>("\"\""));
        }

        [Test]
        public void ToTermUnitThrowsForEmptyStringConsistentWithJsonConverter()
        {
            Assert.Throws<ArgumentException>(() => string.Empty.ToTermUnit());
        }
    }
}
