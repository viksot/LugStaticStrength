using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using System;

namespace LugStaticStrength
{
    internal class Excel
    {
        public string Path { get; set; }

        private ExcelPackage _excelFile;

        public ExcelWorksheets Worksheets { get; set; }

        public ExcelWorksheet ActiveWorksheet { get; set; }

        public Excel(string filePath)
        {
            try
            {
                Path = filePath;

                _excelFile = new ExcelPackage(Path);
            }
            catch
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            if (_excelFile.Workbook.Worksheets.Count == 0)
                _excelFile.Workbook.Worksheets.Add("Sheet1");

            Worksheets = _excelFile.Workbook.Worksheets;
            ActiveWorksheet = Worksheets[0];
        }

        public string ReadCell(int rowIndex, int columnIndex)
        {
            if (!IsCellEmpty(rowIndex, columnIndex))
            {
                return ActiveWorksheet.Cells[rowIndex, columnIndex].Value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public string[,] ReadRange(int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            int rowsCount = endRowIndex - startRowIndex + 1;

            int columnsCount = endColumnIndex - startColumnIndex + 1;

            string[,] result = new string[rowsCount, columnsCount];

            for (int rowIndex = startRowIndex; rowIndex <= endRowIndex; rowIndex++)
            {
                List<string> row = ActiveWorksheet.Cells[rowIndex, startColumnIndex, rowIndex, endColumnIndex]
                                            .Select(cell => cell.Value == null ? string.Empty : cell.Value.ToString())
                                            .ToList();

                for (int i = 0; i < row.Count; i++)
                {
                    result[rowIndex - startRowIndex, i] = row[i];
                }
            }

            return result;
        }

        public double[,] ReadRangeInDoubleArray(int startRowIndex, int startColumnIndex, int endRowIndex, int endColumnIndex)
        {
            string[,] stringArray = ReadRange(startRowIndex, startColumnIndex, endRowIndex, endColumnIndex);

            double[,] result = new double[stringArray.GetLength(0), stringArray.GetLength(1)];

            for (int i = 0; i < stringArray.GetLength(0); i++)
            {
                for (int j = 0; j < stringArray.GetLength(1); j++)
                {
                    try
                    {
                        result[i, j] = double.Parse(stringArray[i, j]);
                    }
                    catch
                    {
                        throw new FormatException($"Unable to covert string \"{stringArray[i, j]}\" to double");
                    }
                }
            }

            return result;
        }

        public bool IsCellEmpty(int rowIndex, int columnIndex)
        {
            return ActiveWorksheet.Cells[rowIndex, columnIndex].Value == null;
        }

        public int LastNotEmptyRowIndex()
        {
            return ActiveWorksheet.Dimension.Rows;
        }

        public void WriteCell(int rowIndex, int columnIndex, object value)
        {
            ActiveWorksheet.Cells[rowIndex, columnIndex].Value = value;
        }

        public void WriteArrayInRange(int rowIndex, int columnIndex, object[,] values)
        {
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    WriteCell(rowIndex + i, columnIndex + j, values[i, j]);
                }
            }
        }

        public void SaveAndClose()
        {
            Save();

            Close();
        }

        public void Save()
        {
            _excelFile.Save();
        }

        public void Close()
        {
            _excelFile.Dispose();
        }
    }
}




