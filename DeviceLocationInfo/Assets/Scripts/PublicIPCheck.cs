using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class PublicIPCheck : MonoBehaviour
{

    public Text languageText;
    private GameObject clone;
    // Start is called before the first frame update
    void Start()
    {
        string externalip = new WebClient().DownloadString("http://icanhazip.com");
        Debug.Log(externalip);
        //Debug.Log(GetInfo());
        Debug.Log(GetCountryByIP(externalip));
        Debug.Log(GetCountryByIP("134.201.250.155"));
        Debug.Log(GetCountryByIP("120.21.195.17"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //static public string GetCountry()
    //{
    //    return new WebClient().DownloadString("http://api.hostip.info/country.php");
    //}
    //static public string GetInfo()
    //{
    //    return new WebClient().DownloadString("http://api.hostip.info/get_json.php");
    //}

    public string GetCountryByIP(string ipAddress)
    {
        string strReturnVal;
        string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + ipAddress);

        //return ipResponse;
        XmlDocument ipInfoXML = new XmlDocument();
        ipInfoXML.LoadXml(ipResponse);
        XmlNodeList responseXML = ipInfoXML.GetElementsByTagName("query");
        
        NameValueCollection dataXML = new NameValueCollection();

        dataXML.Add(responseXML.Item(0).ChildNodes[2].InnerText, responseXML.Item(0).ChildNodes[2].Value);

        strReturnVal = responseXML.Item(0).ChildNodes[1].InnerText.ToString(); // Contry
        //strReturnVal += "(" +responseXML.Item(0).ChildNodes[2].InnerText.ToString() + ")";  // Contry Code 
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[3].InnerText.ToString() + ")";  // State Code
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[4].InnerText.ToString() + ")";  // State Name 
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[5].InnerText.ToString() + ")";  // City Name
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[6].InnerText.ToString() + ")";  // Zip/postal Code 
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[7].InnerText.ToString() + ")";  // Lat 
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[8].InnerText.ToString() + ")";  // Long 
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[9].InnerText.ToString() + ")";  // Timezone 
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[10].InnerText.ToString() + ")";  // isp 
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[11].InnerText.ToString() + ")";  // Org
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[12].InnerText.ToString() + ")";  // AS number and Organization
        //strReturnVal += "(" + responseXML.Item(0).ChildNodes[13].InnerText.ToString() + ")";  // Requested IP
        return strReturnVal;
    }

    public string IPRequestHelper(string url)
    {

        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

        StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
        string responseRead = responseStream.ReadToEnd();
        Debug.Log(objResponse);
        responseStream.Close();
        responseStream.Dispose();

        return responseRead;
    }

    public void ShowSupportedLanguage()
    {
        string externalip = new WebClient().DownloadString("http://icanhazip.com");
        string countryName = GetCountryByIP(externalip);

        switch (countryName)
        {
            case "Australia":
                //for (int i = 0; i < 2; i++)
                //{
                    //Instantiate(languageTextPrefab, new Vector3(0, i * 50, 0), Quaternion.identity);
                    languageText.text += " \n English and Chinese.";
                //}
                Debug.Log("English, Chinese");
                break;
            case "United States":
                Debug.Log("English Only");
                break;
        }
    }
}

