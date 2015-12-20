using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using HtmlAgilityPack;

namespace GetUrl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string m_WebSite = "http://cubingchina.com/";
        string m_WebSearch = "results/person?region=World&gender=all&name={0}";
        string m_Html = "";
        //private string GetUrl(string Url, string type)
        //{
        //    WebClient client = new WebClient();
        //    string strBuff = "";
        //    char[] cbuffer = new char[2048];
        //    int byteRead = 0;
        //    client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
        //    label2.Text += DateTime.Now.ToLongTimeString() + "\r\n";
        //    Stream data = client.OpenRead(Url);
        //    StreamReader reader = new StreamReader(data, Encoding.GetEncoding(type)); // 注：汉字需要转为UTF8格式
        //    label2.Text += DateTime.Now.ToLongTimeString() + "\r\n";
        //    byteRead = reader.Read(cbuffer, 0, 2048);
        //    while (byteRead != 0)
        //    {
        //        string strResp = new string(cbuffer, 0, byteRead);
        //        strBuff = strBuff + strResp;
        //        if (strBuff.Length > 14053)
        //        {
        //            break;
        //        }
        //        byteRead = reader.Read(cbuffer, 0, 2048);
        //    }
        //    label2.Text += DateTime.Now.ToLongTimeString() + "\r\n";
        //    //return strBuff;
        //    //string s = new string(cbuffer, 0, reader.Read(cbuffer, 0, 5000));
        //    data.Close();
        //    reader.Close();
        //    return strBuff;
        //}
        public static string GetUrltoHtml(string Url, string type)
        {
            string strBuff = "";
            char[] cbuffer = new char[256];
            int byteRead = 0;
            try
            {
                WebRequest wReq = WebRequest.Create(Url);
                wReq.Timeout = 15000;
                WebResponse wResp = wReq.GetResponse();
                Stream respStream = wResp.GetResponseStream();
                using (StreamReader reader = new StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    return reader.ReadToEnd();
                    //byteRead = reader.Read(cbuffer, 0, 256);
                    //while (byteRead != 0)
                    //{
                    //    string strResp = new string(cbuffer, 0, byteRead);
                    //    strBuff = strBuff + strResp;
                    //    if (strBuff.Length > 17921)
                    //    {
                    //        break;
                    //    }
                    //    byteRead = reader.Read(cbuffer, 0, 256);
                    //}
                    //respStream.Close();
                    //reader.Close();
                    //return strBuff;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            DateTime beforDT = DateTime.Now;
            label2.Text = "";
            label2.Text += DateTime.Now.ToLongTimeString() + "\r\n";
            string sSearch = string.Format(m_WebSite + m_WebSearch, txtUrl.Text.Trim());
            m_Html = GetUrltoHtml(sSearch, "UTF-8");
            //m_Html = GetUrl(sSearch, "UTF-8");
            label2.Text += DateTime.Now.ToLongTimeString() + "\r\n";
            List<string> Links = new List<string>();
            string sResult = "";
            MatchCollection TitleMatchs = Regex.Matches(m_Html, "(?<=<td>)(.*?)(?=</td>)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (TitleMatchs.Count > 0)
            {
                foreach (Match NextMatch in TitleMatchs)
                {
                    if (NextMatch.Value.Contains("<a"))
                    {
                        if (!NextMatch.Value.Contains("<img"))
                        {
                            sResult += Regex.Match(NextMatch.Value, "<a.*?>(.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Multiline).Groups[1].Value + "|";
                        }
                        else
                        {
                            sResult += Regex.Match(NextMatch.Value, "(?=</a>(.*))", RegexOptions.IgnoreCase | RegexOptions.Multiline).Groups[1].Value + "|";
                        }
                        if (!string.IsNullOrEmpty(GetLink(NextMatch)))
                        {
                            Links.Add(GetLink(NextMatch));
                        }
                    }
                    else
                    {
                        sResult += NextMatch.Value + "\r\n";
                    }
                }
                if (Links.Count == 1)
                {
                    label2.Text += DateTime.Now.ToLongTimeString() + "\r\n";
                    m_Html = GetUrltoHtml(m_WebSite + Links.ToArray()[0], "UTF-8");
                    //m_Html = GetUrl(m_WebSite + Links.ToArray()[0], "UTF-8");
                    label2.Text += DateTime.Now.ToLongTimeString() + "\r\n";
                    string temp = "";
                    Match tempMatch = Regex.Match(m_Html, "<div class=\"table-responsive\" id=\"yw0\"+>([\\s\\S]+?)</div>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    if (tempMatch != null)
                    {
                        temp = tempMatch.Groups[1].Value;
                    }
                    MatchCollection MatchItems = Regex.Matches(temp, "<a [^>]+>(.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    //MatchCollection MatchScore = Regex.Matches(temp, "<a class='e'.*?>(.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    for (int i = 0; i < MatchItems.Count; i++)
                    {
                        if (MatchItems[i].Groups[1].Value.Contains("<span"))
                        {
                            if (i == 0)
                            {
                                sResult += Regex.Match(MatchItems[i].Groups[1].Value, "<span [^>]+>(.*?)</span>", RegexOptions.IgnoreCase | RegexOptions.Multiline).Groups[1].Value + ":";
                            }
                            else
                            {
                                sResult = sResult.Substring(0, sResult.Length - 1);
                                sResult += "\r\n" + Regex.Match(MatchItems[i].Groups[1].Value, "<span [^>]+>(.*?)</span>", RegexOptions.IgnoreCase | RegexOptions.Multiline).Groups[1].Value + ":";
                            }
                        }
                        else
                        {
                            sResult += MatchItems[i].Groups[1].Value + "|";
                        }
                    }
                    sResult = sResult.Substring(0, sResult.Length - 1);
                    //foreach (Match NextMatch in MatchItems)
                    //{
                    //    sResult += "\r\n" + NextMatch.Groups[1].Value + ":";
                    //    for (int i = n; i < MatchScore.Count; i++)
                    //    {
                    //        sResult += MatchScore[i].Groups[1].Value + "|";
                    //        n++;
                    //        if (n % 2 == 0)
                    //        {
                    //            break;
                    //        }
                    //    }
                    //    sResult = sResult.Substring(0, sResult.Length - 1);
                    //}
                    sResult = sResult.Replace("#039;", "'");
                    sResult = sResult.Replace("<span style='color:#999'>", "(");
                    sResult = sResult.Replace("</span>|", ")");
                    DateTime afterDT = System.DateTime.Now;
                    label2.Text += DateTime.Now.ToLongTimeString() + "\r\n";
                    TimeSpan ts = afterDT.Subtract(beforDT);
                    labA.Text = sResult + "\r\n" + ts.TotalSeconds;
                }
                else
                {
                    labA.Text = sResult;
                }
            }
            else
            {
                labA.Text = "怎么找还是找不到呀~";
            }
        }
        private string GetLink(Match NextMatch)
        {
            string sResult = "";
            MatchCollection matches = Regex.Matches(NextMatch.Value, "<a href=\"(.*?)\"", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                sResult = match.Groups[1].Value;
            }
            if (!string.IsNullOrEmpty(sResult))
            {
                return sResult;
            }
            else
            {
                return null;
            }
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGet_Click(null, null);
            }
        }

        private void test()
        {
            string sSearch = string.Format(m_WebSite + m_WebSearch, txtUrl.Text.Trim());

            HtmlAgilityPack.HtmlDocument Hdoc = new HtmlAgilityPack.HtmlDocument();
            Hdoc.LoadHtml(GetUrltoHtml(sSearch, "UTF-8"));
            //Hdoc.DocumentNode.ChildNodes;//获得根节点的一级节点
            //Hdoc.DocumentNode.Descendants();//获得所有子孙节点
            //Hdoc.DocumentNode.Descendants("div");//获得所有子孙节点的div节点
            foreach (HtmlNode divNode in Hdoc.DocumentNode.Descendants("tr"))
            {
                if (divNode.GetAttributeValue("class", null) == "odd" || divNode.GetAttributeValue("class", null) == "even")
                {
                    string name = divNode.Descendants("a").First().InnerText;
                    string wcaid = divNode.Descendants("td").ElementAt(2).InnerText;
                    string url = divNode.Descendants("a").First().GetAttributeValue("href", null);
                    string country = divNode.Descendants("td").ElementAt(3).InnerText;
                    string sex = divNode.Descendants("td").ElementAt(4).InnerText;
                    //
                    label3.Text += string.Format("{0} {1} {2} {3}\r\n", name, wcaid, country, sex);

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            test();
        }
    }
}
