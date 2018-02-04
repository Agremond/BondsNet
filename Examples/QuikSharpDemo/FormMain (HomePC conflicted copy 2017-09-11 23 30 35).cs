using System;
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

namespace QuikSharpDemo
{
    public partial class FormMain : Form
    {
        Char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
        public static Quik _quik;
        bool isServerConnected = false;
        bool isSubscribedToolOrderBook = false;
        bool pause = false;
        
        bool isSubscribedToolCandles = false;
        string secCode = "RU000A0JTF68";
    
        //string[] Securities = { "RU000A0JSQ58", "RU000A0JTF68", "RU000A0JNPK5" , "RU000A0JR647", "RU000A0JUWJ8" };
        string[] Securities = {"RU000A0JTF68", "RU000A0JNPK5" };

        ICollection<string> keys;

        int secCodeindex = 0;

        string classCode = "";
      
        string clientCode;
        bool started = false;
               
        List<Candle> toolCandles;
        List<Order> listOrders;
        List<Trade> listTrades;
        List<DepoLimitEx> listDepoLimits;
        List<PortfolioInfoEx> listPortfolio;
        List<MoneyLimit> listMoneyLimits;
        List<MoneyLimitEx> listMoneyLimitsEx;
        Settings settings;

        List<OrderBook> toolsOrderBook;
        List<Tool> tools;
        List<Position> positions;

        FormOutputTable toolCandlesTable;
        Order order;
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

            foreach (string curSec in Securities)
            {
                listBoxSecCode.Items.Add(curSec);
            }
            listBoxSecCode.SelectedIndex = 0;

            tools = new List<Tool>(Securities.Length);
            toolsOrderBook = new List<OrderBook>(Securities.Length);
            positions = new List<Position>(Securities.Length);




        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxLogsWindow.AppendText("Подключаемся к терминалу Quik..." + Environment.NewLine);
                _quik = new Quik(Quik.DefaultPort, new InMemoryStorage());    // инициализируем объект Quik
                //_quik = new Quik(34136, new InMemoryStorage());    // отладочное подключение
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
            if (started)
            {
                buttonStartStop.Text = "СТАРТ";
                buttonStartStop.Enabled = true;
                timerRenewForm.Enabled = false ;
                started = false;
            }
            else
            {
                try
                {
                    //secCode = listBoxSecCode.SelectedItem.ToString();
                    int i = 0;

                    foreach (string curSec in Securities)
                    {
                        secCode = curSec;
                        textBoxLogsWindow.AppendText("Определяем код класса инструмента " + secCode + ", по списку классов" + "..." + Environment.NewLine);
                        try
                        {
                            classCode = _quik.Class.GetSecurityClass("SPBFUT,TQBR,TQBS,TQNL,TQLV,TQNE,TQOB,EQOB", secCode).Result;
                        }
                        catch
                        {
                            textBoxLogsWindow.AppendText("Ошибка определения класса инструмента. Убедитесь, что тикер указан правильно" + Environment.NewLine);
                        }
                        if (classCode != null && classCode != "")
                        {
                            textBoxClassCode.Text = classCode;
                            textBoxLogsWindow.AppendText("Определяем код клиента..." + Environment.NewLine);
                            clientCode = _quik.Class.GetClientCode().Result;
                            textBoxClientCode.Text = clientCode;
                            textBoxLogsWindow.AppendText("Создаем экземпляр инструмента " + secCode + "|" + classCode + "..." + Environment.NewLine);
                            tools.Add(new Tool(_quik, secCode, classCode, settings.KoefSlip));
                            positions.Add(new Position());

                            if (tools[i] != null && tools[i].Name != null && tools[i].Name != "")
                            {
                                textBoxLogsWindow.AppendText("Инструмент " + tools[i].Name + " создан." + Environment.NewLine);
                                if (secCodeindex == i)
                                {
                                    textBoxAccountID.Text = tools[i].AccountID;
                                    textBoxFirmID.Text = tools[i].FirmID;
                                    textBoxShortName.Text = tools[i].Name;
                                    textBoxLot.Text = Convert.ToString(tools[i].Lot);
                                    textBoxStep.Text = Convert.ToString(tools[i].Step);
                                    textBoxValue.Text = Convert.ToString(tools[i].GuaranteeProviding);
                                    textBoxLastPrice.Text = Convert.ToString(tools[i].LastPrice);
                                    textBoxCoupon.Text = Convert.ToString(GetPositionT2(_quik, tools[i], clientCode));
                                    //tools[i].

                                }
                                textBoxLogsWindow.AppendText("Подписываемся на стакан..." + Environment.NewLine);
                                _quik.OrderBook.Subscribe(tools[i].ClassCode, tools[i].SecurityCode).Wait();
                                isSubscribedToolOrderBook = _quik.OrderBook.IsSubscribed(tools[i].ClassCode, tools[i].SecurityCode).Result;


                                if (isSubscribedToolOrderBook)
                                {
                                    textBoxLogsWindow.AppendText("Подписка на стакан прошла успешно." + Environment.NewLine);

                                    toolsOrderBook.Add(new OrderBook());
                                    // toolOrderBook = new OrderBook();



                                    timerRenewForm.Enabled = true;
                                    started = true;

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

                            buttonStartStop.Text = "СТОП";

                        }
                        i += 1;
                    }

                }
                catch
                {
                    textBoxLogsWindow.AppendText("Ошибка получения данных по инструменту." + Environment.NewLine);
                }
                textBoxLogsWindow.AppendText("Подписываемся на колбэк 'OnQuote'..." + Environment.NewLine);
                _quik.Events.OnQuote += OnQuoteDo;

            }
        }
        void Run()
        {
            //State entry = new State();

            //InMemoryStorage tasks = new InMemoryStorage();
            IDictionary<string,string> tasks = new IDictionary<string, string>();
            //entry = State.Active;

            //tasks.Set("111//111", entry);
         
            


            GetIndicators();
            HandleIncomingTransactions();

            if (!pause)//пауза на время обработки входящих транзакций
            {
                
                CheckConditionEntrance();
            }



            textBoxBestBid.Text = "НД";
            textBoxBestOffer.Text = "НД";

            if( positions.Count != 0 )
            {  
                textBoxPositionPE.Text = Math.Round(positions[secCodeindex].priceEntrance, tools[secCodeindex].PriceAccuracy).ToString();
                textBoxPositionQty.Text = positions[secCodeindex].toolQty.ToString();
            }

            if (toolsOrderBook != null)
            {
                if (toolsOrderBook[secCodeindex].bid != null)
                {
                    textBoxBestBid.Text = Convert.ToString(toolsOrderBook[secCodeindex].bid[0].price);
                }

            }

            if ( tools != null)
            {
                if (tools[secCodeindex].CurrentACY != -999)
                {
                    textBoxACY.Text = Convert.ToString(tools[secCodeindex].CurrentACY + "%");
                }
                if (tools[secCodeindex].Offer != -999)
                {
                    textBoxBestOffer.Text = Convert.ToString(tools[secCodeindex].Offer);
                }

                //try
                //{
                //    listOrders = _quik.Orders.GetOrders().Result;
                //    foreach (Order _order in listOrders)
                //    {
                //        if (_order.TransID == transactionID && _order.ClassCode == tools[secCodeindex].ClassCode && _order.SecCode == tools[secCodeindex].SecurityCode)
                //        {
                //            textBoxLogsWindow.AppendText("Заявка выставлена. Номер заявки - " + _order.OrderNum + Environment.NewLine);

                //            textBoxOrderDirection.Text = _order.Operation.ToString();
                //            textBoxOrderBalance.Text = _order.Balance.ToString();
                //            textBoxOrderNumber.Text = _order.OrderNum.ToString();
                //            textBoxOrderPrice.Text = _order.Price.ToString();
                //            textBoxOrderQty.Text = _order.Quantity.ToString();
                //            order = _order;
                //        }
                //    }
                //}
                //catch (Exception er)
                //{
                //    textBoxLogsWindow.AppendText("Ошибка получения номера заявки. Error: " + er.Message + Environment.NewLine);
                //}
            }
                
            
            
            
            //if (futuresPosition != null) textBoxVarMargin.Text = futuresPosition.varMargin.ToString(); 

        }
        void HandleIncomingTransactions()//обработчик входящих транзакций
        {
            try
            {
                keys = _quik.Storage.Keys();//получаем список ключей хранилища транзакций
                foreach (string key in keys)//обрабатываем все транзакции в хранилище
                {
                    var trans = _quik.Storage.Get<Transaction>(key);//получаем транзакцию из списка
                                       
                }
            }
            catch (Exception er)
            {
                textBoxLogsWindow.AppendText("Ошибка получения очереди транзакций. Error: " + er.Message + Environment.NewLine);
            }
            

        }
        void GetIndicators()
        {

            decimal offer = 0;
            decimal coupon;
            decimal value;
            decimal couponPeriod;
            decimal lastPrice;

            if(tools != null)//если tools существует, обрабатываем
                for ( int i = 0; i <= tools.Capacity-1;i++)
                {
                    coupon = tools[i].Coupon;
                    value = tools[i].Value;
                    couponPeriod = tools[i].CouponPeriod;
                    lastPrice = tools[i].LastPrice;
              
                    if (toolsOrderBook != null)
                    {
                        for (int j = 0; j <= toolsOrderBook.Capacity-1; j++)
                        {
                            if (toolsOrderBook[j].sec_code != null && toolsOrderBook[j].class_code != null)
                                if (tools[i].SecurityCode == toolsOrderBook[j].sec_code && tools[i].ClassCode == toolsOrderBook[j].class_code)
                                {
                                    if (toolsOrderBook[i].offer != null)
                                    {
                                        tools[i].Offer = toolsOrderBook[i].offer[0].price;
                                        offer = Convert.ToDecimal(tools[i].Offer);
                                    }
                                    break;
                                }
                        }
                    }

                    if (value != 0 && offer != 0 && coupon != 0 && couponPeriod != 0)
                    {
                        tools[i].CurrentACY = Convert.ToDouble(Math.Round(((364 / couponPeriod) * coupon / value) / (offer / 100), 5) * 100);
                    }
                    else
                    {
                        tools[i].CurrentACY = -999;
                    }

                    //if (settings.RobotMode == "Боевой")
                    //{
                    //    int toolQty = GetPositionT2(_quik, tools[i], clientCode);
                    //}
                    //else
                    //{
                    //    toolQty = position.toolQty;
                    //}

                    i += 1;
                }
           
        }
        void CheckConditionEntrance()
        {
            // при подходящей доходности отправляем заявку на покупку
            int i = 0;
  
            foreach (Tool tool in tools)
            {
                if(positions[i].toolQty == 0 )
                {
                    if (tool.CurrentACY >= tool.GoalACY )
                    {
                        if(tool.Offer != -999)
                        {
                            decimal priceEntrance = Convert.ToDecimal(tool.Offer) + tool.Slip;
                            
                            EntrancePosition(Operation.Buy, priceEntrance, settings.QtyOrder, i);
                            textBoxLogsWindow.AppendText("Сигнал на вход в позицию (long): " + tool.SecurityCode + " : " + priceEntrance.ToString() + Environment.NewLine);
                        }
                    }
                }
                else
                {
                    ;
                }

                i++;
            }

            //if (!entrancePause)
            //{
            //    if ((double)tool.LastPrice > (double)indicatorPriceChannel.Rows[indicatorPriceChannel.Rows.Count - 1]["High"] && (double)indicatorPriceChannel.Rows[indicatorPriceChannel.Rows.Count - 1]["High"] > 0)
            //    {
            //        entrancePause = true;
            //        textBoxLogs.AppendText("Сигнал на вход в позицию (long)!" + Environment.NewLine);
            //        decimal priceEntrance = tool.LastPrice + tool.Slip;
            //        EntrancePosition(Operation.Buy, priceEntrance, settings.QtyOrder);
            //    }
            //    else if ((double)tool.LastPrice < (double)indicatorPriceChannel.Rows[indicatorPriceChannel.Rows.Count - 1]["Low"] && tool.LastPrice > 0 && (double)indicatorPriceChannel.Rows[indicatorPriceChannel.Rows.Count - 1]["Low"] > 0)
            //    {
            //        entrancePause = true;
            //        textBoxLogs.AppendText("Сигнал на вход в позицию (short)!" + Environment.NewLine);
            //        decimal priceEntrance = tool.LastPrice - tool.Slip;
            //        EntrancePosition(Operation.Sell, priceEntrance, settings.QtyOrder);
            //    }
            //}
        }
        void EntrancePosition(Operation operation, decimal price, int qty, int toolID)
        {
            if(!pause)
            {
                if (settings.RobotMode == "Боевой")
                {
                    positions[toolID].entranceOrderID = NewOrder(_quik, tools[toolID], operation, price, qty);
                    if (positions[toolID].entranceOrderID != 0)
                    {
                        pause = true;
                        positions[toolID].priceEntrance = price;
                        positions[toolID].dateTimeEntrance = DateTime.Now;
                        textBoxLogsWindow.AppendText("ID заявки - " + positions[toolID].entranceOrderID + Environment.NewLine);
                    }
                }
                else
                {
                    if (operation == Operation.Buy) positions[toolID].toolQty = settings.QtyOrder * tools[toolID].Lot;
                    else positions[toolID].toolQty = settings.QtyOrder * tools[toolID].Lot * -1;
                    textBoxLogsWindow.AppendText("ID заявки - ТЕСТ" + Environment.NewLine);

                    positions[toolID].priceEntrance = price;
                    positions[toolID].dateTimeEntrance = DateTime.Now;

                    NewPos2Table(tools[toolID].Name, positions[toolID].toolQty, positions[toolID].dateTimeEntrance, positions[toolID].priceEntrance, toolID);
                }
            }
        }

        void NewPos2Table(string toolName, int qty, DateTime timeEntr, decimal price, int toolID)
        {
            dataGridViewDeals.Rows.Add();
            int i = dataGridViewDeals.Rows.Count - 1;
            dataGridViewDeals.Rows[i].Cells["ToolName"].Value = toolName;
            dataGridViewDeals.Rows[i].Cells["Qty"].Value = qty;
            dataGridViewDeals.Rows[i].Cells["TimeEntr"].Value = timeEntr.ToShortTimeString();
            dataGridViewDeals.Rows[i].Cells["PriceEntr"].Value = Math.Round(price, tools[toolID].PriceAccuracy);
            int LastRow = dataGridViewDeals.RowCount - 1;
            dataGridViewDeals.FirstDisplayedScrollingRowIndex = LastRow;
        }

        void OnQuoteDo(OrderBook quote)
        {
            int i = 0;
            foreach (Tool tool in tools)
            {
                if (tool.SecurityCode == quote.sec_code && tool.ClassCode == quote.class_code)
                {
                    toolsOrderBook[i] = quote;
                }
                i += 1;
            }
        }

        void OnParamDo(Param param)
        {
            
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

            textBoxLastPrice.Text = Convert.ToString(tools[secCodeindex].LastPrice);
            textBoxCoupon.Text = Convert.ToString(tools[secCodeindex].Coupon);
            textBoxValue.Text = Convert.ToString(tools[secCodeindex].Value);
            textBoxCPeriod.Text = Convert.ToString(tools[secCodeindex].CouponPeriod);

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
                        textBoxLogsWindow.AppendText("Выставляем заявку на покупку, по цене:" + priceInOrder + " ..." + Environment.NewLine);
                        long transactionID = NewOrder(_quik, tools[secCodeindex], Operation.Buy, priceInOrder, 1);
                        if (transactionID > 0)
                        {
                            Thread.Sleep(500);
                            textBoxLogsWindow.AppendText("Заявка выставлена. ID транзакции - " + transactionID + Environment.NewLine);
                            try
                            {
                                listOrders = _quik.Orders.GetOrders().Result;
                                foreach (Order _order in listOrders)
                                {
                                    if (_order.TransID == transactionID && _order.ClassCode == tools[secCodeindex].ClassCode && _order.SecCode == tools[secCodeindex].SecurityCode)
                                    {
                                        textBoxLogsWindow.AppendText("Заявка выставлена. Номер заявки - " + _order.OrderNum + Environment.NewLine);
                                        
                                        textBoxOrderDirection.Text = _order.Operation.ToString();
                                        textBoxOrderBalance.Text = _order.Balance.ToString();
                                        textBoxOrderNumber.Text = _order.OrderNum.ToString();
                                        textBoxOrderPrice.Text = _order.Price.ToString();
                                        textBoxOrderQty.Text = _order.Quantity.ToString();
                                        order = _order;
                                    }
                                }
                            }
                            catch (Exception er)
                            {
                                textBoxLogsWindow.AppendText("Ошибка получения номера заявки. Error: " + er.Message + Environment.NewLine);
                            }
                        }
                        else
                        {
                            textBoxLogsWindow.AppendText("Неудачная попытка выставления заявки." + Environment.NewLine);
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
                        textBoxLogsWindow.AppendText("Выставляем заявку на покупку, по цене:" + priceInOrder + " ..." + Environment.NewLine);
                        long transactionID = NewOrder(_quik, tools[secCodeindex], Operation.Buy, priceInOrder, 1);
                        if (transactionID > 0)
                        {
                            textBoxLogsWindow.AppendText("Заявка выставлена. ID транзакции - " + transactionID + Environment.NewLine);
                            Thread.Sleep(500);
                            try
                            {
                                listOrders = _quik.Orders.GetOrders().Result;
                                foreach (Order _order in listOrders)
                                {
                                    if (_order.TransID == transactionID && _order.ClassCode == tools[secCodeindex].ClassCode && _order.SecCode == tools[secCodeindex].SecurityCode)
                                    {
                                        textBoxLogsWindow.AppendText("Заявка выставлена. Номер заявки - " + _order.OrderNum + Environment.NewLine);
                                        textBoxOrderNumber.Text = _order.OrderNum.ToString();
                                        order = _order;
                                    }
                                    else
                                    {
                                        textBoxOrderNumber.Text = "---";
                                    }
                                }
                            }
                            catch
                            {
                                textBoxLogsWindow.AppendText("Ошибка получения номера заявки." + Environment.NewLine);
                            }
                        }
                        else
                        {
                            textBoxLogsWindow.AppendText("Неудачная попытка выставления заявки." + Environment.NewLine);
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
                        if (order != null && order.OrderNum > 0)
                        {
                            textBoxLogsWindow.AppendText("Удаляем заявку на покупку с номером - " + order.OrderNum + " ..." + Environment.NewLine);
                        }
                        long x = _quik.Orders.KillOrder(order).Result;
                        textBoxLogsWindow.AppendText("Результат - " + x + " ..." + Environment.NewLine);
                        textBoxOrderNumber.Text = "";
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
                        listOrders = _quik.Orders.GetOrders().Result;

                        if (listOrders.Count > 0)
                        {
                            textBoxLogsWindow.AppendText("Выводим данные о заявках в таблицу..." + Environment.NewLine);
                            toolCandlesTable = new FormOutputTable(listOrders);
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
                        listTrades = _quik.Trading.GetTrades().Result;

                        if (listTrades.Count > 0)
                        {
                            textBoxLogsWindow.AppendText("Выводим данные о сделках в таблицу..." + Environment.NewLine);
                            toolCandlesTable = new FormOutputTable(listTrades);
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
                catch (Exception e) { Console.WriteLine("GetPositionT2: SPBFUT, ошибка - " + e.Message); }
            }
            else
            {
                // акции
                try
                {
                    DepoLimitEx q1 = _quik.Trading.GetDepoEx(instrument.FirmID, clientCode, instrument.SecurityCode, instrument.AccountID, 2).Result;
                    if (q1 != null) qty = Convert.ToInt32(q1.CurrentBalance);
                }
                catch (Exception e) { Console.WriteLine("GetPositionT2: ошибка - " + e.Message); }
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
                    Console.WriteLine("Неудачная попытка отправки заявки");
                }
            }
            else
            {
                res = -999;
            }

            return res;
        }

        private void listBoxSecCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            secCode = listBoxSecCode.SelectedItem.ToString();
            secCodeindex = listBoxSecCode.SelectedIndex;
            textBoxSecCode.Text = secCode;
        }

        private void textBoxShortName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void labelQty_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.RobotMode = comboBoxMode.SelectedItem.ToString();
        }
    }
}
