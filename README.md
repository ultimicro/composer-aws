# AWS supports for Composer

This is a library for [Composer](https://github.com/ultimicro/composer) to send email via Amazon SES.

## Usage

```csharp
services
    .AddComposer()
    .AddAmazonSimpleEmailService()
    .AddTemplateProvider<TemplateProviderImplementation>();
```

You also need to allow `ses:SendRawEmail` for IAM role that used by your application.

## License

MIT
