using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;

/// <summary>
/// Summary description for EmailContactsManager
/// </summary>
public class EmailContactsManager
{
	public EmailContactsManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private const string _addressBookUrl = "http://address.yahoo.com/yab/us/Yahoo_ab.csv?loc=us&.rand=1671497644&A=H&Yahoo_ab.csv";
    private const string _authUrl = "https://login.yahoo.com/config/login?";
    private const string _loginPage = "https://login.yahoo.com/config/login";
    private const string _userAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.3) Gecko/20070309 Firefox/2.0.0.3";
    private string username = string.Empty;
    private string password = string.Empty;

    public string Authenticate(string Username, string Password)
    {
        try
        {
            string url = "https://www.google.com/accounts/ClientLogin";
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "Email=" + Username;
            postData += ("&Passwd=" + Password);
            postData += ("&service=" + "cp");
            postData += ("&source=" + "exampleCo-exampleApp-1");
            postData += ("&accountType=" + "GOOGLE");

            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.Method = "POST";
            myRequest.ContentLength = data.Length;

            Stream myStream = myRequest.GetRequestStream();
            myStream.Write(data, 0, data.Length);
            myStream.Close();

            // Do the request to get the response
            StreamReader stIn = new StreamReader(myRequest.GetResponse().GetResponseStream());
            string result = stIn.ReadToEnd();
            stIn.Close();
            int AuthIndex = result.IndexOf("Auth=");
            string Auth = result.Substring(AuthIndex + 5, result.Length - AuthIndex - 5);
            return Auth;
        }
        catch
        {
            return "";
        }
    }
    public string GetGoogleResponse(string url, string Auth)
    {
        ASCIIEncoding encoding = new ASCIIEncoding();

        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
        myRequest.ContentType = "application/x-www-form-urlencoded";
        myRequest.Headers.Add("Authorization: GoogleLogin auth=" + Auth);
        myRequest.Method = "GET";

        StreamReader stIn = new StreamReader(myRequest.GetResponse().GetResponseStream());
        string result = stIn.ReadToEnd();
        stIn.Close();
        return result;
    }
    public string[] GetAllContacts(string GResponse, string UserEmail)
    {
        string AllContacts = "";
        XmlDocument accountinfoXML = new XmlDocument(); accountinfoXML.LoadXml(GResponse);
        XmlNodeList entries = accountinfoXML.GetElementsByTagName("entry");
        NameValueCollection profiles = new NameValueCollection();
        for (int i = 0; i < entries.Count; i++)
        {
            string CurEmail = (((XmlNode)entries[i])).LastChild.Attributes["address"].Value;
            if (CurEmail.ToUpper() != UserEmail.ToUpper())
            {
                if (AllContacts == "")
                    AllContacts = CurEmail;
                else
                    AllContacts += "," + CurEmail;
            }
        }

        return AllContacts.Split(',');

    }
    public static string GArequestResponseHelper(string url, string token, AuthModes mode)
    {

        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);

        //will always be a token of some sort required in the header but the format
        //it is passed in will depend on what type of authorization is being used
        if (mode == AuthModes.ClientLogin)
        {
            myRequest.Headers.Add("Authorization: GoogleLogin auth=" + token);
        }
        else if (mode == AuthModes.AuthSub)
        {
            myRequest.Headers.Add("Authorization: AuthSub token=" + token);
        }

        //obviously you need some kind of try/catch here
        //but OK to bubble auth/connection failures up for demo
        HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
        Stream responseBody = myResponse.GetResponseStream();

        Encoding encode = System.Text.Encoding.UTF8;
        StreamReader readStream = new StreamReader(responseBody, encode);

        //return string itself (easier to work with)
        return readStream.ReadToEnd();

    }
    public static string getSessionTokenAuthSub(string tempToken)
    {
        string response = GArequestResponseHelper("https://www.google.com/accounts/AuthSubSessionToken", tempToken, AuthModes.AuthSub);

        //temp (once off) token will have been exchanged for session token, return it
        return response.Split('=')[1];
    }
    public ArrayList Extract(string uname, string upass)
    {
        bool result = false;
        ArrayList myarray = new ArrayList();


        //list = New MailContactList()

        try
        {
            WebClient webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.UserAgent] = _userAgent;
            webClient.Encoding = Encoding.UTF8;

            byte[] firstResponse = webClient.DownloadData(_loginPage);
            string firstRes = Encoding.UTF8.GetString(firstResponse);


            NameValueCollection postToLogin = new NameValueCollection();
            Regex regex = new Regex("type=\"hidden\" name=\"(.*?)\" value=\"(.*?)\"", RegexOptions.IgnoreCase);
            Match match = regex.Match(firstRes);
            while (match.Success)
            {
                if (match.Groups[0].Value.Length > 0)
                {
                    postToLogin.Add(match.Groups[1].Value, match.Groups[2].Value);
                }
                match = regex.Match(firstRes, match.Index + match.Length);
            }


            postToLogin.Add(".save", "Sign In");
            postToLogin.Add(".persistent", "y");

            //Dim login As String = credential.UserName.Split("@"c)(0)
            string login = uname.Split('@').GetValue(0).ToString();

            postToLogin.Add("login", login);
            postToLogin.Add("passwd", upass);

            webClient.Headers[HttpRequestHeader.UserAgent] = _userAgent;
            webClient.Headers[HttpRequestHeader.Referer] = _loginPage;
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers[HttpRequestHeader.Cookie] = webClient.ResponseHeaders[HttpResponseHeader.SetCookie];

            webClient.UploadValues(_authUrl, postToLogin);
            string cookie = webClient.ResponseHeaders[HttpResponseHeader.SetCookie];

            //If String.IsNullOrEmpty(cookie) Then
            //Return False
            //End If

            string newCookie = string.Empty;
            string[] tmp1 = cookie.Split(',');
            foreach (string var in tmp1)
            {
                string[] tmp2 = var.Split(';');
                newCookie = (String.IsNullOrEmpty(newCookie) ? tmp2[0] : newCookie + ";" + tmp2[0]);
            }

            // set login cookie
            webClient.Headers[HttpRequestHeader.Cookie] = newCookie;
            byte[] thirdResponse = webClient.DownloadData(_addressBookUrl);
            string thirdRes = Encoding.UTF8.GetString(thirdResponse);

            string crumb = string.Empty;
            Regex regexCrumb = new Regex("type=\"hidden\" name=\"\\.crumb\" id=\"crumb1\" value=\"(.*?)\"", RegexOptions.IgnoreCase);
            match = regexCrumb.Match(thirdRes);
            if (match.Success && match.Groups[0].Value.Length > 0)
            {
                crumb = match.Groups[1].Value;
            }


            NameValueCollection postDataAB = new NameValueCollection();
            postDataAB.Add(".crumb", crumb);
            postDataAB.Add("vcp", "import_export");
            postDataAB.Add("submit[action_export_yahoo]", "Export Now");

            webClient.Headers[HttpRequestHeader.UserAgent] = _userAgent;
            webClient.Headers[HttpRequestHeader.Referer] = _addressBookUrl;

            byte[] FourResponse = webClient.UploadValues(_addressBookUrl, postDataAB);
            string csvData = Encoding.UTF8.GetString(FourResponse);

            string[] lines = csvData.Split('\n');
            //Dim list1 As Hashtable()

            foreach (string line in lines)
            {
                string[] items = line.Split(',');
                if (items.Length < 5)
                {
                    continue;
                }
                string email = items[4];
                string name = items[3];
                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(name))
                {
                    email = email.Trim('"');
                    name = name.Trim('"');
                    if (!email.Equals("Email") && !name.Equals("Nickname"))
                    {
                        MailContact mailContact = new MailContact();
                        mailContact.Name = name;
                        mailContact.Email = email;
                        myarray.Add(email);
                        //list.Add(mailContact)


                    }
                }
            }

            result = true;
        }
        catch
        {
        }
        return myarray;
    }

    public string[] GetYahooContacts(string Username, string Password)
    {
        string EmailList = "";
        ArrayList contacts = new ArrayList();
        contacts = Extract(Username, Password);
        for (int i = 0; i < contacts.Count; i++)
        {
            if (contacts[i].ToString().Trim() != "")
            {
                if (EmailList == "")
                    EmailList = contacts[i].ToString();
                else
                    EmailList += "," + contacts[i].ToString();
            }
        }
        return EmailList.Split(',');
    }
    public string[] GetGoogleContacts(string Email, string Password)
    {
        string Auth = Authenticate(Email, Password);
        if (Auth != "")
        {
            string strContactXML = GetGoogleResponse("http://www.google.com/m8/feeds/contacts/" + Email.Replace("@", "%40") + "/full", Auth);
            return GetAllContacts(strContactXML, Email);
        }
        else
            return null;

    }
    public enum AuthModes
    {
        ClientLogin,
        AuthSub
    }

}

public class MailContact
{
    private string _email = string.Empty;
    private string _name = string.Empty;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string FullEmail
    {
        get { return Email; }
    }
}
