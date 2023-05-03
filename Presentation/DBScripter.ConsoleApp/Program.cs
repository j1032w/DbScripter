//*****************************************************************
//DBScripterCmd - Copyright (C) 2015 Jim.1032w@gmail.com
//Version: 1.0.8

//        http://dbscirptercmd.codeplex.com

//This program comes with ABSOLUTELY NO WARRANTY.
//This is free software, and you are welcome to
//redistribute it and/or modify it under the terms of
//GNU General Public License version 2 (GPLv2).
//****************************************************************




// DBScripterCmd.exe localhost sa test AdventureWorks2008R2 "d:\output"
// DBScripterCmd.exe win2008 sa test AdventureWorks "d:\output"

using System;
using CommonLibrary;
using DBScripter.CompositionRoot;
using DBScripter.ConsoleApp.Properties;
using DBScripter.Service;
using Microsoft.Practices.Unity;

namespace DBScripter.ConsoleApp
{
    class Program
    {
        private static void Main(string[] args)
        {

#if DEBUG
            if (args.Length == 0)
            {
                args = new[] { "localhost", "sa", "test", "AdventureWorks2008R2", @"d:\output" };    
            }
            
#endif


            Resources.Message_VersionInfo.ConsoleWhite();
            "\n".ConsoleGray();

            if (args.Length < 5) // At least 5 arguments requires. ServerName Username Password DatabaseName OutputPath
            {
                OutputUsage();
                Environment.Exit((int)ExitCode.InvalidArgs);
                
            }
          

            using ( var theContainer = new UnityContainer() )
            {
                try
                {
                    theContainer.AddNewExtension<DBScripterContainerExtension>();
                    var theScripterController = theContainer.Resolve<ScripterController>();

                    
                    Resources.StartMessage.ConsoleCyan();
                    "\n".ConsoleGray();

                    theScripterController.Script(args);

                    Resources.EndMessage.ConsoleGreen();
                }
                catch (Exception e)
                {
                    "\n".ConsoleGray();
                    "************************ Error: ******************************".ConsoleRed();

                    e.ToString().ConsoleYellow();
                    "\n".ConsoleGray();
                    
                    Resources.Message_Note.ConsoleGray();
                    "\n".ConsoleGray();

                    

                    Environment.Exit( (int)ExitCode.UnknownError );
                    return;
                }
            }

            
            Environment.Exit( (int)ExitCode.Success ); 

        }



        

        private static void OutputUsage()
        {
            Resources.Message_Example.ConsoleCyan();
            "\n".ConsoleGray();
            
            Resources.Message_Usage.ConsoleGray();
            "\n".ConsoleGray();
            
            Resources.Message_Note.ConsoleGray();
            "\n".ConsoleGray();
            
        }

    }
}