using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TradeCategoryQuestion.Classes;

namespace TradeCategoryQuestion
{
    public class TradeCategoryQuestion
    {
        private const string usageText = "Usage: TRADECATEGORYQUESTION  inputfile.txt outputfile.txt";


        private static string outputFile = string.Empty;
        private static string inputFile = string.Empty;


        [STAThread]
        static void Main(string[] args)
        {
            MainTitle();


            if (Debugger.IsAttached)
            {
                inputFile = @"c:\Temp\TradeCategory - Input.txt";
                outputFile = @"c:\Temp\TradeCategory - Output.txt";
            }
            else
            {
                // Validate program call
                if (args.Length != 2)
                {
                    Console.WriteLine(usageText);
                    ApplicationEnd();
                }


                // Identifies input and output files
                inputFile = args[0].Trim();
                outputFile = args[1].Trim();
            }

            // Sets culture to English
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;


            // Checks if the input file exists
            if (! File.Exists(inputFile))
            {
                Console.WriteLine("WARNING: Input file not found!");
                ApplicationEnd();
            }


            // Checks if the output file exists
            if (File.Exists(outputFile))
            {
                Console.WriteLine("WARNING: Output file already exists!");
                Console.WriteLine(BlankLines(1) + "Do you want to overwrite? (Y)es or (N)o?");

                if (Console.ReadKey(true).Key.ToString().ToUpper() == "N")
                    ApplicationEnd();
            }


            try
            {
                if (InputFile.ProcessFile(inputFile, outputFile))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(BlankLines(2));

                    Console.WriteLine("Process completed successfully!" + BlankLines(1) + "Press any key...");
                    Console.Read();
                }
            }
            catch (Exception ex)
            {
                // An error occurred in the execution
                ExecutionError(ex.Message);
            }


            // Finishing the application
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Environment.Exit(0);
        }



        private static void MainTitle()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;

            Console.WriteLine(BlankLines(2));
            Console.WriteLine("                                                             ");
            Console.WriteLine("          ::.::. Credit Suisse – IT DEV RISK .::.::          ");
            Console.WriteLine("                                                             ");
            Console.WriteLine(BlankLines(3));

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private static string BlankLines(int howMany)
        {
            string blankLines = Environment.NewLine;

            for (int i = 1; i < howMany; i++)
                blankLines += Environment.NewLine;

            return blankLines;
        }

        private static void ApplicationEnd()
        {
            Console.WriteLine(BlankLines(2));
            Console.WriteLine( "Finishing the application." + BlankLines(1) + "Press any key...");
            Console.Read();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Environment.Exit(0);
        }

        private static void ExecutionError(string message)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(BlankLines(2));

            Console.WriteLine(message + BlankLines(1) + "Press any key...");
            Console.Read();
        }
    }

}
