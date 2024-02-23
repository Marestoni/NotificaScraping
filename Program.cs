using System.Net.Mail;
using HtmlAgilityPack;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "Site que deseja monitorar";
        string emailRecipient = "email destinatário";

        while (true)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(url);

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    
                    if (doc.DocumentNode.SelectSingleNode("ID E TIPO DE TAG QUE SERÁ VERIFICADA") != null)
                    {
                        SendEmail(emailRecipient, "ASSUNTO DO EMAIL", "CORPO DO EMAIL");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromHours(1));
        }
    }

    static void SendEmail(string to, string subject, string body)
    {
        using (SmtpClient smtpClient = new SmtpClient("SMTP DO CLIENTE"))
        {
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("EMAIL DE ACESSO", "SENHA DE ACESSO");
            smtpClient.EnableSsl = true;

            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("EMAIL QUE IRÁ ENVIAR");
                mailMessage.To.Add(to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                smtpClient.Send(mailMessage);
            }
        }
    }
    
}
