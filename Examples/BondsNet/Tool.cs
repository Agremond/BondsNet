﻿using System;
using QuikSharp;

public class Tool   
{
    Char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

    Quik _quik;
    const int DAYS_YEAR = 365;
    string name;
    string securityCode;
    string classCode;
    public string sectype;
    public int expdate;
    public int mat_date;
    public int days_to_mat;
 //   string clientCode;
    string accountID;
    string firmID;
    int lot;
    int priceAccuracy;
    int couponPeiod;
    double couponPercent;
    double guaranteeProviding;
    double goalACY;

    decimal step;
    decimal slip;
    decimal lastPrice;
    Rank rank;
    Bollinger _boll;

    decimal coupon;
    decimal value;
    double currACY;
    double offer;
    double bid;


    #region Свойства
    /// <summary>
    /// Краткое наименование инструмента (бумаги)
    /// </summary>
    public string Name { get { return name; } }
    /// <summary>
    /// Шаг проскальзывания цены
    /// </summary>
    public decimal Slip { get { return slip; } }
    /// <summary>
    /// Код инструмента (бумаги)
    /// </summary>
    public string SecurityCode { get { return securityCode; } }
    /// <summary>
    /// Код класса инструмента (бумаги)
    /// </summary>
    public string ClassCode { get { return classCode; } }
    /// <summary>
    /// Счет клиента
    /// </summary>
    public string AccountID { get { return accountID; } }
    /// <summary>
    /// Код фирмы
    /// </summary>
    public string FirmID { get { return firmID; } }
    /// <summary>
    /// Количество акций в одном лоте
    /// Для инструментов класса SPBFUT = 1
    /// </summary>
    public int Lot { get { return lot; } }
    /// <summary>
    /// Точность цены (количество знаков после запятой)
    /// </summary>
    public int PriceAccuracy { get { return priceAccuracy; } }
    /// <summary>
    /// Шаг цены
    /// </summary>
    public decimal Step { get { return step; } }

    /// <summary>
    /// Размер купона
    /// </summary>
    public decimal Coupon { get { return coupon; } }
    /// <summary>
    /// Требуемая текущая доходность
    /// </summary>
    public double GoalACY { get { return goalACY; } set { goalACY = value; } }
    /// <summary>
    /// Номинал купона
    /// </summary>
    public decimal Value { get { return value; } }

    /// <summary>
    /// Рейтинг  бумаги/эммитента
    /// </summary>
    public Rank Rank { get { return rank; } }
    /// <summary>
    /// Индикатор Боллинджера
    /// </summary>
    public Bollinger Bollinger { get { return _boll; } set { _boll = value; } }

    /// ///  /// <summary>
    /// Текущая доходность
    /// </summary>
    public double CurrentACY
    {
        get
        { return currACY; }
        set
        { currACY = value; }
    }
    /// ///  /// <summary>
    /// Лучшее предложение
    /// </summary>
    public double Offer
    {
        get
        { return offer; }
        set
        { offer = value; }
    }
    /// <summary>
    /// Лучший спрос
    /// </summary>
    public double Bid
    {
        get
        { return bid; }
        set
        { bid = value; }
    }
    ///  /// <summary>
    /// Гарантийное обеспечение (только для срочного рынка)
    /// для фондовой секции = 0
    /// </summary>
    ///  
    public double GuaranteeProviding { get { return guaranteeProviding; } }
    /// <summary>
    /// Цена последней сделки
    /// </summary>
    /// 
    public decimal LastPrice
    {
        get
        {
            lastPrice = Convert.ToDecimal(_quik.Trading.GetParamEx(classCode, securityCode, "LAST").Result.ParamValue.Replace('.', separator));
            return lastPrice;
        }
    }

    /// <summary>
    /// Длительность купона
    /// </summary>
    /// 
    public decimal CouponPeriod { get { return couponPeiod; } }

    /// <summary>
    /// Размер купона в %
    /// </summary>
    public double CouponPercent { get { return couponPercent; } }
    //public decimal CouponPeriod
    //{
    //    get
    //    {
    //        //couponPeiod = 0;
    //        couponPeiod = Convert.ToInt32(Convert.ToDecimal(_quik.Trading.GetParamEx(classCode, securityCode, "COUPONPERIOD").Result.ParamValue.Replace('.', separator)));
    //        return couponPeiod;
    //    }
    //}




    #endregion

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="_quik"></param>
    /// <param name="securityCode">Код инструмента</param>
    /// <param name="classCode">Код класса</param>
    public Tool(Quik quik, Security sec, int koefSlip)
    {
        _quik = quik;
        GetBaseParam(quik, sec.SecCode, sec.ClassCode, koefSlip);
        goalACY = sec.goalACY;
        currACY = 0;
        offer = 0;
        bid = 0;
        rank = sec.Rank;
        
    }

    void GetBaseParam(Quik quik, string secCode, string _classCode, int _koefSlip)
    {
        try
        {
            securityCode = secCode;
            classCode = _classCode;
            if (quik != null)
            {
                if (classCode != null && classCode != "")
                {
                    try
                    {
                        name = quik.Class.GetSecurityInfo(classCode, securityCode).Result.ShortName;
                        accountID = quik.Class.GetTradeAccount(classCode).Result;
                        firmID = quik.Class.GetClassInfo(classCode).Result.FirmId;
                        step = Convert.ToDecimal(quik.Trading.GetParamEx(classCode, securityCode, "SEC_PRICE_STEP").Result.ParamValue.Replace('.', separator));
                        priceAccuracy = Convert.ToInt32(Convert.ToDouble(quik.Trading.GetParamEx(classCode, securityCode, "SEC_SCALE").Result.ParamValue.Replace('.', separator)));
                        
                        coupon = Convert.ToDecimal(_quik.Trading.GetParamEx(classCode, securityCode, "COUPONVALUE").Result.ParamValue.Replace('.', separator));
                        couponPeiod = Convert.ToInt32(Convert.ToDecimal(_quik.Trading.GetParamEx(classCode, securityCode, "COUPONPERIOD").Result.ParamValue.Replace('.', separator)));

                        sectype = Convert.ToString(quik.Trading.GetParamEx(classCode, securityCode, "SECTYPE").Result.ParamValue.Replace('.', separator));
                        mat_date = Convert.ToInt32(Convert.ToDecimal(quik.Trading.GetParamEx(classCode, securityCode, "MAT_DATE").Result.ParamValue.Replace('.', separator)));
                        days_to_mat = Convert.ToInt32(Convert.ToDecimal(quik.Trading.GetParamEx(classCode, securityCode, "DAYS_TO_MAT_DATE").Result.ParamValue.Replace('.', separator)));

                        value = Convert.ToInt32(Convert.ToDouble(quik.Trading.GetParamEx(classCode, securityCode, "SEC_FACE_VALUE").Result.ParamValue.Replace('.', separator)));
                        if (couponPeiod != 0)
                        {
                            couponPercent = Convert.ToDouble(Math.Round((coupon * (Math.Round(DAYS_YEAR / Convert.ToDecimal(couponPeiod))))/ value,4))*100;
                        }
                        else
                        {
                            couponPercent = 0;
                            Console.WriteLine("Tool.GetBaseParam. Ошибка вычисления рамзера купона в процентах." + securityCode);

                        }
                        slip = _koefSlip * step;



                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Tool.GetBaseParam. Ошибка получения наименования для " + securityCode + ": " + e.Message);
                    }

                    if (classCode == "SPBFUT")
                    {
                        Console.WriteLine("Получаем 'guaranteeProviding'.");
                        lot = 1;
                        guaranteeProviding = Convert.ToDouble(quik.Trading.GetParamEx(classCode, securityCode, "BUYDEPO").Result.ParamValue.Replace('.', separator));
                    }
                    else
                    {
                        Console.WriteLine("Получаем 'lot'.");
                        lot = Convert.ToInt32(Convert.ToDouble(quik.Trading.GetParamEx(classCode, securityCode, "LOTSIZE").Result.ParamValue.Replace('.', separator)));
                        guaranteeProviding = 0;
                    }
                }
                else
                {
                    Console.WriteLine("Tool.GetBaseParam. Ошибка: classCode не определен.");
                    lot = 0;
                    guaranteeProviding = 0;
                }
            }
            else
            {
                Console.WriteLine("Tool.GetBaseParam. quik = null !");
            }
        }
        catch (NullReferenceException e)
        {
            Console.WriteLine("Ошибка NullReferenceException в методе GetBaseParam: " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка в методе GetBaseParam: " + e.Message);
        }
    }
}
