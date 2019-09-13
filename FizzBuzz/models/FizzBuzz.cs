using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FizzBuzzProgram.models
{
    class FizzBuzz
    {
        //enum for errors
        public enum ErrorType
        {
            OK = 0,
            InvalidArgsLength = 1,
            InvalidArgsFormat = 2,
            VeryLargeStartRange = 3,
            VeryLargeEndRange = 4,
            InvalidRangeStartGreaterThanEnd = 5,
            ShowHelp = 6,
        } 

        //data
        private int _start;
        private int _end;
        private string[] _args;        
        private ErrorType _errType;
        private string _exceptionMessage;
        private string _filename;

        public string RawRange { get; }
        public string ExceptionMessage
        {
            get { return _exceptionMessage;  }
        }
        public ErrorType Error
        {
            get { return _errType;  }
        }
        public int Length
        {
            get { return (_errType == ErrorType.OK) ?_end - _start + 1 : 0; }
        }
        public int Start
        {
            get { return _start; }
        }
        public int End
        {
            get { return _end; }
        }
        public string OutputFilename
        {
            get { return _filename; }
        }
        public string OutputFilenameFromArgs { get; private set; }

        public bool DebugFlag { get; private set;  }
        public string DebugFile { get; private set; }

        const string DEBUGFILE = "Debug.txt"; 

        //logic
        public FizzBuzz(string[] consoleArgs)
        {
            _args = consoleArgs;
            
            //default values
            _start = 0;
            _end = 0;
            _errType = ErrorType.OK;
            _exceptionMessage = "";
            _filename = null;

            RawRange = "";
            DebugFlag = false;
            DebugFile = DEBUGFILE;
            OutputFilenameFromArgs = null;

            //validate arguments
            ValidateArguments();

            //If no issues continue to validate Range string 
            if (_errType != ErrorType.OK) return;
            
            // Since we know first argument exist,
            // we can Set the Raw Range String
            RawRange = _args[0];

            //CHeck for help
            ValidateHelpString();

            //If no help string it will continue 
            if (_errType != ErrorType.OK) return;
            
            ValidateRangeString();
            //if no issues continue to parse the range string
            if (_errType != ErrorType.OK) return;

            ParseRangeString();

            ParseDestinationFileName();

        }

        public bool PushSerieToFile(string FileHeader, string FileFooter)
        {
            bool result = true;

            try
            {
                string fileName = OutputFilename;
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine(FileHeader);
                    for (int i = Start; i <= End; i++)
                    {
                        sw.WriteLine(GetSerieItem(i));
                    }
                    sw.WriteLine(FileFooter);
                }

            }
            catch (Exception Ex)
            {
                result = false;

                //here add a logger
                using (StreamWriter debugsw = File.CreateText(DebugFile))
                {
                    debugsw.WriteLine("Program: FizzBuzz.cs");
                    debugsw.WriteLine("Method: PushSerieToFile");
                    debugsw.WriteLine("Exception:");
                    debugsw.WriteLine("Message: " + Ex.Message);
                    debugsw.WriteLine("InnerException: " + Ex.InnerException);
                    debugsw.WriteLine("");

                    DebugFlag = true;
                }                          
        }

            return result;
        }

        private void ParseDestinationFileName()
        {
            //check if the filename was specified
            //the output file should have been specified in the second argument 
            if (_args.Length >= 2)
            {
                //get destination filename
                string fileName = _args[1];
                OutputFilenameFromArgs = fileName;

                //check if the extension is txt, if not force it
                int extPos = fileName.LastIndexOf(".");
                string ext = "";
                if(extPos>=0) fileName.Substring(extPos);
                if (ext != ".txt") fileName += ".txt";               

                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    _filename = fileName;
                }
                else
                {
                    try
                    {
                        using (StreamWriter sw = File.CreateText(fileName));
                        //StreamWriter sw = File.CreateText(fileName);
                     
                        //if not exception then it is a valid filename
                        //we were just checking
                        _filename = fileName;

                        //delete file that was just created!
                        File.Delete(fileName);
                    }
                    catch (Exception Ex)
                    {
                        //here add a logger
                        //here add a logger
                        using (StreamWriter debugtxt = File.CreateText(DebugFile))
                        {
                            debugtxt.WriteLine("Program: FizzBuzz.cs");
                            debugtxt.WriteLine("Method: ParseDestinationFileName");
                            debugtxt.WriteLine("Exception:");
                            debugtxt.WriteLine("Message: " + Ex.Message);
                            debugtxt.WriteLine("InnerException: " + Ex.InnerException);
                            debugtxt.WriteLine("");

                            DebugFlag = true;
                        }
                    }
                }
            }

            if (_args.Length >= 3)
            {
                //get destination filename
                string fileName = _args[2];               

                //check if the extension is txt, if not force it
                int extPos = fileName.LastIndexOf(".");
                string ext = "";
                if (extPos >= 0) fileName.Substring(extPos);
                if (ext != ".txt") fileName += ".txt";

                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    DebugFile = fileName;
                }
                else
                {
                    try
                    {
                        using (StreamWriter sw = File.CreateText(fileName)) ;
                        //StreamWriter sw = File.CreateText(fileName);

                        //if not exception then it is a valid filename
                        //we were just checking
                        DebugFile = fileName;

                        //delete file that was just created!
                        File.Delete(fileName);
                    }
                    catch (Exception Ex)
                    {
                        //here add a logger
                        //here add a logger
                        using (StreamWriter debugtxt = File.CreateText(DebugFile))
                        {
                            debugtxt.WriteLine("Program: FizzBuzz.cs");
                            debugtxt.WriteLine("Method: ParseDestinationFileName");
                            debugtxt.WriteLine("Exception:");
                            debugtxt.WriteLine("Message: " + Ex.Message);
                            debugtxt.WriteLine("InnerException: " + Ex.InnerException);
                            debugtxt.WriteLine("");

                            DebugFlag = true;
                        }
                    }
                }
            }

        }

        public bool IsRangeValid()
        {
            return (_errType == ErrorType.OK);
        }


        /// <summary>
        /// Validate the input arguments
        /// </summary>
        /// <param name="args"></param>
        private void ValidateArguments()
        {        
            if (_args.Length == 0)
            {
                _errType = ErrorType.InvalidArgsLength;
            }
        }

        /// <summary>
        /// Validate the input arguments
        /// </summary>
        /// <param name="args"></param>
        private void ValidateRangeString()
        {
            string RangeStr = RemoveDoubleQuotesIfAny(RawRange); 
            var inputRegEx = new Regex(@"\d+\|\d+", RegexOptions.ECMAScript);

            if (inputRegEx.Match(RangeStr).ToString() != RangeStr)
            {
                //"The Number must start with numbers then pipe and ends with numbers",
                _errType = ErrorType.InvalidArgsFormat;
            }
        }

        /// <summary>
        /// Validate the input arguments checking for Help string
        /// </summary>
        /// <param name="args"></param>
        private void ValidateHelpString()
        {
            string RangeStr = RemoveDoubleQuotesIfAny(RawRange);
            
            if (RangeStr == "help" || RangeStr == "/help" || RangeStr.Equals("-help"))
            {
                //Retuns a Help Error Code
                _errType = ErrorType.ShowHelp;
            }
        }


        /// <summary>
        /// Remove the double quotes if they where added to enclose the range
        /// </summary>
        /// <param name="rangeStr">range string</param>
        /// <returns>the input string without double quotes</returns>
        /// <remarks>it will remove the quotes only if the string starts and ends with double quotes, otherwise it will return the same string</remarks>
        private string RemoveDoubleQuotesIfAny(string rangeStr)
        {
            if (rangeStr.StartsWith("\"") && rangeStr.EndsWith("\"") && rangeStr.Length > 2)
            {
                rangeStr = rangeStr.Substring(1, rangeStr.Length - 2);
            }

            return rangeStr;
        }

        /// <summary>
        /// Parse a valid range and set the start and end limits for the serie
        /// </summary>
        private void ParseRangeString()
        {
            string RangeStr = RemoveDoubleQuotesIfAny(RawRange);

            int start = 0;
            int end = 0;

            //validate that the number enteres is supported by the Int32
            try
            {
                //The maximum integer number supported by Int16 is Int16.MaxValue = 32767
                //I used this data type on propose, to trigger the Exception
                start = Convert.ToInt16(RangeStr.Substring(0, RangeStr.IndexOf("|")));
            }
            catch (Exception Ex)
            {
                _exceptionMessage = Ex.Message + " " + Ex.InnerException;
                _errType = ErrorType.VeryLargeStartRange;
                return;
            }

            //validate that the number enteres is supported by the Int32
            try
            {
                end = Convert.ToInt16(RangeStr.Substring(RangeStr.IndexOf("|")+1));
            }
            catch (Exception Ex)
            {
                _exceptionMessage = Ex.Message + " " + Ex.InnerException;
                _errType = ErrorType.VeryLargeEndRange;
                return;
            }

            // Validate that start is greater than end in Range
            if( start > end)
            {
                _errType = ErrorType.InvalidRangeStartGreaterThanEnd;
                return;
            }

            //if we pass the validations setting the values
            _start = Convert.ToInt32(start);
            _end = Convert.ToInt32(end);
           
        }

        public string[] GetSerie()
        {
            //This implementation works for Arraysize lower than pow(2,31) -> 2GB

            int ArraySize = _end - _start + 1;
            string[] FizzBuzzSerie = new string[ArraySize]; 

            for(int i = 0 ; i < ArraySize ; i++)
            {
                //set number to compute or print
                int num = i + _start;
                
                //BuzzFizz logic
                string PrintString = "";
                if (num % 3 == 0) PrintString = "Fizz";
                if (num % 5 == 0) PrintString += "Buzz";
                if(PrintString == "")
                {
                    PrintString = num.ToString();
                }

                FizzBuzzSerie[i] = PrintString;
            }

            return FizzBuzzSerie;
        }

        public string GetSerieItem(int num)
        {
            string FizzBuzzItem = "";
            if (num >= _start && num <= _end)
            {
                //BuzzFizz logic
                if (num % 3 == 0) FizzBuzzItem = "Fizz";
                if (num % 5 == 0) FizzBuzzItem += "Buzz";
                if (FizzBuzzItem == "")
                {
                    FizzBuzzItem = num.ToString();
                }
            }

            return FizzBuzzItem;
        }
    }
}
