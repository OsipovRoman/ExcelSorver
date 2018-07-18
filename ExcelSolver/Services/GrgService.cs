using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelSolver.Enums;

namespace ExcelSolver.Services
{
    public class GrgService
    {
        /// <summary>
        /// Сервис предоставляющй возможность рассчета производных
        /// </summary>
        private static readonly DerivativesService derivativesService;

        /// <summary>
        /// Подсчитывает значения функций ограничений и целевой функции
        /// </summary>
        /// <param name="g">массив функций</param>
        /// <param name="x">массив значений</param>
        /// <returns>Массив значений ограничений и целевой функции</returns>
        private static double[] gCompute(Func<double[], double>[] g, double[] x)
        {
            List<double> result = new List<double>();
            foreach (Func<double[], double> gi in g)
                result.Add(gi(x));

            return result.ToArray();
        }

        /// <summary>
        /// Вычисление значений производных для каждой из переменных методом конечно разностой апроксимации
        /// </summary>
        /// <param name="g">Функция для которой нужно вычислить значений производной</param>
        /// <param name="x">Значения точек в которых нужно вычислить производную</param>
        /// <param name="sheme">Тип схемы апрокисмации</param>
        /// <param name="h">шаг пространственной сетки</param>
        /// <returns>Массив значений производных функций в точках</returns>
        private static double[][] parsh(Func<double[], double>[] function, double[] x, ApproximationScheme sheme = ApproximationScheme.Explicit, double h = 0.0001)
        {
            double[][] derivativeValuesForXForFunc = new double[function.Length][];
            double[] xPlusH = new double[x.Length];
            double[] xMinusH = new double[x.Length];

            for (int i = 0; i < function.Length; i++)
            {
                derivativeValuesForXForFunc[i] = derivativesService.DerivativeValues(function[i], x, sheme, h);
            }

            return derivativeValuesForXForFunc;
        }

        /// <summary>
        /// Вычисляет Basis Inverse
        /// </summary>
        /// <param name="iCandidate">список допустимых переменных</param>
        /// <param name="n"></param>
        /// <param name="g">Список функций с ограничениями и минимизируемой функции</param>
        /// <param name="x">Список значений переменных</param>
        /// <param name="LV">индекс базовой переменной на ее границе</param>
        private static void computeBasisInverse(double[] iCandidate, int n, Func<double[], double>[] g, double[] x, int LV)
        {
            int ICT = 0;
            int[] ignore = new int[n];
            for (int i = 0; i < ignore.Length; i++)
                ignore[i] = 0;

            //количество ограничений привязки
            int NB = 0;

            //Определить индексы привязки и строго соблюдаемые ограничения.


            parsh(g, x);

            if (NB != 0)
            {
                //Хранить градиенты связанных ограничений в массиве TAB

                //сортировать переменные в порядке возрастания Z (J)

                //IREM - список строк, еще не
                int[] IREM = new int[NB];
                for (int i = 0; i < NB; i++)
                    IREM[i] = i + 1;
                //NREM - количество строк, еще не развернутых в
                int NREM = NB;

                bool flagLV = true;

                if (LV != 0)
                {
                    //label variable leaving basic inadmissible
                    //Флаг не позволяющий менять базовую переменную
                    flagLV = false;
                }

                int mode = 1;

                if (iCandidate.Length == n)
                    mode = 2;

                do
                {
                    ICT = ICT + 1;
                    if (mode == 2)
                    {
                        for (int i = 0; i < ignore.Length; i++)
                            ignore[i] = 0;
                    }

                    if (ICT > NREM)
                    {
                        ICT = 0;
                        mode = 2;
                        continue;
                    }

                    int IROW = IREM[ICT];

                    //Выберите (до) 5 допустимых переменных-кандидатов с IGNORE (I) = 0, 
                    //которые имеют Z (J)> EPBOUN и с наибольшими значениями Z (J).
                    //Храните индексы в ISV. Пусть NSV число индексов в ISV
                    int[] ISV = null;

                    if (ISV.Length == 0)
                    {
                        if (mode == 1)
                            continue;
                        else
                        {
                            //Найдите все допустимые переменные-кандидаты, которые имеют IGNORE (I) = 0 и Z (J) <EPBOUN. 
                            //Если ни один из них не установлен PIV = 0. 
                            //В противном случае установите PIV = элемент наибольшего абсолютного значения в этих столбцах в строке IROW.
                            // Установите ICOL на индекс максимального столбца

                            if (Math.Abs(PIV) > EPSPIV)
                            {

                            }

                            //Является ли переменный оставляющий базис таким же, как провисание в строке IROW

                            //Введите провисание в строке IRON в BINV

                        }

                    }

                    //Сканировать строку IROW TAB. 
                    //Из столбцов с индексами в ISV выберите один элемент с наибольшим абсолютным значением. 
                    //Пусть значение элемента = PIV, индекс столбца - ICOL



                }
                while (true);

            }

            //Переупорядочить столбцы вкладки с индексами в базовом списке переменных IBV в первые NB столбцов вкладки. 
            //Порядок определен в IBV. 
            //Поскольку TAB и BINV занимают одинаковые места хранения (по оператору эквивалентности), эти столбцы содержат BINV


            //построить индексный набор небазисных переменных
        }
    }
}
