﻿using System;
using System.Text;
using System.Reflection;


using bcg_system_verification.verifiers;
using bcg_system_verification.http;

namespace bcg_system_verification
{
    class Program
    {
        static void Main(string[] args)
        {

            UnhandledExceptionHandler handler = new UnhandledExceptionHandler();

            AppDomain.CurrentDomain.UnhandledException += 
                new UnhandledExceptionEventHandler(
                    handler.Application_Exception);
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            
            //Startup
  

            Globals.init();
            Arguments.check(args);

            //Primary App
            App();
        }

        private static void App(){
            Console.WriteLine("System verification starting...\n");
            //Connect to web server to grab uuid and ip
            get.start();

            firewall.Verify();
            antivirus.Verify();
            network.Verify();

            post.final();
            Console.WriteLine("System verification complete.");

            //Debugging console
            if (Globals.debugMode) Debug.results();         
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("bcg_system_verification.Newtonsoft.Json.dll"))
            {
                byte[] assemblyData = new byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }
    }

    /// 
    /// Handles an unhandled exception.
    /// 
    internal class UnhandledExceptionHandler
    {
        /// 
        /// Handles the exception.
        /// 
        public void Application_Exception(
            object sender, UnhandledExceptionEventArgs args)
        {

            Exception e = (Exception)args.ExceptionObject;

            string errorMessage = "";
            
            // If debugging is enabled print more details message
            if (Globals.debugMode)
            {
                    errorMessage = "UnhandledException:\n\n" +
                    e.Message + "\n\n" +
                    e.GetType() + "\n\nStack Trace:\n" +
                    e.StackTrace;
            } else
            {
                    errorMessage = e.Message;
            }

            Console.WriteLine("System verification failed.\n");
            Console.WriteLine(errorMessage);
            Console.WriteLine("\nPress <Enter> to close the window.");
            Console.Read();

            // Prevents annoying Windows 7 crash message
            Environment.Exit(1);
        }

    } // End UnhandledExceptionHandler
}
