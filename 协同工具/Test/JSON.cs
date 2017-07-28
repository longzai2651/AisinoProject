using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace demo
{
    public partial class JSON : Form
    {
        CryptCtlDll.Common common = new CryptCtlDll.Common();
        public JSON()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string aa="";

            Dictionary<string,string> dic=new Dictionary<string,string>();

            dic=getJsonob(richTextBox1.Text.ToString());

            richTextBox2.Text = dic["key1"].ToString();
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static Dictionary<string, string> getJsonob(string jsonString)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                //将指定的 JSON 字符串转换为 Dictionary<string, object> 类型的对象
                return jss.Deserialize<Dictionary<string, string>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //{"key1":"01","key2":"4155544853455256455248454c4c4f3203002400060000000000000000002400b4859b3d4d4c10cfe91f8575a20000f82e137059b4859b3d4d4c10cfe91f8575a20000f8","key3":"4155544853455256455248454c4c4f3203002400060000000000000000002400b4859b3d4d4c10cfe91f8575a20000f82e137059b4859b3d4d4c10cfe91f8575a20000f8"}

            //{"key1":"01","key2":{"sEcho":1,"iTotalRecords":13,"iTotalDisplayRecords":13,"aaData":[["0=0=0=0","1100171130","07885330","2017-07-19","北京京东世纪信息技术有限公司","127.35","21.65","0","0",null,"0",null,"0",null,"110192562134916"],["0=0=0=0","4400163130","22842757","2017-07-18","广东东方雨虹防水工程有限公司","35688.89","6067.11","0","0",null,"0",null,"0",null,"91440106689326982B"],["0=0=0=0","1100171130","07978895","2017-07-11","北京京东世纪信息技术有限公司","298.29","50.71","0","0",null,"0",null,"0",null,"110192562134916"],["0=0=0=0","1200171130","00464175","2017-07-11","天津京东海荣贸易有限公司","50.43","8.57","0","0",null,"0",null,"0",null,"91120110MA07C4747P"],["0=0=0=0","1100171130","01517676","2017-07-10","北京安顺泰建材技术有限公司","1820.51","309.49","0","0",null,"0",null,"0",null,"91110115726330754C"],["0=0=0=0","3100171130","05453754","2017-07-07","上海依航计算机科技有限公司","84.62","14.38","0","0",null,"0",null,"0",null,"91310120794539373H"],["0=0=0=0","1200171130","00452121","2017-07-06","天津京东海荣贸易有限公司","76.07","12.93","0","0",null,"0",null,"0",null,"91120110MA07C4747P"],["0=0=0=0","1100171130","06767484","2017-07-06","北京京东世纪信息技术有限公司","3527.43","599.67","0","0",null,"0",null,"0",null,"110192562134916"],["0=0=0=0","1100164130","00634544","2017-07-06","北京岳泰同盛经贸发展有限公司","3666.67","623.33","0","0",null,"0",null,"0",null,"91110105681247569U"],["0=0=0=0","1100171130","06230160","2017-07-02","北京兴奥晟通汽车销售服务有限公司","2013.68","342.32","0","0",null,"0",null,"0",null,"9111011506282657X1"],["0=0=0=0","1100162130","10779288","2017-07-02","北京三合兴华汽车销售服务有限公司","366.67","62.33","0","0",null,"0",null,"0",null,"91110115067311946K"],["0=0=0=0","1300163130","14045915","2017-06-29","唐山京玉水泥有限公司","42777.78","7272.22","0","0",null,"0",null,"0",null,"130229746867604"],["0=0=0=0","1100171130","06750964","2017-06-23","北京京东世纪信息技术有限公司","869.23","147.77","0","0",null,"0",null,"0",null,"110192562134916"]]},"key3":"3d146642-21f9-4561-92fe-2916835415c7","key4":"13"}
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic=common.getJsonob(richTextBox1.Text.ToString());



        }


        /// <summary>
        /// 获取一个类指定的属性值
        /// </summary>
        /// <param name="info">object对象</param>
        /// <param name="field">属性名称</param>
        /// <returns></returns>
        public static object GetPropertyValue(object info, string field)
        {
            if (info == null) return null;
            Type t = info.GetType();
            IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            return property.First().GetValue(info, null);
        }

        public class keys
        {
            public string key1;
            public string key2;
            public string key3;
            public string key4;

            public keys(){}
            public keys(string _key1,string _key2,string _key3,string _key4)
            {
                this.key1 = _key1;
                this.key2 = _key2;
                this.key3 = _key3;
                this.key4 = _key4;
            }

        }



    }
}
