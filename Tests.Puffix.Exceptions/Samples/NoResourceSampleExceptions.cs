using Puffix.Exceptions;
using Puffix.Exceptions.Basic;
using System;

namespace Tests.Puffix.Exceptions.Samples
{
    /// <summary>
    /// Base class for exeption management for the class <c>NoResourceSample.</c>.
    /// </summary>
	/// <remarks>The error message patterns are stored in the file NoResourceSampleExceptionsResources.resx. The key is the exception class name.</remarks>
    [Serializable]
#pragma warning disable S3376
    public abstract class NoResourceSampleExceptions : BaseException
#pragma warning restore S3376
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private static readonly ILog log = EmptyLog.GetLogger(typeof(NoResourceSampleExceptions));

        /// <summary>
        /// Constructor for basic exceptions.
        /// </summary>
        /// <param name="exceptionType">Child exception type.</param>
		/// <param name="messageParams">Error message parameters.</param>
        protected NoResourceSampleExceptions(Type exceptionType, params object[] messageParams)
            : base(typeof(NoResourceSampleExceptionsResources), exceptionType, log, messageParams)
        { }

        /// <summary>
        /// Constructor for encapsulated exceptions.
        /// </summary>
        /// <param name="exceptionType">Child exception type.</param>
		/// <param name="innerException">Inner exception.</param>
		/// <param name="messageParams">Error message parameters.</param>
        protected NoResourceSampleExceptions(Type exceptionType, Exception innerException, params object[] messageParams)
            : base(typeof(NoResourceSampleExceptionsResources), exceptionType, log, innerException, messageParams)
        { }
    }

    /// <summary>
    /// Invalid class for resource management.
    /// </summary>
    public class NoResourceSampleExceptionsResources { }
}

namespace Tests.Puffix.Exceptions.Samples.NoResourceSample
{
    /// <summary>
    /// Exception with no resource file.
    /// </summary>
    [Serializable]
    public sealed class NoResourceException : NoResourceSampleExceptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public NoResourceException()
            : base(typeof(NoResourceException))
        { }
    }

    /// <summary>
    /// Outer exception with no resource file.
    /// </summary>
    [Serializable]
    public sealed class NoResourceOuterException : NoResourceSampleExceptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="innerException">Inner error.</param>
        public NoResourceOuterException(Exception innerException)
            : base(typeof(NoResourceOuterException), innerException)
        { }
    }
}
