
using static FizzBuzzProgram.models.FizzBuzz;

namespace FizzBuzzProgran.models
{
    class ErrorLogger
    {
        private ErrorType _type;
        private string ErrorMsg; 
        
        public ErrorLogger(ErrorType type)
        {
            _type = type;
        }

        public string ErrorMessage()
        {
            bool addHelpAppend = false;

            switch (_type)
            {
                case ErrorType.InvalidArgsLength:
                    ErrorMsg = "Invalid number of parameters. ";
                    addHelpAppend = true;
                    break;

                case ErrorType.InvalidArgsFormat:
                    ErrorMsg = "Invalid format to specify the range for the FizzBuzz serie. ";
                    addHelpAppend = true;
                    break;

                case ErrorType.VeryLargeStartRange:
                    ErrorMsg = "The Start of the Range is not Valid. Please try a smaller number. ";
                    break;

                case ErrorType.VeryLargeEndRange:
                    ErrorMsg = "The End of the Range is not Valid. Please try a smaller number. ";
                    break;

                case ErrorType.InvalidRangeStartGreaterThanEnd:
                    ErrorMsg = "Invalid Range, the Start of range is greater than the End of the range. ";
                    break;

                case ErrorType.ShowHelp:
                    ErrorMsg = "This program Prints the BuzzFizz serie.\r\n" +
                                "\r\n" +
                                "BUZZFIZZ [help] \"Start|End\" destination\r\n" +
                                "\r\n" +
                                "help         Shows this help\r\n" +
                                "Start        Start of the Range of the serie.\r\n" +
                                "End          End of the Range of the serie.\r\n" +
                                "destination  Specifies the directory and/or filename for the output file.\r\n" +
                                "debugfile    Specifies the directory and/or filename for the debug file.\r\n" +
                                "\r\n" +
                                "To list the BuzzFizz serie is required to enter the Start|End range, it will \r\n" +
                                "start printing in the Start number and it will finish the serie up to the End\r\n" +
                                " number. It is required to type the pipe character to separate the Start and \r\n" +
                                "the End.\r\n" +
                                "\r\n" +
                                "For example to print the BuzzFizz serie starting in the third item and finishing\r\n" +
                                "in the 9th item it is required to execute the program as next: BUZZFIZZ \"3|5\"\r\n" +
                                "Make noticed that it is important to enclose the range between double quotes.\r\n";
                    break;

                case ErrorType.OK:
                    ErrorMsg = "BuzzFizz. FizzBuzz. BuzzFizz. BuzzFizz. FizzBuzz. BuzzFizz. BuzzFizz. FizzBuzz. BuzzFizz. ";
                    break;

                default:
                    ErrorMsg = "Unkown Error. ";
                    addHelpAppend = true;
                    break;
            }
            if(addHelpAppend)
                ErrorMsg += "\r\nType 'FizzBuzz help' for instructions about how it works.\r\n";

            return ErrorMsg;
        }

    }
}
