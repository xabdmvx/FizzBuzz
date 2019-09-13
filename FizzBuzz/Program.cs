using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FizzBuzzProgram.models;
using FizzBuzzProgran.models;

namespace FizzBuzzProgram
{
    class Program
    {
        /// <summary>
        /// version of the program
        /// </summary>
        public const string Version = "1.0.0.0";

        /// <summary>
        /// Application entry point
        /// </summary>
        /// <param name="args">command line arguments passed to the program</param>
        static void Main(string[] args)
        {    
            FizzBuzz myfizzBuzzSerie = new FizzBuzz(args);
            if(myfizzBuzzSerie.Error == FizzBuzz.ErrorType.OK)
            {
                string FizzBuzzVersion = "FizzBuzz v." + Version;
                string FileHeader = "\r\nGenerating The FizzBuzz serie from " + myfizzBuzzSerie.Start.ToString() + " to " + myfizzBuzzSerie.End.ToString() + ".";
                string FileFooter = "\r\nFizzBuzz Serie Printing Completed.";

                if (myfizzBuzzSerie.OutputFilename == null && myfizzBuzzSerie.OutputFilenameFromArgs == null)
                {
                    Console.WriteLine(FizzBuzzVersion);
                    Console.WriteLine(FileHeader);

                    for (int i = myfizzBuzzSerie.Start; i <= myfizzBuzzSerie.End; i++ )
                    {             
                        Console.WriteLine(myfizzBuzzSerie.GetSerieItem(i));
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo cki = new ConsoleKeyInfo();
                            cki = Console.ReadKey(true);
                            Console.WriteLine("Generation of the FizzBuzz Serie was paused by the user. Type 'Q' to Exit.");
                            
                            //Exit if typed 'Q'
                            if(Console.ReadKey().ToString().ToUpper() == "Q") break; 
                        }
                    }

                    Console.WriteLine(FileFooter);
                }
                else
                {
                    Console.WriteLine(FizzBuzzVersion);

                    if (myfizzBuzzSerie.OutputFilename != null && myfizzBuzzSerie.PushSerieToFile(FizzBuzzVersion + FileHeader, FileFooter))
                    {
                        Console.WriteLine(FileFooter);
                        Console.WriteLine("It was generated in the file: " + myfizzBuzzSerie.OutputFilename);
                    }
                    else
                    {
                        string tryFile = myfizzBuzzSerie.OutputFilename;
                        if (tryFile == null) tryFile = myfizzBuzzSerie.OutputFilenameFromArgs;
                        Console.WriteLine("The FizzBuzz serie was not created. There was a Problem creating the file: " + tryFile);
                        if (myfizzBuzzSerie.DebugFlag)
                            Console.WriteLine("See "+ myfizzBuzzSerie.DebugFile + " file for more details.");
                    }
                }
            }
            else
            {
                ErrorLogger errLog = new ErrorLogger(myfizzBuzzSerie.Error);                
                Console.Write(errLog.ErrorMessage());
            }
        }
    }
}
