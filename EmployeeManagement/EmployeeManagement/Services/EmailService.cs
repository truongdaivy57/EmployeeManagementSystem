using System.Net.Mail;
using System.Net;
using EmployeeManagement.Dtos;

namespace EmployeeManagement.Services
{
    public interface IEmailService
    {
        void SendMail(SendMailDto dto);
    }

    public class EmailService : IEmailService
    {

        public EmailService()
        {

        }

        public void SendMail(SendMailDto dto)
        {
            try
            {
                MailMessage mailMessage = new MailMessage()
                {
                    Subject = "",
                    Body = dto.Content,
                    IsBodyHtml = false,
                };
                mailMessage.From = new MailAddress(EmailSettingDto.Instance.FromEmailAddress, EmailSettingDto.Instance.FromDisplayName);
                mailMessage.To.Add(dto.ReceiveAddress);

                var smtp = new SmtpClient()
                {
                    EnableSsl = EmailSettingDto.Instance.Smtp.EnableSsl,
                    Host = EmailSettingDto.Instance.Smtp.Host,
                    Port = EmailSettingDto.Instance.Smtp.Port,
                };
                var network = new NetworkCredential(EmailSettingDto.Instance.Smtp.EmailAddress, EmailSettingDto.Instance.Smtp.Password);
                smtp.Credentials = network;

                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
