using Puffix.Exceptions;
using Puffix.Exceptions.Basic;
using System;
using System.Runtime.Serialization;

namespace $rootnamespace$
{
    /// <summary>
    /// Base class for exeption management for the class <c>$fileinputname$.</c>.
    /// </summary>
	/// <remarks>The error message patterns are stored in the file $fileinputname$ExceptionsResources.resx. The key is the exception class name.</remarks>
    [Serializable]
    #pragma warning disable S3376
    public abstract class $fileinputname$Exceptions : BaseException
    #pragma warning restore S3376
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private static readonly ILog log = EmptyLog.GetLogger(typeof($fileinputname$Exceptions));

        /// <summary>
        /// Constructor for basic exceptions.
        /// </summary>
        /// <param name="exceptionType">Child exception type.</param>
		/// <param name="messageParams">Error message parameters.</param>
        protected $fileinputname$Exceptions(Type exceptionType, params object[] messageParams)
            : base(typeof($fileinputname$ExceptionsResources), exceptionType, log, messageParams)
        { }

        /// <summary>
        /// Constructor for encapsulated exceptions.
        /// </summary>
        /// <param name="exceptionType">Child exception type.</param>
		/// <param name="innerException">Inner exception.</param>
		/// <param name="messageParams">Error message parameters.</param>
        protected $fileinputname$Exceptions(Type exceptionType, Exception innerException, params object[] messageParams)
            : base(typeof($fileinputname$ExceptionsResources), exceptionType, log, innerException, messageParams)
        { }
		
		#region Exception serialization.
		/// <summary>
        /// Constructor for the serialization (DO NOT MODIFY).
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Context.</param>
        protected $fileinputname$Exceptions(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
		#endregion
    }
}

namespace $rootnamespace$.$fileinputname$
{
	// Add your exceptions here. You can use PuffixExcepetion and PuffixInnerException code snippets.
}
