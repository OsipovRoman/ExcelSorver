using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelSolver.Enums;

namespace ExcelSolver.Services
{
    public class DerivativesService
    {
        /// <summary>
        /// Вычисление значений производных для каждой из переменных методом конечно разностой апроксимации
        /// </summary>
        /// <param name="g">Функция для которой нужно вычислить значений производной</param>
        /// <param name="x">Значения точек в которых нужно вычислить производную</param>
        /// <param name="sheme">Тип схемы апрокисмации</param>
        /// <param name="h">шаг пространственной сетки</param>
        /// <returns>Массив значений производных функций в точках</returns>
        public double[] DerivativeValues(Func<double[], double> function, double[] x, ApproximationScheme sheme = ApproximationScheme.Explicit, double h = 0.0001)
        {
            double[] derivativeValuesForX = new double[x.Length];
            double[] xPlusH = new double[x.Length];
            double[] xMinusH = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                derivativeValuesForX[i] = DerivativeValue(function, x, i, sheme, h);
            }

            return derivativeValuesForX;
        }

        /// <summary>
        /// Вычисление значений производной методом конечно разностой апроксимации
        /// </summary>
        /// <param name="g">Функция для которой нужно вычислить значений производной</param>
        /// <param name="x">Значения точек в которых нужно вычислить производную</param>
        /// <param name="index">Индекс переменной для которой нужно вычислить производную</param>
        /// <param name="sheme">Тип схемы апрокисмации</param>
        /// <param name="h">шаг пространственной сетки</param>
        /// <returns>Массив значений производных функций в точках</returns>
        private double DerivativeValue(Func<double[], double> function, double[] x, int index, ApproximationScheme sheme = ApproximationScheme.Explicit, double h = 0.0001)
        {
            double derivativeValue = 0;
            double[] xPlusH = new double[x.Length];
            double[] xMinusH = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                if (i == index)
                {
                    xPlusH[i] = x[i] + h;
                    xMinusH[i] = x[i] - h;
                }
                else
                {
                    xPlusH[i] = x[i];
                    xMinusH[i] = x[i];
                }
            }

            double functionValuePlusH = function(xPlusH);
            double functionValueMinusH = function(xMinusH);
            double functionValue = 0;
            if (sheme != ApproximationScheme.Explicit)
                functionValue = function(x);

            switch (sheme)
            {
                case ApproximationScheme.Explicit:
                    derivativeValue = ExplicitderivativeValues(functionValuePlusH, functionValueMinusH, h);
                    break;
                case ApproximationScheme.Implicit:
                    derivativeValue = ImplicitderivativeValues(functionValuePlusH, functionValueMinusH, functionValue);
                    break;
                case ApproximationScheme.SemiExplicit:
                    derivativeValue = (ExplicitderivativeValues(functionValuePlusH, functionValueMinusH, h) - ImplicitderivativeValues(functionValuePlusH, functionValueMinusH, functionValue)) / 2;
                    break;
                default:
                    throw new NotImplementedException("Получени тип вычисление производной для которой не реализовано вычисление.");
            }

            return derivativeValue;
        }

        /// <summary>
        /// Вычисление значения производной явным методом
        /// </summary>
        /// <param name="valuePlusH">Значение функции в точке X плюс приращение</param>
        /// <param name="valueMinusH">Значение функции в точке X минус приращение</param>
        /// <param name="h">Размер приращения</param>
        /// <returns>Значение приращения</returns>
        private double ExplicitderivativeValues(double valuePlusH, double valueMinusH, double h)
        {
            return (valuePlusH - valueMinusH) / (2 * h);
        }

        /// <summary>
        /// Вычисление значения производной не явным методом
        /// </summary>
        /// <param name="valuePlusH">Значение функции в точке X плюс приращение</param>
        /// <param name="valueMinusH">Значение функции в точке X минус приращение</param>
        /// <param name="value">Значение функции в точке X</param>
        /// <returns>Значение приращения</returns>
        private double ImplicitderivativeValues(double valuePlusH, double valueMinusH, double value)
        {
            return valuePlusH - 2 * value + valueMinusH;
        }
    }
}
