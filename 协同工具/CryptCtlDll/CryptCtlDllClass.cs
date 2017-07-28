using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//因为Guid是属性，应该用的是System.Runtime.InteropServices.GuidAttribute而不是System.Guid   
using System.Runtime.InteropServices;
using Cryp_Ctl;

namespace CryptCtlDll
{

    /// <summary>
    /// 
    /// </summary>
    [Guid("304FED34-F140-42ED-ADD1-9E4F780F970C")]     //GUID值
    public interface ICryptCtl    //定义接口名
    {
        //[DispId(1)]
        //string ClientHello(string str, string str2);    //返回的字符串值，与需要调用的类的方法名一致。
        [DispId(1)]
        string Test();    //返回的字符串值，与需要调用的类的方法名一致。
        //[DispId(2)]
        //string ClientHello();
        //[DispId(3)]
        //string ClientAuth(string str);
        //[DispId(4)]
        //string GetCertInfo();
    }

    //public class RetInfo
    //{

    //}

    /// <summary>
    /// CryptCtlDllClass
    /// </summary>
    [Guid("259569E4-60A6-47D5-A536-A495B5B98CA5"), ClassInterface(ClassInterfaceType.None)]     //GUID值
    public class CryptCtlDllClass : ICryptCtl
    {
        CryptCtl cryptCtl = null;
        Common common = null;
        //RetInfo retinfo = new RetInfo();

        public int code { get; set; }
        public string msg { get; set; }
        public string rst { get; set; }
        public int lenrst { get; set; }

        public string Test()
        {
            int code = 0;
            //code = cryptCtl.ErrCode;
            return "Success:" + code;
            //return "Success:";
        }


        /// <summary>
        /// CheckKey检测盘
        /// </summary>
        /// <param name="cryptCtl"></param>
        public string CheckKey()
        {
            cryptCtl = new CryptCtl();
            common = new Common();
            //try
            //{
            cryptCtl.CheckKey();
            return cryptCtl.ErrCode.ToString() + cryptCtl.strResult;
            //}
            //catch (Exception ex)
            //{
            //    common.WriteLogFile(0, "CheckKey",ex.ToString());
            //    return "-1";
            //}
        }

        /// <summary>
        /// OpenDevice打开盘
        /// </summary>
        /// <param name="cryptCtl"></param>
        /// <param name="certPass"></param>
        public string OpenDevice(string certPass)
        {
            //throw new NotImplementedException();
            cryptCtl = new CryptCtl();
            common = new Common();
            //try
            //{
            cryptCtl.OpenDevice(certPass);
            return cryptCtl.ErrCode.ToString() + cryptCtl.strResult;
            //}
            //catch (Exception ex)
            //{
            //    common.WriteLogFile(0, "OpenDevice", ex.ToString());
            //    return "-1";
            //}
        }

        /// <summary>
        /// OpenDevice(免传参数)
        /// </summary>
        /// <param name="cryptCtl"></param>
        public string OpenDevice()
        {
            //throw new NotImplementedException();
            cryptCtl = new CryptCtl();
            common = new Common();
            //try
            //{
            cryptCtl.OpenDevice("");
            return cryptCtl.ErrCode.ToString() + cryptCtl.strResult;
            //}
            //catch (Exception ex)
            //{
            //    common.WriteLogFile(0, "OpenDevice", ex.ToString());
            //    return "-1";
            //}
        }


        /// <summary>
        /// ClientHello
        /// </summary>
        /// <param name="cryptCtl"></param>
        public string ClientHello()
        {
            //throw new NotImplementedException();
            cryptCtl = new CryptCtl();
            common = new Common();
            //try
            //{
            cryptCtl.CheckKey();
            cryptCtl.OpenDevice("");
            cryptCtl.ClientHello(0);
            return cryptCtl.strResult;
            //}
            //catch (Exception ex)
            //{
            //    common.WriteLogFile(0, "OpenDevice", ex.ToString());
            //    return "-1";
            //}

        }

        /// <summary>
        /// ClientAuth
        /// </summary>
        /// <param name="cryptCtl"></param>
        /// <param name="strServerHello"></param>
        public string ClientAuth(string strServerHello, string password)
        {
            //throw new NotImplementedException();
            cryptCtl = new CryptCtl();
            common = new Common();
            //try
            //{
            cryptCtl.CheckKey();
            cryptCtl.OpenDevice("");
            cryptCtl.ClientHello(0);
            //cryptCtl.OpenDeviceEx("88888888");
            cryptCtl.OpenDeviceEx(password);
            cryptCtl.ClientAuth(strServerHello);
            return cryptCtl.strResult;
            //}
            //catch (Exception ex)
            //{
            //    common.WriteLogFile(0, "OpenDevice", ex.ToString());
            //    return "-1";
            //}

        }


        /// <summary>
        /// GetCertInfo
        /// </summary>
        /// <param name="cryptCtl"></param>
        /// <param name="strCert"></param>
        /// <param name="certInfoNo"></param>
        public void GetCertInfo(string strCert, int certInfoNo)
        {
            //throw new NotImplementedException();
            cryptCtl = new CryptCtl();
            cryptCtl.GetCertInfo(strCert, certInfoNo);
        }

        /// <summary>
        /// GetCertInfo(免传参数)
        /// </summary>
        /// <param name="cryptCtl"></param>
        public string GetCertInfo()
        {
            //throw new NotImplementedException();
            cryptCtl = new CryptCtl();
            common = new Common();
            //try
            //{

            cryptCtl.CheckKey();
            cryptCtl.OpenDevice("");
            cryptCtl.GetCertInfo("", 71);
            return cryptCtl.strResult;
            //}
            //catch (Exception ex)
            //{
            //    common.WriteLogFile(0, "OpenDevice", ex.ToString());
            //    return "-1";
            //}
        }



        /// <summary>
        /// GetCertSerNum
        /// </summary>
        /// <param name="cryptCtl"></param>
        public void GetCertSerNum()
        {
            //throw new NotImplementedException();
            cryptCtl = new CryptCtl();
            cryptCtl.GetCertSerNum();
        }

    }
}

