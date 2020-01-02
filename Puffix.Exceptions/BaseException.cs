using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;

namespace Puffix.Exceptions
{
    /// <summary>
    /// Base class for exception managment in .Net applications.
    /// </summary>
    /// <remarks>This class is extended i your own code. This class loads automatically the error message pattern from the full exception class name, and build the error message
    /// with the provided parameters. Error message patterns are stored in en embedded resource file (RexX file).</remarks>
    [Serializable]
    public abstract class BaseException : ApplicationException
    {
        /// <summary>
        /// Nom de ressource utilisé pour vérifier la validité du Resource Manager.
        /// </summary>
        private const string RESOURCE_NAME_CHECKER = "ResourceAvailabilityChecker";

        /// <summary>
        /// Constructeur pour la sérialisation (technique, NE PAS MODIFIER).
        /// </summary>
        /// <param name="info">Informations de sérialisation.</param>
        /// <param name="context">Contexte.</param>
        protected BaseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="resourceManagerType">Type du conteneur de ressouces.</param>
        /// <param name="exceptionType">Type de l'exception.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="errorMessageArguments">Arguments pour le formattage du message d'erreur.</param>
        protected BaseException(Type resourceManagerType, Type exceptionType, ILog logger, params object[] errorMessageArguments)
            : base(BuildErrorMessage(resourceManagerType, exceptionType, logger, errorMessageArguments))
        {
            // Log automatique de l'erreur.
            logger.Error(this);
        }

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="resourceManagerType">Type du conteneur de ressouces.</param>
        /// <param name="exceptionType">Type de l'exception.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="innerException">Erreur originelle.</param>
        /// <param name="errorMessageArguments">Arguments pour le formattage du message d'erreur.</param>
        protected BaseException(Type resourceManagerType, Type exceptionType, ILog logger, Exception innerException, params object[] errorMessageArguments)
            : base(BuildErrorMessage(resourceManagerType, exceptionType, logger, errorMessageArguments), innerException)
        {
            // Log automatique de l'erreur.
            logger.Error(this);
        }

        /// <summary>
        /// Construction du mesage d'erreur.
        /// </summary>
        /// <param name="resourceManagerType">Type du conteneur de ressouces.</param>
        /// <param name="exceptionType">Type de l'exception.</param>
        /// <param name="log">Logger.</param>
        /// <param name="errorMessageArguments">Arguments pour le formattage du message d'erreur.</param>
        /// <returns>Message d'erreur.</returns>
        private static string BuildErrorMessage(Type resourceManagerType, Type exceptionType, ILog log, params object[] errorMessageArguments)
        {
            // Récupération de la culture de l'assembly.
            CultureInfo neutralCulture = GetAssemblyCulture();

            // Si le conteneur de ressources est nul, on renvoie une erreur car il s'agit d'un bug dans la définition de l'exception.
            if (resourceManagerType == null)
                throw new ArgumentNullException(BuildFatalMessage(log, neutralCulture, "InvalidResourcesException"));

            // Si le type d'exception est nul, on renvoie une erreur car il s'agit d'un bug dans la définition de l'exception.
            if (exceptionType == null)
                throw new ArgumentNullException(BuildFatalMessage(log, neutralCulture, "UnknownExceptionTypeMessage"));

            // Recuperation du pattern dans les ressources.
            string messagePattern;
            try
            {
                messagePattern = GetMessagePatternFromRessources(resourceManagerType, exceptionType, neutralCulture);
            }
            catch (InvalidOperationException)
            {
                // Le Resource Manager n'a pas pu être instancié.
                return BuildFatalMessage(log, neutralCulture, "UnavailableResourceManagerPattern", resourceManagerType.FullName);
            }

            // On verifie si le pattern de message a été trouvé.
            if (string.IsNullOrEmpty(messagePattern))
                return BuildFatalMessage(log, neutralCulture, "DefaultMessagePattern", exceptionType.FullName);

            try
            {
                return string.Format(messagePattern, errorMessageArguments);
            }
            catch (FormatException)
            {
                return BuildFatalMessage(log, neutralCulture, "InvalidMessagePattern", exceptionType.FullName);
            }
        }

        /// <summary>
        /// Récupération de la culture de la librairie.
        /// </summary>
        /// <returns>Culture de la librairie courante.</returns>
        private static CultureInfo GetAssemblyCulture()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            // Chargement de l'attribut NeutralResourcesLanguage.
            var neutralCultureAttribute = currentAssembly.GetCustomAttribute<NeutralResourcesLanguageAttribute>();

            // Extraction de la valeur.
            CultureInfo neutralCulture = null;
            if (neutralCultureAttribute != null)
                neutralCulture = new CultureInfo(neutralCultureAttribute.CultureName);

            return neutralCulture;
        }

        /// <summary>
        /// Contruction et log d'un message d'erreur.
        /// </summary>
        /// <param name="log">Log.</param>
        /// <param name="neutralCulture">Culture de récupération des ressources.</param>
        /// <param name="resourceName">Nom de la ressource.</param>
        /// <param name="resourceParams">Paramètres pour la construction du message.</param>
        /// <returns>Message construit.</returns>
        private static string BuildFatalMessage(ILog log, CultureInfo neutralCulture, string resourceName, params object[] resourceParams)
        {
            // Récupération de la ressource dans les ressources du gestionnaire des exceptions.
            string resourceValue = neutralCulture == null ?
                BaseExceptionResources.ResourceManager.GetString(resourceName) :
                BaseExceptionResources.ResourceManager.GetString(resourceName, neutralCulture);

            // Contruction du message si nécessaire.
            if (resourceValue != null && resourceParams != null && resourceParams.Length > 0)
                resourceValue = string.Format(resourceValue, resourceParams);

            // Log du message.
            log.Fatal(resourceValue);

            return resourceValue;
        }

        /// <summary>
        /// Recherche du pattern de message d'erreurs dans les ressources.
        /// </summary>
        /// <param name="resourceManagerType">Type du conteneur de ressouces.</param>
        /// <param name="currentCulture">Culture des ressources.</param>
        /// <param name="exceptionType">Type de l'exception.</param>
        /// <returns>Pattern du message d'erreur correspondant.</returns>
        private static string GetMessagePatternFromRessources(Type resourceManagerType, Type exceptionType, CultureInfo currentCulture)
        {
            // Chargement du gestionnaire de ressources.
            ResourceManager resourceManager = LoadExceptionResourceManager(resourceManagerType, currentCulture);

            // Recherche du message dans les ressources passées en paramètre, en fonction du type de l'erreur.
            // Recherche intelligente : 2 cas, type simple, type complet.
            string resourceName = exceptionType.Name;
            string resourceValue = currentCulture == null ? resourceManager.GetString(resourceName) : resourceManager.GetString(resourceName, currentCulture);
            if (string.IsNullOrEmpty(resourceValue))
            {
                resourceName = exceptionType.FullName;
                resourceValue = currentCulture == null ? resourceManager.GetString(resourceName) : resourceManager.GetString(resourceName, currentCulture);
            }

            return resourceValue;
        }

        /// <summary>
        /// Chargement du gestionnaire de ressource contenant les messages d'erreur.
        /// </summary>
        /// <remarks>Cette méthode est exposée pour faciliter le stockage et l'accès aux codes d'erreurs SOAP dans le fichier de ressources
        /// associé à la classe d'exception.</remarks>
        /// <param name="resourceManagerType">Type du conteneur de ressouces.</param>
        /// <param name="currentCulture">Culture cournante.</param>
        /// <returns>Gestionnaire de ressources contenant les ressources sur les exceptions.</returns>
        private static ResourceManager LoadExceptionResourceManager(Type resourceManagerType, CultureInfo currentCulture)
        {
            // HACK : Instanciation d'un Resource Manager pour contrôler le nom complet du type de base. Pour les fichiers associés aux 
            // classes d'exception il faut suffixer le nom par Base (nom du type de l'exception de base), sinon les ressources ne sont pas 
            // accessibles. Comme les ressources peuvent être externalisées dans un autre fichier, non associé à la classe d'exception de base, 
            // on garde la possibilité d'instancier un Resource Manager sans le suffixer par base (dans les blocs 'catch').
            ResourceManager resourceManager;
            try
            {
                // On teste si le nom finit pas Base et on le rajoute si ce n'est pas le cas.
                string resourceManagerTypeName = resourceManagerType.FullName;
                resourceManager = new ResourceManager(resourceManagerTypeName, resourceManagerType.Assembly);
                if (currentCulture == null)
                    resourceManager.GetString(RESOURCE_NAME_CHECKER);
                else
                    resourceManager.GetString(RESOURCE_NAME_CHECKER, currentCulture);
            }
            catch (MissingManifestResourceException)
            {
                resourceManager = new ResourceManager(resourceManagerType.FullName, resourceManagerType.Assembly);
            }
            catch (InvalidOperationException)
            {
                resourceManager = new ResourceManager(resourceManagerType.FullName, resourceManagerType.Assembly);
            }

            return resourceManager;
        }
    }
}
