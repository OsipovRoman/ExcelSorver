using System;

namespace ExcelSolver.Interfaces
{
    /// <summary>
    /// Интерфейс предоставляющий методы для расчета производной
    /// </summary>
    public interface IDerivativeMethod
    {
        /// <summary>
        /// Вычисление значений производной методом конечно разностной аппроксимации
        /// </summary>
        /// <param name="g">Функция для которой нужно вычислить значений производной</param>
        /// <param name="x">Значения точек в которых нужно вычислить производную</param>
        /// <param name="index">Индекс переменной для которой нужно вычислить производную</param>
        /// <param name="h">шаг пространственной сетки</param>
        /// <returns>Массив значений производных функций в точках</returns>
        double DerivativeValue(Func<double[], double> function, double[] x, int index, double h = 0.0001);
    }
}
