using System;
using Tests.Puffix.Exceptions.Samples.Sample;
using Xunit;

namespace Tests.Puffix.Exceptions
{
    public class ExceptionTest
    {
        [Fact]
        public void TestBasicExceptionGeneration()
        {
            const string expectedErrorMessage = "The provided value ('Provided') is not the one expected ('Expected').";
            const string expectedValue = "Expected";
            const string providedValue = "Provided";
            
            var error = new InvalidValueException(providedValue, expectedValue);

            Assert.NotNull(error);
            Assert.Null(error.InnerException);
            Assert.Equal(expectedErrorMessage, error.Message);
        }

        [Fact]
        public void TestOuterExceptionGeneration()
        {
            const string expectedErrorMessage = "The provided value ('Provided') is not the one expected ('Expected').";
            const string expectedOuterErrorMessage = "An error occured while checking the provided value ('Provided').";
            const string expectedValue = "Expected";
            const string providedValue = "Provided";

            var error = new InvalidValueException(providedValue, expectedValue);

            var outerError = new CheckProvidedValueException(error, providedValue);

            Assert.NotNull(outerError);
            Assert.NotNull(outerError.InnerException);
            Assert.Equal(expectedOuterErrorMessage, outerError.Message);
            Assert.Equal(expectedErrorMessage, outerError.InnerException.Message);
        }

        [Fact]
        public void TestExceptionWithNoMessage()
        {
            const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message unavailable. Exception type: Tests.Puffix.Exceptions.Samples.Sample.NoMessageSampleException.";

            var error = new NoMessageSampleException();

            Assert.NotNull(error);
            Assert.Equal(expectedErrorMessage, error.Message);
        }

        [Fact]
        public void TestOuterExceptionWithNoMessage()
        {
            const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message unavailable. Exception type: Tests.Puffix.Exceptions.Samples.Sample.NoMessageSampleException.";
            const string expectedOuterErrorMessage = "FATAL - Puffix.Exceptions : Message unavailable. Exception type: Tests.Puffix.Exceptions.Samples.Sample.NoMessageSampleOuterException.";

            var error = new NoMessageSampleException();
            var outerError = new NoMessageSampleOuterException(error);

            Assert.NotNull(outerError);
            Assert.NotNull(outerError.InnerException);
            Assert.Equal(expectedOuterErrorMessage, outerError.Message);
            Assert.Equal(expectedErrorMessage, outerError.InnerException.Message);
        }

        // TODO add test with error in pattern
    }
}
