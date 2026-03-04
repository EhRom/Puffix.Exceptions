using NUnit.Framework;
using Tests.Puffix.Exceptions.Samples.NoResourceSample;
using Tests.Puffix.Exceptions.Samples.Sample;

namespace Tests.Puffix.Exceptions;

public class ExceptionTest
{
    [Test]
    public void TestBasicExceptionGeneration()
    {
        const string expectedErrorMessage = "The provided value ('Provided') is not the one expected ('Expected').";
        const string expectedValue = "Expected";
        const string providedValue = "Provided";

        InvalidValueException error = new InvalidValueException(providedValue, expectedValue);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(error, Is.Not.Null);
            Assert.That(error.InnerException, Is.Null);
            Assert.That(error.Message, Is.EqualTo(expectedErrorMessage));
        }
    }

    [Test]
    public void TestOuterExceptionGeneration()
    {
        const string expectedErrorMessage = "The provided value ('Provided') is not the one expected ('Expected').";
        const string expectedOuterErrorMessage = "An error occured while checking the provided value ('Provided').";
        const string expectedValue = "Expected";
        const string providedValue = "Provided";

        InvalidValueException error = new InvalidValueException(providedValue, expectedValue);
        CheckProvidedValueException outerError = new CheckProvidedValueException(error, providedValue);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(outerError, Is.Not.Null);
            Assert.That(outerError.InnerException, Is.Not.Null);
            Assert.That(outerError.Message, Is.EqualTo(expectedOuterErrorMessage));
            Assert.That(outerError.InnerException.Message, Is.EqualTo(expectedErrorMessage));
        }
    }

    [Test]
    public void TestExceptionWithNoMessage()
    {
        const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message unavailable. Exception type: Tests.Puffix.Exceptions.Samples.Sample.NoMessageSampleException.";

        NoMessageSampleException error = new NoMessageSampleException();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(error, Is.Not.Null);
            Assert.That(error.Message, Is.EqualTo(expectedErrorMessage));
        }
    }

    [Test]
    public void TestOuterExceptionWithNoMessage()
    {
        const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message unavailable. Exception type: Tests.Puffix.Exceptions.Samples.Sample.NoMessageSampleException.";
        const string expectedOuterErrorMessage = "FATAL - Puffix.Exceptions : Message unavailable. Exception type: Tests.Puffix.Exceptions.Samples.Sample.NoMessageSampleOuterException.";

        NoMessageSampleException error = new NoMessageSampleException();
        NoMessageSampleOuterException outerError = new NoMessageSampleOuterException(error);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(outerError, Is.Not.Null);
            Assert.That(outerError.InnerException, Is.Not.Null);
            Assert.That(outerError.Message, Is.EqualTo(expectedOuterErrorMessage));
            Assert.That(outerError.InnerException.Message, Is.EqualTo(expectedErrorMessage));
        }
    }

    [Test]
    public void TestExceptionWithInvalidPattern()
    {
        const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.InvalidPatternException. Pattern: The provided value ('{0') is not valid..";

        InvalidPatternException error = new InvalidPatternException();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(error, Is.Not.Null);
            Assert.That(error.Message, Is.EqualTo(expectedErrorMessage));
        }
    }

    [Test]
    public void TestOuterExceptionWithInvalidPattern()
    {
        const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.InvalidPatternException. Pattern: The provided value ('{0') is not valid..";
        const string expectedOuterErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.InvalidPatternOuterException. Pattern: The provided value ('{0') is not valid..";

        InvalidPatternException error = new InvalidPatternException();
        InvalidPatternOuterException outerError = new InvalidPatternOuterException(error);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(outerError, Is.Not.Null);
            Assert.That(outerError.InnerException, Is.Not.Null);
            Assert.That(outerError.Message, Is.EqualTo(expectedOuterErrorMessage));
            Assert.That(outerError.InnerException.Message, Is.EqualTo(expectedErrorMessage));
        }
    }

    [Test]
    public void TestExceptionWithMissingParameter()
    {
        const string expectedValue = "Expected";
        const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.MissingParameterException. Pattern: The provided value ('{0}') is not the one expected ('{1}')..";

        MissingParameterException error = new MissingParameterException(expectedValue);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(error, Is.Not.Null);
            Assert.That(error.Message, Is.EqualTo(expectedErrorMessage));
        }
    }

    [Test]
    public void TestOuterExceptionWithMissingParameter()
    {
        const string expectedValue = "Expected";
        const string expectedErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.MissingParameterException. Pattern: The provided value ('{0}') is not the one expected ('{1}')..";
        const string expectedOuterErrorMessage = "FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: Tests.Puffix.Exceptions.Samples.Sample.MissingParameterOuterException. Pattern: The provided value ('{0}') is not the one expected ('{1}')..";

        MissingParameterException error = new MissingParameterException(expectedValue);
        MissingParameterOuterException outerError = new MissingParameterOuterException(error, expectedValue);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(outerError, Is.Not.Null);
            Assert.That(outerError.InnerException, Is.Not.Null);
            Assert.That(outerError.Message, Is.EqualTo(expectedOuterErrorMessage));
            Assert.That(outerError.InnerException.Message, Is.EqualTo(expectedErrorMessage));
        }
    }

    [Test]
    public void TestExceptionWithCorruptedResourceFile()
    {
        const string expectedErrorMessage = "FATAL - Puffix.Exceptions : The resource manager 'Tests.Puffix.Exceptions.Samples.NoResourceSampleExceptionsResources' has not been found.";

        NoResourceException error = new NoResourceException();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(error, Is.Not.Null);
            Assert.That(error.Message, Is.EqualTo(expectedErrorMessage));
        }
    }

    [Test]
    public void TestOuterExceptionWithCorruptedResourceFile()
    {
        const string expectedErrorMessage = "FATAL - Puffix.Exceptions : The resource manager 'Tests.Puffix.Exceptions.Samples.NoResourceSampleExceptionsResources' has not been found.";

        NoResourceException error = new NoResourceException();
        NoResourceOuterException outerError = new NoResourceOuterException(error);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(outerError, Is.Not.Null);
            Assert.That(outerError.InnerException, Is.Not.Null);
            Assert.That(outerError.Message, Is.EqualTo(expectedErrorMessage));
            Assert.That(outerError.InnerException.Message, Is.EqualTo(expectedErrorMessage));
        }
    }
}
