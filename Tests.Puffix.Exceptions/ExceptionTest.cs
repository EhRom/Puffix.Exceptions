using Tests.Puffix.Exceptions.Samples.NoResourceSample;
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

        [Fact]
        public void TestExceptionWithInvalidPattern()
        {
            const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.InvalidPatternException. Pattern: The provided value ('{0') is not valid..";

            var error = new InvalidPatternException();

            Assert.NotNull(error);
            Assert.Equal(expectedErrorMessage, error.Message);
        }

        [Fact]
        public void TestOuterExceptionWithInvalidPattern()
        {
            const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.InvalidPatternException. Pattern: The provided value ('{0') is not valid..";
            const string expectedOuterErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.InvalidPatternOuterException. Pattern: The provided value ('{0') is not valid..";

            var error = new InvalidPatternException();
            var outerError = new InvalidPatternOuterException(error);

            Assert.NotNull(outerError);
            Assert.NotNull(outerError.InnerException);
            Assert.Equal(expectedOuterErrorMessage, outerError.Message);
            Assert.Equal(expectedErrorMessage, outerError.InnerException.Message);
        }

        [Fact]
        public void TestExceptionWithMissingParameter()
        {
            const string expectedValue = "Expected";
            const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.MissingParameterException. Pattern: The provided value ('{0}') is not the one expected ('{1}')..";

            var error = new MissingParameterException(expectedValue);

            Assert.NotNull(error);
            Assert.Equal(expectedErrorMessage, error.Message);
        }

        [Fact]
        public void TestOuterExceptionWithMissingParameter()
        {
            const string expectedValue = "Expected";
            const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.MissingParameterException. Pattern: The provided value ('{0}') is not the one expected ('{1}')..";
            const string expectedOuterErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.MissingParameterOuterException. Pattern: The provided value ('{0}') is not the one expected ('{1}')..";

            var error = new MissingParameterException(expectedValue);
            var outerError = new MissingParameterOuterException(error, expectedValue);

            Assert.NotNull(outerError);
            Assert.NotNull(outerError.InnerException);
            Assert.Equal(expectedOuterErrorMessage, outerError.Message);
            Assert.Equal(expectedErrorMessage, outerError.InnerException.Message);
        }

        [Fact]
        public void TestExceptionWithCorruptedResourceFile()
        {
            const string expectedErrorMessage = "FATAL - Puffix.Exceptions : The resource manager 'Tests.Puffix.Exceptions.Samples.NoResourceSampleExceptionsResources' has not been found.";

            var error = new NoResourceException();

            Assert.NotNull(error);
            Assert.Equal(expectedErrorMessage, error.Message);
        }

        [Fact]
        public void TestOuterExceptionWithCorruptedResourceFile()
        {
            const string expectedErrorMessage = "FATAL - Puffix.Exceptions : The resource manager 'Tests.Puffix.Exceptions.Samples.NoResourceSampleExceptionsResources' has not been found.";

            var error = new NoResourceException();
            var outerError = new NoResourceOuterException(error);

            Assert.NotNull(outerError);
            Assert.NotNull(outerError.InnerException);
            Assert.Equal(expectedErrorMessage, outerError.Message);
            Assert.Equal(expectedErrorMessage, outerError.InnerException.Message);
        }
    }
}
