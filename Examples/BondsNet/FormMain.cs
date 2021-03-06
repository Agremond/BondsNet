﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuikSharp;
using QuikSharp.DataStructures;
using QuikSharp.DataStructures.Transaction;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Net;
using System.Xml;




namespace BondsNet
{
    public partial class FormMain : Form
    {
        Char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
        public static Quik _quik;//экземпляр интерфейса QUIK
        const int BONDS_LIMIT = 190;
        bool isServerConnected = false; //подключен к сервер QUIK
        
        bool started = false;//флаг запуска робота

        //Заменить на класс Securities;
       //string[] Securities = { "RU000A0JV3Z4", "RU000A0JV3M2" , "RU000A0JVPR3", "RU000A0JVM81", "RU000A0JV2H4" };

        string BCatalog_path = @"bondscatalog.csv";//заменить на Securities
        string LombardList_url = @"http://www.cbr.ru/analytics/Plugins/LombardList.aspx?xml_ver=true";

        // string[] Securities = { "RU000A0JTF68" };

        //Глубина расчета индикатора Боллинджера. Рекомендуется значение 20.
        const int BB_DEEP = 20;
        //Длительность года в днях
        const int DAYS_YEAR = 365;
        //Желаемая минимальная доходность
        const int defaultGoalACY = 13;
        //текущий выбранный инструмент в форме
        string secCode;
        //класс текущего выбранного инструмента
        string classCode = "";
        //индекс текущего инструмента
        int secCodeindex = 0;
        
        //код клиента.
        string clientCode;


        //список инструментов.
        List<Security> Securities = new List<Security>();
        //список транзакций изменений заявок
        List<Order> listOrders;
        //список транзакций сделок
        List<Trade> listTrades;
        //список ответов на отправленные транзакци
        List<TransactionReply> listTransactionReply;

        //Рассчитанные значения бумаг в портфеле
        List<Portfolio> portfolio;

        List<DepoLimitEx> listDepoLimits;
        List<PortfolioInfoEx> listPortfolio;
        List<MoneyLimit> listMoneyLimits;
        List<MoneyLimitEx> listMoneyLimitsEx;
        Settings settings;

        //стаканы по инструментам
        List<OrderBook> toolsOrderBook = null;
        //инструменты
        List<Tool> tools = null;
        //Инструменты портфеля
        //List<Tool> portfolioTools = null;
        //позиции по инструментам
        List<Position> positions = null;

        //Сделки по инструментам
        List<List <Candle> > candles = null;

        DataGridViewRow row;

        // FuturesClientHolding futuresPosition;

        public FormMain()
        {
            InitializeComponent();
            Init();
        }
        void Init()
        {


            textBoxSecCode.Text = secCode;
        
            textBoxClassCode.Text = classCode;
            buttonStartStop.Enabled = false;
            buttonCommandRun.Enabled = false;
            timerRenewForm.Enabled = false;
            listBoxCommands.Enabled = false;
            settings = new Settings();
            listBoxCommands.Items.Add("Получить исторические данные");
            listBoxCommands.Items.Add("Выставить заявку (без сделки)");
            listBoxCommands.Items.Add("Выставить заявку (c выполнением!!!)");
            listBoxCommands.Items.Add("Удалить активную заявку");
            listBoxCommands.Items.Add("Получить таблицу лимитов по бумаге");
            listBoxCommands.Items.Add("Получить таблицу лимитов по всем бумагам");
            listBoxCommands.Items.Add("Получить таблицу заявок");
            listBoxCommands.Items.Add("Получить таблицу сделок");
            listBoxCommands.Items.Add("Получить таблицу `Клиентский портфель`");
            listBoxCommands.Items.Add("Получить таблицы денежных лимитов");
            comboBoxMode.SelectedItem = "Виртуальный";

            
            textBoxQty.Text = settings.QtyOrder.ToString();
            textBoxSlip.Text = settings.KoefSlip.ToString();
          
            
            tools = new List<Tool>();
            //portfolioTools = new List<Tool>();
            toolsOrderBook = new List<OrderBook>();
            positions = new List<Position>();
            candles = new List<List <Candle>>();
            portfolio = new List<Portfolio>();

            listTransactionReply = new List<TransactionReply>();
            listOrders = new List<Order>();
            listTrades = new List<Trade>();


   //         Graphics graf = picGraph.CreateGraphics();
            
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            
            try
            {
                textBoxLogsWindow.AppendText("Подключаемся к терминалу Quik..." + Environment.NewLine);
                _quik = new Quik(Quik.DefaultPort, new InMemoryStorage());    // инициализируем объект Quik
            }
            catch
            {
                textBoxLogsWindow.AppendText("Ошибка инициализации объекта Quik..." + Environment.NewLine);
            }
            if (_quik != null)
            {
                textBoxLogsWindow.AppendText("Экземпляр Quik создан." + Environment.NewLine);
                try
                {
                    textBoxLogsWindow.AppendText("Получаем статус соединения с сервером...." + Environment.NewLine);
                    isServerConnected = _quik.Service.IsConnected().Result;
                    if (isServerConnected)
                    {
                        textBoxLogsWindow.AppendText("Соединение с сервером установлено." + Environment.NewLine);
                        buttonStartStop.Enabled = true;
                        buttonStart.Enabled = false;
                    }
                    else
                    {
                        textBoxLogsWindow.AppendText("Соединение с сервером НЕ установлено." + Environment.NewLine);
                        buttonStartStop.Enabled = false;
                        buttonStart.Enabled = true;
                    }
                }
                catch
                {
                    textBoxLogsWindow.AppendText("Неудачная попытка получить статус соединения с сервером." + Environment.NewLine);
                }
            }
        }
        private void buttonRun_Click(object sender, EventArgs e)
        {
            bool isSubscribedToolOrderBook = false;//успешно подписан на стакан
            if (started)
            {
                buttonStartStop.Text = "СТАРТ";
                buttonStartStop.Enabled = true;
                timerRenewForm.Enabled = false ;

                started = false;
            }
            else
            {
                string ISIN = "";
                double goalACY = 0;
                int rank = 3;
                //загрузка ломбардного списка бумаг
                try
                {
                    string xmlStr;
                    using (var wc = new WebClient())
                    {
                        wc.Encoding = System.Text.Encoding.UTF8;
                        xmlStr = wc.DownloadString(LombardList_url);


                    }
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlStr);
                    textBoxLogsWindow.AppendText("Загружаем ломбардный список облигаций..." + LombardList_url + Environment.NewLine);
                    foreach (XmlNode n_bond in xmlDoc.SelectNodes("/LombardList/Papers/Emitent/Paper"))
                    {

                        ISIN = n_bond.Attributes["ISIN"].Value;
                        goalACY = defaultGoalACY;
                        rank = 3;

                        if (ISIN.StartsWith("RU"))
                            Securities.Add(new Security(ISIN, rank, goalACY));

                    }

                    textBoxLogsWindow.AppendText("Ломбардный список успешно загружен." + Environment.NewLine);

                }
                catch (Exception e1)
                {
                    textBoxLogsWindow.AppendText("Ошибка загрузка ломбардного списка облигаций." + Environment.NewLine);
                    textBoxLogsWindow.AppendText(e1.Message);
                }

                //загрузка пользовательского списка бумаг
                try
                {
                    textBoxLogsWindow.AppendText("Загружаем пользовательский список облигаций..." + Environment.NewLine);
                    using (StreamReader reader = new StreamReader(BCatalog_path))
                    {//открытие файла для чтения
                        string textline;
                        while ((textline = reader.ReadLine()) != null)
                        {
                            if (textline.Split(':') != null)
                            {
                                ISIN = textline.Split(':')[0];
                                goalACY = Convert.ToDouble(textline.Split(':')[1]);
                                rank = Convert.ToInt32(textline.Split(':')[2]);

                                int index = Securities.IndexOf(Securities.Where(n => n.SecCode == ISIN).FirstOrDefault());
                                if (index >= 0)
                                    Securities[index].goalACY = (Securities[index].goalACY > goalACY) ? Securities[index].goalACY : goalACY;
                                else
                                {
                                    Securities.Add(new Security(ISIN, rank, goalACY));

                                }
                            }

                            else { MessageBox.Show("Ошибка чтения списка бумаг!"); }

                        }

                    }
                    textBoxLogsWindow.AppendText("Пользовательский список успешно загружен." + Environment.NewLine);
                }
                catch (Exception e2)
                {
                    textBoxLogsWindow.AppendText("Ошибка загрузка списка облигаций." + Environment.NewLine);
                    textBoxLogsWindow.AppendText(e2.Message);
                    return;
                }
                secCode = Securities[0].SecCode;


                // добавить вывод итогов по каждому типу загрузки
                
                try
                {
                    int i = 0;

                   foreach(Security security in Securities)
                   {
                        secCode = security.SecCode;
                        //   textBoxLogsWindow.AppendText("Определяем код класса инструмента " + secCode + ", по списку классов" + "..." + Environment.NewLine);
                        try
                        {
                            security.ClassCode = _quik.Class.GetSecurityClass("SPBFUT,TQBR,TQBS,TQNL,TQLV,TQNE,TQOB,EQOB", secCode).Result;
                        }
                        catch
                        {
                            textBoxLogsWindow.AppendText("Ошибка определения класса инструмента. Убедитесь, что тикер указан правильно" + Environment.NewLine);
                        }
                        if (security.ClassCode != null && security.ClassCode != "")
                        {
                            textBoxClassCode.Text = security.ClassCode;
                            //     textBoxLogsWindow.AppendText("Определяем код клиента..." + Environment.NewLine);
                            clientCode = _quik.Class.GetClientCode().Result;
                            textBoxClientCode.Text = clientCode;

                            
                            //      textBoxLogsWindow.AppendText("Создаем экземпляр инструмента " + secCode + "|" + classCode + "..." + Environment.NewLine);
                            tools.Add(new Tool(_quik, security, settings.KoefSlip));

                            i++;

                        }
                        else
                        {
                            textBoxLogsWindow.AppendText("не удалось определить класс инструмента " + secCode  + Environment.NewLine);
                        }

                    }
                    
                }
                catch
                {
                    textBoxLogsWindow.AppendText("Ошибка получения данных по инструменту." + Environment.NewLine);
                }

                //отсортировать список инструментов по купону по убыванию
                tools.Sort((x, y) => y.CouponPercent.CompareTo(x.CouponPercent));

                //оставить первые BONDS_LIMIT бумаг. Соблюдаем ограничение quik на число открытых подписок(200)
                if (tools.Count > BONDS_LIMIT)
                    tools.RemoveRange(BONDS_LIMIT-1, tools.Count - BONDS_LIMIT);

                foreach (Tool tool in tools)
                {
                    //      подписываемся на стакан
                    if (tool != null && tool.Name != null && tool.Name != "")
                    {
                        //  textBoxLogsWindow.AppendText("Подписываемся на стакан...");
                        _quik.OrderBook.Subscribe(tool.ClassCode, tool.SecurityCode).Wait();

                        isSubscribedToolOrderBook = _quik.OrderBook.IsSubscribed(tool.ClassCode, tool.SecurityCode).Result;
                        // Выводим в форму таблицу инструментов
                        listBoxSecCode.Items.Add(tool.SecurityCode);
                        dataGridViewRecom.Rows.Add(tool.Name, tool.SecurityCode, tool.CouponPercent, 0, tool.days_to_mat);
                        if (isSubscribedToolOrderBook)
                        {
                            //    textBoxLogsWindow.AppendText("Подписка на стакан прошла успешно." + Environment.NewLine);

                           

                            timerRenewForm.Enabled = true;
                            started = true;
                            buttonStartStop.Text = "СТОП";
                            listBoxCommands.SelectedIndex = 0;
                            listBoxCommands.Enabled = true;
                            buttonCommandRun.Enabled = true;
                            
                        }
                        else
                        {
                            textBoxLogsWindow.AppendText("Подписка на стакан не удалась." + Environment.NewLine);
                            textBoxBestBid.Text = "-";
                            textBoxBestOffer.Text = "-";
                            timerRenewForm.Enabled = false;
                            listBoxCommands.Enabled = false;
                            buttonCommandRun.Enabled = false;
                        }

                        
                        //textBoxLogsWindow.AppendText("Подписываемся на колбэк 'OnFuturesClientHolding'..." + Environment.NewLine);
                        //_quik.Events.OnFuturesClientHolding += OnFuturesClientHoldingDo;
                    }
                    else
                    {
                        textBoxLogsWindow.AppendText("Инструмент не создан." + Environment.NewLine);
                    }

                }

                textBoxLogsWindow.AppendText("Подписываемся на колбэк 'OnQuote', 'OnTransReplay', 'OnTrade', 'Onorder'..." + Environment.NewLine);

                _quik.Events.OnQuote += OnQuoteDo;
                _quik.Events.OnTrade += OnTradeDo;
                
                _quik.Events.OnTransReply += OnTransReplyDo;
                _quik.Events.OnOrder += OnOrderDo;
                //подписаться на событие изменения баланс по бумагам
                // _quik.Events.OnDepoLimit += OnDepoLimitDo;

                
                listBoxSecCode.SelectedIndex = 0;

               // загрузка истории торгов для расчета индикатора Боллинджера

                GetTradesHistory();

                //Вывести текущий портфель.
                try
                {
                    textBoxLogsWindow.AppendText("Получаем таблицу портфеля..." + Environment.NewLine);
                    listDepoLimits = _quik.Trading.GetDepoLimits().Result;

                    if (listDepoLimits.Count > 0)
                    {
                        Tool _tool;
                        textBoxLogsWindow.AppendText("Выводим данные о портфеле в таблицу..." + Environment.NewLine);

                        
                        int id_tool;
                        foreach(DepoLimitEx p_item in listDepoLimits)
                        {
                            _tool = null;
                            if (p_item.LimitKind != LimitKind.T0)
                                continue;
  
                            id_tool = tools.IndexOf(tools.Where(s => s.SecurityCode == p_item.SecCode).FirstOrDefault());

                            if (id_tool >= 0)
                            {
                                portfolio.Add(new Portfolio(tools[id_tool], p_item));
                                dataGridViewPortf.Rows.Add(portfolio.Last().Name, portfolio.Last().SecurityCode, p_item.CurrentBalance, portfolio.Last().AwgPosPrice, tools[id_tool].CouponPercent, 0, 0, tools[id_tool].days_to_mat);
                                
                            }
                            else
                            {
                                try
                                {
                                    Security sec = new Security(p_item.SecCode, 0, 14);
                                    sec.ClassCode = _quik.Class.GetSecurityClass("SPBFUT,TQBR,TQBS,TQNL,TQLV,TQNE,TQOB,EQOB", secCode).Result;
                                    if (sec.ClassCode != "" && sec.ClassCode != null && sec.SecCode.Count() > 6)
                                    {
                                        _tool = new Tool(_quik, sec, 0);
                                        portfolio.Add(new Portfolio(_tool, p_item));
                                        dataGridViewPortf.Rows.Add(portfolio.Last().Name, portfolio.Last().SecurityCode, p_item.CurrentBalance, portfolio.Last().AwgPosPrice, _tool.CouponPercent, 0, 0, _tool.days_to_mat);

                                    }

                                }
                                catch
                                {
                                    textBoxLogsWindow.AppendText("Ошибка определения класса инструмента. Убедитесь, что тикер указан правильно" + Environment.NewLine);
                                }
                            }
                        
                               
                        }
                     }
                    else
                        textBoxLogsWindow.AppendText("В таблице `Клиентский портфель` отсутствуют записи." + Environment.NewLine);
                }
                catch
                {
                    textBoxLogsWindow.AppendText("Ошибка получения клиентского портфеля." + Environment.NewLine);
                }
            }
        }

        double CalcCurrACY(decimal _value, decimal _coupon, decimal _offer, decimal _couponPeriod)
        {
            return Convert.ToDouble(Math.Round(((DAYS_YEAR / _couponPeriod) * _coupon / _value) / (_offer / 100), 5) * 100);

        }

        void Run()
        {
            decimal offer = 0;
            decimal coupon = 0;
            decimal value = 0;
            decimal couponPeriod = 0;
            double tmpGoalACY = 0;
            int index;
            int pos_id;

            decimal priceEntrance = 0;

            if (tools != null )//если tools существует, обрабатываем
            {
                GetBestOfferAndBid();//рассчитать лучшее предложение

               
                for (int i = 0; i < tools.Count; i++)
                {
                    coupon = tools[i].Coupon;
                    value = tools[i].Value;
                    couponPeriod = tools[i].CouponPeriod;

                    #region Рассчет текущей доходности
                    

                    if (value > 0 && coupon > 0 && couponPeriod > 0)
                    {
                        offer = Convert.ToDecimal(tools[i].Offer);
                        if(offer > 0)
                        {
                            //рассчет текущей доходности
                            tools[i].CurrentACY = CalcCurrACY(value, coupon, offer, couponPeriod);
                            if (tools[i].days_to_mat < DAYS_YEAR)
                            {
                                //если время жизни меньше года, то учитываем дисконт/премию
                                tools[i].CurrentACY += (100 - Convert.ToDouble(offer)) / 100;
                            }
                          
                        }

                        // Расчет доходности по индикатору Боллинджера(SMA_Low)
                        if(tools[i].Bollinger != null && tools[i].Bollinger.SMA_Low != Double.NaN )
                        {

                            offer = Convert.ToDecimal(tools[i].Bollinger.SMA_Low);
                        
                            //если есть предложение, то обновляем индикатор
                            if (offer > 0)
                            {
                                tmpGoalACY = CalcCurrACY(value, coupon, offer, couponPeriod);
                                if (tmpGoalACY > tools[i].GoalACY)
                                    tools[i].GoalACY = tmpGoalACY;

                            }
                        }
                      
                    }
                    else
                    {
                        tools[i].CurrentACY = 0;
                    }
                    
                    
                    //выводим статистику для бумаг существующих в портфеле
                    index = portfolio.IndexOf(portfolio.Where(n => n.SecurityCode == tools[i].SecurityCode).FirstOrDefault());

                    if (index >= 0)
                    {

                        portfolio[index].CurrentACY = tools[i].CurrentACY;
                        portfolio[index].LastPrice = tools[i].LastPrice;
                        portfolio[index].Bid = tools[i].Bid;
                    }
                    #endregion



                    #region Расчет позиции для входа

                    pos_id = positions.IndexOf(positions.Where(n => n.SecurityCode == tools[i].SecurityCode).FirstOrDefault());
                    if(pos_id >= 0)
                    {
                        if (positions[pos_id].toolQty == 0 && positions[pos_id].State == State.Waiting)
                        {
                            //Продумать и реализовать ротацию бумаг с целью повышения кредитного рейтинга портфеля и повышения доходности.

                            if (tools[i].CurrentACY >= tools[i].GoalACY && tools[i].GoalACY > 0) // при подходящей доходности  больше "0" отправляем заявку на покупку
                            {
                                if (tools[i].Offer > 0)
                                {
                                    priceEntrance = Convert.ToDecimal(tools[i].Offer) + tools[i].Slip;
                                    int qtyOrder = Convert.ToInt32(settings.QtyOrder / (priceEntrance / 100) * tools[i].Value);


                                    EntrancePosition(Operation.Buy, priceEntrance, qtyOrder, tools[i]);

                                    textBoxLogsWindow.AppendText("Сигнал на вход в позицию (long): " + tools[i].SecurityCode + " : " + priceEntrance.ToString() + Environment.NewLine);
                                }

                            }

                        }
                        else
                        {
                            ;//есть позиция по инструменту
                        }
                    }
                    
                #endregion


                }

                Positions2Table();
            }
                
            if(positions != null && positions.Count != 0)
                if(positions[secCodeindex] != null )
                {  
                    textBoxPositionPE.Text = Math.Round(positions[secCodeindex].priceEntrance, tools[secCodeindex].PriceAccuracy).ToString();
                    textBoxPositionQty.Text = positions[secCodeindex].toolQty.ToString();
                }
         
            //заменить на tool!!!!
            if (toolsOrderBook != null && toolsOrderBook.Count != 0)
                if( toolsOrderBook[secCodeindex].bid != null)
                {
                    int bidID = toolsOrderBook[secCodeindex].bid.Length - 1;
                    if (toolsOrderBook[secCodeindex].bid[bidID].price > 0)
                        textBoxBestBid.Text = Convert.ToString(toolsOrderBook[secCodeindex].bid[bidID].price);
                    else
                        textBoxBestBid.Text = "НД";

                  }
            else
            {
                textBoxBestBid.Text = "НД";
            }
            if ( tools != null)
            {
                if (tools[secCodeindex].CurrentACY >= 0)
                    textBoxACY.Text = Convert.ToString(tools[secCodeindex].CurrentACY + "%");
                else
                    textBoxACY.Text = "НД";

                if (tools[secCodeindex].Offer >= 0)
                    textBoxBestOffer.Text = Convert.ToString(tools[secCodeindex].Offer);
                else
                    textBoxBestOffer.Text = "НД";
            }

            //if (futuresPosition != null) textBoxVarMargin.Text = futuresPosition.varMargin.ToString(); 

        }

        /// <summary>
        /// Функция загрузки истории последних N сделок.
        /// </summary>
        void GetTradesHistory()
        {
            int i = tools.Count;
            int id = 0;
            bool isSubscribedToolCandles = false;
            Queue<double> _temp_val = new Queue<double>();
            textBoxLogsWindow.AppendText("Получение истории торгов" + Environment.NewLine);
            
            foreach (Tool tool in tools)
            {
                _temp_val.Clear();
                try
                {
                    _quik.Candles.Subscribe(tool.ClassCode, tool.SecurityCode, CandleInterval.H1).Wait();
                    isSubscribedToolCandles = _quik.Candles.IsSubscribed(tool.ClassCode, tool.SecurityCode, CandleInterval.H1).Result;
                    if (isSubscribedToolCandles)
                    {
                        //  candles.Add(new List<Candle>(_quik.Candles.GetLastCandles(tool.ClassCode, tool.SecurityCode, CandleInterval.H1, BB_DEEP).Result));
                       List <Candle> candle =  _quik.Candles.GetLastCandles(tool.ClassCode, tool.SecurityCode, CandleInterval.H1, BB_DEEP).Result;
                       candle.ForEach(c => _temp_val.Enqueue(Convert.ToDouble(c.Close)));
                       id = tools.IndexOf(tool);
                        if (id >= 0)
                        {
                            tools[id].Bollinger = new Bollinger(_temp_val);
                            
                        }
                        textBoxLogsWindow.AppendText("Осталось загрузить: " + i  + Environment.NewLine);

                    }
                    else
                    {
                        textBoxLogsWindow.AppendText("Не удалось получить историю торгов " + tool.SecurityCode + Environment.NewLine);

                    }
                }
                catch (Exception er)
                {
                    textBoxLogsWindow.AppendText("Ошибка получения истории торгов. Error: " + er.Message + Environment.NewLine);
                }
                i--;
                
            }


        }

        void GetBestOfferAndBid()//реализовать non-void с возвращением ошибки
        {
           
            if (toolsOrderBook != null)
            {
                for (int i = 0; i < toolsOrderBook.Count; i++)
                {
                    int tool_id = tools.IndexOf(tools.Where(n => n.SecurityCode == toolsOrderBook[i].sec_code).FirstOrDefault());

                    if (tool_id >= 0)
                        if (toolsOrderBook[i].sec_code != null && toolsOrderBook[i].class_code != null)
                            if (tools[tool_id].SecurityCode == toolsOrderBook[i].sec_code && tools[tool_id].ClassCode == toolsOrderBook[i].class_code)
                            {
                                if (toolsOrderBook[i].offer != null)
                                {

                                    tools[tool_id].Offer = toolsOrderBook[i].offer[0].price;
                                }
                                if (toolsOrderBook[i].bid != null)
                                {
                                    int bidID = toolsOrderBook[i].bid.Length - 1;
                                    if (toolsOrderBook[i].bid[bidID].price > 0)
                                        tools[tool_id].Bid = toolsOrderBook[i].bid[bidID].price;
                                    
                                }
                            }
                            else
                            {
                                textBoxLogsWindow.AppendText("Рассинхронизация OrderBook" + Environment.NewLine);
                            }

                }
            }

        }


       
        void EntrancePosition(Operation operation, decimal price, int qty, Tool tool)
        {
            positions.Add(new Position());
            dataGridViewPositions.Rows.Add();
            int i = positions.Count - 1;

            positions[i].priceEntrance = price;
            positions[i].entranceOrderQty = qty * tool.Lot;
            positions[i].toolQty = positions[i].entranceOrderQty;
            positions[i].dateTimeEntrance = DateTime.Now;
            positions[i].SecurityCode = tool.SecurityCode;

            if (settings.RobotMode == "Боевой")
            {
                positions[i].entranceOrderID = NewOrder(_quik, tool, operation, price, positions[i].entranceOrderQty);
                if (positions[i].entranceOrderID != 0)
                {
                        
                    textBoxLogsWindow.AppendText("ID заявки - " + positions[i].entranceOrderID + Environment.NewLine);
                }
            }
            else
            {
                textBoxLogsWindow.AppendText("ID заявки - ТЕСТ" + Environment.NewLine);
                NewPos2Table(tool, positions[i].toolQty, positions[i].dateTimeEntrance, positions[i].priceEntrance);
            }

        }
        /// <summary>
        /// Функция вывода состояния портфеля и заявок на сделку
        /// </summary>
        void Positions2Table()
        {

            int row_id = 0;

            //выводим текущие открытые позиции по инструментам и их статус
            for (int i = 0; i < positions.Count; i++)
            {
                if(positions[i].entranceOrderQty != 0)
                {

                    row = dataGridViewPositions.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["posToolName"].Value.ToString().Equals(positions[i].SecurityCode))
                    .FirstOrDefault();
                    
                    if(row != null)
                    {
                        row_id = row.Index;
                        dataGridViewPositions.Rows[row_id].Cells["posToolName"].Value = tools[i].SecurityCode;
                        dataGridViewPositions.Rows[row_id].Cells["posOperation"].Value = "Покупка";//dirty hack
                        dataGridViewPositions.Rows[row_id].Cells["posPrice"].Value = positions[i].priceEntrance;
                        dataGridViewPositions.Rows[row_id].Cells["posQty"].Value = positions[i].entranceOrderQty;
                        dataGridViewPositions.Rows[row_id].Cells["posRemains"].Value = positions[i].toolQty;
                        dataGridViewPositions.Rows[row_id].Cells["posState"].Value = positions[i].State.ToString();
                    }
                    

      
                }
                else
                {
                    //allRows = ((DataTable)dataGridViewPositions.DataSource).Rows;

                    //searchedRows = ((DataTable)dataGridViewPositions.DataSource).Select(tools[i].SecurityCode);

                    //if(searchedRows != null)
                    //{
                    //    rowIndex = allRows.IndexOf(searchedRows[0]);

                    //    dataGridViewPositions.Rows.RemoveAt(rowIndex);
                    //}


                }

            }

            int sec_id = 0;


            
            foreach (Portfolio portTool in portfolio)
            {


                row = dataGridViewPortf.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["portSecCode"].Value.ToString().Equals(portTool.SecurityCode))
                    .FirstOrDefault();

                if(row != null)
                {
                    row_id = row.Index;
                    //Есть ли бумаги из списка в тек. портфеле
                    sec_id = Securities.IndexOf(Securities.Where(n => n.SecCode == portTool.SecurityCode).FirstOrDefault());

                    if (sec_id < 0)
                    {
                        //если бумаги не входят в ломбардный лист, то подсвечиваем их
                        dataGridViewPortf.Rows[row_id].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                        dataGridViewPortf.Rows[row_id].DefaultCellStyle.ForeColor = Color.Black;

                    }

                    //если есть данные о доходности, то выводим и подсвечиваем, если она больше текущей
                    if (portTool.Bid > 0 && portTool.AwgPosPrice > 0)
                    {

                        double sellACY = portTool.СurrentCoupon + (Convert.ToDouble(portTool.Bid) - portTool.AwgPosPrice) / portTool.AwgPosPrice;
                        dataGridViewPortf.Rows[row_id].Cells["portSellACY"].Value = Math.Round(sellACY, 3);
                        if (portTool.СurrentCoupon < sellACY)
                        {
                            dataGridViewPortf.Rows[row_id].Cells["portSellACY"].Style.BackColor = Color.LightGreen;
                            dataGridViewPortf.Rows[row_id].DefaultCellStyle.ForeColor = Color.Black;
                        }


                    }
                    else
                        dataGridViewPortf.Rows[row_id].Cells["portSellACY"].Value = 0;

                    dataGridViewPortf.Rows[row_id].Cells["portCurrACY"].Value = portTool.CurrentACY;
                }
                    


                         
            }
        }
        /// <summary>
        /// Функция вывода информации о новых сделках клиента
        /// </summary>
        void NewPos2Table(Tool tool, int qty, DateTime timeEntr, decimal price)
        {
            dataGridViewDeals.Rows.Add();
            int i = dataGridViewDeals.Rows.Count - 1;

            dataGridViewDeals.Rows[i].Cells["ToolName"].Value = tool.Name;
            dataGridViewDeals.Rows[i].Cells["Qty"].Value = qty;
            dataGridViewDeals.Rows[i].Cells["TimeEntr"].Value = timeEntr.ToShortTimeString();
            dataGridViewDeals.Rows[i].Cells["PriceEntr"].Value = Math.Round(price, tool.PriceAccuracy);
            int LastRow = dataGridViewDeals.RowCount - 1;
            dataGridViewDeals.FirstDisplayedScrollingRowIndex = LastRow;
        }
        /// <summary>
        /// Функция вызывается терминалом QUIK при получении сделки.
        /// </summary>
        void OnTradeDo(Trade trade)
        {
            //исключаем сделки вне списка бумаг
            int i = GetIndexOfTool(trade.SecCode, trade.ClassCode);

            if (i == -1)
                return;

            //исключаем повторный вызов OnTrade
            if (listTrades.Contains(trade))
                return;

            String corrID;

            //добавляем новую сделку в хранилище сделок
            listTrades.Add(trade);

            corrID = clientCode + "//" + positions[i].entranceOrderID;

            //переместить за пределы проверок сщуествующих транзакций
            tools[i].Bollinger.Values.Dequeue(); ;
            tools[i].Bollinger.Values.Enqueue(trade.Price);

            //обрабатываем сделки относящиейся к текущему инструменту by ID
            if (trade.Comment.Equals(corrID))
            {
                lock (positions)
                {
                    int pos_id = positions.IndexOf(positions.Where(n => n.SecurityCode == tools[i].SecurityCode).FirstOrDefault());

                    
                    if (positions[pos_id].State == State.Active || positions[pos_id].State == State.Waiting)
                    {
                        positions[pos_id].toolQty -= trade.Quantity;//прошла сделка.корректируем текущий остаток в позиции

                        //зафиксировать цену покупки для статистики
                        //....
                        ///////////////////////

                        if (positions[pos_id].toolQty <= 0)//заявка полностью удовлетворена
                            positions[pos_id].State = State.Completed;//заявка выполнена     
                    }
                }
            }
        }

        /// <summary>
        /// Функция вызывается терминалом QUIK при получении ответа на транзакцию пользователя.
        /// </summary>
        void OnTransReplyDo(TransactionReply reply)
        {
            int i = GetIndexOfTool(reply.SecCode, reply.ClassCode);

            if (i == -1)
                return;

            //исключаем повторный вызов OnTransReply
            if (listTransactionReply.Contains(reply))
               return;
            listTransactionReply.Add(reply);

            int pos_id = positions.IndexOf(positions.Where(n => n.SecurityCode == tools[i].SecurityCode).FirstOrDefault());
            if (reply.TransID == positions[pos_id].entranceOrderID)
            {
                if (reply.Status == 3)
                { 
                    positions[pos_id].State = (positions[pos_id].State == State.Waiting) ? State.Active : positions[pos_id].State;
                }
                else
                {
                    if (reply.Status > 3)
                        positions[pos_id].State = (positions[pos_id].State == State.Waiting) ? State.Error : positions[pos_id].State;
                }
            }

           
        }
        /// <summary>
        /// Функция вызывается терминалом QUIK при получении заявки или изменении параметров заявки.
        /// </summary>
        void OnOrderDo(Order order)
        {
            int i = GetIndexOfTool(order.SecCode, order.ClassCode);

            if (i == -1)
                return;

            //исключаем повторный вызов OnOrder
            if (listOrders.Contains(order))
                return;

            listOrders.Add(order);//добавляем транзацию изменения заявки в хранилище

            String corrID;


            corrID = clientCode + "//" + positions[i].entranceOrderID;

            //обрабатываем заявки относящиейся к текущему нструменту

            lock (positions)
            {
                if (order.Comment.Equals(corrID))//FYI: what is the format: СС//transid or transid
                {
                    int pos_id = positions.IndexOf(positions.Where(n => n.SecurityCode == tools[i].SecurityCode).FirstOrDefault());

                    if (order.Capacity == 0 || order.State == State.Completed)
                    {
                        positions[pos_id].State = State.Completed;//заявка выполнена
                    }
                    else
                    {
                        if (order.Flags.HasFlag(OrderTradeFlags.Canceled))
                        {
                            positions[pos_id].State = State.Canceled;//заявка отменена
                        }
                    }
                }
            }

           
           
        }

        void OnQuoteDo(OrderBook quote)
        {
            lock(toolsOrderBook)
            {
                int orderbook_id = toolsOrderBook.IndexOf(toolsOrderBook.Where(n => n.sec_code == quote.sec_code).FirstOrDefault());
                if (orderbook_id == -1)
                {
                    toolsOrderBook.Add(new OrderBook());
                    toolsOrderBook[toolsOrderBook.Count - 1] = quote;
                }
                else
                    toolsOrderBook[orderbook_id] = quote;
            }
           

        }

        void OnDepoLimitDo(DepoLimit depolimit)
        {
            ; textBoxLogsWindow.AppendText("OnDepoLimit: изменение баланса."+ depolimit.DepoCurrentBalance + Environment.NewLine);
        }



        void OnFuturesClientHoldingDo(FuturesClientHolding futPos)
        {
           // if (futPos.secCode == tool.SecurityCode) futuresPosition = futPos;
        }

        private void timerRenewForm_Tick(object sender, EventArgs e)
        {

            textBoxAccountID.Text = tools[secCodeindex].AccountID;
            textBoxFirmID.Text = tools[secCodeindex].FirmID;
            textBoxShortName.Text = tools[secCodeindex].Name;
            textBoxLot.Text = Convert.ToString(tools[secCodeindex].Lot);
            textBoxStep.Text = Convert.ToString(tools[secCodeindex].Step);
            textBoxValue.Text = Convert.ToString(tools[secCodeindex].GuaranteeProviding);
            textBoxGoalACY.Text = Convert.ToString(tools[secCodeindex].GoalACY);

            textBoxLastPrice.Text = Convert.ToString(tools[secCodeindex].LastPrice);
            textBoxCoupon.Text = Convert.ToString(tools[secCodeindex].CouponPercent);
            textBoxValue.Text = Convert.ToString(tools[secCodeindex].Value);
            textBoxCPeriod.Text = Convert.ToString(tools[secCodeindex].CouponPeriod);
            textBoxRank.Text = Convert.ToString(tools[secCodeindex].Rank);

            Run();

        }
        private void listBoxCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCommand = listBoxCommands.SelectedItem.ToString();
          
            switch (selectedCommand)
            {
                case "Получить исторические данные":
                    textBoxDescription.Text = "Получить и отобразить исторические данные котировок по заданному инструменту. Тайм-фрейм = 1 Hour";
                    break;
                case "Выставить заявку (без сделки)":
                    textBoxDescription.Text = "Будет выставлена заявку на покупку 1-го лота заданного инструмента, по цене на 5% ниже текущей цены (вероятность срабатывания такой заявки достаточно низкая, чтобы успеть ее отменить)";
                    break;
                case "Выставить заявку (c выполнением!!!)":
                    textBoxDescription.Text = "Будет выставлена заявку на покупку 1-го лота заданного инструмента, по цене на 5 шагов цены выше текущей цены (вероятность срабатывания такой заявки достаточно высокая!!!)";
                    break;
                case "Выставить заявку (Удалить активную заявку)":
                    textBoxDescription.Text = "Если предварительно была выставлена заявка, заявка имеет статус 'Активна' и ее номер отображается в форме, то эта заявка будет удалена/отменена";
                    break;
                case "Получить таблицу лимитов по бумаге":
                    textBoxDescription.Text = "Получить и отобразить таблицу лимитов по бумагам. quik.Trading.GetDepoLimits(securityCode)";
                    break;
                case "Получить таблицу лимитов по всем бумагам":
                    textBoxDescription.Text = "Получить и отобразить таблицу лимитов по бумагам. quik.Trading.GetDepoLimits()";
                    break;
                case "Получить таблицу заявок":
                    textBoxDescription.Text = "Получить и отобразить таблицу всех клиентских заявок. quik.Orders.GetOrders()";
                    break;
                case "Получить таблицу сделок":
                    textBoxDescription.Text = "Получить и отобразить таблицу всех клиентских сделок. quik.Trading.GetTrades()";
                    break;
                case "Получить таблицу `Клиентский портфель`":
                    textBoxDescription.Text = "Получить и отобразить таблицу `Клиентский портфель`. quik.Trading.GetPortfolioInfoEx()";
                    break;
                case "Получить таблицы денежных лимитов":
                    textBoxDescription.Text = "Получить и отобразить таблицы денежных лимитов (стандартную и дополнительную Т2). Работает только на инструментах фондовой секции. quik.Trading.GetMoney() и quik.Trading.GetMoneyEx()";
                    break;
            }
        }
        private void buttonCommandRun_Click(object sender, EventArgs e)
        {
            List<Order> tmplistOrders;
            List<Trade> tmplistTrades;

            Order order = null;
            List<Candle> toolCandles = null;
            FormOutputTable toolCandlesTable = null;
            bool isSubscribedToolCandles = false;
            string selectedCommand = listBoxCommands.SelectedItem.ToString();
            switch (selectedCommand)
            {
                case "Получить исторические данные":
                    try
                    {
                        secCodeindex = listBoxSecCode.SelectedIndex;
                       
                        textBoxLogsWindow.AppendText("Подписываемся на получение исторических данных..." + Environment.NewLine);

                        _quik.Candles.Subscribe(tools[secCodeindex].ClassCode, tools[secCodeindex].SecurityCode, CandleInterval.H1).Wait();
                        textBoxLogsWindow.AppendText("Проверяем состояние подписки..." + Environment.NewLine);
                        isSubscribedToolCandles = _quik.Candles.IsSubscribed(tools[secCodeindex].ClassCode, tools[secCodeindex].SecurityCode, CandleInterval.H1).Result;
                        if (isSubscribedToolCandles)
                        {
                            textBoxLogsWindow.AppendText("Получаем исторические данные..." + Environment.NewLine);
                            toolCandles = _quik.Candles.GetAllCandles(tools[secCodeindex].ClassCode, tools[secCodeindex].SecurityCode, CandleInterval.H1).Result;
                            
                            //toolCandles = _quik.Candles.GetLastCandles(tools[secCodeindex].ClassCode, tools[secCodeindex].SecurityCode, CandleInterval.H1).Result;

                            textBoxLogsWindow.AppendText("Выводим исторические данные в таблицу..." + Environment.NewLine);
                            toolCandlesTable = new FormOutputTable(toolCandles);
                            toolCandlesTable.Show();
                        }
                        else
                        {
                            textBoxLogsWindow.AppendText("Неудачная попытка подписки на исторические данные." + Environment.NewLine);
                        }
                    }
                    catch
                    {
                        textBoxLogsWindow.AppendText("Ошибка получения исторических данных." + Environment.NewLine);
                    }
                    break;
                case "Выставить заявку (без сделки)":
                    try
                    {
                        decimal priceInOrder = Math.Round(tools[secCodeindex].LastPrice - tools[secCodeindex].LastPrice / 20, tools[secCodeindex].PriceAccuracy);
                        int qtyOrder = Convert.ToInt32(settings.QtyOrder / (priceInOrder / 100) * tools[secCodeindex].Value);
                        textBoxLogsWindow.AppendText("Выставляем заявку на покупку, по цене:" + priceInOrder + " ..." + Environment.NewLine);

                        EntrancePosition(Operation.Buy, priceInOrder, qtyOrder, tools[secCodeindex]);

                        long transactionID = positions[secCodeindex].entranceOrderID;
                        try
                        {
                            tmplistOrders = _quik.Orders.GetOrders().Result;
                            foreach (Order _order in tmplistOrders)
                            {
                                if (_order.TransID == transactionID && _order.ClassCode == tools[secCodeindex].ClassCode && _order.SecCode == tools[secCodeindex].SecurityCode)
                                {
                                    textBoxLogsWindow.AppendText("Заявка выставлена. Номер заявки - " + _order.OrderNum + Environment.NewLine);

                                    order = _order;
                                }
                            }
                        }
                        catch (Exception er)
                        {
                            textBoxLogsWindow.AppendText("Ошибка получения номера заявки. Error: " + er.Message + Environment.NewLine);
                        }

                    }
                    catch
                    {
                        textBoxLogsWindow.AppendText("Ошибка выставления заявки." + Environment.NewLine);
                    }
                    break;
                case "Выставить заявку (c выполнением!!!)":
                    try
                    {
                        decimal priceInOrder = Math.Round(tools[secCodeindex].LastPrice + tools[secCodeindex].Step * 5, tools[secCodeindex].PriceAccuracy);
                        int qtyOrder = Convert.ToInt32(settings.QtyOrder / (priceInOrder / 100) * tools[secCodeindex].Value);
                        textBoxLogsWindow.AppendText("Выставляем заявку на покупку, по цене:" + priceInOrder + " ..." + Environment.NewLine);

                        EntrancePosition(Operation.Buy, priceInOrder, qtyOrder, tools[secCodeindex]);

                        long transactionID = positions[secCodeindex].entranceOrderID;
                        try
                        {
                            tmplistOrders = _quik.Orders.GetOrders().Result;
                            foreach (Order _order in tmplistOrders)
                            {
                                if (_order.TransID == transactionID && _order.ClassCode == tools[secCodeindex].ClassCode && _order.SecCode == tools[secCodeindex].SecurityCode)
                                {
                                    textBoxLogsWindow.AppendText("Заявка выставлена. Номер заявки - " + _order.OrderNum + Environment.NewLine);

                                    order = _order;
                                }
                            }
                        }
                        catch (Exception er)
                        {
                            textBoxLogsWindow.AppendText("Ошибка получения номера заявки. Error: " + er.Message + Environment.NewLine);
                        }
                    }
                    catch
                    {
                        textBoxLogsWindow.AppendText("Ошибка выставления заявки." + Environment.NewLine);
                    }
                    break;
                case "Удалить активную заявку":
                    try
                    {
                        //KillOrder(secCodeindex);
                    }
                    catch
                    {
                        textBoxLogsWindow.AppendText("Ошибка удаления заявки." + Environment.NewLine);
                    }
                    break;
                case "Получить таблицу лимитов по бумаге":
                    try
                    {
                        textBoxLogsWindow.AppendText("Получаем таблицу лимитов..." + Environment.NewLine);
                        listDepoLimits = _quik.Trading.GetDepoLimits(tools[secCodeindex].SecurityCode).Result;

                        if (listDepoLimits.Count > 0)
                        {
                            textBoxLogsWindow.AppendText("Выводим данные лимитов в таблицу..." + Environment.NewLine);
                            toolCandlesTable = new FormOutputTable(listDepoLimits);
                            toolCandlesTable.Show();
                        }
                        else
                        {
                            textBoxLogsWindow.AppendText("Бумага '" + tools[secCodeindex].Name + "' в таблице лимитов отсутствует." + Environment.NewLine);
                        }
                    }
                    catch
                    {
                        textBoxLogsWindow.AppendText("Ошибка получения лимитов." + Environment.NewLine);
                    }
                    break;
                case "Получить таблицу лимитов по всем бумагам":
                    try
                    {
                        textBoxLogsWindow.AppendText("Получаем таблицу лимитов..." + Environment.NewLine);
                        listDepoLimits = _quik.Trading.GetDepoLimits().Result;

                        if (listDepoLimits.Count > 0)
                        {
                            textBoxLogsWindow.AppendText("Выводим данные лимитов в таблицу..." + Environment.NewLine);
                            toolCandlesTable = new FormOutputTable(listDepoLimits);
                            toolCandlesTable.Show();
                        }
                    }
                    catch
                    {
                        textBoxLogsWindow.AppendText("Ошибка получения лимитов." + Environment.NewLine);
                    }
                    break;
                case "Получить таблицу заявок":
                    try
                    {
                        textBoxLogsWindow.AppendText("Получаем таблицу заявок..." + Environment.NewLine);
                        tmplistOrders = _quik.Orders.GetOrders().Result;

                        if (tmplistOrders.Count > 0)
                        {
                            textBoxLogsWindow.AppendText("Выводим данные о заявках в таблицу..." + Environment.NewLine);
                            toolCandlesTable = new FormOutputTable(tmplistOrders);
                            toolCandlesTable.Show();
                        }
                    }
                    catch
                    {
                        textBoxLogsWindow.AppendText("Ошибка получения заявок." + Environment.NewLine);
                    }
                    break;
                case "Получить таблицу сделок":
                    try
                    {
                        textBoxLogsWindow.AppendText("Получаем таблицу сделок..." + Environment.NewLine);
                        tmplistTrades = _quik.Trading.GetTrades().Result;

                        if (tmplistTrades.Count > 0)
                        {
                            textBoxLogsWindow.AppendText("Выводим данные о сделках в таблицу..." + Environment.NewLine);
                            toolCandlesTable = new FormOutputTable(tmplistTrades);
                            toolCandlesTable.Show();
                        }
                    }
                    catch
                    {
                        textBoxLogsWindow.AppendText("Ошибка получения сделок." + Environment.NewLine);
                    }
                    break;
                case "Получить таблицу `Клиентский портфель`":
                    try
                    {
                        textBoxLogsWindow.AppendText("Получаем таблицу `Клиентский портфель`..." + Environment.NewLine);
                        listPortfolio = new List<PortfolioInfoEx>();
                        if (classCode == "SPBFUT") listPortfolio.Add(_quik.Trading.GetPortfolioInfoEx(tools[secCodeindex].FirmID, tools[secCodeindex].AccountID, 0).Result);
                        else listPortfolio.Add(_quik.Trading.GetPortfolioInfoEx(tools[secCodeindex].FirmID, clientCode, 2).Result);

                        if (listPortfolio.Count > 0)
                        {
                            textBoxLogsWindow.AppendText("Выводим данные о портфеле в таблицу..." + Environment.NewLine);
                            toolCandlesTable = new FormOutputTable(listPortfolio);
                            toolCandlesTable.Show();
                        }
                        else
                        {
                            textBoxLogsWindow.AppendText("В таблице `Клиентский портфель` отсутствуют записи." + Environment.NewLine);
                        }
                    }
                    catch
                    {
                        textBoxLogsWindow.AppendText("Ошибка получения клиентского портфеля." + Environment.NewLine);
                    }
                    break;
                case "Получить таблицы денежных лимитов":
                    try
                    {
                        textBoxLogsWindow.AppendText("Получаем таблицу денежных лимитов..." + Environment.NewLine);
                        listMoneyLimits = new List<MoneyLimit>();
                        listMoneyLimits.Add(_quik.Trading.GetMoney(clientCode, tools[secCodeindex].FirmID, "EQTV", "SUR").Result);

                        if (listMoneyLimits.Count > 0)
                        {
                            textBoxLogsWindow.AppendText("Выводим данные о денежных лимитах в таблицу..." + Environment.NewLine);
                            toolCandlesTable = new FormOutputTable(listMoneyLimits);
                            toolCandlesTable.Show();
                        }

                        textBoxLogsWindow.AppendText("Получаем расширение таблицы денежных лимитов..." + Environment.NewLine);
                        listMoneyLimitsEx = new List<MoneyLimitEx>();
                        listMoneyLimitsEx.Add(_quik.Trading.GetMoneyEx(tools[secCodeindex].FirmID, clientCode, "EQTV", "SUR", 2).Result);

                        if (listMoneyLimitsEx.Count > 0)
                        {
                            textBoxLogsWindow.AppendText("Выводим данные о денежных лимитах в таблицу..." + Environment.NewLine);
                            toolCandlesTable = new FormOutputTable(listMoneyLimitsEx);
                            toolCandlesTable.Show();
                        }
                    }
                    catch
                    {
                        textBoxLogsWindow.AppendText("Ошибка получения денежных лимитов." + Environment.NewLine);
                    }
                    break;
            }
        }

        int GetIndexOfTool(string SecCode, string ClassCode)
        {
            for(int i = 0; i < tools.Count; i++)
                if(tools[i].SecurityCode == SecCode && tools[i].ClassCode == ClassCode)
                    return i;
            return -1;

        }

        

        int GetPositionT2(Quik _quik, Tool instrument, string clientCode)
        {
            // возвращает чистую позицию по инструменту
            // для срочного рынка передаем номер счета, для спот-рынка код-клиента
            int qty = 0;
            if (instrument.ClassCode == "SPBFUT")
            {
                // фьючерсы
                try
                {
                    FuturesClientHolding q1 = _quik.Trading.GetFuturesHolding(instrument.FirmID, instrument.AccountID, instrument.SecurityCode, 0).Result;
                    if (q1 != null) qty = Convert.ToInt32(q1.totalNet);
                }
                catch (Exception e) { textBoxLogsWindow.AppendText("GetPositionT2: SPBFUT, ошибка - " + e.Message + Environment.NewLine); }
            }
            else
            {
                // акции
                try
                {
                    DepoLimitEx q1 = _quik.Trading.GetDepoEx(instrument.FirmID, clientCode, instrument.SecurityCode, instrument.AccountID, 2).Result;
                    if (q1 != null) qty = Convert.ToInt32(q1.CurrentBalance);
                }
                catch (Exception e)
                {
                    textBoxLogsWindow.AppendText("GetPositionT2: ошибка - " + e.Message  + Environment.NewLine);
                }
            }
            return qty;
        }
        long NewOrder(Quik _quik, Tool _tool, Operation operation, decimal price, int qty)
        {
            long res = 0;
            if (settings.RobotMode == "Боевой")
            {
                Order order_new = new Order();
                order_new.ClassCode = _tool.ClassCode;
                order_new.SecCode = _tool.SecurityCode;
                order_new.Operation = operation;
                order_new.Price = price;
                order_new.Quantity = qty;
                
                order_new.ClientCode = clientCode;

                order_new.Account = _tool.AccountID;
                try
                {
                    res = _quik.Orders.CreateOrder(order_new).Result;

                }
                catch
                {
                    textBoxLogsWindow.AppendText("Неудачная попытка отправки заявки" + Environment.NewLine);
                }
            }
            return res;
        }

        private void listBoxSecCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            secCode = listBoxSecCode.SelectedItem.ToString();
            secCodeindex = listBoxSecCode.SelectedIndex;
            textBoxSecCode.Text = secCode;
        }

        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.RobotMode = comboBoxMode.SelectedItem.ToString();
        }

        private void buttonKillOrder_Click(object sender, EventArgs e)
        {

            if (positions[secCodeindex] != null)
            {
                if (positions[secCodeindex].entranceOrderNumber != 0)
                {
                    if (positions[secCodeindex].State != State.Completed)
                    {
                        Order order = new Order();
                        order.ClassCode = tools[secCodeindex].ClassCode;
                        order.SecCode = tools[secCodeindex].SecurityCode;
                        order.OrderNum = positions[secCodeindex].entranceOrderNumber;

                        positions[secCodeindex].State = State.Canceling;
                        long x = _quik.Orders.KillOrder(order).Result;
                        
                        //positions[secCodeindex].entranceOrderID = x;

                        // positions[secCodeindex].State = State.Canceled;
                        textBoxLogsWindow.AppendText("Результат - " + x + " ..." + Environment.NewLine);
                     
                    }
                }
            }
            else
            {
                textBoxLogsWindow.AppendText("Невозможно снять заявку. Заявка не существует." + Environment.NewLine);
            }
        }

        private void buttonPositionReset_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewPositions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
