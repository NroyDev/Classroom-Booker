using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS_LAB2
{
    public partial class MailWeb : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SendOverDueMail();
        }


        protected void SendOverDueMail()
        {
            // SMTP 設定
            string smtpAddress = "smtp.gmail.com"; // 替換為你的 SMTP Server
            int portNumber = 587; // TLS 使用的埠號
            bool enableSSL = true;

            string emailFrom = "your-mailaddress@gmail.com"; // 發件者
            string password = ""; // Gmail 應用程式密碼


            string connecitonString = System.Configuration.ConfigurationManager.ConnectionStrings["AccessDB"].ConnectionString;
            string query = @"
                SELECT user.email,cname,bw.berid,dept.email
                FROM ((bw INNER JOIN classroom ON bw.cid=classroom.cid ) INNER JOIN [user] ON bw.berid = [user].id) INNER JOIN dept ON classroom.dept = dept.dept_id
                WHERE rdate is null AND mailed = false
                    AND (bdate < Date() OR (bdate = Date() AND Hour([endtime]) <= Hour(Now()) ))
            ;";

            string query2 = @"
                SELECT user.email,dname,bw.berid,dept.email
                FROM ((bw INNER JOIN device ON bw.did=device.did ) INNER JOIN [user] ON bw.berid = [user].id) INNER JOIN dept ON device.dept = dept.dept_id
                WHERE rdate is null and mailed = false
                    AND (bdate < Date() OR (bdate = Date() AND Hour([endtime]) <= Hour(Now()) ))
            ;";

            string update = "UPDATE bw SET mailed = true WHERE (bdate < Date() OR (bdate = Date() AND Hour([endtime]) <= Hour(Now()) ))";
            using (OleDbConnection conneciton = new OleDbConnection(connecitonString))
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(query, conneciton);
                    conneciton.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(emailFrom);
                            mail.To.Add(reader.GetString(0));
                            mail.Subject = "[測試訊息] 教室借用逾期通知";
                            mail.Body = $"親愛的用戶 {reader.GetString(2)} 您好" +
                                $"你有一筆逾期的教室租用申請 - {reader.GetString(1)}，請盡速前往系辦處理";
                            mail.IsBodyHtml = false; // 如果是 HTML 格式，改為 true

                            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                            {
                                smtp.Credentials = new NetworkCredential(emailFrom, password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                            }
                        }


                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(emailFrom);
                            mail.To.Add(reader.GetString(3));
                            mail.Subject = "[測試訊息] 教室借用逾期通知";
                            mail.Body = $"用戶 {reader.GetString(2)}" +
                                $"有一筆逾期的教室租用申請 - {reader.GetString(1)}，請盡速處理";
                            mail.IsBodyHtml = false; // 如果是 HTML 格式，改為 true

                            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                            {
                                smtp.Credentials = new NetworkCredential(emailFrom, password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                            }
                        }
                    }
                    reader.Close();

                    command = new OleDbCommand(query2, conneciton);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(emailFrom);
                            mail.To.Add(reader.GetString(0));
                            mail.Subject = "[測試訊息] 設備借用逾期通知";
                            mail.Body = $"親愛的用戶 {reader.GetString(2)} 您好" +
                                $"你有一筆逾期的設備租用申請 - {reader.GetString(1)}，請盡速前往系辦處理";
                            mail.IsBodyHtml = false; // 如果是 HTML 格式，改為 true

                            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                            {
                                smtp.Credentials = new NetworkCredential(emailFrom, password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                            }
                        }


                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(emailFrom);
                            mail.To.Add(reader.GetString(3));
                            mail.Subject = "[測試訊息] 設備借用逾期通知";
                            mail.Body = $"用戶 {reader.GetString(2)}" +
                                $"有一筆逾期的設備租用申請 - {reader.GetString(1)}，請盡速處理";
                            mail.IsBodyHtml = false; // 如果是 HTML 格式，改為 true

                            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                            {
                                smtp.Credentials = new NetworkCredential(emailFrom, password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                            }
                        }
                    }

                    command = new OleDbCommand(update, conneciton);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
    }
}