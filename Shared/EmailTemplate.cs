namespace DemoAppBE.Shared
{
    public static class EmailTemplate
    {
        public static string EmailFormat(string Link)
        {
            string htmlBody = $@"
<!doctype html>
<html>
<head>
  <meta charset=""utf-8""/>
  <meta name=""viewport"" content=""width=device-width,initial-scale=1""/>
</head>
<body style=""margin:0;padding:0;font-family:Arial,Helvetica,sans-serif;background:#f5f7fa;color:#222;"">
  <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f5f7fa;padding:24px 0;"">
    <tr>
      <td align=""center"">
        <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background:#ffffff;border-radius:8px;box-shadow:0 1px 3px rgba(0,0,0,0.08);overflow:hidden;"">
          
          <!-- Header -->
          <tr>
            <td style=""padding:28px 32px;text-align:center;border-bottom:1px solid #eee;"">
              <h1 style=""margin:0;font-size:22px;color:#111;"">A file was shared with you</h1>
            </td>
          </tr>

          <!-- Body -->
          <tr>
            <td style=""padding:22px 32px;"">
              <p style=""margin:0 0 12px;font-size:15px;line-height:1.45;color:#333;"">
                <strong></strong>
              </p>

              <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""margin:8px 0 18px;border:1px solid #eef2f5;border-radius:6px;"">
                <tr>
                  <td style=""padding:12px 14px;font-size:14px;color:#444;"">
                    <div style=""margin-bottom:6px;""><strong>File:</strong> FILE</div>
                    <div style=""margin-bottom:0;""><strong>Expires:</strong> 10 minutes</div>
                  </td>
                </tr>
              </table>

              <p style=""margin:0 0 18px;font-size:15px;color:#333;"">
                Click the button below to open the file. If the button doesn't work, copy and paste the link that appears under the button into your browser.
              </p>

              <div style=""text-align:center;margin:18px 0;"">
                <a href=""{Link}"" target=""_blank"" style=""display:inline-block;padding:12px 20px;border-radius:8px;background:#2563eb;color:#ffffff;text-decoration:none;font-weight:600;font-size:15px;"">
                  Open Shared File
                </a>
              </div>

              <p style=""word-break:break-all;font-size:13px;color:#666;margin:8px 0 0;padding:8px 12px;border-radius:6px;background:#f7f9fc;border:1px solid #eef4ff;"">
                {Link}
              </p>

              <hr style=""border:none;border-top:1px solid #eee;margin:20px 0;"" />

              <p style=""font-size:13px;color:#666;margin:0;"">
                If you were not expecting this file or you have concerns, please contact the sender or ignore this email.
              </p>
            </td>
          </tr>

          <!-- Footer -->
          <tr>
            <td style=""padding:14px 32px;background:#fafafa;font-size:12px;color:#999;text-align:center;"">
              <div>Sent by Your App • &copy; {DateTime.UtcNow.Year} Your Company</div>
            </td>
          </tr>

        </table>
      </td>
    </tr>
  </table>
</body>
</html>
";
            return htmlBody;
        }
    }
}
