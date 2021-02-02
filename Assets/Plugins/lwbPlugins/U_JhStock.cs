



//#region 聚合
//public const string appKey = "4b2a4db6e9837f88bec34c209c5e4e6a";
//public const string jh基本数据 = @"http://web.juhe.cn:8080/finance/stock/hs";

//[EditorButton]
//public void DebugStock(string number)
//{
//    Stock stock = GetJhStock(number);
//    if (stock != null)
//    {
//        StockData data = stock.result[0].data;
//        Debug.Log(data.name + "\t" + data.nowPri + "\t" + data.increPer + "%");
//    }
//}
//public Stock GetJhStock(string number)
//{
//    string stockCode = Get加上前缀(number);
//    if (stockCode.Length != 8) return null;
//    string url1 = jh基本数据;
//    var parameters1 = new Dictionary<string, string>();
//    parameters1.Add("gid", stockCode); //股票编号，上海股市以sh开头，深圳股市以sz开头如：sh601009
//    parameters1.Add("key", appKey);//你申请的key
//    string result1 = SendPost_JH(url1, parameters1, "get");

//    JObject json = JObject.Parse(result1);
//    string errorCode1 = json["error_code"].ToString();
//    if (errorCode1 != "0")
//    {
//        Debug.Log("获取数据失败 errorCode1!=0");
//        return null;
//    }
//    else
//    {
//        //Debug.Log("成功" + json.ToString());
//        Stock stock = JsonConvert.DeserializeObject<Stock>(result1);

//        return stock;
//    }
//}
//[EditorButton]
//public void DebugStockS()
//{
//    foreach (var item in numberS)
//    {
//        DebugStock(item);
//    }
//}
//static string SendPost_JH(string url, IDictionary<string, string> parameters, string method)
//{
//    if (method.ToLower() == "post")
//    {
//        HttpWebRequest req = null;
//        HttpWebResponse rsp = null;
//        System.IO.Stream reqStream = null;
//        try
//        {
//            req = (HttpWebRequest)WebRequest.Create(url);
//            req.Method = method;
//            req.KeepAlive = false;
//            req.ProtocolVersion = HttpVersion.Version10;
//            req.Timeout = 5000;
//            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
//            byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));
//            reqStream = req.GetRequestStream();
//            reqStream.Write(postData, 0, postData.Length);
//            rsp = (HttpWebResponse)req.GetResponse();
//            Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
//            return GetResponseAsString(rsp, encoding);
//        }
//        catch (Exception ex)
//        {
//            return ex.Message;
//        }
//        finally
//        {
//            if (reqStream != null) reqStream.Close();
//            if (rsp != null) rsp.Close();
//        }
//    }
//    else
//    {
//        //创建请求
//        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + BuildQuery(parameters, "utf8"));

//        //GET请求
//        request.Method = "GET";
//        request.ReadWriteTimeout = 5000;
//        request.ContentType = "text/html;charset=UTF-8";
//        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
//        Stream myResponseStream = response.GetResponseStream();
//        StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

//        //返回内容
//        string retString = myStreamReader.ReadToEnd();
//        return retString;
//    }
//}


///// 组装普通文本请求参数。
///// <param name="parameters">Key-Value形式请求参数字典</param>
///// <returns>URL编码后的请求数据</returns>
//static string BuildQuery(IDictionary<string, string> parameters, string encode)
//{
//    StringBuilder postData = new StringBuilder();
//    bool hasParam = false;
//    IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
//    while (dem.MoveNext())
//    {
//        string name = dem.Current.Key;
//        string value = dem.Current.Value;
//        // 忽略参数名或参数值为空的参数
//        if (!string.IsNullOrEmpty(name))//&& !string.IsNullOrEmpty(value)
//        {
//            if (hasParam)
//            {
//                postData.Append("&");
//            }
//            postData.Append(name);
//            postData.Append("=");
//            if (encode == "gb2312")
//            {
//                //postData.Append(System.Web.HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
//            }
//            else if (encode == "utf8")
//            {
//                //xg
//                //postData.Append(System.Web.HttpUtility.UrlEncode(value, Encoding.UTF8));
//                postData.Append(value);
//            }
//            else
//            {
//                postData.Append(value);
//            }
//            hasParam = true;
//        }
//    }
//    return postData.ToString();
//}

///// 把响应流转换为文本。
///// <param name="rsp">响应流对象</param>
///// <param name="encoding">编码方式</param>
///// <returns>响应文本</returns>
//static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
//{
//    System.IO.Stream stream = null;
//    StreamReader reader = null;
//    try
//    {
//        // 以字符流的方式读取HTTP响应
//        stream = rsp.GetResponseStream();
//        reader = new StreamReader(stream, encoding);
//        return reader.ReadToEnd();
//    }
//    finally
//    {
//        // 释放资源
//        if (reader != null) reader.Close();
//        if (stream != null) stream.Close();
//        if (rsp != null) rsp.Close();
//    }
//}






////void GetHushiTable()
////{
//////7.沪股列表
////string url7 = "http://web.juhe.cn:8080/finance/stock/shall";

////var parameters7 = new Dictionary<string, string>();

////parameters7.Add("key", appkey);//你申请的key
////parameters7.Add("page", ""); //第几页,每页20条数据,默认第1页

////string result7 = sendPost(url7, parameters7, "get");

////JsonObject newObj7 = new JsonObject(result7);
////String errorCode7 = newObj7["error_code"].Value;

////if (errorCode7 == "0")
////{
////    Debug.WriteLine("成功");
////    Debug.WriteLine(newObj7);
////}
////else
////{
////    //Debug.WriteLine("失败");
////    Debug.WriteLine(newObj7["error_code"].Value + ":" + newObj7["reason"].Value);
////}
////}

////void GetShenshiTable()
////{
//////6.深圳股市列表
////string url6 = "http://web.juhe.cn:8080/finance/stock/szall";
////var parameters6 = new Dictionary<string, string>();
////parameters6.Add("key", appkey);//你申请的key
////parameters6.Add("page", ""); //第几页(每页20条数据),默认第1页
////string result6 = sendPost(url6, parameters6, "get");
////JsonObject newObj6 = new JsonObject(result6);
////String errorCode6 = newObj6["error_code"].Value;
////if (errorCode6 == "0")
////{
////    Debug.WriteLine("成功");
////    Debug.WriteLine(newObj6);
////}
////else
////{
////    //Debug.WriteLine("失败");
////    Debug.WriteLine(newObj6["error_code"].Value + ":" + newObj6["reason"].Value);
////}
////}





//#region 聚合基类

//public class StockData
//{
//    public string buyFive { get; set; }
//    public string buyFivePri { get; set; }
//    public string buyFour { get; set; }
//    public string buyFourPri { get; set; }
//    public string buyOne { get; set; }
//    public string buyOnePri { get; set; }
//    public string buyThree { get; set; }
//    public string buyThreePri { get; set; }
//    public string buyTwo { get; set; }
//    public string buyTwoPri { get; set; }
//    public string competitivePri { get; set; }
//    public string date { get; set; }
//    public string gid { get; set; }
//    public string increPer { get; set; }
//    public string increase { get; set; }
//    public string name { get; set; }
//    public string nowPri { get; set; }
//    public string reservePri { get; set; }
//    public string sellFive { get; set; }
//    public string sellFivePri { get; set; }
//    public string sellFour { get; set; }
//    public string sellFourPri { get; set; }
//    public string sellOne { get; set; }
//    public string sellOnePri { get; set; }
//    public string sellThree { get; set; }
//    public string sellThreePri { get; set; }
//    public string sellTwo { get; set; }
//    public string sellTwoPri { get; set; }
//    public string time { get; set; }
//    public string todayMax { get; set; }
//    public string todayMin { get; set; }
//    public string todayStartPri { get; set; }
//    public string traAmount { get; set; }
//    public string traNumber { get; set; }
//    public string yestodEndPri { get; set; }
//}

//public class Dapandata
//{
//    public string dot { get; set; }
//    public string name { get; set; }
//    public string nowPic { get; set; }
//    public string rate { get; set; }
//    public string traAmount { get; set; }
//    public string traNumber { get; set; }
//}

//public class Gopicture
//{
//    public string minurl { get; set; }
//    public string dayurl { get; set; }
//    public string weekurl { get; set; }
//    public string monthurl { get; set; }
//}

//public class ResultItem
//{
//    public StockData data { get; set; }
//    public Dapandata dapandata { get; set; }
//    public Gopicture gopicture { get; set; }
//}

//public class Stock
//{
//    public string resultcode { get; set; }
//    public string reason { get; set; }
//    public List<ResultItem> result { get; set; }
//    public int error_code { get; set; }
//}
//#endregion



//#endregion