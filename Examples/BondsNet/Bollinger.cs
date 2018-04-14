using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Bollinger
{
    double smaMed;
    double smaHigh;
    double smaLow;

    #region Свойства
    /// <summary>
    /// Средняя линия индикатора Боллинджера
    /// </summary>
    public double SMA { get { return smaMed; } set { smaMed = SMA; } }
    /// <summary>
    /// Верхняя линия индикатора Боллинджера
    /// </summary>
    public double SMA_High { get { return smaHigh; } set { smaHigh = SMA_High; } }
    /// <summary>
    /// Нижняя линия индикатора Боллинджера
    /// </summary>
    public double SMA_Low { get { return smaLow; } set { smaLow = SMA_Low; } }

    #endregion

    public Bollinger(double _smaMed, double _smaHigh, double _smaLow)
    {
        
        if(_smaLow < _smaMed && _smaMed < _smaHigh)
        {
            smaMed = _smaMed;
           
        }
    }
}

