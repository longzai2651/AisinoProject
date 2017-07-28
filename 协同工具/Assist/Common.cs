using Microsoft.Win32;
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

namespace Assist
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
            //try
            //{
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
                    //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
                    string retString = reader.ReadToEnd();
                    return retString;
                }
                return result;


            //}
            //catch (Exception ex)
            //{
            //    WriteLogFile(0, "RequestPost", ex.ToString());
            //    //throw new Exception(ex.Message);
            //    return "Exception详细信息参考ExLog";
            //}
        }

        #region
        /// <summary>
        /// json->Dictionary
        /// <param name="jsonString"></param>
        /// <returns>返回Dictionary</returns>
        /// </summary>
        public Dictionary<string, string> getJsonob(string jsonString)
        {
            //try
            //{
                JavaScriptSerializer jss = new JavaScriptSerializer();

                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, string>>(jsonString);
            //}
            //catch (Exception ex)
            //{
            //    WriteLogFile(0, "getJsonob", ex.ToString());
            //    throw new Exception(ex.Message);
            //}
        }


        /// <summary>
        /// // 从一个对象信息生成Json串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string ObjectToJson(object obj)
        {
            //try
            //{
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                MemoryStream stream = new MemoryStream();
                serializer.WriteObject(stream, obj);
                byte[] dataBytes = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(dataBytes, 0, (int)stream.Length);
                return Encoding.UTF8.GetString(dataBytes);
            //}
            //catch (Exception ex)
            //{
            //    WriteLogFile(0, "ObjectToJson", ex.ToString());
            //    return "Exception详细信息参考ExLog";
            //}
        }
        
        /// <summary>
        /// // 从一个Json串生成对象信息
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object JsonToObject(string jsonString, object obj)
        {
            //try
            //{
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                return serializer.ReadObject(mStream);
            //}
            //catch (Exception ex)
            //{
            //    WriteLogFile(0, "JsonToObject", ex.ToString());
            //    throw new Exception(ex.Message);
            //}
        }



        /// <summary>
        /// // 从一个Json串生成JObject对象信息
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JObject JsonToObject(string jsonString)
        {

            //try
            //{
                JObject jo = new JObject();
                jo = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
                return jo;
            //}
            //catch (Exception ex)
            //{
            //    WriteLogFile(0, "JsonToObject", ex.ToString());
            //    throw new Exception(ex.Message);
            //}
        }



        #endregion



        
        /// <summary>
        /// LOG:0异常；1系统错误；2业务数据;
        /// <param name="LOG_LEVENL"></param>
        /// <param name="function"></param>
        /// <param name="content"></param>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:不要多次释放对象")]
        public void WriteLogFile(int LOG_LEVENL,string function,string content)
        {
            
            string datenow = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string path = "";
            if (LOG_LEVENL == 2)
            {
                path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Data\data" + DateTime.Today.ToString("_yyyyMMdd") + ".txt";
            }
            else
            {
                path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Log\Log" + DateTime.Today.ToString("_yyyyMMdd") + ".txt";
            }

            if (!File.Exists(path))//(Directory.Exists(Server.MapPath("~/upimg/hufu")) == false)//
            {
                if (!Directory.Exists(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Log"))
                {
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Log");
                }
                if (!Directory.Exists(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Data"))
                {
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\Data");
                }

                //FileStream fs1 = new FileStream(path, FileMode.Create | FileMode.Append, FileAccess.Write);//创建写入文件 
                FileStream fs1 = new FileStream(path, FileMode.Append, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                if (LOG_LEVENL == 2)
                {
                    sw.WriteLine("\n" + datenow + content);//开始写入值             
                }
                else
                {
                    sw.WriteLine("\n" + datenow + "******LOG_LEVENL:" + LOG_LEVENL.ToString() + "******" + "function:" + function + "\n\n" + content);//开始写入值
                }

                sw.Close();
                fs1.Close();
            }
            else
            {
                //FileStream fs = new FileStream(path, FileMode.Open | FileMode.Append, FileAccess.Write);
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                if (LOG_LEVENL == 2)
                {
                    sr.WriteLine("\n" + datenow + content);//开始写入值             
                }
                else
                {
                    sr.WriteLine("\n" + datenow + "******LOG_LEVENL:" + LOG_LEVENL.ToString() + "******" + "function:" + function + "\n\n" + content);//开始写入值
                }
                sr.Close();
                fs.Close();
            }
            /* */

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





        /// <summary>
        /// 获取指定路径的大小
        /// </summary>
        /// <param name="dirPath">路径</param>
        /// <returns></returns>
        public double GetDirectoryLength(string dirPath)
        {
            double len = 0;
            //判断该路径是否存在（是否为文件夹）
            if (!Directory.Exists(dirPath))
            {
                //查询文件的大小
                len = 0;
            }
            else
            {
                //定义一个DirectoryInfo对象
                DirectoryInfo di = new DirectoryInfo(dirPath);

                //通过GetFiles方法，获取di目录中的所有文件的大小
                foreach (FileInfo fi in di.GetFiles())
                {
                    len += fi.Length;
                }
                //获取di中所有的文件夹，并存到一个新的对象数组中，以进行递归
                DirectoryInfo[] dis = di.GetDirectories();
                if (dis.Length > 0)
                {
                    for (int i = 0; i < dis.Length; i++)
                    {
                        len += GetDirectoryLength(dis[i].FullName);
                    }
                }
            }
            return len;
        }

        //所给路径中所对应的文件大小
        public static double FileSize(string filePath)
        {
            //定义一个FileInfo对象，是指与filePath所指向的文件相关联，以获取其大小
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }



        #region 加密
        /// <summary>
        /// 将字符串转换为Base64字符串
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string EncodeBase64(string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }



        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="codeName">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public string DecodeBase64(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }

        #endregion

    }


}
