using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CryptCtlDll
{
    /// <summary>
    /// 通用类
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 获得post请求后响应的数据
        /// <param name="postUrl">请求地址</param>
        /// <param name="data">请求带的数据</param>
        /// <returns>响应内容</returns>
        /// </summary>
        public string RequestPost(string postUrl, string data)
        {
            string result = "";
            try
            {
                //命名空间System.Net下的HttpWebRequest类
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUrl);
                //参照浏览器的请求报文 封装需要的参数 这里参照ie9
                //浏览器可接受的MIME类型
                request.Accept = "text/plain, */*; q=0.01";
                //浏览器类型，如果Servlet返回的内容与浏览器类型有关则该值非常有用
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                //请求方式
                request.Method = "POST";
                //是否保持常连接
                request.KeepAlive = false;
                //request.Headers.Add("Accept-Encoding", "gzip, deflate");
                //表示请求消息正文的长度
                request.ContentLength = data.Length;

                Stream postStream = request.GetRequestStream();
                byte[] postData = Encoding.UTF8.GetBytes(data);


                //将传输的数据，请求正文写入请求流
                postStream.Write(postData, 0, postData.Length);
                postStream.Dispose();
                //响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    string retString = reader.ReadToEnd();
                    return retString;
                }
                return result;


            }
            catch (Exception ex)
            {
                WriteLogFile(0, "RequestPost", ex.ToString());
                //throw new Exception(ex.Message);
                return "Exception详细信息参考ExLog";
            }
        }

        #region
        /// <summary>
        /// json->Dictionary
        /// <param name="jsonString"></param>
        /// <returns>返回Dictionary</returns>
        /// </summary>
        public Dictionary<string, string> getJsonob(string jsonString)
        {
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();

                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, string>>(jsonString);
            }
            catch (Exception ex)
            {
                WriteLogFile(0, "getJsonob", ex.ToString());
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// // 从一个对象信息生成Json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ObjectToJson(object obj)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                MemoryStream stream = new MemoryStream();
                serializer.WriteObject(stream, obj);
                byte[] dataBytes = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(dataBytes, 0, (int)stream.Length);
                return Encoding.UTF8.GetString(dataBytes);
            }
            catch (Exception ex)
            {
                WriteLogFile(0, "ObjectToJson", ex.ToString());
                return "Exception详细信息参考ExLog";
            }
        }
        
        /// <summary>
        /// // 从一个Json串生成对象信息
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object JsonToObject(string jsonString, object obj)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                return serializer.ReadObject(mStream);
            }
            catch (Exception ex)
            {
                WriteLogFile(0, "JsonToObject", ex.ToString());
                throw new Exception(ex.Message);
            }
        }



        /// <summary>
        /// // 从一个Json串生成JObject对象信息
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JObject JsonToObject(string jsonString)
        {

            try
            {
                JObject jo = new JObject();
                jo = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
                return jo;
            }
            catch (Exception ex)
            {
                WriteLogFile(0, "JsonToObject", ex.ToString());
                throw new Exception(ex.Message);
            }
        }





        #region//using System.Runtime.Serialization.Json;
        public T parse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        public string stringify(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }



        #endregion


        #endregion




        /// <summary>
        /// LOG:0异常；1系统错误；2业务数据;3业务数据+调试信息
        /// <param name="LOG_LEVENL"></param>
        /// <param name="function"></param>
        /// <param name="content"></param>
        /// </summary>
        public void WriteLogFile(int LOG_LEVENL,string function,string content)
        {
            string datenow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Log\Log"+DateTime.Today.ToString("_yyyyMMdd")+".txt";
            if (!File.Exists(path))//(Directory.Exists(Server.MapPath("~/upimg/hufu")) == false)//
            {
                if (!Directory.Exists(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase+@"\Log"))//
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase+@"\Log");
                //FileStream fs1 = new FileStream(path, FileMode.Create | FileMode.Append, FileAccess.Write);//创建写入文件 
                FileStream fs1 = new FileStream(path, FileMode.Append, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine("\n" + datenow + "******LOG_LEVENL:" + LOG_LEVENL.ToString() + "******" + "function:" + function + "\n\n" + content);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                //FileStream fs = new FileStream(path, FileMode.Open | FileMode.Append, FileAccess.Write);
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine("\n" + datenow + "******LOG_LEVENL:" + LOG_LEVENL.ToString() + "******" + "function:" + function + "\n\n" + content);//开始写入值
                sr.Close();
                fs.Close();
            }


            /*
                         sw.WriteLine("*********************" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "*********************");
            sw.Write("错误消息:" + e.Message);
            sw.WriteLine();
            sw.Write("错误源:" + e.Source);
            sw.WriteLine();
            sw.Write("堆栈信息:" + e.StackTrace);
            sw.WriteLine();
            sw.Write("引发异常方法:" + e.TargetSite.Name);
            sw.WriteLine();
            sw.WriteLine("*********************结束*********************");
            sw.Flush();
            sw.Close();
             */
        }

        /// <summary>  
        /// 字符串转为UniCode码字符串  
        /// </summary>  
        /// <param name="s"></param>  
        /// <returns></returns>  
        public string StringToUnicode(string s)
        {
            char[] charbuffers = s.ToCharArray();
            byte[] buffer;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < charbuffers.Length; i++)
            {
                buffer = System.Text.Encoding.Unicode.GetBytes(charbuffers[i].ToString());
                sb.Append(String.Format("//u{0:X2}{1:X2}", buffer[1], buffer[0]));
                //sb.Append(String.Format("/u{0:X2}{1:X2}", buffer[1], buffer[0]));
            }
            return sb.ToString();
        }
        /// <summary>  
        /// Unicode字符串转为正常字符串  
        /// </summary>  
        /// <param name="srcText"></param>  
        /// <returns></returns>  
        public string UnicodeToString(string srcText)
        {
            string dst = "";
            string src = srcText;
            int len = srcText.Length / 6;
            for (int i = 0; i <= len - 1; i++)
            {
                string str = "";
                str = src.Substring(0, 6).Substring(2);
                src = src.Substring(6);
                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), NumberStyles.HexNumber).ToString());
                dst += Encoding.Unicode.GetString(bytes);
            }
            return dst;
        } 



        /*
         
         {\"key1\":\"01\",\"key2\":{\"sEcho\":1,\"iTotalRecords\":13,\"iTotalDisplayRecords\":13,\"aaData\":[[\"0=0=0=0\",\"1100171130\",\"07885330\",\"2017-07-19\",\"北京京东世纪信息技术有限公司\",\"127.35\",\"21.65\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"4400163130\",\"22842757\",\"2017-07-18\",\"广东东方雨虹防水工程有限公司\",\"35688.89\",\"6067.11\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91440106689326982B\"],[\"0=0=0=0\",\"1100171130\",\"07978895\",\"2017-07-11\",\"北京京东世纪信息技术有限公司\",\"298.29\",\"50.71\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"1200171130\",\"00464175\",\"2017-07-11\",\"天津京东海荣贸易有限公司\",\"50.43\",\"8.57\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91120110MA07C4747P\"],[\"0=0=0=0\",\"1100171130\",\"01517676\",\"2017-07-10\",\"北京安顺泰建材技术有限公司\",\"1820.51\",\"309.49\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110115726330754C\"],[\"0=0=0=0\",\"3100171130\",\"05453754\",\"2017-07-07\",\"上海依航计算机科技有限公司\",\"84.62\",\"14.38\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91310120794539373H\"],[\"0=0=0=0\",\"1200171130\",\"00452121\",\"2017-07-06\",\"天津京东海荣贸易有限公司\",\"76.07\",\"12.93\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91120110MA07C4747P\"],[\"0=0=0=0\",\"1100171130\",\"06767484\",\"2017-07-06\",\"北京京东世纪信息技术有限公司\",\"3527.43\",\"599.67\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"],[\"0=0=0=0\",\"1100164130\",\"00634544\",\"2017-07-06\",\"北京岳泰同盛经贸发展有限公司\",\"3666.67\",\"623.33\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110105681247569U\"],[\"0=0=0=0\",\"1100171130\",\"06230160\",\"2017-07-02\",\"北京兴奥晟通汽车销售服务有限公司\",\"2013.68\",\"342.32\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"9111011506282657X1\"],[\"0=0=0=0\",\"1100162130\",\"10779288\",\"2017-07-02\",\"北京三合兴华汽车销售服务有限公司\",\"366.67\",\"62.33\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"91110115067311946K\"],[\"0=0=0=0\",\"1300163130\",\"14045915\",\"2017-06-29\",\"唐山京玉水泥有限公司\",\"42777.78\",\"7272.22\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"130229746867604\"],[\"0=0=0=0\",\"1100171130\",\"06750964\",\"2017-06-23\",\"北京京东世纪信息技术有限公司\",\"869.23\",\"147.77\",\"0\",\"0\",null,\"0\",null,\"0\",null,\"110192562134916\"]]},\"key3\":\"86d3d079-66c7-4f77-925f-4891938a5b96\",\"key4\":\"13\"}
         */




        /*
         
                 /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public void JsonToo(string jsonStr)
        {
            //jsonStr = "{\"name\":\"tom\",\"value\":11}";   //jsonStr 为json格式的字符串
            JavaScriptSerializer json = new JavaScriptSerializer();   //实例化一个能够序列化数据的类
            ToJson list = json.Deserialize<ToJson>(jsonStr);    //将json数据转化为对象类型并赋值给list
            string Name = list.name;      //Name的值为tom..  list可点出name
        }

        /// <summary>
        /// 
        /// </summary>
        public class ToJson
        {
            public string name { get; set; }  //属性的名字，必须与json格式字符串中的"key"值一样。
            public string value { get; set; }
        }
         */

    }


}
