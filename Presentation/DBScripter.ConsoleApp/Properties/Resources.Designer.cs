﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBScripter.ConsoleApp.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DBScripter.ConsoleApp.Properties.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///Export completed OK..
        /// </summary>
        public static string EndMessage {
            get {
                return ResourceManager.GetString("EndMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error: .
        /// </summary>
        public static string Message_Error {
            get {
                return ResourceManager.GetString("Message_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXAMPLE:
        ///  DBScripterCmd.exe localhost sa test AdventureWorks2008R2 &quot;d:\output&quot;.
        /// </summary>
        public static string Message_Example {
            get {
                return ResourceManager.GetString("Message_Example", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to NOTE:
        ///   .Net Framework 4.5.1 is required to run this program.
        ///   At present this program supports Microsoft SQL Server 2014, 2012, 2008, 2005 and 2000, and
        ///   database objects: table, stored procedure, view, user defined function, aggregate, data type, table type, type.
        ///   Email: Jim.1032w@gmail.com.
        /// </summary>
        public static string Message_Note {
            get {
                return ResourceManager.GetString("Message_Note", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to USAGE:
        ///  DBScripterCmd.exe &lt;server&gt; &lt;uername&gt; &lt;password&gt; &lt;database&gt; &lt;output folder&gt; [s][t][v][u]
        ///
        ///where
        ///  s        Stored Procedure
        ///  t        Table
        ///  v        view
        ///  u        User Defined Object (Aggregate, Function, Data Type, Table Type, Type)
        ///  Empty    All.
        /// </summary>
        public static string Message_Usage {
            get {
                return ResourceManager.GetString("Message_Usage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to *****************************************************************
        ///DBScripterCmd - Copyright (C) 2015 Jim.1032w@gmail.com
        ///Version: 1.0.8
        ///        
        ///        http://dbscirptercmd.codeplex.com
        ///
        ///This program comes with ABSOLUTELY NO WARRANTY. 
        ///This is free software, and you are welcome to redistribute it 
        ///and/or modify it under the terms of 
        ///GNU General Public License version 2 (GPLv2).    
        ///****************************************************************
        ///.
        /// </summary>
        public static string Message_VersionInfo {
            get {
                return ResourceManager.GetString("Message_VersionInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///Starting DBScripter, it may take several minutes . . ..
        /// </summary>
        public static string StartMessage {
            get {
                return ResourceManager.GetString("StartMessage", resourceCulture);
            }
        }
    }
}
