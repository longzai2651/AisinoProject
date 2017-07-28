namespace Assist
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AisinoAssist = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemON = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemMiNi = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOUT = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.txtAbout = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbFrequency = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbDays = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnModify = new System.Windows.Forms.Button();
            this.rdbHandsSync = new System.Windows.Forms.RadioButton();
            this.rdbAtoAsync = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPSW = new System.Windows.Forms.TextBox();
            this.btnLogPathOpen = new System.Windows.Forms.Button();
            this.txtDataPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDataMax = new System.Windows.Forms.TextBox();
            this.btnSaveSetting = new System.Windows.Forms.Button();
            this.lblWar = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUpdateURL = new System.Windows.Forms.TextBox();
            this.txtAskURL = new System.Windows.Forms.TextBox();
            this.txtLoginURL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpBeginTime = new System.Windows.Forms.DateTimePicker();
            this.btnSync = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtRst = new System.Windows.Forms.RichTextBox();
            this.btnMiNi = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnChk = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AisinoAssist
            // 
            this.AisinoAssist.ContextMenuStrip = this.contextMenuStrip1;
            this.AisinoAssist.Text = "AisinoAssist";
            this.AisinoAssist.Visible = true;
            this.AisinoAssist.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemON,
            this.MenuItemMiNi,
            this.MenuItemOUT});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 70);
            // 
            // MenuItemON
            // 
            this.MenuItemON.Name = "MenuItemON";
            this.MenuItemON.Size = new System.Drawing.Size(148, 22);
            this.MenuItemON.Text = "显示";
            this.MenuItemON.Click += new System.EventHandler(this.MenuItemON_Click);
            // 
            // MenuItemMiNi
            // 
            this.MenuItemMiNi.Name = "MenuItemMiNi";
            this.MenuItemMiNi.Size = new System.Drawing.Size(148, 22);
            this.MenuItemMiNi.Text = "最小化到托盘";
            this.MenuItemMiNi.Click += new System.EventHandler(this.MenuItemMiNi_Click);
            // 
            // MenuItemOUT
            // 
            this.MenuItemOUT.Name = "MenuItemOUT";
            this.MenuItemOUT.Size = new System.Drawing.Size(148, 22);
            this.MenuItemOUT.Text = "退出";
            this.MenuItemOUT.Click += new System.EventHandler(this.MenuItemOUT_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.txtAbout);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(577, 381);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "关于";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // txtAbout
            // 
            this.txtAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAbout.Location = new System.Drawing.Point(0, 0);
            this.txtAbout.Name = "txtAbout";
            this.txtAbout.ReadOnly = true;
            this.txtAbout.Size = new System.Drawing.Size(577, 381);
            this.txtAbout.TabIndex = 2;
            this.txtAbout.Text = "\n\n\n\n进项发票协同工具\n航天信息股份有限公司";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.cmbFrequency);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.cmbDays);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.lblInfo);
            this.tabPage2.Controls.Add(this.btnModify);
            this.tabPage2.Controls.Add(this.rdbHandsSync);
            this.tabPage2.Controls.Add(this.rdbAtoAsync);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtPSW);
            this.tabPage2.Controls.Add(this.btnLogPathOpen);
            this.tabPage2.Controls.Add(this.txtDataPath);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.lblData);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.txtDataMax);
            this.tabPage2.Controls.Add(this.btnSaveSetting);
            this.tabPage2.Controls.Add(this.lblWar);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.txtUpdateURL);
            this.tabPage2.Controls.Add(this.txtAskURL);
            this.tabPage2.Controls.Add(this.txtLoginURL);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.dtpEndTime);
            this.tabPage2.Controls.Add(this.dtpBeginTime);
            this.tabPage2.Controls.Add(this.btnSync);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(577, 381);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "同步设置及数据同步";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmbFrequency
            // 
            this.cmbFrequency.Enabled = false;
            this.cmbFrequency.FormattingEnabled = true;
            this.cmbFrequency.Items.AddRange(new object[] {
            "8640",
            "4320",
            "360",
            "180",
            "6"});
            this.cmbFrequency.Location = new System.Drawing.Point(132, 140);
            this.cmbFrequency.Name = "cmbFrequency";
            this.cmbFrequency.Size = new System.Drawing.Size(121, 20);
            this.cmbFrequency.TabIndex = 29;
            this.cmbFrequency.Text = "6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 140);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 12);
            this.label11.TabIndex = 28;
            this.label11.Text = "同步频率(10000ms)：";
            // 
            // cmbDays
            // 
            this.cmbDays.Enabled = false;
            this.cmbDays.FormattingEnabled = true;
            this.cmbDays.Items.AddRange(new object[] {
            "30",
            "29",
            "28",
            "27",
            "26",
            "25",
            "24",
            "23",
            "22",
            "21",
            "20",
            "19",
            "18",
            "17",
            "16",
            "15",
            "14",
            "13",
            "12",
            "11",
            "10",
            "9",
            "8",
            "7",
            "6",
            "5",
            "4",
            "3",
            "2",
            "1"});
            this.cmbDays.Location = new System.Drawing.Point(132, 114);
            this.cmbDays.Name = "cmbDays";
            this.cmbDays.Size = new System.Drawing.Size(121, 20);
            this.cmbDays.TabIndex = 27;
            this.cmbDays.Text = "30";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 26;
            this.label10.Text = "自动同步天数：";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(259, 295);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(89, 12);
            this.lblInfo.TabIndex = 25;
            this.lblInfo.Text = "自动同步已关闭";
            this.lblInfo.Visible = false;
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(130, 193);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(123, 23);
            this.btnModify.TabIndex = 24;
            this.btnModify.Text = "修改设置(2)";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // rdbHandsSync
            // 
            this.rdbHandsSync.AutoSize = true;
            this.rdbHandsSync.Checked = true;
            this.rdbHandsSync.Location = new System.Drawing.Point(336, 276);
            this.rdbHandsSync.Name = "rdbHandsSync";
            this.rdbHandsSync.Size = new System.Drawing.Size(71, 16);
            this.rdbHandsSync.TabIndex = 23;
            this.rdbHandsSync.TabStop = true;
            this.rdbHandsSync.Text = "手动同步";
            this.rdbHandsSync.UseVisualStyleBackColor = true;
            this.rdbHandsSync.CheckedChanged += new System.EventHandler(this.rdbAtoAsync_CheckedChanged);
            this.rdbHandsSync.Click += new System.EventHandler(this.rdbAtoAsync_Click);
            // 
            // rdbAtoAsync
            // 
            this.rdbAtoAsync.AutoSize = true;
            this.rdbAtoAsync.Location = new System.Drawing.Point(259, 276);
            this.rdbAtoAsync.Name = "rdbAtoAsync";
            this.rdbAtoAsync.Size = new System.Drawing.Size(71, 16);
            this.rdbAtoAsync.TabIndex = 22;
            this.rdbAtoAsync.Text = "自动同步";
            this.rdbAtoAsync.UseVisualStyleBackColor = true;
            this.rdbAtoAsync.CheckedChanged += new System.EventHandler(this.rdbAtoAsync_CheckedChanged);
            this.rdbAtoAsync.Click += new System.EventHandler(this.rdbAtoAsync_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(46, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "金税盘密钥：";
            // 
            // txtPSW
            // 
            this.txtPSW.Location = new System.Drawing.Point(132, 6);
            this.txtPSW.Name = "txtPSW";
            this.txtPSW.ReadOnly = true;
            this.txtPSW.Size = new System.Drawing.Size(400, 21);
            this.txtPSW.TabIndex = 20;
            this.txtPSW.Text = "88888888";
            // 
            // btnLogPathOpen
            // 
            this.btnLogPathOpen.Location = new System.Drawing.Point(130, 344);
            this.btnLogPathOpen.Name = "btnLogPathOpen";
            this.btnLogPathOpen.Size = new System.Drawing.Size(123, 23);
            this.btnLogPathOpen.TabIndex = 19;
            this.btnLogPathOpen.Text = "打开(5)";
            this.btnLogPathOpen.UseVisualStyleBackColor = true;
            this.btnLogPathOpen.Click += new System.EventHandler(this.btnLogPathOpen_Click);
            // 
            // txtDataPath
            // 
            this.txtDataPath.Location = new System.Drawing.Point(130, 310);
            this.txtDataPath.Name = "txtDataPath";
            this.txtDataPath.ReadOnly = true;
            this.txtDataPath.Size = new System.Drawing.Size(398, 21);
            this.txtDataPath.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 313);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "缓存数据地址：";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(259, 169);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(143, 12);
            this.lblData.TabIndex = 16;
            this.lblData.Text = "kb  设置0不自动清除缓存";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 24);
            this.label9.TabIndex = 15;
            this.label9.Text = "缓存数据\r\n存储上限设置：";
            // 
            // txtDataMax
            // 
            this.txtDataMax.Location = new System.Drawing.Point(132, 166);
            this.txtDataMax.Name = "txtDataMax";
            this.txtDataMax.ReadOnly = true;
            this.txtDataMax.Size = new System.Drawing.Size(121, 21);
            this.txtDataMax.TabIndex = 14;
            this.txtDataMax.Text = "1024";
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.Location = new System.Drawing.Point(261, 193);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(123, 23);
            this.btnSaveSetting.TabIndex = 13;
            this.btnSaveSetting.Text = "保存设置(3)";
            this.btnSaveSetting.UseVisualStyleBackColor = true;
            this.btnSaveSetting.Click += new System.EventHandler(this.btnSaveSetting_Click);
            // 
            // lblWar
            // 
            this.lblWar.AutoSize = true;
            this.lblWar.ForeColor = System.Drawing.Color.Red;
            this.lblWar.Location = new System.Drawing.Point(413, 278);
            this.lblWar.Name = "lblWar";
            this.lblWar.Size = new System.Drawing.Size(137, 12);
            this.lblWar.TabIndex = 12;
            this.lblWar.Text = "未检测金税盘，无法同步";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "本地系统地址：";
            // 
            // txtUpdateURL
            // 
            this.txtUpdateURL.Location = new System.Drawing.Point(132, 87);
            this.txtUpdateURL.Name = "txtUpdateURL";
            this.txtUpdateURL.ReadOnly = true;
            this.txtUpdateURL.Size = new System.Drawing.Size(400, 21);
            this.txtUpdateURL.TabIndex = 10;
            this.txtUpdateURL.Text = "http://192.168.0.100:8080/AisinoYz/jxfp/xjxc/zdtb.action";
            // 
            // txtAskURL
            // 
            this.txtAskURL.Location = new System.Drawing.Point(132, 60);
            this.txtAskURL.Name = "txtAskURL";
            this.txtAskURL.ReadOnly = true;
            this.txtAskURL.Size = new System.Drawing.Size(400, 21);
            this.txtAskURL.TabIndex = 8;
            this.txtAskURL.Text = "https://fpdk.bjsat.gov.cn/SbsqWW/gxcx.do";
            // 
            // txtLoginURL
            // 
            this.txtLoginURL.Location = new System.Drawing.Point(132, 33);
            this.txtLoginURL.Name = "txtLoginURL";
            this.txtLoginURL.ReadOnly = true;
            this.txtLoginURL.Size = new System.Drawing.Size(400, 21);
            this.txtLoginURL.TabIndex = 0;
            this.txtLoginURL.Text = "https://fpdk.bjsat.gov.cn/SbsqWW/login.do";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "税局数据请求地址：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(257, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(305, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "自动同步时，截止日期为当前日期，同步一个月的数据。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "同步截止日期：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "同步开始日期：";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(130, 249);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(123, 21);
            this.dtpEndTime.TabIndex = 4;
            // 
            // dtpBeginTime
            // 
            this.dtpBeginTime.Location = new System.Drawing.Point(130, 222);
            this.dtpBeginTime.Name = "dtpBeginTime";
            this.dtpBeginTime.Size = new System.Drawing.Size(123, 21);
            this.dtpBeginTime.TabIndex = 3;
            // 
            // btnSync
            // 
            this.btnSync.Enabled = false;
            this.btnSync.Location = new System.Drawing.Point(130, 276);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(123, 23);
            this.btnSync.TabIndex = 2;
            this.btnSync.Text = "开始同步(4)";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "税局后台地址：";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtRst);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(577, 381);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "检测硬件";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtRst
            // 
            this.txtRst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRst.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRst.Location = new System.Drawing.Point(3, 3);
            this.txtRst.Name = "txtRst";
            this.txtRst.Size = new System.Drawing.Size(571, 375);
            this.txtRst.TabIndex = 1;
            this.txtRst.Text = "";
            // 
            // btnMiNi
            // 
            this.btnMiNi.Location = new System.Drawing.Point(375, 22);
            this.btnMiNi.Name = "btnMiNi";
            this.btnMiNi.Size = new System.Drawing.Size(103, 23);
            this.btnMiNi.TabIndex = 3;
            this.btnMiNi.Text = "最小化到托盘(6)";
            this.btnMiNi.UseVisualStyleBackColor = true;
            this.btnMiNi.Click += new System.EventHandler(this.btnMiNi_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(484, 22);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "退出服务(7)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnChk
            // 
            this.btnChk.Location = new System.Drawing.Point(274, 22);
            this.btnChk.Name = "btnChk";
            this.btnChk.Size = new System.Drawing.Size(95, 23);
            this.btnChk.TabIndex = 0;
            this.btnChk.Text = "检测金税盘(1)";
            this.btnChk.UseVisualStyleBackColor = true;
            this.btnChk.Click += new System.EventHandler(this.btnChk_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(585, 407);
            this.tabControl1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Controls.Add(this.btnMiNi);
            this.groupBox1.Controls.Add(this.btnChk);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 415);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 57);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.Location = new System.Drawing.Point(38, 27);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(82, 14);
            this.lblTime.TabIndex = 25;
            this.lblTime.Text = "当前时间：";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(259, 143);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(137, 12);
            this.label12.TabIndex = 30;
            this.label12.Text = "修改频率后请重启该程序";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 472);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assist";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon AisinoAssist;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblWar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUpdateURL;
        private System.Windows.Forms.TextBox txtAskURL;
        private System.Windows.Forms.TextBox txtLoginURL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpBeginTime;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox txtRst;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnChk;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnSaveSetting;
        private System.Windows.Forms.Button btnLogPathOpen;
        private System.Windows.Forms.TextBox txtDataPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDataMax;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemON;
        private System.Windows.Forms.ToolStripMenuItem MenuItemMiNi;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOUT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPSW;
        private System.Windows.Forms.Button btnMiNi;
        private System.Windows.Forms.RichTextBox txtAbout;
        private System.Windows.Forms.RadioButton rdbAtoAsync;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbHandsSync;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ComboBox cmbDays;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbFrequency;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}

