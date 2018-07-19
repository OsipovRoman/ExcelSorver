using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelSolver.Interfaces;
using ExcelSolver.Enums;
using ExcelSolver.FiniteDifferenceMethod;

namespace ExcelSolver.Services
{
    public class DerivativeMethodFactory
    {
        /// <summary>
        /// Заданная схема аппроксимации
        /// </summary>
        private static ApproximationScheme _sheme = ApproximationScheme.Explicit;

        /// <summary>
        /// Установка схемы аппроксимации
        /// </summary>
        /// <param name="sheme">схема аппроксимации</param>
        public static void SetSheme(ApproximationScheme sheme)
        {
            _sheme = sheme;
        }

        /// <summary>
        /// Получить метод расчета производной
        /// </summary>
        /// <returns>Объект предоставляющий методы расчета производной</returns>
        public static IDerivativeMethod GetMethod()
        {
            switch (_sheme)
            {
                case ApproximationScheme.Explicit:
                    return new ExplicitFiniteDifferenceMethod();
                case ApproximationScheme.Implicit:
                    return new ImplicitFiniteDifferenceMethod();
                case ApproximationScheme.SemiExplicit:
                    return new SemiExplicitFiniteDifferenceMethod();
                default:
                    throw new NotImplementedException("Получена тип расчета производной для которого нет варианта реализации");
            }
        }
    }
}
