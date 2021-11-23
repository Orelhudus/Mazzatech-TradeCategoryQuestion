using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace TradeCategoryQuestion.Classes
{
    static class InputFile
    {
        private const string invalidFormat = "File with invalid format!\n";
        private const string emptyFile = "File is empty!\n";


        public static bool ProcessFile(string inputFile, string outputFile)
        {
            // Reading the input file
            string[] lines = File.ReadAllLines(inputFile);

            if (lines.Length == 0)
                throw new ApplicationException(emptyFile + inputFile);

            if (lines.Length < 3)
                throw new ApplicationException(invalidFormat + inputFile);


            // First line -> Reference date
            DateTime referenceDate;

            if (!DateTime.TryParse(lines[0], out referenceDate))
                throw new ApplicationException(invalidFormat + "The first line must have the reference date.");


            // Second line - Number of trades in the portifolio
            int numberOfTrades;

            if (!int.TryParse(lines[1], out numberOfTrades))
                throw new ApplicationException(invalidFormat + "The second line must have the number of trades in portifolio.");

            if ((lines.Length - numberOfTrades - 2) != 0)
                throw new ApplicationException(invalidFormat + "The number of trades in portifolio is incorret.");


            StringBuilder output = new StringBuilder();


            // Trade lines
            for (int i = 2; i < lines.Length; i++)
            {
                Trade trade = new Trade(lines[i].Split(" "));

                // Validate if next payment date is late
                if ((trade.NextPaymentDate - referenceDate).Days > -30)
                {
                    output.AppendLine(trade.Category);
                }
                else
                {
                    output.AppendLine("EXPIRED");
                }
            }


            File.WriteAllText(outputFile, output.ToString());


            return true;
        }

    }
}
