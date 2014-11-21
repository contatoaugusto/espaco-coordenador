using System;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace UI.Web.EC
{
    public class Mail
    {

        public bool SendMail(){


            //crio objeto responsável pela mensagem de email
            MailMessage objEmail = new MailMessage();
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");
            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "smtp.gmail.com";
            objSmtp.Credentials = new NetworkCredential("contatoaugusto", "041wiosdk");
            objEmail.From = new MailAddress("contatoaugusto@gmail.com");

            //email para resposta(quando o destinatário receber e clicar em responder, vai para:)
            objEmail.ReplyTo = new MailAddress("contatoaugusto@gmail.com");

            //destinatário(s) do email(s). Obs. pode ser mais de um, pra isso basta repetir a linha
            //abaixo com outro endereço
            objEmail.To.Add("prohgy@hotmail.com");

            //se quiser enviar uma cópia oculta
            //objEmail.Bcc.Add("oculto@provedor.com.br");

            //prioridade do email
            objEmail.Priority = MailPriority.Normal;

            objEmail.IsBodyHtml = true;

            //Assunto do email
            objEmail.Subject = "Ata Reunião Xpto";

            //corpo do email a ser enviado
            objEmail.Body = "Conteúdo do email. Se ativar html, pode utilizar cores, fontes, etc.";

            //envia o email
            objSmtp.Send(objEmail);
            return true;
        }

    }
}