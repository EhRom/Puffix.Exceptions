using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Puffix.Exceptions;

/// <summary>
/// Base class for exception managment in .Net applications.
/// </summary>
/// <remarks>This class is extended i your own code. This class loads automatically the error message pattern from the full exception class name, and build the error message
/// with the provided parameters. Error message patterns are stored in en embedded resource file (RexX file).</remarks>
[Serializable]
public abstract class BaseException : ApplicationException
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="resourceManagerType">Resource Manager type.</param>
    /// <param name="exceptionType">Type of the exception.</param>
    /// <param name="logger">Logger instance.</param>
    /// <param name="errorMessageArguments">Arguments to build the error message.</param>
    protected BaseException(Type resourceManagerType, Type exceptionType, ILog logger, params object[] errorMessageArguments)
        : base(BuildErrorMessage(resourceManagerType, exceptionType, logger, errorMessageArguments))
    {
        // Log automatique de l'erreur.
        logger.Error(this);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="resourceManagerType">Resource Manager type.</param>
    /// <param name="exceptionType">Type of the exception.</param>
    /// <param name="logger">Logger instance.</param>
    /// <param name="innerException">Inner exception.</param>
    /// <param name="errorMessageArguments">Arguments to build the error message.</param>
    protected BaseException(Type resourceManagerType, Type exceptionType, ILog logger, Exception innerException, params object[] errorMessageArguments)
        : base(BuildErrorMessage(resourceManagerType, exceptionType, logger, errorMessageArguments), innerException)
    {
        // Log automatique de l'erreur.
        logger.Error(this);
    }

    /// <summary>
    /// Build the error message..
    /// </summary>
    /// <param name="resourceManagerType">Resource Manager type.</param>
    /// <param name="exceptionType">Type of the exception.</param>
    /// <param name="logger">Logger instance.</param>
    /// <param name="errorMessageArguments">Arguments to build the error message.</param>
    /// <returns>Message d'erreur.</returns>
    private static string BuildErrorMessage(Type resourceManagerType, Type exceptionType, ILog logger, params object[] errorMessageArguments)
    {
        // Get the assembly culture.
        CultureInfo neutralCulture = GetAssemblyCulture();

        // Throw exception if the resource manager is not specified or if the exception type is not defined. It is a bug in the exception definition.
        if (resourceManagerType == null)
            throw new ArgumentNullException(BuildFatalMessage(logger, neutralCulture, "InvalidResourcesException"));
        if (exceptionType == null)
            throw new ArgumentNullException(BuildFatalMessage(logger, neutralCulture, "UnknownExceptionTypeMessage"));

        // Get the error message pattern from the resources.
        string messagePattern;
        try
        {
            messagePattern = GetMessagePatternFromRessources(resourceManagerType, exceptionType, neutralCulture);
        }
        catch (MissingManifestResourceException)
        {
            // The Resource Manager is not available.
            return BuildFatalMessage(logger, neutralCulture, "UnavailableResourceManagerPattern", resourceManagerType.FullName);
        }
        catch (InvalidOperationException)
        {
            // The Resource Manager is not available.
            return BuildFatalMessage(logger, neutralCulture, "UnavailableResourceManagerPattern", resourceManagerType.FullName);
        }

        // Check if the error message pattern was found. If not, a default message is returned.
        if (string.IsNullOrEmpty(messagePattern))
            return BuildFatalMessage(logger, neutralCulture, "DefaultMessagePattern", exceptionType.FullName);

        try
        {
            // Build the error message with the arguments.
            return string.Format(messagePattern, errorMessageArguments);
        }
        catch (FormatException)
        {
            // Error while formatting the message. A default message is returned.
            return BuildFatalMessage(logger, neutralCulture, "InvalidMessagePattern", exceptionType.FullName, messagePattern);
        }
    }

    /// <summary>
    /// Get the assembly culture.
    /// </summary>
    /// <returns>Current assembly culture.</returns>
    private static CultureInfo GetAssemblyCulture()
    {
        // Get the executing assembly.
        Assembly currentAssembly = Assembly.GetExecutingAssembly();

        // Load the NeutralResourcesLanguage attribute.
        var neutralCultureAttribute = currentAssembly.GetCustomAttribute<NeutralResourcesLanguageAttribute>();

        // Extract the culture value.
        CultureInfo neutralCulture = null;
        if (neutralCultureAttribute != null)
            neutralCulture = new CultureInfo(neutralCultureAttribute.CultureName);

        return neutralCulture;
    }

    /// <summary>
    /// Build and log fatal error message (when regular behavior enounters errors).
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="neutralCulture">Neutral culture to retrieve the ressources.</param>
    /// <param name="resourceName">Resource name.</param>
    /// <param name="resourceParams">Arguments to build the message.</param>
    /// <returns>Message construit.</returns>
    private static string BuildFatalMessage(ILog logger, CultureInfo neutralCulture, string resourceName, params object[] resourceParams)
    {
        // Get the resource (message pattern) in the BaseException resource manager.
        string resourceValue = neutralCulture == null ?
            BaseExceptionResources.ResourceManager.GetString(resourceName) :
            BaseExceptionResources.ResourceManager.GetString(resourceName, neutralCulture);

        // Build message if required.
        if (resourceValue != null && resourceParams != null && resourceParams.Length > 0)
            resourceValue = string.Format(resourceValue, resourceParams);

        // Log message.
        logger.Fatal(resourceValue);

        return resourceValue;
    }

    /// <summary>
    /// Recherche du pattern de message d'erreurs dans les ressources.
    /// </summary>
    /// <param name="resourceManagerType">Resource Manager type.</param>
    /// <param name="exceptionType">Type of the exception.</param>
    /// <param name="currentCulture">Current culture.</param>
    /// <returns>Message pattern mathcing with the error.</returns>
    private static string GetMessagePatternFromRessources(Type resourceManagerType, Type exceptionType, CultureInfo currentCulture)
    {
        // Load resource manager.
        ResourceManager resourceManager = new ResourceManager(resourceManagerType.FullName, resourceManagerType.Assembly);

        // Looking for the message in the provided resource maanger. 2 mode are available : complete type name, short type name (class name).
        string resourceName = exceptionType.Name;
        string resourceValue = currentCulture == null ? resourceManager.GetString(resourceName) : resourceManager.GetString(resourceName, currentCulture);
        if (string.IsNullOrEmpty(resourceValue))
        {
            resourceName = exceptionType.FullName;
            resourceValue = currentCulture == null ? resourceManager.GetString(resourceName) : resourceManager.GetString(resourceName, currentCulture);
        }

        return resourceValue;
    }

    /////// <summary>
    /////// Load the resource manager containing the customized error message.
    /////// </summary>
    /////// <param name="resourceManagerType">Resource Manager type.</param>
    /////// <param name="currentCulture">Current culture.</param>
    /////// <returns>Reousrce manager containing error message pattenrs.</returns>
    ////private static ResourceManager LoadExceptionResourceManager(Type resourceManagerType, CultureInfo currentCulture)
    ////{
    ////    // HACK: the resource manager is directly instanciated to keep control on the complete name of the base type. For files associated with exception classes container, the 
    ////    // HACK : Instanciation d'un Resource Manager pour contrôler le nom complet du type de base. Pour les fichiers associés aux 
    ////    // classes d'exception il faut suffixer le nom par Base (nom du type de l'exception de base), sinon les ressources ne sont pas 
    ////    // accessibles. Comme les ressources peuvent être externalisées dans un autre fichier, non associé à la classe d'exception de base, 
    ////    // on garde la possibilité d'instancier un Resource Manager sans le suffixer par base (dans les blocs 'catch').
    ////    ResourceManager resourceManager;
    ////    try
    ////    {
    ////        string resourceManagerTypeName = resourceManagerType.FullName;
    ////        resourceManager = new ResourceManager(resourceManagerTypeName, resourceManagerType.Assembly);
    ////        if (currentCulture == null)
    ////            resourceManager.GetString(RESOURCE_NAME_CHECKER);
    ////        else
    ////            resourceManager.GetString(RESOURCE_NAME_CHECKER, currentCulture);
    ////    }
    ////    catch (MissingManifestResourceException)
    ////    {
    ////        resourceManager = new ResourceManager(resourceManagerType.FullName, resourceManagerType.Assembly);
    ////    }
    ////    catch (InvalidOperationException)
    ////    {
    ////        resourceManager = new ResourceManager(resourceManagerType.FullName, resourceManagerType.Assembly);
    ////    }

    ////    return resourceManager;
    ////}
}
