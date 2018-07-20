using System;
using ExcelSolver.Enums;
using ExcelSolver.Interfaces;

namespace ExcelSolver.Services
{
    public class DerivativesService: IDerivativesService
    {
        /// <summary>
        /// Сервис предоставляющий реализацию расчет производной
        /// </summary>
        private readonly IDerivativeMethod derivativeMethod = DerivativeMethodFactory.GetMethod();

        /// <summary>
        /// Вычисление значений производных для каждой из переменных методом конечно разностной аппроксимации
        /// </summary>
        /// <param name="g">Функция для которой нужно вычислить значений производной</param>
        /// <param name="x">Значения точек в которых нужно вычислить производную</param>
        /// <param name="h">шаг пространственной сетки</param>
        /// <returns>Массив значений производных функций в точках</returns>
        public double[] DerivativeValues(Func<double[], double> function, double[] x, double h = 0.0001)
        {
            double[] derivativeValuesForX = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                derivativeValuesForX[i] = derivativeMethod.DerivativeValue(function, x, i, h);
            }

            return derivativeValuesForX;
        }
    }
}
