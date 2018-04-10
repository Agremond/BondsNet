﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikSharp;
using QuikSharp.DataStructures;

public class Portfolio
{
    string name;
    string securityCode;
    string classCode;
    long toolQty;
    double AweragePositionPrice;
    double currentCoupon;
    double sellACY;
    double currACY;
    decimal lastPrice;

    #region Свойства
    /// <summary>
    /// Краткое наименование инструмента (бумаги)
    /// </summary>
    public string Name { get { return name; } set { name = Name; } }
    /// <summary>
    /// Цена последней сделки
    /// </summary>
    public decimal LastPrice { get { return lastPrice; } set { lastPrice = LastPrice; } }
    /// <summary>
    /// Код инструмента (бумаги)
    /// </summary>
    public string SecurityCode { get { return securityCode; } set { securityCode = SecurityCode; } }
    /// <summary>
    /// Класс инструмента (бумаги)
    /// </summary>
    public string ClassCode { get { return classCode; } set { classCode = ClassCode; } }
    /// <summary>
    /// Кол-во ценных бумаг в портфеле
    /// </summary>
    public long ToolQty { get { return toolQty; } set { toolQty = ToolQty; } }
    /// <summary>
    /// Средняя цена покупки
    /// </summary>
    public double AwgPosPrice { get { return AweragePositionPrice; } set { AweragePositionPrice = AwgPosPrice; } }

    /// <summary>
    /// Текущий купон
    /// </summary>
    public double СurrentCoupon { get { return currentCoupon; } set { currentCoupon = СurrentCoupon; } }
    /// <summary>
    /// Дохожность продажи. Если меньше купона, то бумага с дисконтом, если больше, то с премией.
    /// </summary>
    public double SellACY { get { return sellACY; } set { sellACY = SellACY; } }
    /// <summary>
    /// Текущая дохождность покупки.
    /// </summary>
    public double CurrACY { get { return currACY; } set { currACY = CurrACY; } }

    #endregion

    /// <summary>
    /// Конструктор класса
    /// </summary>
    public Portfolio(Tool _tool, DepoLimitEx _depo )
    {
        if(_tool != null && _depo != null)
        {
            name = _tool.Name;
            securityCode = _tool.SecurityCode;
            classCode = _tool.ClassCode;

            toolQty = _depo.CurrentBalance;
            AweragePositionPrice = _depo.AweragePositionPrice;
            currentCoupon = _tool.CouponPercent;
            sellACY = 0;
            currACY = 0;
            lastPrice = _tool.LastPrice;
        }
            
         
    }
        
}