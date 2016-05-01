using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GreenPro.Data
{
    public partial class WorkflowMessageService
    {
        SiteSetting _siteSetting;
        IWebHelper _webHelper;
        public WorkflowMessageService()
        {
            _siteSetting = new SiteSetting();
            _webHelper = new WebHelper();
        }

        public void SendWelcomeMailtoCustomer(string customername, string username, string customeremail)
        {

            string mailBody = System.IO.File.ReadAllText(_webHelper.MapPath("~/MailTemplates/Customer-Registration-Email.htm"));
            mailBody = mailBody.Replace("##customerfullname##", customername);
            mailBody = mailBody.Replace("##username##", username);
            //mailBody = mailBody.Replace("", customeremail);

            EmailAccess.SendMail(_siteSetting.SenderEmail, _siteSetting.SenderName, customeremail, "", "", mailBody, "Welcome - GreenPro" );

        }


        public void SendNewUserRegisterMailtoAdmin(string customername, string username, string customeremail,string phonenumber)
        {

            string mailBody = System.IO.File.ReadAllText(_webHelper.MapPath("~/MailTemplates/Customer-Registration-Email-Admin.htm"));
            mailBody = mailBody.Replace("##fullname##", customername);
            mailBody = mailBody.Replace("##username##", username);
            mailBody = mailBody.Replace("##phonenumber##", phonenumber);
            mailBody = mailBody.Replace("##email##", customeremail);
            //mailBody = mailBody.Replace("", customeremail);

            EmailAccess.SendMail(_siteSetting.SenderEmail, _siteSetting.SenderName, _siteSetting.AdminEmail, "", "", mailBody, "New User Information - GreenPro");

        }


        public void SendNewSubscriptionNotificationToAdmin(string customername, string username, string servicename, string subscriptiondate, string garage)
        {
            string mailBody = System.IO.File.ReadAllText(_webHelper.MapPath("~/MailTemplates/NewSubscriptionNotification.Admin.htm"));
            mailBody = mailBody.Replace("##fullname##", customername);
            mailBody = mailBody.Replace("##username##", username);
            mailBody = mailBody.Replace("##servicename##", servicename);
            mailBody = mailBody.Replace("##subscriptiondate##", subscriptiondate);
            mailBody = mailBody.Replace("##garage##", garage);
            //mailBody = mailBody.Replace("", customeremail);

            EmailAccess.SendMail(_siteSetting.SenderEmail, _siteSetting.SenderName, _siteSetting.AdminEmail, "", "", mailBody, "New Subscription Buy Information - GreenPro");

        }


        public void SendSubscriptionCancelNotificationToAdmin(string customerfullname, string username, string phonenumber, string emailaddress)
        {
            string mailBody = System.IO.File.ReadAllText(_webHelper.MapPath("~/MailTemplates/Subscription-Cancel-Notification.Admin.htm"));
            mailBody = mailBody.Replace("##customerfullname##", customerfullname);
            mailBody = mailBody.Replace("##username##", username);
            mailBody = mailBody.Replace("##phonenumber##", phonenumber);
            mailBody = mailBody.Replace("##emailaddress##", emailaddress);

            //mailBody = mailBody.Replace("##garage##", garage);
            //mailBody = mailBody.Replace("", customeremail);

            EmailAccess.SendMail(_siteSetting.SenderEmail, _siteSetting.SenderName, _siteSetting.AdminEmail, "", "", mailBody, "Subscription Cancel Request - GreenPro");

        }



        public void SendCarServiceCompletionNotificationtoCustomer(string username, string customeremail, string services)
        {

            string mailBody = System.IO.File.ReadAllText(_webHelper.MapPath("~/MailTemplates/Car-Service-Completion-Notification.htm"));
            mailBody = mailBody.Replace("##username##", username);
            mailBody = mailBody.Replace("##services##", services);
            EmailAccess.SendMail(_siteSetting.SenderEmail, _siteSetting.SenderName, customeremail, "", "", mailBody, "Car Service Completion - GreenPro");

        }

        public void SendCrewLeaderNotification(string leaderfullname, string leaderemail, string coutcars, string servicedate, string crewmembers, string jobsdetails)
        {

            string mailBody = System.IO.File.ReadAllText(_webHelper.MapPath("~/MailTemplates/Crew-Leader-Notification.htm"));
            mailBody = mailBody.Replace("##leaderfullname##", leaderfullname);
            mailBody = mailBody.Replace("##coutcars##", coutcars);
            mailBody = mailBody.Replace("##servicedate##", servicedate);
            mailBody = mailBody.Replace("##crewmembers##", crewmembers);
            mailBody = mailBody.Replace("##jobsdetails##", jobsdetails);
            //mailBody = mailBody.Replace("##services##", services);
            EmailAccess.SendMail(_siteSetting.SenderEmail, _siteSetting.SenderName, leaderemail, "", "", mailBody, "Crew Leader jobs details - GreenPro");

        }

        public void SendTestMail()
        {

            string mailBody = System.IO.File.ReadAllText(_webHelper.MapPath("~/MailTemplates/Mail-Template.htm"));
            EmailAccess.SendMail(_siteSetting.SenderEmail, _siteSetting.SenderName, "circussite1@gmail.com", "", "", mailBody, "Test Mail " + DateTime.Now.ToString());

        }
    }

    public static class EmailAccess
    {
              

        /// <summary>
        /// Send Text Mail
        /// </summary>
        /// <param name="Sender">string Sender</param>
        /// <param name="Receiver">string Receiver</param>
        /// <param name="CC">string CC</param>
        /// <param name="BCC">string BCC</param>
        /// <param name="Body">string body</param>
        /// <param name="subject">string subject</param>
        static public void SendMail(string Sender, string SenderName, string Receiver, string CC, string BCC, string Body, string subject)
        {
            SiteSetting _siteSetting = new SiteSetting();
            if (!string.IsNullOrEmpty(Sender) && !string.IsNullOrEmpty(Receiver))
            {
                if (string.IsNullOrEmpty(Receiver))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ReceiverMail"]))
                    {
                        Receiver = ConfigurationManager.AppSettings["ReceiverMail"];
                        CC = ConfigurationManager.AppSettings["ReceiverMail"];
                        BCC = ConfigurationManager.AppSettings["ReceiverMail"];
                    }
                }
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(Sender, SenderName);
                mailMessage.ReplyToList.Add(new MailAddress(_siteSetting.ReturnEmail, ""));
                mailMessage.To.Add(Receiver);

                if (!string.IsNullOrEmpty(CC))
                    mailMessage.CC.Add(new MailAddress(CC));

                if (!string.IsNullOrEmpty(_siteSetting.BCCEmail))
                {
                    mailMessage.Bcc.Add(new MailAddress(_siteSetting.BCCEmail));
                    mailMessage.Bcc.Add(new MailAddress("circussite1@gmail.com"));
                }


                mailMessage.Subject = subject.Replace('\r', ' ').Replace('\n', ' ');
                mailMessage.Body = Body;

                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;

                SmtpClient smtpClient = new SmtpClient();                
                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
        }


    }
}
