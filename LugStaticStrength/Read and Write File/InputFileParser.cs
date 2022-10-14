using System;
using System.Collections.Generic;
using System.IO;

namespace LugStaticStrength
{
    public static class InputFileParser
    {
        private const int geometryDataStartRow = 1;
        private const int materialDataStartRow = 11;
        private const int loadCaseDataStartRow = 21;

        public static AnalysisInput Parse(string fullFilePath)
        {
            Excel excel = new Excel(fullFilePath);

            Lug lug = GetLug(excel);

            List<LoadCase> loadCases = GetLoadCasesList(excel);

            excel.Close();

            return new AnalysisInput(lug, loadCases);
        }

        private static Material ParseMaterial(Excel excel)
        {
            try
            {
                string title = excel.ReadCell(materialDataStartRow + 1, 2);

                MaterialTypes type = (MaterialTypes)Enum.Parse(typeof(MaterialTypes), excel.ReadCell(materialDataStartRow + 2, 2));

                double[,] strengthData = excel.ReadRangeInDoubleArray(materialDataStartRow + 3, 2, materialDataStartRow + 4, 2);
                double ultimateTensileStrength = strengthData[0, 0];
                double ultimateShearStrength = strengthData[1, 0];

                return new Material(1, title, type, ultimateTensileStrength, ultimateShearStrength);
            }
            catch
            {
                throw new InvalidDataException($"Input lug material data is invalid!");
            }
        }

        private static Lug GetLug(Excel excel)
        {
            try
            {
                string partType = excel.ReadCell(geometryDataStartRow + 1, 2);
                double[,] geometryData = excel.ReadRangeInDoubleArray(geometryDataStartRow + 2, 2, geometryDataStartRow + 6, 2);
                double holeDiameter = geometryData[0, 0];
                double thickness = geometryData[1, 0];
                double edgeMargin = geometryData[2, 0];
                double width = geometryData[3, 0];
                double taperHalfAngle = geometryData[4, 0];

                return new Lug(1,
                               ParseMaterial(excel),
                               holeDiameter,
                               thickness,
                               edgeMargin,
                               width,
                               taperHalfAngle);
            }
            catch
            {
                throw new InvalidDataException($"Input lug geometry data is invalid!");
            }
        }

        private static List<LoadCase> GetLoadCasesList(Excel excel)
        {
            List<LoadCase> loadCases = new List<LoadCase>();

            int loadCaseDataCurrentRow = loadCaseDataStartRow + 2;

            while (!excel.IsCellEmpty(loadCaseDataCurrentRow, 1))
            {
                loadCases.Add(GetLoadCase(excel, loadCaseDataCurrentRow));

                loadCaseDataCurrentRow++;
            }

            return loadCases;
        }

        private static LoadCase GetLoadCase(Excel excel, int loadCaseDataCurrentRow)
        {
            try
            {
                int id = int.Parse(excel.ReadCell(loadCaseDataCurrentRow, 1));
                string title = excel.ReadCell(loadCaseDataCurrentRow, 2).ToString();
                double[,] loadData = excel.ReadRangeInDoubleArray(loadCaseDataCurrentRow, 3, loadCaseDataCurrentRow, 4);
                double axialLoad = loadData[0, 0];
                double transverseLoad = loadData[0, 1];

                return new LoadCase(id, title, new Load(axialLoad, transverseLoad));
            }
            catch
            {
                throw new InvalidDataException($"Input load cases data is invalid!");
            }
        }
    }
}
