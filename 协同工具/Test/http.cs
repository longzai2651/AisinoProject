using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using CryptCtlDll;
using Newtonsoft.Json.Linq;



namespace demo
{
    public partial class http : Form
    {
        CryptCtlDll.CryptCtlDllClass cryptCtlDllClass = new CryptCtlDll.CryptCtlDllClass();
        /// <summary>
        /// Common通用类
        /// </summary>
        CryptCtlDll.Common common = null;
        /// <summary>
        /// Token
        /// </summary>
        public string Token = "";

        /// <summary>
        /// CertInfo
        /// </summary>
        public string CertInfo = "";

        public string UpdateUrl = "http://192.168.0.20:8080/jxProject/jxfp/xjxc/zdtb.action";

        public http()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kkey k = new kkey();
            string str = "{\"key1\":\"01\",\"key2\":{\"sEcho\":1,\"iTotalRecords\":2,\"iTotalDisplayRecords\":2,\"aaData\":[[\"0=0=0=0\",\"1100171130\",\"07885330\",\"2017-07-19\",\"北京京东世纪信息技术有限公司\",\"127.35\",\"21.65\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"1100164130\",\"00634544\",\"2017-07-06\",\"北京岳泰同盛经贸发展有限公司\",\"3666.67\",\"623.33\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110105681247569U\"]]},\"key3\":\"d6d506bd-6e12-4015-a0fe-0921bc4f1d51\",\"key4\":\"2\"}";
            JObject jo = new JObject();
            common = new Common();
            jo = common.JsonToObject(str);
            string aa = jo["key2"]["aaData"].ToString();
            //aa = "test测试测试测试测试测试测试12345678901234567890";
            //aa = common.StringToUnicode(aa);

            StringBuilder sb = null;
            UpdateUrl = "http://192.168.1.100:8080/AisinoYz/jxfp/xjxc/zdtb.action";
            //UpdateUrl = "http://localhost:24029/Default.aspx";
            sb = new StringBuilder();
            sb.AppendFormat(@"zdtbdata={0}"
                    , aa
                    );


            string zdtbdata = common.StringToUnicode("zdtbdata=[[\"0=0=0=0\",\"1100171130\",\"07885330\",\"2017-07-19\",\"北京京东世纪信息技术有限公司\",\"127.35\",\"21.65\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"1100164130\",\"00634544\",\"2017-07-06\",\"北京岳泰同盛经贸发展有限公司\",\"3666.67\",\"623.33\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110105681247569U\"]]");
            //zdtbdata = "测试1234567890";
            //zdtbdata = common.StringToUnicode(zdtbdata);

            string msg = common.RequestPost(UpdateUrl, "zdtbdata="+zdtbdata);//第三次请求
            common.WriteLogFile(0, "http_Load", msg);
            richTextBox1.Text = msg;
            /**/
            

            
            
        }





        private void fun()
        {



            try
            {
                //CertInfo = cryptCtlDllClass.GetCertInfo();
                common = new CryptCtlDll.Common();

                string PostResult = "";
                PostResult = FirReq();//第一次请求
                if (PostResult.Length == 0)
                    return;
                PostResult = PostResult.Replace("null(", "").Replace(")", "");
                Dictionary<string, string> dic = null;
                dic = new Dictionary<string, string>();
                dic = common.getJsonob(PostResult);
                if (dic["key1"].ToString() == "01")
                {
                    //string ClientAuth = cryptCtlDllClass.ClientAuth(dic["key2"].ToString());
                    PostResult = SecReq(dic);//common.RequestPost(url, sb.ToString());//第二次请求
                    if (PostResult.Length == 0)
                        return;
                    PostResult = PostResult.Replace("null(", "").Replace(")", "");
                    dic = new Dictionary<string, string>();
                    dic = common.getJsonob(PostResult);

                    if (dic["key1"].ToString() == "03")
                    {
                        string toc = dic["key2"].ToString();
                        PostResult = ThirReq(dic);//common.RequestPost(url, sb.ToString());//第三次请求
                        if (PostResult.Length == 0)
                            return;
                        PostResult = PostResult.Replace("null(", "").Replace(")", "");
                        dic = new Dictionary<string, string>();
                        dic = common.getJsonob(PostResult);
                        string aoData = "";
                        aoData = dic["aoData"].ToString();

                        string rst = Update(dic);

                        common.WriteLogFile(3, "已从后台下载数据aaData，并更新到本系统，更新操作状态" + rst, aoData);

                        richTextBox1.Text = PostResult;
                    }

                    richTextBox1.Text = PostResult;
                }
            }
            catch (Exception ex)
            {
                common.WriteLogFile(0, "button1_Click", ex.ToString());
                //return "Exception详细信息参考ExLog";
            }
        }

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
            StringBuilder sb = null;
            common = new CryptCtlDll.Common();
            url = "https://fpdk.bjsat.gov.cn/SbsqWW/login.do";
            sb = new StringBuilder();
            sb.AppendFormat("clientHello={0}&type={1}&ymbb={2}"
                , "3077020103305E310B300906035504061302636E31153013060355040B1E0C56FD5BB67A0E52A1603B5C40311D301B06035504031E147A0E52A175355B508BC14E667BA174064E2D5FC331193017060355040D1E10006300610031003000300030003000320207020100001449ADA209310702010102020402"
                //, cryptCtlDllClass.ClientHello()
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
        /// <param name="dic"></param>
        /// <returns></returns>
        private string SecReq(Dictionary<string, string> dic)
        {
            string url;
            StringBuilder sb = null;
            common = new CryptCtlDll.Common();
            url = "https://fpdk.bjsat.gov.cn/SbsqWW/login.do";
            sb = new StringBuilder();
            sb.AppendFormat("type={0}&clientAuthCode={2}&serverRandom={4}&password={5}&cert={3}&ymbb={1}"
               , "CLIENT-AUTH"
               , "3.0.09"
               , cryptCtlDllClass.ClientAuth(dic["key2"].ToString(),"88888888")
               , cryptCtlDllClass.GetCertInfo()
               , dic["key3"].ToString()
               , "88888888"
               );
            return common.RequestPost(url, sb.ToString());//第二次请求

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
        /// <param name="dic"></param>
        /// <returns></returns>
        private string ThirReq(Dictionary<string, string> dic)
        {
            string url;
            string  Datenow ="";
            Datenow = DateTime.Today.ToString("yyyy-MM-dd");
            string DateLast = "";
            DateLast = Convert.ToDateTime(Datenow).AddMonths(-1).ToString("yyyy-MM-dd");
            StringBuilder sb = null;
            common = new CryptCtlDll.Common();
            url = "https://fpdk.bjsat.gov.cn/SbsqWW/gxcx.do";
            sb = new StringBuilder();
            sb.AppendFormat(@"fpdm={0}&fphm={1}&rq_q={2}&rq_z={3}&xfsbh={4}&rzfs={5}&rzzt={6}&gxzt={7}&fpzt={8}&fplx={9}&cert={10}&token={11}&ymbb={12}&aoData={13}"
                    , "", ""
                    , DateLast, Datenow
                    , "", ""
                    , 0, 0, 0, -1
                    , cryptCtlDllClass.GetCertInfo()
                    , dic["key2"].ToString()
                    , "3.0.09"
                    , "[{\"name\":\"sEcho\",\"value\":0},{\"name\":\"iColumns\",\"value\":14},{\"name\":\"sColumns\",\"value\":\",,,,,,,,,,,,,\"},{\"name\":\"iDisplayStart\",\"value\":0},{\"name\":\"iDisplayLength\",\"value\":8000},{\"name\":\"mDataProp_0\",\"value\":0},{\"name\":\"mDataProp_1\",\"value\":1},{\"name\":\"mDataProp_2\",\"value\":2},{\"name\":\"mDataProp_3\",\"value\":3},{\"name\":\"mDataProp_4\",\"value\":4},{\"name\":\"mDataProp_5\",\"value\":5},{\"name\":\"mDataProp_6\",\"value\":6},{\"name\":\"mDataProp_7\",\"value\":7},{\"name\":\"mDataProp_8\",\"value\":8},{\"name\":\"mDataProp_9\",\"value\":9},{\"name\":\"mDataProp_10\",\"value\":10},{\"name\":\"mDataProp_11\",\"value\":11},{\"name\":\"mDataProp_12\",\"value\":12}]"
                    );
            return common.RequestPost(url, sb.ToString());//第三次请求

        }

        private string Update(Dictionary<string, string> dic)
        {
            StringBuilder sb = null;
            common = new CryptCtlDll.Common();
            UpdateUrl = "http://192.168.0.100:8080/AisinoYz/jxfp/xjxc/zdtb.action";
            sb = new StringBuilder();
            string str = "{\"key1\":\"01\",\"key2\":{\"sEcho\":1,\"iTotalRecords\":13,\"iTotalDisplayRecords\":13,\"aaData\":[[\"0=0=0=0\",\"1100171130\",\"07885330\",\"2017-07-19\",\"北京京东世纪信息技术有限公司\",\"127.35\",\"21.65\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"4400163130\",\"22842757\",\"2017-07-18\",\"广东东方雨虹防水工程有限公司\",\"35688.89\",\"6067.11\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91440106689326982B\"],[\"0=0=0=0\",\"1100171130\",\"07978895\",\"2017-07-11\",\"北京京东世纪信息技术有限公司\",\"298.29\",\"50.71\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"1200171130\",\"00464175\",\"2017-07-11\",\"天津京东海荣贸易有限公司\",\"50.43\",\"8.57\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91120110MA07C4747P\"],[\"0=0=0=0\",\"1100171130\",\"01517676\",\"2017-07-10\",\"北京安顺泰建材技术有限公司\",\"1820.51\",\"309.49\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110115726330754C\"],[\"0=0=0=0\",\"3100171130\",\"05453754\",\"2017-07-07\",\"上海依航计算机科技有限公司\",\"84.62\",\"14.38\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91310120794539373H\"],[\"0=0=0=0\",\"1200171130\",\"00452121\",\"2017-07-06\",\"天津京东海荣贸易有限公司\",\"76.07\",\"12.93\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91120110MA07C4747P\"],[\"0=0=0=0\",\"1100171130\",\"06767484\",\"2017-07-06\",\"北京京东世纪信息技术有限公司\",\"3527.43\",\"599.67\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"1100164130\",\"00634544\",\"2017-07-06\",\"北京岳泰同盛经贸发展有限公司\",\"3666.67\",\"623.33\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110105681247569U\"],[\"0=0=0=0\",\"1100171130\",\"06230160\",\"2017-07-02\",\"北京兴奥晟通汽车销售服务有限公司\",\"2013.68\",\"342.32\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"9111011506282657X1\"],[\"0=0=0=0\",\"1100162130\",\"10779288\",\"2017-07-02\",\"北京三合兴华汽车销售服务有限公司\",\"366.67\",\"62.33\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110115067311946K\"],[\"0=0=0=0\",\"1300163130\",\"14045915\",\"2017-06-29\",\"唐山京玉水泥有限公司\",\"42777.78\",\"7272.22\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"130229746867604\"],[\"0=0=0=0\",\"1100171130\",\"06750964\",\"2017-06-23\",\"北京京东世纪信息技术有限公司\",\"869.23\",\"147.77\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"]]},\"key3\":\"86d3d079-66c7-4f77-925f-4891938a5b96\",\"key4\":\"13\"}";
            //return str;
            sb.AppendFormat(@"fpdm={0}"
                    //, dic["aoData"].ToString()
                    , str
                    );


            JObject jo = new JObject();
            common = new Common();
            jo = common.JsonToObject(str);
            string aa = jo["key2"]["aaData"].ToString();
            sb = new StringBuilder();
            sb.AppendFormat(@"fpdm={0}"
                    , aa
                    );
            string msg = common.RequestPost(UpdateUrl, sb.ToString());//第三次请求
            
            common.WriteLogFile(0, "http_Load", msg);
            return msg;









        }


        private void http_Load(object sender, EventArgs e)
        {
            /*
            common = new Common();
            string str = "[[\"0=0=0=0\",\"1100171130\",\"07885330\",\"2017-07-19\",\"北京京东世纪信息技术有限公司\",\"127.35\",\"21.65\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"1100164130\",\"00634544\",\"2017-07-06\",\"北京岳泰同盛经贸发展有限公司\",\"3666.67\",\"623.33\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110105681247569U\"]]";
            str = "测试1234567890";
            string aa = "";
            aa = common.StringToUnicode(str);


            string bb = common.UnicodeToString(aa);
            richTextBox1.Text = bb.ToString();
            */

        }

        public class kkey 
        {
            public string key1 { get; set; }
            public string key2 { get; set; }
            public string key3 { get; set; }
            public string key4 { get; set; }
        }



    }
}
