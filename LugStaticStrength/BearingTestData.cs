using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LugStaticStrength
{
    internal static class BearingTestData
    {
        /// <summary>
        /// Словарь данных кривых, ключом которого является параметр кривой, а значением - лист с  коэффициентами соответствующего полинома
        /// </summary>
        public static Dictionary<double, List<double>> _dataCurves;

        static BearingTestData()
        {
            Excel excel = new Excel(Path.Combine(Environment.CurrentDirectory, "BearingStrengthFactorTestData.xlsx"));

            //Таблица коэффициентов полиномов с шапкой из параметров кривых
            double[,] dataCurvesInfo = excel.ReadRangeInDoubleArray(2, 2, 9, 8);

            _dataCurves = GetDataCurves(dataCurvesInfo);

            excel.Close();
        }

        /// <summary>
        /// Получение словаря с данными кривых
        /// </summary>
        /// <returns></returns>
        private static Dictionary<double, List<double>> GetDataCurves(double[,] dataCurvesInfo)
        {
            Dictionary<double, List<double>> dataCurves = new Dictionary<double, List<double>>();

            for (int i = 0; i < dataCurvesInfo.GetLength(1); i++)
            {
                double curveKey = dataCurvesInfo[0, i];

                List<double> dataCurvePolynomialСoefficients = new List<double>();

                //Шапку таблицы пропускаем - это параметры кривых
                for (int j = 1; j < dataCurvesInfo.GetLength(0); j++)
                {
                    dataCurvePolynomialСoefficients.Add(dataCurvesInfo[j, i]);
                }

                dataCurves.Add(curveKey, dataCurvePolynomialСoefficients);
            }

            return dataCurves;
        }

        public static double GetStrengthFactor(Lug lug)
        {
            double holeDiameterToThickness = (double)lug.HoleDiameter / lug.Thickness;

            if (holeDiameterToThickness < 2)
                holeDiameterToThickness = 2;

            double edgeMarginToHoleDiameter = lug.EdgeMargin/ lug.HoleDiameter;

            if (holeDiameterToThickness > _dataCurves.Keys.Last())
            {
                throw new ArgumentException($"Hole Diameter - to - thickness parameter {holeDiameterToThickness} is out of test data range");
            }

            double lowCurveKey = _dataCurves.Keys.ToList().Where(key => key <= holeDiameterToThickness).Last();

            double highCurveKey = _dataCurves.Keys.ToList().Where(key => key >= holeDiameterToThickness).First();


            return TestDataHandler.GetLinearInterpolatedValue(
                        new DataPoint(lowCurveKey, TestDataHandler.GetPolynomialValue(_dataCurves[lowCurveKey], edgeMarginToHoleDiameter)),
                        new DataPoint(highCurveKey, TestDataHandler.GetPolynomialValue(_dataCurves[highCurveKey], edgeMarginToHoleDiameter)),
                        holeDiameterToThickness);                      
        }

    }
}
