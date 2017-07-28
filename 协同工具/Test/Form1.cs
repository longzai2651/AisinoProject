using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace demo
{
    public partial class Form1 : Form
    {
        //Cryp_Ctl.CryptCtl cryptCtl = new Cryp_Ctl.CryptCtl();
        CryptCtlDll.CryptCtlDllClass cryptCtlDllClass = new CryptCtlDll.CryptCtlDllClass();
        //CryptCtlDll.RetInfo retinfo = new CryptCtlDll.RetInfo();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }


        private void button2_Click(object sender, EventArgs e)
        {
            /*
            string nsrsbh = "";


            #region 检测ukey的操作-王雷-2017年4月17日16:40:08
            try
            {
                //1. 检查是否安装ukey驱动
                cryptCtl.CheckKey();
            }
            catch (Exception)
            {
                //return "请安装USBKEY驱动";
                MessageBox.Show("请安装USBKEY驱动");
                return;
            }

            //2. 判断是否插入ukey
            int ErrCode = cryptCtl.ErrCode;
            if (ErrCode != 0)
            {
                MessageBox.Show(cryptCtl.ErrMsg);
            }



            //3.打开密码设备（打开USBKEY）。
            cryptCtl.OpenDevice();
            if (cryptCtl.ErrCode != 0)
            {
                MessageBox.Show(cryptCtl.ErrMsg);
            }

            cryptCtl.ClientHello(0);


            //4.取证书信息(默认使用16进制表示；纳税人识别号  71；)
            string signCertstr = "";
            //cryptCtl.GetCertInfo(signCertstr, 71);
            cryptCtl.ClientAuth("4155544853455256455248454c4c4f3203002400060000000000000000002400bbf6e5d19a9bbb89bab8a64fb5fdb87cf2656859bbf6e5d19a9bbb89bab8a64fb5fdb87c");



            if (cryptCtl.ErrCode != 0)
            {
                MessageBox.Show(cryptCtl.ErrMsg);
            }
            //5.返回结果判断
            if (cryptCtl.strResult == null || cryptCtl.strResult == "")
            {
                //获取信息失败
                //return "查询结果不存在" ;
                MessageBox.Show("查询结果不存在");
            }
            #endregion

            nsrsbh = cryptCtl.strResult;



            //MessageBox.Show("nsrsbh1" + nsrsbh1 + "nsrsbh:" + nsrsbh);
            //return cryptCtl.ErrMsg;

            */

        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                richTextBox2.Text = cryptCtlDllClass.CheckKey();
            }
            catch (Exception ex)
            {
                MessageBox.Show("请安装USBKEY驱动");
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //cryptCtlDllClass.CheckKey();
            //int aa = retinfo.code;
            //int ErrCode = retinfo.code;
            //if (ErrCode != 0)
            //{
            //    MessageBox.Show(retinfo.code.ToString());
            //    return;
            //}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //cryptCtlDllClass.CheckKey();
            //if (retinfo.code != 0)
            //{
            //    MessageBox.Show(retinfo.code.ToString());
            //    return;
            //}
        }
        
        private void button8_Click(object sender, EventArgs e)
        {
            //richTextBox2.Text = cryptCtlDllClass.Test();


            richTextBox2.Text = cryptCtlDllClass.ClientHello();

            /*
            ErrCode = cryptCtl.ErrCode;
            #region//CheckKey
            try
            {
                //cryptCtlDllClass.CheckKey(cryptCtl);
                cryptCtlDllClass.CheckKey();
                ErrCode = cryptCtl.ErrCode;
                if (ErrCode != 0)
                {
                    MessageBox.Show(cryptCtl.ErrMsg);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("请安装USBKEY驱动");
                return;
            }
            #endregion

            #region//OpenDevice
            //cryptCtlDllClass.OpenDevice(cryptCtl, "");
            //cryptCtlDllClass.OpenDevice(cryptCtl);
            cryptCtlDllClass.OpenDevice();
            ErrCode = cryptCtl.ErrCode;
            if (ErrCode != 0)
            {
                MessageBox.Show(cryptCtl.ErrMsg);
                return;
            }
            #endregion

            #region//ClientHello
            //cryptCtlDllClass.ClientHello(cryptCtl);
            cryptCtlDllClass.ClientHello();
            richTextBox2.Text = cryptCtl.strResult;
            #endregion
            */


        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = cryptCtlDllClass.ClientAuth(this.richTextBox1.Text.ToString(),"88888888");

            /*
            #region//ClientAuth
            cryptCtl.strResult = null;
            //cryptCtlDllClass.ClientHello(cryptCtl);
            //cryptCtlDllClass.ClientAuth(cryptCtl, this.richTextBox1.Text.ToString());
            cryptCtlDllClass.ClientAuth(this.richTextBox1.Text.ToString());
            ErrCode = cryptCtl.ErrCode;
            #endregion

            if (ErrCode != 0)
            {
                MessageBox.Show(cryptCtl.ErrMsg);
                return;
            }

            if (cryptCtl.strResult == null || cryptCtl.strResult == "")
            {
                //获取信息失败
                //return "查询结果不存在" ;
                MessageBox.Show("查询结果不存在");
            }
            else 
            {
                richTextBox2.Text = cryptCtl.strResult;
            }
            */
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = cryptCtlDllClass.GetCertInfo();

            /*
            //cryptCtlDllClass.OpenDevice(cryptCtl,"");
            //cryptCtlDllClass.OpenDevice(cryptCtl);
            cryptCtlDllClass.OpenDevice();
            //cryptCtlDllClass.GetCertInfo(cryptCtl,"",71);
            //cryptCtlDllClass.GetCertInfo(cryptCtl);
            cryptCtlDllClass.GetCertInfo();
            ErrCode = cryptCtl.ErrCode;
            if (ErrCode != 0)
            {
                MessageBox.Show(cryptCtl.ErrMsg);
                return;
            }
            if (cryptCtl.strResult == null || cryptCtl.strResult == "")
            {
                //获取信息失败
                //return "查询结果不存在" ;
                MessageBox.Show("查询结果不存在");
            }
            else
            {
                richTextBox2.Text = cryptCtl.strResult;
            }
            */

        }









    }
}
