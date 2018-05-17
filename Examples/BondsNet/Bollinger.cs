using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Bollinger
{
    double smaHigh;
    double smaLow;
    double smaMed;
    Queue<double> _values;
    int Dshift;

    #region Свойства

    /// <summary>
    /// Средняя линия индикатора Боллинджера
    /// </summary>
    public double SMA_Med  {get { return smaMed;} }
    /// <summary>
    /// Верхняя линия индикатора Боллинджера
    /// </summary>
    public double SMA_High { get { return smaHigh;} }
    /// <summary>
    /// Нижняя линия индикатора Боллинджера
    /// </summary>
    public double SMA_Low { get { return smaLow; }  }
    /// <summary>
    /// Значения цены закрытия последних N сделок
    /// </summary>
    public Queue<double> Values
    {
        get { return _values; }
        set
        {
            _values = value;
            calculateB();
        }
    }

    #endregion


    private double getStandardDeviation(Queue<double> doubleList)
    {
        if (doubleList != null)
        {
            double sumOfDerivation = 0;
            foreach (double value in doubleList)
            {
                sumOfDerivation += (smaMed - value)* (smaMed - value);
            }
            double sumOfDerivationAverage = sumOfDerivation / (doubleList.Count - 1);
            return Math.Sqrt(sumOfDerivationAverage);
        }
        else
        {

            return -1;

        }
          
      
    }
    public void calculateB()
    {
        if (_values != null && _values.Count != 0)
        {
            smaMed = _values.Sum() / _values.Count;
        
            smaHigh = smaMed + Dshift * getStandardDeviation(_values);
            smaLow = smaMed - Dshift * getStandardDeviation(_values);
        }
        else
        {
            smaMed = smaHigh = smaLow = 0;
        }
    }
    public Bollinger(Queue<double> values)
    {
        _values = values;
        
        smaHigh = 0;
        smaLow = 0;
        smaMed = 0;
        Dshift = 2;
        calculateB();

    }
}



