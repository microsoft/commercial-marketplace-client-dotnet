// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Core.TestFramework
{
    public class TestRecordingMismatchException : Exception
    {
        public TestRecordingMismatchException()
        {
        }

        public TestRecordingMismatchException(string message) : base(message)
        {
        }

        public TestRecordingMismatchException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
