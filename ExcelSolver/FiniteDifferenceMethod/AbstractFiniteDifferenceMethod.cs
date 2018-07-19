using System;
using ExcelSolver.Interfaces;

namespace ExcelSolver.FiniteDifferenceMethod
{
    /// <summary>
    /// Абстрактный класс являющийся родительским для семейства классов 
    /// реализующий вычисление производной методов конечно разностных аппроксимаций
    /// </summary>
    public abstract class AbstractFiniteDifferenceMethod: IDerivativeMethod
    {

        /// <summary>
        /// Вычисление значений производной методом конечно разностной аппроксимации
        /// </summary>
        /// <param name="g">Функция для которой нужно вычислить значений производной</param>
        /// <param name="x">Значения точек в которых нужно вычислить производную</param>
        /// <param name="index">Индекс переменной для которой нужно вычислить производную</param>
        /// <param name="h">шаг пространственной сетки</param>
        /// <returns>Массив значений производных функций в точках</returns>
        public abstract double DerivativeValue(Func<double[], double> function, double[] x, int index, double h = 0.0001);

        /// <summary>
        /// Получение массива переменных с увеличенной переменной по которой планируется дифференцирование
        /// </summary>
        /// <param name="x">Массив значений переменных</param>
        /// <param name="index">Индекс переменной по которой планируется дифференцирование</param>
        /// <param name="h">Приращение к дифференцируемой переменной</param>
        /// <returns>значения переменный с увеличенной переменной по которой планируется дифференцирование</returns>
        protected double[] GetValueXPlusH(double[] x, int index, double h = 0.0001)
        {
            double[] xPlusH = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                if (i == index)
                    xPlusH[i] = x[i] + h;
                else
                    xPlusH[i] = x[i];
            }
            return xPlusH;
        }

        /// <summary>
        /// Получение массива переменных с уменьшенной переменной по которой планируется дифференцирование
        /// </summary>
        /// <param name="x">Массив значений переменных</param>
        /// <param name="index">Индекс переменной по которой планируется дифференцирование</param>
        /// <param name="h">Приращение к дифференцируемой переменной</param>
        /// <returns>значения переменный с уменьшенной переменной по которой планируется дифференцирование</returns>
        protected double[] GetValueXMinusH(double[] x, int index, double h = 0.0001)
        {
            return GetValueXPlusH(x, index, -1 * h);
        }

        /// <summary>
        /// Вычисление значения производной явным методом
        /// </summary>
        /// <param name="valuePlusH">Значение функции в точке X плюс приращение</param>
        /// <param name="valueMinusH">Значение функции в точке X минус приращение</param>
        /// <param name="h">Размер приращения</param>
        /// <returns>Значение приращения</returns>
        protected double ExplicitDerivativeValues(double valuePlusH, double valueMinusH, double h)
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
        protected double ImplicitDerivativeValues(double valuePlusH, double valueMinusH, double value)
        {
            return valuePlusH - 2 * value + valueMinusH;
        }
    }
}
