using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;


namespace SimulatorApiAppDFCUBank
{
    public partial class frmRequestSimulator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) return;
            cmdMakeRequest.Enabled = false;
        }

        /* Global collecton file */
        List<string> logMsgs = new List<string>();

        protected void callResorurce_Click(Object sender, EventArgs e) {

            if (txtResponse.Text == string.Empty) return;
            int wfcontroller = 0;

            string[] accounts = txtResponse.Text.Split(',');

            for (int i = 0; i < accounts.Length; i++)
            { 
                requestAccountStatus(accounts[i],wfcontroller);
                wfcontroller++;
            }

            string fileSink = Server.MapPath("~/Simulator/apicallResults.txt");
            FileStream fstr = new FileStream(fileSink, FileMode.Append, FileAccess.Write);
            StreamWriter stmWtr = new StreamWriter(fstr);

            foreach (string item in logMsgs) {
                stmWtr.WriteLine(item);
            }

            stmWtr.Close();
            cmdMakeRequest.Enabled = false;

            Response.Write("<script type='text/javascript'> alert('Api-Log saved to the path: /Simulator/apicallResults.txt on the server.')</script>");

        }


        public void requestAccountStatus(string accountNo,int commitFlag){ /* start mehod request accountStatus */  
            try{
                
                        string accessToken = getAuthTok();
                        HttpWebRequest statusReqObj = WebRequest.CreateHttp("http://localhost:1900/api/customer/loans/status/" + accountNo);            
                        statusReqObj.Method = "GET";
                        statusReqObj.Accept = "application/json";
                        statusReqObj.Headers.Add("x-auth-token", accessToken);

                        WebResponse statusResponseObj = statusReqObj.GetResponse();
                        Stream responseStream = statusResponseObj.GetResponseStream();

                        /* if a positive result is returned */
                        if (responseStream != null)
                        {
                            StreamReader stmRdr = new StreamReader(responseStream);
                            string accountStatusJson = null;
                            accountStatusJson = stmRdr.ReadToEnd();

                            txtjwt.Text = "Requesting AccountNo >> " + accountNo + " Request Date: " + DateTime.Now + " :api-response-msg: " + accountStatusJson;
                            logMsgs.Add("Requesting AccountNo >> " + accountNo + " Request Date: " + DateTime.Now + " :api-response-msg: " + accountStatusJson);

                            stmRdr.Close();
                            responseStream.Close();
                        }            
                    
                        statusResponseObj.Close();

                }catch(Exception ex){
                        logMsgs.Add(ex.Message + "....Failure: Attempt to retrieve loan-status for a non-existant Account : " + accountNo + ". <br/>");
                        //Response.Write(ex.Message);
                }

                

        } /* end mehod request accountStatus */


        public string getAuthTok(){
            string token = null;
            try
            {
                HttpWebRequest req = WebRequest.CreateHttp("http://localhost:1900/api/auth/login");

                req.Method = "POST";
                req.ContentType = "application/json";
                req.Accept = "application/json";
                req.Accept = "text/plain";

                Apiuser nodeApiuser = new Apiuser();
                nodeApiuser.email = ConfigurationManager.AppSettings["dfcuapiusername"];
                nodeApiuser.password = ConfigurationManager.AppSettings["dfcuapiuserkey"];

                JavaScriptSerializer jSerializer = new JavaScriptSerializer();
                string reqJsonStr = jSerializer.Serialize(nodeApiuser);

                byte[] reqpayload = Encoding.ASCII.GetBytes(reqJsonStr.ToCharArray());
                Stream reqStrm = req.GetRequestStream();
                reqStrm.Write(reqpayload, 0, reqpayload.Length);
                reqStrm.Close();

                WebResponse respobj = req.GetResponse();
                Stream resStrm = respobj.GetResponseStream();
                StreamReader rdr = new StreamReader(resStrm);

                if (resStrm != null)
                {
                    token = rdr.ReadToEnd();
                    rdr.Close();
                    resStrm.Close();

                    return token;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            return token;

        }

        protected void LoadRandomList_Click(object sender, EventArgs e)
        {
            // Generic list of accounts
            List<string> customerAccounts = new List<string>();
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                customerAccounts.Add(rnd.Next(1, 50).ToString("0000000000"));
            }

            string createdAccounts = null;
            foreach (string item in customerAccounts)
            {
                createdAccounts = createdAccounts + "," + item;
            }

            /* push the list to the UI */
            txtResponse.Text = createdAccounts.Substring(1);
            cmdMakeRequest.Enabled = true;

        }//end method


    }// end class
} // end namespace