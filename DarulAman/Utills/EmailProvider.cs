using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DarulAman.Utills
{
    public static class EmailProvider
    {
        public static void Email(string receiverEmail, string emailSubject, string mailBody)//BODY OF EMAIL WITH EMAIL
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ConfigurationManager.AppSettings["email"]);
                message.To.Add(new MailAddress(receiverEmail));
                MailAddress CarbonCopy = new MailAddress("waqassiddique9@gmail.com");
                message.CC.Add(CarbonCopy);
                message.Subject = emailSubject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = mailBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["Password"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
    }
}