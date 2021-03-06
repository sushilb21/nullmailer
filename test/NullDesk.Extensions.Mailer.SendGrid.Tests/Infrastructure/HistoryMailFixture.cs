﻿using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Options;
using NullDesk.Extensions.Mailer.Core;

namespace NullDesk.Extensions.Mailer.SendGrid.Tests.Infrastructure
{
    public class HistoryMailFixture : IDisposable
    {
        public HistoryMailFixture()
        {
            var loggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() }, new LoggerFilterOptions() { MinLevel = LogLevel.Debug });


            var sendGridSettings = new SendGridMailerSettings
            {
                ApiKey = "abc",
                FromDisplayName = "xunit",
                FromEmailAddress = "xunit@nowhere.com"
            };

            var logger = loggerFactory.CreateLogger<SendGridMailer>();

            MailerFactoryForHistoryWithSerializableAttachments.Register(() => new SendGridMailerFake(
                new OptionsWrapper<SendGridMailerSettings>(sendGridSettings),
                logger,
                StoreWithSerializableAttachments));

            MailerFactoryForHistoryWithoutSerializableAttachments.Register(() => new SendGridMailerFake(
                new OptionsWrapper<SendGridMailerSettings>(sendGridSettings),
                logger,
                StoreWithoutSerializableAttachments));
        }

        public MailerFactory MailerFactoryForHistoryWithSerializableAttachments { get; } = new MailerFactory();

        public MailerFactory MailerFactoryForHistoryWithoutSerializableAttachments { get; } = new MailerFactory();


        public IHistoryStore StoreWithSerializableAttachments { get; set; } =
            new InMemoryHistoryStore(new StandardHistoryStoreSettings {StoreAttachmentContents = true});

        public IHistoryStore StoreWithoutSerializableAttachments { get; set; } =
            new InMemoryHistoryStore(new StandardHistoryStoreSettings {StoreAttachmentContents = false});


        public void Dispose()
        {
        }
    }
}