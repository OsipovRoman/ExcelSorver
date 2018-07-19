using System;
using ExcelSolver.Enums;

namespace ExcelSolver.Interfaces
{
    /// <summary>
    /// Интерфейс предоставляющий описания методов расчета массива производных для каждой переменной
    /// </summary>
    public interface IDerivativesService
    {
        /// <summary>
        /// Вычисление значений производных для каждой из переменных методом конечно разностной аппроксимации
        /// </summary>
        /// <param name="g">Функция для которой нужно вычислить значений производной</param>
        /// <param name="x">Значения точек в которых нужно вычислить производную</param>
        /// <param name="sheme">Тип схемы аппроксимации</param>
        /// <param name="h">шаг пространственной сетки</param>
        /// <returns>Массив значений производных функций в точках</returns>
        double[] DerivativeValues(Func<double[], double> function, double[] x, ApproximationScheme sheme = ApproximationScheme.Explicit, double h = 0.0001)

    }
}
