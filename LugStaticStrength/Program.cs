using System;
using System.IO;

namespace LugStaticStrength
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFileFullPath = Path.Combine(Environment.CurrentDirectory, "InputDataTest.xlsx");

            AnalysisInput analysisInput = InputFileParser.Parse(inputFileFullPath);

            AnalysisOutput analysisOutput = Analysis.GetAnalysisOutput(analysisInput);

            string reportFileFullPath = Path.Combine(Environment.CurrentDirectory, "Report.xlsx");

            Report.WriteAndSave(analysisOutput, reportFileFullPath);
        }

    }
}
