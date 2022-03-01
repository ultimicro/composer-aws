namespace Composer.Aws;

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using MimeKit;

/// <summary>
/// <see cref="IEmailSender"/> implementation to send email over Amazon Simple Email Service.
/// </summary>
/// <remarks>
/// This class use AWS API directly, not SMTP.
/// </remarks>
public sealed class SimpleEmailSender : IDisposable, IEmailSender
{
    private readonly AmazonSimpleEmailServiceClient client;

    public SimpleEmailSender()
    {
        this.client = new();
    }

    public void Dispose()
    {
        this.client.Dispose();
    }

    public async Task SendAsync(MimeMessage mail, CancellationToken cancellationToken = default)
    {
        // Serialize mail.
        await using var raw = new MemoryStream();

        await mail.WriteToAsync(raw, cancellationToken);
        raw.Seek(0, SeekOrigin.Begin);

        // Set up API request.
        var request = new SendRawEmailRequest()
        {
            Destinations = mail.To.Concat(mail.Cc.Concat(mail.Bcc)).Select(a => a.ToString(true)).ToList(),
            RawMessage = new RawMessage(raw),
        };

        // Send the request.
        await this.client.SendRawEmailAsync(request, cancellationToken);
    }
}
