using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using System.Timers;

namespace Assist
{
    public partial class Form1 : Form
    {
        //Cryp_Ctl.CryptCtl cryptCtl = new Cryp_Ctl.CryptCtl();
        CryptCtlDllClass cryptCtlDllClass = null;
        Common common = null;
        //创建NotifyIcon对象 
        NotifyIcon notifyicon = new NotifyIcon();
        //创建托盘图标对象 
        //Icon ico = new Icon("AisinoAssist.ico");
        Icon ico = Properties.Resources.AisinoAssist;
        //创建托盘菜单对象 
        ContextMenu notifyContextMenu = new ContextMenu();
        /// <summary>
        /// 更新本地系统地址
        /// </summary>
        private string UpdateUrl = "";

        /// <summary>
        /// 检测线程
        /// </summary>
        Thread threadRepeatFunRepeatUkeyChk = null;
        /// <summary>
        /// 同步线程
        /// </summary>
        Thread threadRepeatFunRepeatSyncFun = null;


        /// <summary>
        /// 允许自动同步（手控，或同步过程中与盘交互的三步任一异常则停止自动同步）
        /// </summary>
        bool bAlSync = true;

        bool tempBl = false;


        /// <summary>
        /// 间隔时间1000*60*60*24(24小时)
        /// </summary>
        private double WaitMilliSeconds = 1000 * 10;//

        private string strClientHello = "3077020103305E310B300906035504061302636E31153013060355040B1E0C56FD5BB67A0E52A1603B5C40311D301B06035504031E147A0E52A175355B508BC14E667BA174064E2D5FC331193017060355040D1E10006300610031003000300030003000320207020100001449ADA209310702010102020402";



        public Form1()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.AisinoAssist;
            threadRepeatFunRepeatUkeyChk = new Thread(RepeatUkeyChk);
            threadRepeatFunRepeatSyncFun = new Thread(RepeatSyncFun);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //try
            //{

                LoadData();
                ClearData();
                UkeyChk();

                threadRepeatFunRepeatUkeyChk = new Thread(new ThreadStart(RepeatUkeyChk));
                threadRepeatFunRepeatUkeyChk.Start();

                //string str = "{\"key1\":\"01\",\"key2\":{\"sEcho\":1,\"iTotalRecords\":3,\"iTotalDisplayRecords\":3,\"aaData\":[[\"0=0=0=0\",\"1100162160\",\"00579134\",\"2017-07-21\",\"�й�ʯ���������޹�˾����ʯ�ͷֹ�˾\",\"13675.21\",\"2324.79\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110105801125305\"],[\"0=0=0=0\",\"1100171130\",\"07885330\",\"2017-07-19\",\"��������������Ϣ�������޹�˾\",\"127.35\",\"21.65\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"1100164130\",\"00634544\",\"2017-07-06\",\"������̩ͬʢ��ó��չ���޹�˾\",\"3666.67\",\"623.33\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110105681247569U\"]]},\"key3\":\"d3a203a0-4aa2-4da4-a3f0-446136e66ba4\",\"key4\":\"3\"}";
                //common=new Common();

                //JObject jb = new JObject();
                //jb = common.JsonToObject(str);
                //string aa = jb["key1"].ToString();
                /**/
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

        /// <summary>
        /// LoadData
        /// </summary>
        private void LoadData()
        {
            try
            {
                rdbAtoAsync.Checked = true;
                if (!Directory.Exists(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Log"))
                {
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Log");
                }
                if (!Directory.Exists(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Data"))
                {
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Data");
                }
                RegistryKey src = Registry.LocalMachine.OpenSubKey("SOFTWARE", true).OpenSubKey("AisinoAssist", true); //获取注册的路径
                common = new Common();
                string str = common.DecodeBase64(src.GetValue("PSW").ToString());
                txtPSW.Text = str;
                txtLoginURL.Text = src.GetValue("LoginURL").ToString();
                txtAskURL.Text = src.GetValue("AskURL").ToString();
                txtUpdateURL.Text = src.GetValue("UpdateURL").ToString();
                txtDataMax.Text = src.GetValue("DataMax").ToString();
                cmbDays.Text = src.GetValue("Days").ToString();
                cmbFrequency.Text = src.GetValue("Frequency").ToString();

                WaitMilliSeconds = Convert.ToDouble(cmbFrequency.Text.ToString());
                WaitMilliSeconds = 10 * 1000;//测试用
            }
            catch (Exception ex)
            {
                SaveSetting();
            }
            txtDataPath.Text = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Data";
            SetControl(false);
            rdbAtoAsync.Checked = true;
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        private void ClearData()
        {
            //throw new NotImplementedException();
            
            double dataMax = Convert.ToDouble(txtDataMax.Text.ToString());
            if (dataMax == 0)
                return;
            //Data数据
            string folderFullName = txtDataPath.Text.ToString();
            DirectoryInfo TheFolder = new DirectoryInfo(folderFullName);
            if (TheFolder.GetFiles().Count() > 0)
            {
                FileInfo File = TheFolder.GetFiles().Distinct().OrderBy(n => n.LastWriteTime).Last();

                common = new Common();
                double data = common.GetDirectoryLength(folderFullName) / 1024;
                if (data > dataMax)
                //if (1 == 1)
                {
                    File.Delete();
                    ClearData();
                }
            }

            //Log日志
            string folderFullNameLog = txtDataPath.Text.ToString().Substring(0, txtDataPath.Text.ToString().Length-4)+"Log";
            DirectoryInfo LogFolder = new DirectoryInfo(folderFullNameLog);
            if (LogFolder.GetFiles().Count() > 0)
            {
                FileInfo LogFile = LogFolder.GetFiles().Distinct().OrderBy(n => n.LastWriteTime).Last();
                double logdata = common.GetDirectoryLength(folderFullNameLog) / 1024;
                if (logdata > dataMax)
                //if (1 == 1)
                {
                    LogFile.Delete();
                    ClearData();
                }
            }

        }

        /// <summary>
        /// 重复执行自动同步功能
        /// </summary>
        private void RepeatSyncFun()
        {
            SyncFun();
            threadRepeatFunRepeatSyncFun.Join((int)WaitMilliSeconds);//阻止设定时间 
        }

        private void btnChk_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            UkeyChk();
        }
        /// <summary>
        /// 检测金税盘
        /// </summary>
        /// <param name="obj"></param>
        private void UkeyChk()
        {
            //throw new NotImplementedException();
            try
            {
                cryptCtlDllClass = new CryptCtlDllClass();
                string rst = cryptCtlDllClass.CheckKey();
                if (rst.StartsWith("0"))//code+rst,code=0为检测到
                {
                    SetControl(true);
                    #region//检测成功，开启同步
                    if (bAlSync)
                    {
                        btnSync.Enabled = false;
                        threadRepeatFunRepeatSyncFun = new Thread(new ThreadStart(RepeatSyncFun));
                        threadRepeatFunRepeatSyncFun.Start();
                    }
                    #endregion
                }
                else
                {
                    SetControl(false);
                    //tabControl1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                SetControl(false);
                //tabControl1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 重复执行检测功能
        /// </summary>
        private void RepeatUkeyChk()
        {
            try
            {
                while (true)
                {
                    UkeyChk();
                    threadRepeatFunRepeatUkeyChk.Join((int)WaitMilliSeconds);//阻止设定时间 
                }
            }
            catch (Exception ex)
            {
                SaveSetting();
            }
        }

        private void UkeyChk(object source, System.Timers.ElapsedEventArgs e)
        {
            UkeyChk();
        }

        delegate void SetControlCallBack(bool bl);
        private void SetControl(bool bl)
        {
            if (this.InvokeRequired)
            {
                SetControlCallBack sccb = new SetControlCallBack(SetControl);
                this.Invoke(sccb, new object[] { bl });
            }
            else
            {
                if (bl)
                {
                    //tabControl1.SelectedIndex = 1;
                    if (rdbHandsSync.Checked && !bAlSync)
                    {
                        btnSync.Enabled = true;
                    }
                    rdbAtoAsync.ForeColor = Color.Black;
                    rdbAtoAsync.Enabled = true;
                    rdbHandsSync.ForeColor = Color.Black;
                    rdbHandsSync.Enabled = true;
                    txtRst.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "***本次检测时间***金税盘连接正常\n" + DateTime.Now.AddMilliseconds(WaitMilliSeconds).ToString("yyyy-MM-dd hh:mm:ss") + "***下次检测时间";
                    txtRst.ForeColor = Color.Green;
                    lblWar.Visible = false;

                }
                else
                {
                    //tabControl1.SelectedIndex = 0;
                    btnSync.Enabled = false;
                    rdbAtoAsync.ForeColor = Color.Gray;
                    rdbAtoAsync.Enabled = false;
                    rdbHandsSync.ForeColor = Color.Gray;
                    rdbHandsSync.Enabled = false;
                    txtRst.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "***本次检测时间***金税盘读取异常，下次将自动再次检测\n" + DateTime.Now.AddMilliseconds(WaitMilliSeconds).ToString("yyyy-MM-dd hh:mm:ss") + "***下次检测时间";
                    txtRst.ForeColor = Color.Red;
                    lblWar.Visible = true;

                }
                common = new Common();
                double data = common.GetDirectoryLength(txtDataPath.Text.ToString());
                lblData.Text = "kb  设置0不自动清除缓存  已缓存" + (data / 1024).ToString(".00") + "kb";
            }
        }



        private void btnSync_Click(object sender, EventArgs e)
        {
            SyncFun();
        }


        /// <summary>
        /// 同步timer
        /// </summary>
        private void SyncFun(object source, System.Timers.ElapsedEventArgs e)
        {
            SyncFun();
        }
        /// <summary>
        /// 同步数据方法
        /// </summary>
        private void SyncFun()
        {
            common = new Common();
            string PostResultFir = "";
            string PostResultSec = "";
            string PostResultThir = "";

            try
            {
                #region//与金税盘通信并发送数据
                PostResultFir = FirReq();//第一次请求
                if (PostResultFir.Length == 0)
                    return;
                PostResultFir = PostResultFir.Replace("null(", "").Replace(")", "");
                JObject job = null;
                job = new JObject();
                job = common.JsonToObject(PostResultFir);
                if (job.Property("key1") == null || job.Property("key2") == null || job.Property("key3") == null)
                {
                    common.WriteLogFile(0, "SyncFun", "第一次请求异常" + PostResultFir);
                    SetControl(false);
                    return;
                }

                if (job["key1"].ToString() == "01")
                {
                    #region//第二次与金税盘通信
                    //common.WriteLogFile(0, "SyncFun", "第一次请求异常" + PostResultFir);//测试用
                    PostResultSec = SecReq(job);//第二次请求
                    if (PostResultSec.Length == 0)
                        return;
                    PostResultSec = PostResultSec.Replace("null(", "").Replace(")", "");
                    job = new JObject();
                    
                    job = common.JsonToObject(PostResultSec);

                    if (job.Property("key1") == null || job.Property("key2") == null || job.Property("key3") == null || job.Property("key4") == null)
                    {
                        common.WriteLogFile(0, "SyncFun", "第一次请求" + PostResultFir + "\n" + "；第二次请求异常" + PostResultSec);
                        SetControl(false);
                        return;
                    }
                    
                    if (job["key1"].ToString() == "03")
                    {
                        #region//第三次与金税盘通信
                        PostResultThir = ThirReq(job);//第三次请求
                        if (PostResultThir.Length == 0)
                            return;
                        PostResultThir = PostResultThir.Replace("null(", "").Replace(")", "");
                        if (!PostResultThir.Contains("\"key2\":{\"sEcho\"") || !PostResultThir.Contains("\"aaData\":[["))//所得数据不合法
                            return;
                        job = new JObject();
                        job = common.JsonToObject(PostResultThir);

                        if (job.Property("key1") == null || job.Property("key2") == null || job.Property("key3") == null || job.Property("key4") == null)
                        {
                            common.WriteLogFile(0, "SyncFun", "第一次请求" + PostResultFir + "\n" + "；第二次请求" + PostResultSec + "；第三次请求异常" + PostResultThir);
                            SetControl(false);
                            return;
                        }
                        else//if (1 == 1)//
                        {
                            #region//与本地系统通信并同步数据至本地系统
                            string aaData = "";
                            string rst = "";
                            string datacount="";
                            datacount = job["key2"]["iTotalRecords"].ToString();
                            aaData = job["key2"]["aaData"].ToString();
                            rst = Update(job);

                            //common.WriteLogFile(2, "", "已从后台下载数据"
                            //    + aaData.ToString().Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("\r", "")
                            //    + "合计条数："
                            //    + datacount);
                            //return;

                            if (1 == 1)//rst结果状态真
                            {
                                common.WriteLogFile(2, "", "已从后台下载数据：" + "\r\n"
                                + aaData.ToString().Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("\r", "") + "\r\n"
                                + "合计条数："
                                + datacount + "\r\n"
                                + "同步状态："
                                + rst);
                            }
                            #endregion
                        }


                        #endregion
                    }
                    #endregion

                }
                #endregion
                SetControl(true);
            }
            catch (Exception ex)
            {
                common.WriteLogFile(0, "SyncFun", ex.ToString());
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            FrmSwitch();
        }

        private void FrmSwitch()
        {
            //判断是否已经最小化于托盘 
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示 
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点 
                this.Activate();
                //任务栏区显示图标 
                this.ShowInTaskbar = true;
                //托盘区图标隐藏 
                notifyicon.Visible = false;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Minimize();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            // 将窗体变为最小化
            this.WindowState = FormWindowState.Minimized;
            Minimize();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Queit();
        }

        private void Queit()
        {
            if (threadRepeatFunRepeatSyncFun.IsAlive)
            {
                threadRepeatFunRepeatSyncFun.Abort();
                threadRepeatFunRepeatUkeyChk.Abort();
            }
            //this.Close();
            //Application.Exit();
            System.Environment.Exit(0); 
        }

        private void btnLogPathOpen_Click(object sender, EventArgs e)
        {
            string path = txtDataPath.Text.ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            tempBl = bAlSync;
            bAlSync = false;
            txtPSW.ReadOnly = false;
            txtLoginURL.ReadOnly = false;
            txtAskURL.ReadOnly = false;
            txtUpdateURL.ReadOnly = false;
            txtDataMax.ReadOnly = false;
            cmbDays.Enabled = true;
            cmbFrequency.Enabled = true;
        }


        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(txtDataMax.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("缓存上限值不合法！");
                return;
            }
            bAlSync = tempBl;
            SaveSetting();
            txtPSW.ReadOnly = true;
            txtLoginURL.ReadOnly = true;
            txtAskURL.ReadOnly = true;
            txtUpdateURL.ReadOnly = true;
            txtDataMax.ReadOnly = true;
            cmbDays.Enabled = false;
            cmbFrequency.Enabled = false;
            MessageBox.Show("保存成功！");
        }
        CancellationTokenSource cts = new CancellationTokenSource();

        /// <summary>
        ///第一次请求参数及结果格式
        /// clientHello=3077020103305E310B300906035504061302636E31153013060355040B1E0C56FD5BB67A0E52A1603B5C40311D301B06035504031E147A0E52A175355B508BC14E667BA174064E2D5FC331193017060355040D1E10006300610031003000300030003000320207020100001449ADA209310702010102020402
        /// type=CLIENT-HELLO
        /// ymbb=3.0.09
        /// 结果string:null({"key1":"01","key2":"4155544853455256455248454c4c4f3203002400060000000000000000002400b4859b3d4d4c10cfe91f8575a20000f82e137059b4859b3d4d4c10cfe91f8575a20000f8","key3":"4155544853455256455248454c4c4f3203002400060000000000000000002400b4859b3d4d4c10cfe91f8575a20000f82e137059b4859b3d4d4c10cfe91f8575a20000f8"})
        /// </summary>
        /// <returns>返回string</returns>
        private string FirReq()
        {
            string url;
            string ClientHello = "";
            ClientHello = strClientHello;
            ClientHello = "3077020103305E310B300906035504061302636E31153013060355040B1E0C56FD5BB67A0E52A1603B5C40311D301B06035504031E147A0E52A175355B508BC14E667BA174064E2D5FC331193017060355040D1E10006300610031003000300030003000320207020100001449ADA209310702010102020402";
            cryptCtlDllClass = new CryptCtlDllClass();
            //ClientHello = cryptCtlDllClass.ClientHello();
            if (ClientHello == null || ClientHello == "")
            {
                bAlSync = false;
                //SetControl(false);
                if (threadRepeatFunRepeatSyncFun.IsAlive)
                    threadRepeatFunRepeatSyncFun.Join();
                return "";
            }


            StringBuilder sb = null;
            common = new Common();
            url = "https://fpdk.bjsat.gov.cn/SbsqWW/login.do";
            url = txtLoginURL.Text.ToString();
            sb = new StringBuilder();
            sb.AppendFormat("clientHello={0}&type={1}&ymbb={2}"
                , ClientHello
                , "CLIENT-HELLO"
                , "3.0.09");
            return common.RequestPost(url, sb.ToString());//第一次请求
        }

        /// <summary>
        /// 第二次请求参数及结果
        /// type=CLIENT-HELLO
        /// ymbb=3.0.09
        /// clientAuthCode=clientAuthCode//这个值是第一次请求key2与盘交互得到的字符串
        /// cert=cert//在盘里获取的税号
        /// serverRandom=serverRandom//这个值是第一次请求key3的值
        /// password=password//目前8个8
        /// 结果string:null({"key1":"03","key2":"ee3c1ffc-7451-4a22-99a8-3bf68ad73d7b","key3":"%E5%8C%97%E4%BA%AC%E5%88%9B%E6%96%B0%E6%B0%B8%E5%85%B4%E8%A3%85%E9%A5%B0%E5%B7%A5%E7%A8%8B%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8","key4":"2017-07-20"})
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        private string SecReq(JObject job)
        {
            try
            {
                string url;
                string ClientAuth = "";
                string password = "";
                password = txtPSW.Text.ToString();
                cryptCtlDllClass = new CryptCtlDllClass();
                ClientAuth = cryptCtlDllClass.ClientAuth(job["key2"].ToString(), password);
                string GetCertInfo = "";
                GetCertInfo = cryptCtlDllClass.GetCertInfo();
                if (ClientAuth == null || GetCertInfo == null || ClientAuth == "" || GetCertInfo == "")
                {
                    bAlSync = false;
                    //SetControl(false);

                    if (threadRepeatFunRepeatSyncFun.IsAlive)
                        threadRepeatFunRepeatSyncFun.Join();
                    return "";
                }
                StringBuilder sb = null;
                common = new Common();
                url = "https://fpdk.bjsat.gov.cn/SbsqWW/login.do";
                url = txtLoginURL.Text.ToString();
                sb = new StringBuilder();
                sb.AppendFormat("type={0}&clientAuthCode={2}&serverRandom={4}&password={5}&cert={3}&ymbb={1}"
                   , "CLIENT-AUTH"
                   , "3.0.09"
                   , ClientAuth
                   , GetCertInfo
                   , job["key3"].ToString()
                   , txtPSW.Text.ToString()//"88888888"
                   );
                return common.RequestPost(url, sb.ToString());//第二次请求
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// 第三次请求参数及结果
        /// fpdm=""
        /// fphm=""
        /// rq_q=rq_q//rq_q 可以获取当前日期减去一个月   日期格式：2017-05-01
        /// rq_z=rq_z//rq_z 当前日期   日期格式：2017-05-01
        /// xfsbh=""
        /// rzfs=""
        /// rzzt=0
        /// gxzt=0
        /// fpzt=0
        /// fplx=-1 
        /// cert=cert//在盘里获取的税号
        /// token=token
        /// ymbb=3.0.09
        /// aoData=aoData
        /// 结果string:null({"key1":"01","key2":{"sEcho":1,"iTotalRecords":13,"iTotalDisplayRecords":13,"aaData":[["0=0=0=0","1100171130","07885330","2017-07-19","北京京东世纪信息技术有限公司","127.35","21.65","0","0",null,"0",null,"0",null,"110192562134916"],["0=0=0=0","4400163130","22842757","2017-07-18","广东东方雨虹防水工程有限公司","35688.89","6067.11","0","0",null,"0",null,"0",null,"91440106689326982B"],["0=0=0=0","1100171130","07978895","2017-07-11","北京京东世纪信息技术有限公司","298.29","50.71","0","0",null,"0",null,"0",null,"110192562134916"],["0=0=0=0","1200171130","00464175","2017-07-11","天津京东海荣贸易有限公司","50.43","8.57","0","0",null,"0",null,"0",null,"91120110MA07C4747P"],["0=0=0=0","1100171130","01517676","2017-07-10","北京安顺泰建材技术有限公司","1820.51","309.49","0","0",null,"0",null,"0",null,"91110115726330754C"],["0=0=0=0","3100171130","05453754","2017-07-07","上海依航计算机科技有限公司","84.62","14.38","0","0",null,"0",null,"0",null,"91310120794539373H"],["0=0=0=0","1200171130","00452121","2017-07-06","天津京东海荣贸易有限公司","76.07","12.93","0","0",null,"0",null,"0",null,"91120110MA07C4747P"],["0=0=0=0","1100171130","06767484","2017-07-06","北京京东世纪信息技术有限公司","3527.43","599.67","0","0",null,"0",null,"0",null,"110192562134916"],["0=0=0=0","1100164130","00634544","2017-07-06","北京岳泰同盛经贸发展有限公司","3666.67","623.33","0","0",null,"0",null,"0",null,"91110105681247569U"],["0=0=0=0","1100171130","06230160","2017-07-02","北京兴奥晟通汽车销售服务有限公司","2013.68","342.32","0","0",null,"0",null,"0",null,"9111011506282657X1"],["0=0=0=0","1100162130","10779288","2017-07-02","北京三合兴华汽车销售服务有限公司","366.67","62.33","0","0",null,"0",null,"0",null,"91110115067311946K"],["0=0=0=0","1300163130","14045915","2017-06-29","唐山京玉水泥有限公司","42777.78","7272.22","0","0",null,"0",null,"0",null,"130229746867604"],["0=0=0=0","1100171130","06750964","2017-06-23","北京京东世纪信息技术有限公司","869.23","147.77","0","0",null,"0",null,"0",null,"110192562134916"]]},"key3":"3d146642-21f9-4561-92fe-2916835415c7","key4":"13"})
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        private string ThirReq(JObject job)
        {
            string url;
            string Datenow = "";
            string DateLast = "";
            string GetCertInfo = "";
            cryptCtlDllClass = new CryptCtlDllClass();
            GetCertInfo = cryptCtlDllClass.GetCertInfo();
            if (GetCertInfo == null || GetCertInfo == "")
            {
                bAlSync = false;
                //SetControl(false);
                if (threadRepeatFunRepeatSyncFun.IsAlive)
                    threadRepeatFunRepeatSyncFun.Join();
                return "";
            }
            if (!bAlSync)
            {
                Datenow = dtpEndTime.Value.ToString("yyyy-MM-dd");
                DateLast = dtpBeginTime.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                Datenow = DateTime.Today.ToString("yyyy-MM-dd");
                int days = Convert.ToInt32(cmbDays.Text.ToString());
                DateLast = Convert.ToDateTime(Datenow).AddDays(-days).ToString("yyyy-MM-dd");
            }


            StringBuilder sb = null;
            common = new Common();
            url = "https://fpdk.bjsat.gov.cn/SbsqWW/gxcx.do";
            url = txtAskURL.Text.ToString();
            sb = new StringBuilder();
            sb.AppendFormat(@"fpdm={0}&fphm={1}&rq_q={2}&rq_z={3}&xfsbh={4}&rzfs={5}&rzzt={6}&gxzt={7}&fpzt={8}&fplx={9}&cert={10}&token={11}&ymbb={12}&aoData={13}"
                    , "", ""
                    , DateLast, Datenow
                    , "", ""
                    , 0, 0, 0, -1
                    , GetCertInfo
                    , job["key2"].ToString()
                    , "3.0.09"
                    , "[{\"name\":\"sEcho\",\"value\":0},{\"name\":\"iColumns\",\"value\":14},{\"name\":\"sColumns\",\"value\":\",,,,,,,,,,,,,\"},{\"name\":\"iDisplayStart\",\"value\":0},{\"name\":\"iDisplayLength\",\"value\":8000},{\"name\":\"mDataProp_0\",\"value\":0},{\"name\":\"mDataProp_1\",\"value\":1},{\"name\":\"mDataProp_2\",\"value\":2},{\"name\":\"mDataProp_3\",\"value\":3},{\"name\":\"mDataProp_4\",\"value\":4},{\"name\":\"mDataProp_5\",\"value\":5},{\"name\":\"mDataProp_6\",\"value\":6},{\"name\":\"mDataProp_7\",\"value\":7},{\"name\":\"mDataProp_8\",\"value\":8},{\"name\":\"mDataProp_9\",\"value\":9},{\"name\":\"mDataProp_10\",\"value\":10},{\"name\":\"mDataProp_11\",\"value\":11},{\"name\":\"mDataProp_12\",\"value\":12}]"
                    );
            return common.RequestPost(url, sb.ToString());//第三次请求

        }

        /// <summary>
        /// {\"key1\":\"01\",\"key2\":{\"sEcho\":1,\"iTotalRecords\":13,\"iTotalDisplayRecords\":13,\"aaData\":[[\"0=0=0=0\",\"1100171130\",\"07885330\",\"2017-07-19\",\"北京京东世纪信息技术有限公司\",\"127.35\",\"21.65\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"4400163130\",\"22842757\",\"2017-07-18\",\"广东东方雨虹防水工程有限公司\",\"35688.89\",\"6067.11\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91440106689326982B\"],[\"0=0=0=0\",\"1100171130\",\"07978895\",\"2017-07-11\",\"北京京东世纪信息技术有限公司\",\"298.29\",\"50.71\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"1200171130\",\"00464175\",\"2017-07-11\",\"天津京东海荣贸易有限公司\",\"50.43\",\"8.57\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91120110MA07C4747P\"],[\"0=0=0=0\",\"1100171130\",\"01517676\",\"2017-07-10\",\"北京安顺泰建材技术有限公司\",\"1820.51\",\"309.49\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110115726330754C\"],[\"0=0=0=0\",\"3100171130\",\"05453754\",\"2017-07-07\",\"上海依航计算机科技有限公司\",\"84.62\",\"14.38\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91310120794539373H\"],[\"0=0=0=0\",\"1200171130\",\"00452121\",\"2017-07-06\",\"天津京东海荣贸易有限公司\",\"76.07\",\"12.93\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91120110MA07C4747P\"],[\"0=0=0=0\",\"1100171130\",\"06767484\",\"2017-07-06\",\"北京京东世纪信息技术有限公司\",\"3527.43\",\"599.67\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"1100164130\",\"00634544\",\"2017-07-06\",\"北京岳泰同盛经贸发展有限公司\",\"3666.67\",\"623.33\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110105681247569U\"],[\"0=0=0=0\",\"1100171130\",\"06230160\",\"2017-07-02\",\"北京兴奥晟通汽车销售服务有限公司\",\"2013.68\",\"342.32\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"9111011506282657X1\"],[\"0=0=0=0\",\"1100162130\",\"10779288\",\"2017-07-02\",\"北京三合兴华汽车销售服务有限公司\",\"366.67\",\"62.33\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110115067311946K\"],[\"0=0=0=0\",\"1300163130\",\"14045915\",\"2017-06-29\",\"唐山京玉水泥有限公司\",\"42777.78\",\"7272.22\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"130229746867604\"],[\"0=0=0=0\",\"1100171130\",\"06750964\",\"2017-06-23\",\"北京京东世纪信息技术有限公司\",\"869.23\",\"147.77\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"]]},\"key3\":\"86d3d079-66c7-4f77-925f-4891938a5b96\",\"key4\":\"13\"}
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        private string Update(JObject job)
        {
            StringBuilder sb = null;
            common = new Common();
            UpdateUrl = "http://192.168.0.13:8080/AisinoYz/jxfp/xjxc/zdtb.action";
            UpdateUrl = txtUpdateURL.Text.ToString();
            sb = new StringBuilder();
            sb.AppendFormat(@"fpdm={0}"
                , job["key2"]["aaData"].ToString()
                //, str
                    );
            return job["key2"]["aaData"].ToString();
            string msg = common.RequestPost(UpdateUrl, sb.ToString());//第三次请求

            return msg;



        }

        /// <summary>
        /// 最小化到系统托盘
        /// </summary>
        private void Minimize()
        {
            //判断是否选择的是最小化按钮 
            if (WindowState == FormWindowState.Minimized)
            {
                //托盘显示图标等于托盘图标对象 
                //注意notifyIcon1是控件的名字而不是对象的名字 
                AisinoAssist.Icon = ico;
                //隐藏任务栏区图标 
                this.ShowInTaskbar = false;
                //图标显示在托盘区 
                notifyicon.Visible = true;
            }
        }

        /// <summary>
        /// 保存设置到注册表
        /// </summary>
        private void SaveSetting()
        {
            RegistryKey src = Registry.LocalMachine.OpenSubKey("SOFTWARE", true); //获取注册的路径
            src.CreateSubKey("AisinoAssist");
            RegistryKey red = src.CreateSubKey("AisinoAssist");//写入注册表项
            common = new Common();
            string str = common.EncodeBase64(txtPSW.Text.ToString());
            red.SetValue("PSW", str);
            red.SetValue("LoginURL", txtLoginURL.Text.ToString());//在这个文件夹内写入值 
            red.SetValue("AskURL", txtAskURL.Text.ToString());
            red.SetValue("UpdateURL", txtUpdateURL.Text.ToString());
            red.SetValue("DataMax", txtDataMax.Text.ToString());
            red.SetValue("Days", cmbDays.Text.ToString());
            red.SetValue("Frequency", cmbFrequency.Text.ToString());

        }

        private void MenuItemON_Click(object sender, EventArgs e)
        {
            FrmSwitch();
        }

        private void MenuItemMiNi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Minimize();
        }

        private void MenuItemOUT_Click(object sender, EventArgs e)
        {
            Queit();
        }

        private void btnMiNi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Minimize();
        }


        private void rdbAtoAsync_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAtoAsync.Checked)
            {
                btnSync.Enabled = false;
            }
            else
            {
                bAlSync = false;
                btnSync.Enabled = true;
            }

        }

        private void rdbAtoAsync_Click(object sender, EventArgs e)
        {
            if (rdbAtoAsync.Checked)
            {
                bAlSync = true;
                lblInfo.Visible = false;
            }
            else
            {
                bAlSync = false;
                lblInfo.Text = "自动同步已关闭";
                lblInfo.Visible = true;
                if (threadRepeatFunRepeatSyncFun.IsAlive)
                    threadRepeatFunRepeatSyncFun.Join();
            }



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }






    }
}
