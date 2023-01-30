using EmailApplication.Implementation;
using System;

namespace EmailApplication
{
    class Program
    {
        static void Main(string[] arguments)
        {
            MailSender mailSender = new MailSender();
            string emailRecipient = String.Empty;
            string emailSubject = String.Empty;


            for (int index = 0; index < arguments.Length; index++)
            {
                string argument = arguments[index];

                switch (argument)
                {
                    case "--email":
                        emailRecipient = arguments[index + 1];
                        break;

                    case "--subject":
                        emailSubject = arguments[index + 1];
                        break;

                    default:
                        break;
                }
            }

            if (emailRecipient.Length > 0)
            {
                Console.WriteLine("Sending report email...");
                mailSender.SendHtmlReportEmail(emailRecipient, emailSubject);
                Console.WriteLine("Email sent successfully!");
            }
            else
            {
                Console.WriteLine("You need to specify the \"--email\" and  \"--subject\" arguments to send an email");
            }
        }
    }
}
