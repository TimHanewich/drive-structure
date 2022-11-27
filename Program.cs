using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using TimHanewich.Toolkit;
using TimHanewich.Toolkit.CommandLine;
using ConsoleVisuals;

namespace DriveStructure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CommandLineArguments clargs = CommandLineArguments.Parse(args);

            if (clargs.Contains("map"))
            {
                map(args);
            }
        }


        public static void map(string[] args)
        {
            CommandLineArguments clargs = CommandLineArguments.Parse(args);



            ArgumentValue path = clargs.Arguments[1];
            string pathstr = path.Label;
            
            Console.Write("Maping directory '");
            ConsoleVisualsToolkit.Write(pathstr, ConsoleColor.Cyan);
            Console.Write("'... ");
            HanewichTimer ht = new HanewichTimer();
            ht.StartTimer();
            Directory d = Directory.Map(pathstr);
            ht.StopTimer();
            ConsoleVisualsToolkit.WriteLine("Success in " + ht.GetElapsedTime().TotalSeconds.ToString("#,##0.0") + " seconds", ConsoleColor.Green);

            //Print it
            Console.WriteLine();
            ConsoleVisualsToolkit.WriteLine(JsonConvert.SerializeObject(d, Formatting.Indented), ConsoleColor.Green);
            
        }
        
    }
}