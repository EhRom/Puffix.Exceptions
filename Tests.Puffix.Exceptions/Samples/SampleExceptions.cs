using Puffix.Exceptions;
using Puffix.Exceptions.Basic;
using System;
using System.Runtime.Serialization;

namespace Tests.Puffix.Exceptions.Samples
{
    /// <summary>
    /// Base class for exeption management for the class <c>Sample</c>.
    /// </summary>
	/// <remarks>The error message patterns are stored in the file SampleExceptionsResources.resx. The key is the exception class name</remarks>
    [Serializable]
#pragma warning disable S3376
    public abstract class SampleExceptions : BaseException
#pragma warning restore S3376
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private static readonly ILog log = EmptyLog.GetLogger(typeof(SampleExceptions));

        /// <summary>
        /// Constructor for basic exceptions.
        /// </summary>
        /// <param name="exceptionType">Child exception type.</param>
		/// <param name="messageParams">Error message parameters.</param>
        protected SampleExceptions(Type exceptionType, params object[] messageParams)
            : base(typeof(SampleExceptionsResources), exceptionType, log, messageParams)
        { }

        /// <summary>
        /// Constructor for encapsulated exceptions.
        /// </summary>
        /// <param name="exceptionType">Child exception type.</param>
		/// <param name="innerException">Inner exception.</param>
		/// <param name="messageParams">Error message parameters.</param>
        protected SampleExceptions(Type exceptionType, Exception innerException, params object[] messageParams)
            : base(typeof(SampleExceptionsResources), exceptionType, log, innerException, messageParams)
        { }

        #region Exception serialization.

        /// <summary>
        /// Constructor for the serialization (DO NOT MODIFY).
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Context.</param>
        protected SampleExceptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion
    }
}

namespace Tests.Puffix.Exceptions.Samples.Sample
{
    /// <summary>
    /// Error when a provided value is not the one expected.
    /// </summary>
    [Serializable]
    public sealed class InvalidValueException : SampleExceptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="providedValue">Provided value.</param>
        /// <param name="expectedValue">Expected value.</param>
        public InvalidValueException(string providedValue, string expectedValue)
            : base(typeof(InvalidValueException), providedValue, expectedValue)
        { }

        #region Exception serialization.

        /// <summary>
        /// Constructor for the serialization (DO NOT MODIFY).
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Context.</param>
        public InvalidValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion
    }

    /// <summary>
    /// Error while checking the provided value.
    /// </summary>
    [Serializable]
    public sealed class CheckProvidedValueException : SampleExceptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="innerException">Inner error.</param>
        public CheckProvidedValueException(Exception innerException, string providedValue)
            : base(typeof(CheckProvidedValueException), innerException, providedValue)
        { }

        #region Exception serialization.

        /// <summary>
        /// Constructor for the serialization (DO NOT MODIFY).
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Context.</param>
        public CheckProvidedValueException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion
    }

    /// <summary>
    /// Exception for which the error message is not provided in the resource file.
    /// </summary>
    [Serializable]
    public sealed class NoMessageSampleException : SampleExceptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NoMessageSampleException()
            : base(typeof(NoMessageSampleException))
        { }

        #region Exception serialization.

        /// <summary>
        /// Constructor for the serialization (DO NOT MODIFY).
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Context.</param>
        public NoMessageSampleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion
    }

    /// <summary>
    /// Outer exception for which the error message is not provided in the resource file.
    /// </summary>
    [Serializable]
    public sealed class NoMessageSampleOuterException : SampleExceptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="innerException">Inner error.</param>
        public NoMessageSampleOuterException(Exception innerException)
            : base(typeof(NoMessageSampleOuterException), innerException)
        { }

        #region Exception serialization.

        /// <summary>
        /// Constructor for the serialization (DO NOT MODIFY).
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Context.</param>
        public NoMessageSampleOuterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion
    }

    // Add your exceptions here. You can use PuffixExcepetion and PuffixInnerException code snippets.
}
