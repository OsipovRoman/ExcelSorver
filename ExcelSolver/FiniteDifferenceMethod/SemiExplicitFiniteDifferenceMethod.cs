using System;

namespace ExcelSolver.FiniteDifferenceMethod
{
    /// <summary>
    /// Реализует полу явный вариант конечно разностной аппроксимации
    /// </summary>
    public class SemiExplicitFiniteDifferenceMethod : AbstractFiniteDifferenceMethod
    {
        /// <summary>
        /// Вычисление значений производной методом конечно разностной аппроксимации
        /// </summary>
        /// <param name="g">Функция для которой нужно вычислить значений производной</param>
        /// <param name="x">Значения точек в которых нужно вычислить производную</param>
        /// <param name="index">Индекс переменной для которой нужно вычислить производную</param>
        /// <param name="h">шаг пространственной сетки</param>
        /// <returns>Массив значений производных функций в точках</returns>
        public override double DerivativeValue(Func<double[], double> function, double[] x, int index, double h = 0.0001)
        {
            double functionValuePlusH = function(GetValueXPlusH(x, index, h));
            double functionValueMinusH = function(GetValueXMinusH(x, index, h));
            double functionValue = function(x);

            return (ExplicitDerivativeValues(functionValuePlusH, functionValueMinusH, h) 
                + ImplicitDerivativeValues(functionValuePlusH, functionValueMinusH, functionValue)) / 2;
        }
    }
}
