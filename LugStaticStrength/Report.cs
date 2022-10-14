using System.Linq;

namespace LugStaticStrength
{
    public static class Report
    {
        public static void WriteAndSave(AnalysisOutput analysisOutput, string fullFilePath)
        {
            Excel excel = new Excel(fullFilePath);

            WriteGeometry(excel, analysisOutput, 1);

            WriteMaterial(excel, analysisOutput, 11);

            WriteFailureModesStrengthFactors(excel, analysisOutput, 21);

            WriteLoadCasesOutput(excel, analysisOutput, 31);

            WriteCriticalLoadCase(excel, analysisOutput, 33 + analysisOutput.LoadCasesOutput.Count + 3);

            SetCellsStyle(excel, analysisOutput);

            excel.SaveAndClose();
        }

        private static void SetCellsStyle(Excel excel, AnalysisOutput analysisOutput)
        {
            SetColumnsWidth(excel, analysisOutput);

            SetColumnsHorizontalAlignment(excel, analysisOutput);

            SetCellsMerge(excel, analysisOutput);

            SetRowsVerticalAlignment(excel);

            SetRangeBorders(excel, 1, 1, 7, 2);
            SetRangeBorders(excel, 11, 1, 15, 2);
            SetRangeBorders(excel, 21, 1, 25, 2);
            SetRangeBorders(excel, 31, 1, 33 + analysisOutput.LoadCasesOutput.Count, 3 + analysisOutput.Lug.FailureModes.Count * 2);
            SetRangeBorders(excel, 33 + analysisOutput.LoadCasesOutput.Count + 3, 1, 33 + analysisOutput.LoadCasesOutput.Count + 7, 2);
        }

        private static void SetRangeBorders(Excel excel, int topLeftRow, int topLeftColumn, int bottomRightRow, int bottomRightColumn)
        {
            excel.ActiveWorksheet.Cells[topLeftRow, topLeftColumn, bottomRightRow, bottomRightColumn].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            excel.ActiveWorksheet.Cells[topLeftRow, topLeftColumn, bottomRightRow, bottomRightColumn].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            excel.ActiveWorksheet.Cells[topLeftRow, topLeftColumn, bottomRightRow, bottomRightColumn].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            excel.ActiveWorksheet.Cells[topLeftRow, topLeftColumn, bottomRightRow, bottomRightColumn].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
        }

        private static void SetColumnsWidth(Excel excel, AnalysisOutput analysisOutput)
        {
            excel.ActiveWorksheet.Column(1).Width = 25;
            excel.ActiveWorksheet.Column(2).Width = 15;

            for (int i = 2; i < 3 + analysisOutput.Lug.FailureModes.Count * 2; i++)
            {
                excel.ActiveWorksheet.Column(i + 1).Width = 13;
            }
        }

        private static void SetColumnsHorizontalAlignment(Excel excel, AnalysisOutput analysisOutput)
        {
            for (int i = 0; i < 3 + analysisOutput.Lug.FailureModes.Count * 2; i++)
            {
                excel.ActiveWorksheet.Column(i + 1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            }
        }

        private static void SetCellsMerge(Excel excel, AnalysisOutput analysisOutput)
        {
            excel.ActiveWorksheet.Cells[1, 1, 1, 2].Merge = true; //Geometry
            excel.ActiveWorksheet.Cells[11, 1, 11, 2].Merge = true; //Material
            excel.ActiveWorksheet.Cells[21, 1, 21, 2].Merge = true; //Strength Factors
            excel.ActiveWorksheet.Cells[31, 1, 31, 3 + analysisOutput.Lug.FailureModes.Count * 2].Merge = true; //Load Case Output
            excel.ActiveWorksheet.Cells[32, 1, 33, 1].Merge = true; //ID
            excel.ActiveWorksheet.Cells[32, 2, 33, 2].Merge = true; //Title
            excel.ActiveWorksheet.Cells[32, 3, 33, 3].Merge = true; //Total Load    

            for (int i = 0; i < analysisOutput.Lug.FailureModes.Count; i++) //Failure Modes
            {
                excel.ActiveWorksheet.Cells[32, 4 + 2 * i, 32, 4 + 2 * i + 1].Merge = true;
            }

            excel.ActiveWorksheet.Cells[33 + analysisOutput.LoadCasesOutput.Count + 3, 1, 33 + analysisOutput.LoadCasesOutput.Count + 3, 2].Merge = true; //Critical Load Case            
        }

        private static void SetRowsVerticalAlignment(Excel excel)
        {
            excel.ActiveWorksheet.Row(31).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            excel.ActiveWorksheet.Row(32).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            excel.ActiveWorksheet.Row(33).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        }

        private static void WriteGeometry(Excel excel, AnalysisOutput analysisOutput, int startRowIndex)
        {
            excel.WriteCell(startRowIndex, 1, "Geometry");
            excel.WriteCell(startRowIndex + 1, 1, "Type");
            excel.WriteCell(startRowIndex + 2, 1, "Hole Diameter");
            excel.WriteCell(startRowIndex + 3, 1, "Thickness");
            excel.WriteCell(startRowIndex + 4, 1, "Edge Margin");
            excel.WriteCell(startRowIndex + 5, 1, "Width");
            excel.WriteCell(startRowIndex + 6, 1, "TaperHalfAngle");

            excel.WriteCell(startRowIndex + 1, 2, "Lug");
            excel.WriteCell(startRowIndex + 2, 2, analysisOutput.Lug.HoleDiameter);
            excel.WriteCell(startRowIndex + 3, 2, analysisOutput.Lug.Thickness);
            excel.WriteCell(startRowIndex + 4, 2, analysisOutput.Lug.EdgeMargin);
            excel.WriteCell(startRowIndex + 5, 2, analysisOutput.Lug.Width);
            excel.WriteCell(startRowIndex + 6, 2, analysisOutput.Lug.TaperHalfAngle);
        }

        private static void WriteMaterial(Excel excel, AnalysisOutput analysisOutput, int startRowIndex)
        {
            excel.WriteCell(startRowIndex, 1, "Material");
            excel.WriteCell(startRowIndex + 1, 1, "Title");
            excel.WriteCell(startRowIndex + 2, 1, "Type");
            excel.WriteCell(startRowIndex + 3, 1, "Ultimate Tensile Strength");
            excel.WriteCell(startRowIndex + 4, 1, "Ultimate Shear Strength");

            excel.WriteCell(startRowIndex + 1, 2, analysisOutput.Lug.Material.Title);
            excel.WriteCell(startRowIndex + 2, 2, analysisOutput.Lug.Material.Type.ToString());
            excel.WriteCell(startRowIndex + 3, 2, analysisOutput.Lug.Material.UltimateTensileStrength);
            excel.WriteCell(startRowIndex + 4, 2, analysisOutput.Lug.Material.UltimateShearStrength);
        }

        private static void WriteFailureModesStrengthFactors(Excel excel, AnalysisOutput analysisOutput, int startRowIndex)
        {
            excel.WriteCell(startRowIndex, 1, "Failure Modes Strength Factors");

            for (int i = 0; i < analysisOutput.Lug.FailureModes.Count; i++)
            {
                excel.WriteCell(startRowIndex + 1 + i, 1, analysisOutput.Lug.FailureModes[i].GetTitle());
                excel.WriteCell(startRowIndex + 1 + i, 2, analysisOutput.Lug.FailureModes[i].StrengthFactor);
            }
        }

        private static void WriteLoadCasesOutput(Excel excel, AnalysisOutput analysisOutput, int startRowIndex)
        {
            WriteLoadCasesOutputHeaders(excel, analysisOutput, startRowIndex);

            var data = GetLoadCasesOutputData(analysisOutput);

            excel.WriteArrayInRange(startRowIndex + 3, 1, data);
        }

        private static void WriteLoadCasesOutputHeaders(Excel excel, AnalysisOutput analysisOutput, int startRowIndex)
        {
            excel.WriteCell(startRowIndex, 1, "Load Cases Output");
            excel.WriteCell(startRowIndex + 1, 1, "ID");
            excel.WriteCell(startRowIndex + 1, 2, "Title");
            excel.WriteCell(startRowIndex + 1, 3, "Total Load");

            for (int j = 0; j < analysisOutput.Lug.FailureModes.Count; j++)
            {
                excel.WriteCell(startRowIndex + 1, 4 + 2 * j, analysisOutput.Lug.FailureModes[j].GetTitle());
                excel.WriteCell(startRowIndex + 2, 4 + 2 * j, "Allowable Load");
                excel.WriteCell(startRowIndex + 2, 4 + 2 * j + 1, "MS");
            }
        }

        private static object[,] GetLoadCasesOutputData(AnalysisOutput analysisOutput)
        {
            object[,] data = new object[analysisOutput.LoadCasesOutput.Count, 11];

            for (int i = 0; i < analysisOutput.LoadCasesOutput.Count; i++)
            {
                data[i, 0] = analysisOutput.LoadCasesOutput[i].LoadCase.ID;
                data[i, 1] = analysisOutput.LoadCasesOutput[i].LoadCase.Title;
                data[i, 2] = analysisOutput.LoadCasesOutput[i].LoadCase.Load.Total;

                for (int j = 0; j < analysisOutput.LoadCasesOutput[i].FailureModesMargins.Count; j++)
                {
                    data[i, 3 + 2 * j] = analysisOutput.LoadCasesOutput[i].FailureModesMargins[j].FailureMode.AllowableLoad;
                    data[i, 3 + 2 * j + 1] = analysisOutput.LoadCasesOutput[i].FailureModesMargins[j].Value;
                }
            }

            return data;
        }

        private static void WriteCriticalLoadCase(Excel excel, AnalysisOutput analysisOutput, int startRowIndex)
        {
            var criticalLoadCaseOutput = GetCriticalLoadCaseOutput(analysisOutput);

            var minMarginOfSafety = GetMinMarginOfSafety(criticalLoadCaseOutput);

            excel.WriteCell(startRowIndex, 1, "Critical Load Case Data");
            excel.WriteCell(startRowIndex + 1, 1, "Load Case ID");
            excel.WriteCell(startRowIndex + 2, 1, "Load Case Title");
            excel.WriteCell(startRowIndex + 3, 1, "Failure Mode");
            excel.WriteCell(startRowIndex + 4, 1, "Margin of Safety");

            excel.WriteCell(startRowIndex + 1, 2, criticalLoadCaseOutput.LoadCase.ID);
            excel.WriteCell(startRowIndex + 2, 2, criticalLoadCaseOutput.LoadCase.Title);
            excel.WriteCell(startRowIndex + 3, 2, minMarginOfSafety.FailureMode.GetTitle());
            excel.WriteCell(startRowIndex + 4, 2, minMarginOfSafety.Value);
        }

        private static LoadCaseOutput GetCriticalLoadCaseOutput(AnalysisOutput analysisOutput)
        {
            var criticalLoadCaseOutput = analysisOutput.LoadCasesOutput[0];

            var criticalMarginOfSafety = criticalLoadCaseOutput.FailureModesMargins.Min(margin => margin.Value);

            for (int i = 1; i < analysisOutput.LoadCasesOutput.Count; i++)
            {
                var minMarginOfSafety = analysisOutput.LoadCasesOutput[i].FailureModesMargins.Min(margin => margin.Value);

                if (criticalMarginOfSafety > minMarginOfSafety)
                {
                    criticalLoadCaseOutput = analysisOutput.LoadCasesOutput[i];
                    criticalMarginOfSafety = minMarginOfSafety;
                }
            }

            return criticalLoadCaseOutput;
        }

        private static MarginOfSafety GetMinMarginOfSafety(LoadCaseOutput loadCaseOutput)
        {
            var minMarginOfSafety = loadCaseOutput.FailureModesMargins[0];

            for (int i = 1; i < loadCaseOutput.FailureModesMargins.Count; i++)
            {
                var currentMarginOfSafety = loadCaseOutput.FailureModesMargins[i];

                if (minMarginOfSafety.Value > currentMarginOfSafety.Value)
                    minMarginOfSafety = currentMarginOfSafety;
            }

            return minMarginOfSafety;
        }
    }
}
