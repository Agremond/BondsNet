namespace QuikSharpDemo
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxLogsWindow = new System.Windows.Forms.TextBox();
            this.listBoxCommands = new System.Windows.Forms.ListBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.buttonCommandRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSecCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxClassCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAccountID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxClientCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxFirmID = new System.Windows.Forms.TextBox();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxShortName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxLot = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxStep = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxBestBid = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxLastPrice = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxBestOffer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.timerRenewForm = new System.Windows.Forms.Timer(this.components);
            this.textBoxOrderNumber = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxCoupon = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxCPeriod = new System.Windows.Forms.TextBox();
            this.listBoxSecCode = new System.Windows.Forms.ListBox();
            this.textBoxACY = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dataGridViewDeals = new System.Windows.Forms.DataGridView();
            this.ToolName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeEntr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceEntr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeExit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceExit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Profit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SummProfit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxActiveOrder = new System.Windows.Forms.GroupBox();
            this.textBoxOrderDirection = new System.Windows.Forms.TextBox();
            this.labelOrderDirection = new System.Windows.Forms.Label();
            this.labelOrderNumber = new System.Windows.Forms.Label();
            this.textBoxOrderPrice = new System.Windows.Forms.TextBox();
            this.labelOrderPrice = new System.Windows.Forms.Label();
            this.textBoxOrderBalance = new System.Windows.Forms.TextBox();
            this.labelOrderBalance = new System.Windows.Forms.Label();
            this.textBoxOrderQty = new System.Windows.Forms.TextBox();
            this.labelOrderQty = new System.Windows.Forms.Label();
            this.groupBoxPosition = new System.Windows.Forms.GroupBox();
            this.textBoxPositionProfit = new System.Windows.Forms.TextBox();
            this.labelPositionProfit = new System.Windows.Forms.Label();
            this.textBoxPositionSL = new System.Windows.Forms.TextBox();
            this.labelSL = new System.Windows.Forms.Label();
            this.textBoxPositionPE = new System.Windows.Forms.TextBox();
            this.labelPositionPE = new System.Windows.Forms.Label();
            this.buttonPositionReset = new System.Windows.Forms.Button();
            this.textBoxPositionQty = new System.Windows.Forms.TextBox();
            this.labelPositionQty = new System.Windows.Forms.Label();
            this.textBoxPositionDirection = new System.Windows.Forms.TextBox();
            this.labelDirection = new System.Windows.Forms.Label();
            this.groupBoxRobotSettings = new System.Windows.Forms.GroupBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.labelMode = new System.Windows.Forms.Label();
            this.labelQty = new System.Windows.Forms.Label();
            this.labelFormula = new System.Windows.Forms.Label();
            this.labelSlip = new System.Windows.Forms.Label();
            this.textBoxQty = new System.Windows.Forms.TextBox();
            this.textBoxSlip = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeals)).BeginInit();
            this.groupBoxActiveOrder.SuspendLayout();
            this.groupBoxPosition.SuspendLayout();
            this.groupBoxRobotSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.Location = new System.Drawing.Point(12, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(221, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "CONNECT";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxLogsWindow
            // 
            this.textBoxLogsWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLogsWindow.Location = new System.Drawing.Point(243, 403);
            this.textBoxLogsWindow.Multiline = true;
            this.textBoxLogsWindow.Name = "textBoxLogsWindow";
            this.textBoxLogsWindow.ReadOnly = true;
            this.textBoxLogsWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogsWindow.Size = new System.Drawing.Size(1187, 132);
            this.textBoxLogsWindow.TabIndex = 1;
            // 
            // listBoxCommands
            // 
            this.listBoxCommands.FormattingEnabled = true;
            this.listBoxCommands.Location = new System.Drawing.Point(805, 23);
            this.listBoxCommands.Name = "listBoxCommands";
            this.listBoxCommands.Size = new System.Drawing.Size(229, 69);
            this.listBoxCommands.TabIndex = 2;
            this.listBoxCommands.SelectedIndexChanged += new System.EventHandler(this.listBoxCommands_SelectedIndexChanged);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.ForeColor = System.Drawing.Color.RoyalBlue;
            this.textBoxDescription.Location = new System.Drawing.Point(1040, 23);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.Size = new System.Drawing.Size(310, 69);
            this.textBoxDescription.TabIndex = 3;
            // 
            // buttonCommandRun
            // 
            this.buttonCommandRun.Location = new System.Drawing.Point(754, 23);
            this.buttonCommandRun.Name = "buttonCommandRun";
            this.buttonCommandRun.Size = new System.Drawing.Size(45, 69);
            this.buttonCommandRun.TabIndex = 4;
            this.buttonCommandRun.Text = "=>";
            this.buttonCommandRun.UseVisualStyleBackColor = true;
            this.buttonCommandRun.Click += new System.EventHandler(this.buttonCommandRun_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "SecCode";
            // 
            // textBoxSecCode
            // 
            this.textBoxSecCode.Location = new System.Drawing.Point(93, 42);
            this.textBoxSecCode.Name = "textBoxSecCode";
            this.textBoxSecCode.Size = new System.Drawing.Size(140, 20);
            this.textBoxSecCode.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "ClassCode";
            // 
            // textBoxClassCode
            // 
            this.textBoxClassCode.Enabled = false;
            this.textBoxClassCode.Location = new System.Drawing.Point(93, 98);
            this.textBoxClassCode.Name = "textBoxClassCode";
            this.textBoxClassCode.Size = new System.Drawing.Size(140, 20);
            this.textBoxClassCode.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "AccountID";
            // 
            // textBoxAccountID
            // 
            this.textBoxAccountID.Enabled = false;
            this.textBoxAccountID.Location = new System.Drawing.Point(93, 151);
            this.textBoxAccountID.Name = "textBoxAccountID";
            this.textBoxAccountID.Size = new System.Drawing.Size(140, 20);
            this.textBoxAccountID.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "ClientCode";
            // 
            // textBoxClientCode
            // 
            this.textBoxClientCode.Enabled = false;
            this.textBoxClientCode.Location = new System.Drawing.Point(93, 124);
            this.textBoxClientCode.Name = "textBoxClientCode";
            this.textBoxClientCode.Size = new System.Drawing.Size(140, 20);
            this.textBoxClientCode.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "FirmID";
            // 
            // textBoxFirmID
            // 
            this.textBoxFirmID.Enabled = false;
            this.textBoxFirmID.Location = new System.Drawing.Point(93, 177);
            this.textBoxFirmID.Name = "textBoxFirmID";
            this.textBoxFirmID.Size = new System.Drawing.Size(140, 20);
            this.textBoxFirmID.TabIndex = 6;
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStartStop.Location = new System.Drawing.Point(13, 69);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(220, 23);
            this.buttonStartStop.TabIndex = 0;
            this.buttonStartStop.Text = "СТАРТ";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "ShortName";
            // 
            // textBoxShortName
            // 
            this.textBoxShortName.Enabled = false;
            this.textBoxShortName.Location = new System.Drawing.Point(93, 213);
            this.textBoxShortName.Name = "textBoxShortName";
            this.textBoxShortName.Size = new System.Drawing.Size(140, 20);
            this.textBoxShortName.TabIndex = 6;
            this.textBoxShortName.TextChanged += new System.EventHandler(this.textBoxShortName_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 242);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Lot";
            // 
            // textBoxLot
            // 
            this.textBoxLot.Enabled = false;
            this.textBoxLot.Location = new System.Drawing.Point(93, 239);
            this.textBoxLot.Name = "textBoxLot";
            this.textBoxLot.Size = new System.Drawing.Size(140, 20);
            this.textBoxLot.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 268);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Шаг цены";
            // 
            // textBoxStep
            // 
            this.textBoxStep.Enabled = false;
            this.textBoxStep.Location = new System.Drawing.Point(93, 265);
            this.textBoxStep.Name = "textBoxStep";
            this.textBoxStep.Size = new System.Drawing.Size(140, 20);
            this.textBoxStep.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(498, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Best Bid";
            // 
            // textBoxBestBid
            // 
            this.textBoxBestBid.Enabled = false;
            this.textBoxBestBid.Location = new System.Drawing.Point(571, 153);
            this.textBoxBestBid.Name = "textBoxBestBid";
            this.textBoxBestBid.Size = new System.Drawing.Size(140, 20);
            this.textBoxBestBid.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(498, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Last Price";
            // 
            // textBoxLastPrice
            // 
            this.textBoxLastPrice.Enabled = false;
            this.textBoxLastPrice.Location = new System.Drawing.Point(571, 127);
            this.textBoxLastPrice.Name = "textBoxLastPrice";
            this.textBoxLastPrice.Size = new System.Drawing.Size(140, 20);
            this.textBoxLastPrice.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(498, 104);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Best Offer";
            // 
            // textBoxBestOffer
            // 
            this.textBoxBestOffer.Enabled = false;
            this.textBoxBestOffer.Location = new System.Drawing.Point(571, 101);
            this.textBoxBestOffer.Name = "textBoxBestOffer";
            this.textBoxBestOffer.Size = new System.Drawing.Size(140, 20);
            this.textBoxBestOffer.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 293);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Номинал";
            // 
            // textBoxValue
            // 
            this.textBoxValue.Enabled = false;
            this.textBoxValue.Location = new System.Drawing.Point(93, 289);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(140, 20);
            this.textBoxValue.TabIndex = 6;
            // 
            // timerRenewForm
            // 
            this.timerRenewForm.Interval = 500;
            this.timerRenewForm.Tick += new System.EventHandler(this.timerRenewForm_Tick);
            // 
            // textBoxOrderNumber
            // 
            this.textBoxOrderNumber.Enabled = false;
            this.textBoxOrderNumber.Location = new System.Drawing.Point(56, 19);
            this.textBoxOrderNumber.Name = "textBoxOrderNumber";
            this.textBoxOrderNumber.Size = new System.Drawing.Size(176, 20);
            this.textBoxOrderNumber.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(20, 317);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Купон";
            // 
            // textBoxCoupon
            // 
            this.textBoxCoupon.Enabled = false;
            this.textBoxCoupon.Location = new System.Drawing.Point(93, 314);
            this.textBoxCoupon.Name = "textBoxCoupon";
            this.textBoxCoupon.Size = new System.Drawing.Size(140, 20);
            this.textBoxCoupon.TabIndex = 6;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(20, 343);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Куп.Период";
            // 
            // textBoxCPeriod
            // 
            this.textBoxCPeriod.Enabled = false;
            this.textBoxCPeriod.Location = new System.Drawing.Point(93, 340);
            this.textBoxCPeriod.Name = "textBoxCPeriod";
            this.textBoxCPeriod.Size = new System.Drawing.Size(140, 20);
            this.textBoxCPeriod.TabIndex = 6;
            // 
            // listBoxSecCode
            // 
            this.listBoxSecCode.FormattingEnabled = true;
            this.listBoxSecCode.Location = new System.Drawing.Point(243, 185);
            this.listBoxSecCode.Name = "listBoxSecCode";
            this.listBoxSecCode.Size = new System.Drawing.Size(130, 212);
            this.listBoxSecCode.TabIndex = 9;
            this.listBoxSecCode.SelectedIndexChanged += new System.EventHandler(this.listBoxSecCode_SelectedIndexChanged);
            // 
            // textBoxACY
            // 
            this.textBoxACY.Enabled = false;
            this.textBoxACY.Location = new System.Drawing.Point(322, 101);
            this.textBoxACY.Name = "textBoxACY";
            this.textBoxACY.Size = new System.Drawing.Size(140, 20);
            this.textBoxACY.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(250, 103);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Доходность";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // dataGridViewDeals
            // 
            this.dataGridViewDeals.AllowUserToAddRows = false;
            this.dataGridViewDeals.AllowUserToDeleteRows = false;
            this.dataGridViewDeals.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDeals.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDeals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDeals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ToolName,
            this.Qty,
            this.TimeEntr,
            this.PriceEntr,
            this.TimeExit,
            this.PriceExit,
            this.Profit,
            this.SummProfit});
            this.dataGridViewDeals.Location = new System.Drawing.Point(752, 101);
            this.dataGridViewDeals.MultiSelect = false;
            this.dataGridViewDeals.Name = "dataGridViewDeals";
            this.dataGridViewDeals.ReadOnly = true;
            this.dataGridViewDeals.RowHeadersVisible = false;
            this.dataGridViewDeals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewDeals.Size = new System.Drawing.Size(598, 291);
            this.dataGridViewDeals.TabIndex = 12;
            // 
            // ToolName
            // 
            this.ToolName.HeaderText = "Инструмент";
            this.ToolName.Name = "ToolName";
            this.ToolName.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Кол-во";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 50;
            // 
            // TimeEntr
            // 
            this.TimeEntr.HeaderText = "Время входа";
            this.TimeEntr.Name = "TimeEntr";
            this.TimeEntr.ReadOnly = true;
            this.TimeEntr.Width = 60;
            // 
            // PriceEntr
            // 
            this.PriceEntr.HeaderText = "Цена входа";
            this.PriceEntr.Name = "PriceEntr";
            this.PriceEntr.ReadOnly = true;
            this.PriceEntr.Width = 80;
            // 
            // TimeExit
            // 
            this.TimeExit.HeaderText = "Время выхода";
            this.TimeExit.Name = "TimeExit";
            this.TimeExit.ReadOnly = true;
            this.TimeExit.Width = 60;
            // 
            // PriceExit
            // 
            this.PriceExit.HeaderText = "Цена выхода";
            this.PriceExit.Name = "PriceExit";
            this.PriceExit.ReadOnly = true;
            this.PriceExit.Width = 80;
            // 
            // Profit
            // 
            this.Profit.HeaderText = "Результат";
            this.Profit.Name = "Profit";
            this.Profit.ReadOnly = true;
            this.Profit.Width = 80;
            // 
            // SummProfit
            // 
            this.SummProfit.HeaderText = "Итого";
            this.SummProfit.Name = "SummProfit";
            this.SummProfit.ReadOnly = true;
            this.SummProfit.Width = 80;
            // 
            // groupBoxActiveOrder
            // 
            this.groupBoxActiveOrder.Controls.Add(this.textBoxOrderDirection);
            this.groupBoxActiveOrder.Controls.Add(this.labelOrderDirection);
            this.groupBoxActiveOrder.Controls.Add(this.labelOrderNumber);
            this.groupBoxActiveOrder.Controls.Add(this.textBoxOrderPrice);
            this.groupBoxActiveOrder.Controls.Add(this.labelOrderPrice);
            this.groupBoxActiveOrder.Controls.Add(this.textBoxOrderBalance);
            this.groupBoxActiveOrder.Controls.Add(this.labelOrderBalance);
            this.groupBoxActiveOrder.Controls.Add(this.textBoxOrderQty);
            this.groupBoxActiveOrder.Controls.Add(this.labelOrderQty);
            this.groupBoxActiveOrder.Controls.Add(this.textBoxOrderNumber);
            this.groupBoxActiveOrder.Location = new System.Drawing.Point(382, 177);
            this.groupBoxActiveOrder.Name = "groupBoxActiveOrder";
            this.groupBoxActiveOrder.Size = new System.Drawing.Size(362, 105);
            this.groupBoxActiveOrder.TabIndex = 13;
            this.groupBoxActiveOrder.TabStop = false;
            this.groupBoxActiveOrder.Text = "Активная заявка";
            // 
            // textBoxOrderDirection
            // 
            this.textBoxOrderDirection.Location = new System.Drawing.Point(235, 45);
            this.textBoxOrderDirection.Name = "textBoxOrderDirection";
            this.textBoxOrderDirection.ReadOnly = true;
            this.textBoxOrderDirection.Size = new System.Drawing.Size(63, 20);
            this.textBoxOrderDirection.TabIndex = 0;
            this.textBoxOrderDirection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelOrderDirection
            // 
            this.labelOrderDirection.AutoSize = true;
            this.labelOrderDirection.Location = new System.Drawing.Point(151, 48);
            this.labelOrderDirection.Name = "labelOrderDirection";
            this.labelOrderDirection.Size = new System.Drawing.Size(78, 13);
            this.labelOrderDirection.TabIndex = 1;
            this.labelOrderDirection.Text = "Направление:";
            // 
            // labelOrderNumber
            // 
            this.labelOrderNumber.AutoSize = true;
            this.labelOrderNumber.Location = new System.Drawing.Point(6, 22);
            this.labelOrderNumber.Name = "labelOrderNumber";
            this.labelOrderNumber.Size = new System.Drawing.Size(44, 13);
            this.labelOrderNumber.TabIndex = 1;
            this.labelOrderNumber.Text = "Номер:";
            // 
            // textBoxOrderPrice
            // 
            this.textBoxOrderPrice.Location = new System.Drawing.Point(82, 45);
            this.textBoxOrderPrice.Name = "textBoxOrderPrice";
            this.textBoxOrderPrice.ReadOnly = true;
            this.textBoxOrderPrice.Size = new System.Drawing.Size(63, 20);
            this.textBoxOrderPrice.TabIndex = 0;
            this.textBoxOrderPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelOrderPrice
            // 
            this.labelOrderPrice.AutoSize = true;
            this.labelOrderPrice.Location = new System.Drawing.Point(6, 48);
            this.labelOrderPrice.Name = "labelOrderPrice";
            this.labelOrderPrice.Size = new System.Drawing.Size(36, 13);
            this.labelOrderPrice.TabIndex = 1;
            this.labelOrderPrice.Text = "Цена:";
            // 
            // textBoxOrderBalance
            // 
            this.textBoxOrderBalance.Location = new System.Drawing.Point(82, 71);
            this.textBoxOrderBalance.Name = "textBoxOrderBalance";
            this.textBoxOrderBalance.ReadOnly = true;
            this.textBoxOrderBalance.Size = new System.Drawing.Size(63, 20);
            this.textBoxOrderBalance.TabIndex = 0;
            this.textBoxOrderBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelOrderBalance
            // 
            this.labelOrderBalance.AutoSize = true;
            this.labelOrderBalance.Location = new System.Drawing.Point(6, 74);
            this.labelOrderBalance.Name = "labelOrderBalance";
            this.labelOrderBalance.Size = new System.Drawing.Size(52, 13);
            this.labelOrderBalance.TabIndex = 1;
            this.labelOrderBalance.Text = "Остаток:";
            // 
            // textBoxOrderQty
            // 
            this.textBoxOrderQty.Location = new System.Drawing.Point(235, 71);
            this.textBoxOrderQty.Name = "textBoxOrderQty";
            this.textBoxOrderQty.ReadOnly = true;
            this.textBoxOrderQty.Size = new System.Drawing.Size(63, 20);
            this.textBoxOrderQty.TabIndex = 0;
            this.textBoxOrderQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelOrderQty
            // 
            this.labelOrderQty.AutoSize = true;
            this.labelOrderQty.Location = new System.Drawing.Point(151, 74);
            this.labelOrderQty.Name = "labelOrderQty";
            this.labelOrderQty.Size = new System.Drawing.Size(69, 13);
            this.labelOrderQty.TabIndex = 1;
            this.labelOrderQty.Text = "Количество:";
            // 
            // groupBoxPosition
            // 
            this.groupBoxPosition.Controls.Add(this.textBoxPositionProfit);
            this.groupBoxPosition.Controls.Add(this.labelPositionProfit);
            this.groupBoxPosition.Controls.Add(this.textBoxPositionSL);
            this.groupBoxPosition.Controls.Add(this.labelSL);
            this.groupBoxPosition.Controls.Add(this.textBoxPositionPE);
            this.groupBoxPosition.Controls.Add(this.labelPositionPE);
            this.groupBoxPosition.Controls.Add(this.buttonPositionReset);
            this.groupBoxPosition.Controls.Add(this.textBoxPositionQty);
            this.groupBoxPosition.Controls.Add(this.labelPositionQty);
            this.groupBoxPosition.Controls.Add(this.textBoxPositionDirection);
            this.groupBoxPosition.Controls.Add(this.labelDirection);
            this.groupBoxPosition.Enabled = false;
            this.groupBoxPosition.Location = new System.Drawing.Point(382, 289);
            this.groupBoxPosition.Name = "groupBoxPosition";
            this.groupBoxPosition.Size = new System.Drawing.Size(362, 105);
            this.groupBoxPosition.TabIndex = 2;
            this.groupBoxPosition.TabStop = false;
            this.groupBoxPosition.Text = "Позиция";
            // 
            // textBoxPositionProfit
            // 
            this.textBoxPositionProfit.Location = new System.Drawing.Point(92, 71);
            this.textBoxPositionProfit.Name = "textBoxPositionProfit";
            this.textBoxPositionProfit.ReadOnly = true;
            this.textBoxPositionProfit.Size = new System.Drawing.Size(63, 20);
            this.textBoxPositionProfit.TabIndex = 0;
            this.textBoxPositionProfit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPositionProfit
            // 
            this.labelPositionProfit.AutoSize = true;
            this.labelPositionProfit.Location = new System.Drawing.Point(8, 74);
            this.labelPositionProfit.Name = "labelPositionProfit";
            this.labelPositionProfit.Size = new System.Drawing.Size(72, 13);
            this.labelPositionProfit.TabIndex = 1;
            this.labelPositionProfit.Text = "Тек. профит:";
            // 
            // textBoxPositionSL
            // 
            this.textBoxPositionSL.Location = new System.Drawing.Point(244, 45);
            this.textBoxPositionSL.Name = "textBoxPositionSL";
            this.textBoxPositionSL.ReadOnly = true;
            this.textBoxPositionSL.Size = new System.Drawing.Size(63, 20);
            this.textBoxPositionSL.TabIndex = 0;
            this.textBoxPositionSL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelSL
            // 
            this.labelSL.AutoSize = true;
            this.labelSL.Location = new System.Drawing.Point(160, 48);
            this.labelSL.Name = "labelSL";
            this.labelSL.Size = new System.Drawing.Size(61, 13);
            this.labelSL.TabIndex = 1;
            this.labelSL.Text = "Стоп-цена:";
            // 
            // textBoxPositionPE
            // 
            this.textBoxPositionPE.Location = new System.Drawing.Point(92, 45);
            this.textBoxPositionPE.Name = "textBoxPositionPE";
            this.textBoxPositionPE.ReadOnly = true;
            this.textBoxPositionPE.Size = new System.Drawing.Size(63, 20);
            this.textBoxPositionPE.TabIndex = 0;
            this.textBoxPositionPE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPositionPE
            // 
            this.labelPositionPE.AutoSize = true;
            this.labelPositionPE.Location = new System.Drawing.Point(8, 48);
            this.labelPositionPE.Name = "labelPositionPE";
            this.labelPositionPE.Size = new System.Drawing.Size(68, 13);
            this.labelPositionPE.TabIndex = 1;
            this.labelPositionPE.Text = "Цена входа:";
            // 
            // buttonPositionReset
            // 
            this.buttonPositionReset.ForeColor = System.Drawing.Color.DarkRed;
            this.buttonPositionReset.Location = new System.Drawing.Point(163, 71);
            this.buttonPositionReset.Name = "buttonPositionReset";
            this.buttonPositionReset.Size = new System.Drawing.Size(144, 23);
            this.buttonPositionReset.TabIndex = 0;
            this.buttonPositionReset.Text = "Закрыть позицию";
            this.buttonPositionReset.UseVisualStyleBackColor = true;
            // 
            // textBoxPositionQty
            // 
            this.textBoxPositionQty.Location = new System.Drawing.Point(244, 19);
            this.textBoxPositionQty.Name = "textBoxPositionQty";
            this.textBoxPositionQty.ReadOnly = true;
            this.textBoxPositionQty.Size = new System.Drawing.Size(63, 20);
            this.textBoxPositionQty.TabIndex = 0;
            this.textBoxPositionQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPositionQty
            // 
            this.labelPositionQty.AutoSize = true;
            this.labelPositionQty.Location = new System.Drawing.Point(160, 22);
            this.labelPositionQty.Name = "labelPositionQty";
            this.labelPositionQty.Size = new System.Drawing.Size(69, 13);
            this.labelPositionQty.TabIndex = 1;
            this.labelPositionQty.Text = "Количество:";
            // 
            // textBoxPositionDirection
            // 
            this.textBoxPositionDirection.Location = new System.Drawing.Point(92, 19);
            this.textBoxPositionDirection.Name = "textBoxPositionDirection";
            this.textBoxPositionDirection.ReadOnly = true;
            this.textBoxPositionDirection.Size = new System.Drawing.Size(63, 20);
            this.textBoxPositionDirection.TabIndex = 0;
            this.textBoxPositionDirection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelDirection
            // 
            this.labelDirection.AutoSize = true;
            this.labelDirection.Location = new System.Drawing.Point(8, 22);
            this.labelDirection.Name = "labelDirection";
            this.labelDirection.Size = new System.Drawing.Size(78, 13);
            this.labelDirection.TabIndex = 1;
            this.labelDirection.Text = "Направление:";
            // 
            // groupBoxRobotSettings
            // 
            this.groupBoxRobotSettings.Controls.Add(this.comboBoxMode);
            this.groupBoxRobotSettings.Controls.Add(this.labelMode);
            this.groupBoxRobotSettings.Controls.Add(this.labelQty);
            this.groupBoxRobotSettings.Controls.Add(this.labelFormula);
            this.groupBoxRobotSettings.Controls.Add(this.labelSlip);
            this.groupBoxRobotSettings.Controls.Add(this.textBoxQty);
            this.groupBoxRobotSettings.Controls.Add(this.textBoxSlip);
            this.groupBoxRobotSettings.Location = new System.Drawing.Point(239, 23);
            this.groupBoxRobotSettings.Name = "groupBoxRobotSettings";
            this.groupBoxRobotSettings.Size = new System.Drawing.Size(505, 60);
            this.groupBoxRobotSettings.TabIndex = 14;
            this.groupBoxRobotSettings.TabStop = false;
            this.groupBoxRobotSettings.Text = "Настройки робота";
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Items.AddRange(new object[] {
            "Виртуальный",
            "Боевой"});
            this.comboBoxMode.Location = new System.Drawing.Point(57, 24);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(101, 21);
            this.comboBoxMode.TabIndex = 2;
            // 
            // labelMode
            // 
            this.labelMode.AutoSize = true;
            this.labelMode.Location = new System.Drawing.Point(6, 27);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(45, 13);
            this.labelMode.TabIndex = 1;
            this.labelMode.Text = "Режим:";
            // 
            // labelQty
            // 
            this.labelQty.AutoSize = true;
            this.labelQty.Location = new System.Drawing.Point(174, 28);
            this.labelQty.Name = "labelQty";
            this.labelQty.Size = new System.Drawing.Size(45, 13);
            this.labelQty.TabIndex = 1;
            this.labelQty.Text = "Объем:";
            this.labelQty.Click += new System.EventHandler(this.labelQty_Click);
            // 
            // labelFormula
            // 
            this.labelFormula.AutoSize = true;
            this.labelFormula.Location = new System.Drawing.Point(408, 28);
            this.labelFormula.Name = "labelFormula";
            this.labelFormula.Size = new System.Drawing.Size(64, 13);
            this.labelFormula.TabIndex = 1;
            this.labelFormula.Text = "x Шаг цены";
            // 
            // labelSlip
            // 
            this.labelSlip.AutoSize = true;
            this.labelSlip.Location = new System.Drawing.Point(265, 28);
            this.labelSlip.Name = "labelSlip";
            this.labelSlip.Size = new System.Drawing.Size(110, 13);
            this.labelSlip.TabIndex = 1;
            this.labelSlip.Text = "Проскальзывание =";
            // 
            // textBoxQty
            // 
            this.textBoxQty.Location = new System.Drawing.Point(225, 25);
            this.textBoxQty.Name = "textBoxQty";
            this.textBoxQty.ReadOnly = true;
            this.textBoxQty.Size = new System.Drawing.Size(34, 20);
            this.textBoxQty.TabIndex = 0;
            this.textBoxQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxSlip
            // 
            this.textBoxSlip.Location = new System.Drawing.Point(378, 25);
            this.textBoxSlip.Name = "textBoxSlip";
            this.textBoxSlip.Size = new System.Drawing.Size(24, 20);
            this.textBoxSlip.TabIndex = 0;
            this.textBoxSlip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 547);
            this.Controls.Add(this.groupBoxRobotSettings);
            this.Controls.Add(this.groupBoxPosition);
            this.Controls.Add(this.groupBoxActiveOrder);
            this.Controls.Add(this.dataGridViewDeals);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBoxACY);
            this.Controls.Add(this.listBoxSecCode);
            this.Controls.Add(this.textBoxFirmID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxClientCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxAccountID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxClassCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxBestOffer);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxLastPrice);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxBestBid);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxCPeriod);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBoxCoupon);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxStep);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxLot);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxShortName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxSecCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCommandRun);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.listBoxCommands);
            this.Controls.Add(this.textBoxLogsWindow);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "FormMain";
            this.Text = "QuikSharp Demo";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeals)).EndInit();
            this.groupBoxActiveOrder.ResumeLayout(false);
            this.groupBoxActiveOrder.PerformLayout();
            this.groupBoxPosition.ResumeLayout(false);
            this.groupBoxPosition.PerformLayout();
            this.groupBoxRobotSettings.ResumeLayout(false);
            this.groupBoxRobotSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxLogsWindow;
        private System.Windows.Forms.ListBox listBoxCommands;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button buttonCommandRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSecCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxClassCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAccountID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxClientCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxFirmID;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxShortName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxLot;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxStep;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxBestBid;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxLastPrice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxBestOffer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Timer timerRenewForm;
        private System.Windows.Forms.TextBox textBoxOrderNumber;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxCoupon;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxCPeriod;
        private System.Windows.Forms.ListBox listBoxSecCode;
        private System.Windows.Forms.TextBox textBoxACY;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dataGridViewDeals;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToolName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeEntr;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceEntr;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Profit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SummProfit;
        private System.Windows.Forms.GroupBox groupBoxActiveOrder;
        private System.Windows.Forms.TextBox textBoxOrderDirection;
        private System.Windows.Forms.Label labelOrderDirection;
        private System.Windows.Forms.Label labelOrderNumber;
        private System.Windows.Forms.TextBox textBoxOrderPrice;
        private System.Windows.Forms.Label labelOrderPrice;
        private System.Windows.Forms.TextBox textBoxOrderBalance;
        private System.Windows.Forms.Label labelOrderBalance;
        private System.Windows.Forms.TextBox textBoxOrderQty;
        private System.Windows.Forms.Label labelOrderQty;
        private System.Windows.Forms.GroupBox groupBoxPosition;
        private System.Windows.Forms.TextBox textBoxPositionProfit;
        private System.Windows.Forms.Label labelPositionProfit;
        private System.Windows.Forms.TextBox textBoxPositionSL;
        private System.Windows.Forms.Label labelSL;
        private System.Windows.Forms.TextBox textBoxPositionPE;
        private System.Windows.Forms.Label labelPositionPE;
        private System.Windows.Forms.Button buttonPositionReset;
        private System.Windows.Forms.TextBox textBoxPositionQty;
        private System.Windows.Forms.Label labelPositionQty;
        private System.Windows.Forms.TextBox textBoxPositionDirection;
        private System.Windows.Forms.Label labelDirection;
        private System.Windows.Forms.GroupBox groupBoxRobotSettings;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.Label labelQty;
        private System.Windows.Forms.Label labelFormula;
        private System.Windows.Forms.Label labelSlip;
        private System.Windows.Forms.TextBox textBoxQty;
        private System.Windows.Forms.TextBox textBoxSlip;
    }
}

