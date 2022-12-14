using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DarulAman.Utills
{
    public class Email
    {
        public static void SendEmail(string receiverEmail, int code)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ConfigurationManager.AppSettings["Email"]);
                message.To.Add(new MailAddress(receiverEmail));
                message.Subject = "Password Verification Code";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "Your Verification code for Forgot Password is: \t" + code;
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