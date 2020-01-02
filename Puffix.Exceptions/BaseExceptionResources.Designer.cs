﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Puffix.Exceptions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class BaseExceptionResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BaseExceptionResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Puffix.Exceptions.BaseExceptionResources", typeof(BaseExceptionResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FATAL - Puffix.Exceptions : Message unavailable. Exception type: {0}..
        /// </summary>
        internal static string DefaultMessagePattern {
            get {
                return ResourceManager.GetString("DefaultMessagePattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FATAL - Puffix.Exceptions : Message is not valid and can not be formatted. Exception type: {0}. Pattern: {1}..
        /// </summary>
        internal static string InvalidMessagePattern {
            get {
                return ResourceManager.GetString("InvalidMessagePattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FATAL - Puffix.Exceptions : The resources container is not valid..
        /// </summary>
        internal static string InvalidResourcesException {
            get {
                return ResourceManager.GetString("InvalidResourcesException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FATAL - Puffix.Exceptions : The resource manager &apos;{0}&apos; has not been found..
        /// </summary>
        internal static string UnavailableResourceManagerPattern {
            get {
                return ResourceManager.GetString("UnavailableResourceManagerPattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FATAL - Puffix.Exceptions : The exception type is not known, the exception can not be handled..
        /// </summary>
        internal static string UnknownExceptionTypeMessage {
            get {
                return ResourceManager.GetString("UnknownExceptionTypeMessage", resourceCulture);
            }
        }
    }
}
