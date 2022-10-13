using EmailApplication.config;
using EmailApplication.Utils;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailApplication.Implementation
{
    class MailSender
    {
        private readonly Config _config;
        private readonly FileManager _fileManager;

        public MailSender()
        {
            _config = new Config();
            _fileManager = new FileManager(_config.GetHtmlReportPath());
        }

        public void SendTextEmail(string emailRecipient, string emailSubject, string emailBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config.GetFrom(), _config.GetEmailSender()));
            message.To.Add(new MailboxAddress("No reply", emailRecipient));
            message.Subject = emailSubject;

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Alice,

                What are you up to this weekend? Monica is throwing one of her parties on
                Saturday and I was hoping you could make it.

                Will you be my +1?

                -- Joey
                "
            };

            SendEmailMessage(message);
        }

        public void SendHtmlEmail(string emailRecipient, string emailSubject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config.GetFrom(), _config.GetEmailSender()));
            message.To.Add(new MailboxAddress("No reply", emailRecipient));
            message.Subject = emailSubject;

            BodyBuilder emailBody = new BodyBuilder();
            string bodyContent = string.Empty;

            using (StreamReader reader = new StreamReader(_fileManager.GetLastHtmlReportFile()))
            {
                bodyContent = reader.ReadToEnd();
            }

            emailBody.HtmlBody = bodyContent;
            message.Body = emailBody.ToMessageBody();

            SendEmailMessage(message);
        }

        private void SendEmailMessage(MimeMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(_config.GetSmtpClient(), _config.GetEmailPort(), SecureSocketOptions.StartTls);
                smtpClient.Authenticate(_config.GetEmailSender(), _config.GetEmailPassword());
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }
        }
    }
}
