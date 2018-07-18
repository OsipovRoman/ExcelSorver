using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelSolver.Enums
{
    /// <summary>
    /// Схема апрокимации при получении значений производных
    /// </summary>
    public enum ApproximationScheme
    {
        /// <summary>
        /// Явная, по значению 2-х точек. Самая наименее ресурсоемкая.
        /// </summary>
        Explicit,
        /// <summary>
        /// Не явная, по значению 3-х точек. 
        /// </summary>
        Implicit,
        /// <summary>
        /// Полу явная, как средняя двух схем явной и неявной. Наиболее ресурсоемкая
        /// </summary>
        SemiExplicit
    }
}
