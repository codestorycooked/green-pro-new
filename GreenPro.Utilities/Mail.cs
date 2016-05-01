using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mandrill;
using System.Net.Mail;
using System.Configuration;
namespace GreenPro.Utilities
{
    public class Mail
    {
        public void SendMail(string toUser, string from, string message, string subject, string toAdmin = "kunal@innoator.com")
        {
            List<EmailAddress> toMailList=new List<EmailAddress>();
            toMailList.Add(new EmailAddress(toUser));
            toMailList.Add(new EmailAddress(toAdmin));
            IEnumerable<EmailAddress> mailAddress = toMailList.ToList();
            
            MandrillApi md = new MandrillApi(GreenPro.Utilities.Properties.Settings.Default.MandrillKey);
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.subject = subject;
            emailMessage.from_email = from;
            emailMessage.to = mailAddress;
            emailMessage.html=message;            
            md.SendMessage(emailMessage);
        }

        public void SendRegistrationMail(string toUser, string from, string message, string subject, string toAdmin = "kunal@innoator.com")
        { 

        }
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
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;

                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }



    }
}
