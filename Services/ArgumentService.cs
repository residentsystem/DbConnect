using System;

namespace DbConnect.Services
{
    public class ArgumentService
    {
        public void ParsingArgumentString(String[] args)
        {
            // Test if multiple arguments were supplied as a string.
            foreach(string arg in args)
            {
                bool IsArgumentString = int.TryParse(arg, out int argument);

                if (IsArgumentString) {
                    Console.WriteLine("Type Error: Argument must be a string.");
                    break;
                }  
            }
        }

        public void ParsingSingleArgumentString(string singlearg)
        {
            // Test if a single argument was supplied as a string.
            bool IsArgumentString = int.TryParse(singlearg, out int argument);

            if (IsArgumentString){
                Console.WriteLine("Type Error: Argument must be a string.\n");
            } 
        }

        public int ParsingArguments(String[] args)
        {
            // The right amount of arguments have to be supplied.
            if (args.Length < 2) {
                Console.WriteLine("\nMissing argument(s): Please supply all arguments.");
                ParsingArgumentString(args);
                return 1;
            }
            else if (args.Length > 2) {
                Console.WriteLine("\nToo many arguments: Please supply a maximum of 2 arguments.");
                ParsingArgumentString(args);
                return 1;
            }           

            // Validate all values if the correct amount of arguments have been supplied.
            if (args.Length == 2) {

                if (!(args[0] == "-ping" || args[0] == "-sql")) {
                    Console.WriteLine("\nValue Error: Please specify a valid command name.");
                    ParsingArgumentString(args);
                    return 1;
                }
            }
            // Return if parsed correctly
            return 0;
        }
    }
}