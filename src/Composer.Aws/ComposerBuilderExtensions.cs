namespace Microsoft.Extensions.DependencyInjection;

using Composer;
using Composer.Aws;

public static class ComposerBuilderExtensions
{
    /// <summary>
    /// Use AWS SES as a sender for Composer.
    /// </summary>
    /// <param name="builder">
    /// A builder to register services to.
    /// </param>
    /// <returns>
    /// <paramref name="builder"/> to chain the call.
    /// </returns>
    public static ComposerBuilder AddAmazonSimpleEmailService(this ComposerBuilder builder)
    {
        builder.Services.AddSingleton<IEmailSender, SimpleEmailSender>();

        return builder;
    }
}
