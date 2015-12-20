using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace GetUrl
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string m_WebSite = "http://cubingchina.com/";
        string m_WebSearch = "results/person?region=World&gender=all&name={0}";
        string m_Html = "";
        public static string GetUrlFromHtml(string Url, string type)
        {
            try
            {
                WebRequest wReq = WebRequest.Create(Url);
                WebResponse wResp = wReq.GetResponse();
                Stream respStream = wResp.GetResponseStream();
                using (StreamReader reader = new StreamReader(respStream, Encoding.GetEncoding(type)))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";
        }
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
                    //return reader.ReadToEnd();
                    byteRead = reader.Read(cbuffer, 0, 256);
                    while (byteRead != 0)
                    {
                        string strResp = new string(cbuffer, 0, byteRead);
                        strBuff = strBuff + strResp;
                        if (strBuff.Length > 17921)
                        {
                            break;
                        }
                        byteRead = reader.Read(cbuffer, 0, 256);
                    }
                    respStream.Close();
                    reader.Close();
                    return strBuff;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            m_Html = GetUrlFromHtml(string.Format(m_WebSite + m_WebSearch, txtUrl.Text.Trim()), "UTF-8");
            List<WCAPerson> lPersons = new List<WCAPerson>();
            HtmlAgilityPack.HtmlDocument Hdoc = new HtmlAgilityPack.HtmlDocument();
            Hdoc.LoadHtml(m_Html);
            foreach (HtmlNode divNode in Hdoc.DocumentNode.Descendants("tr"))
            {
                if (divNode.GetAttributeValue("class", null) == "odd" || divNode.GetAttributeValue("class", null) == "even")
                {
                    WCAPerson wcaPerson = new WCAPerson();
                    wcaPerson.Name = divNode.Descendants("a").First().InnerText;
                    wcaPerson.WcaId = divNode.Descendants("td").ElementAt(2).InnerText;
                    wcaPerson.Url = divNode.Descendants("a").First().GetAttributeValue("href", null);
                    wcaPerson.Country = divNode.Descendants("td").ElementAt(3).InnerText;
                    wcaPerson.Sex = divNode.Descendants("td").ElementAt(4).InnerText;
                    lPersons.Add(wcaPerson);
                }
            }
            if (lPersons.Count == 0)
            {
                MessageBox.Show("怎么找还是找不到呀~");
            }
            else if (lPersons.Count == 1)
            {
                m_Html = GetUrltoHtml(m_WebSite + lPersons[0].Url.Substring(1), "UTF-8");
                Hdoc.LoadHtml(m_Html);
                string sResult = string.Format("{0} {1} {2} {3}  ", lPersons[0].Name, lPersons[0].WcaId, lPersons[0].Country, lPersons[0].Sex);
                Dictionary<string, string> items = new Dictionary<string, string>();
                HtmlNode divTopNode = Hdoc.DocumentNode.SelectSingleNode("//div[@id='yw0']");
                for (int i = 0; i < divTopNode.Descendants("a").Count(); i++)
                {
                    if (divTopNode.Descendants("a").ElementAt(i).InnerHtml.Contains("<span"))
                    {
                        sResult = sResult.Substring(0, sResult.Length - 1);
                        sResult += "\r\n" + divTopNode.Descendants("a").ElementAt(i).InnerText + ":";
                    }
                    else
                    {
                        sResult += divTopNode.Descendants("a").ElementAt(i).InnerText + "|";
                    }
                }
                sResult = sResult.Substring(0, sResult.Length - 1);
                label1.Text = sResult;
            }
            else if (lPersons.Count > 1)
            {
                //foreach (WCAPerson person in lPersons)
                //{
                //    label1.Text += string.Format("{0} {1} {2} {3}\r\n", person.Name, person.WcaId, person.Country, person.Sex);
                //}
                for (int i = 0; i < lPersons.Count; i++)
                {
                    label1.Text += string.Format("{0} {1} {2} {3}\r\n", lPersons[i].Name, lPersons[i].WcaId, lPersons[i].Country, lPersons[i].Sex);
                    if (i>10)
                    {
                        label1.Text += string.Format("等{0}个人", lPersons.Count);
                        break;
                    }
                }
            }
        }

    }
}
