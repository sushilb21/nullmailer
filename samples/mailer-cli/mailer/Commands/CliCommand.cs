﻿using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Mailer.Cli.Commands
{
    public static class CliCommandExtensions
    {
        public static void ConfigureCliCommand<T>(this CommandLineApplication app) where T : CliCommand
        {
            Program.ServiceProvider.GetService<T>().Configure(app);
        }
    }

    public abstract class CliCommand
    {
        protected CliCommand(AnsiConsole console)
        {
            Reporter = console;
        }

        protected AnsiConsole Reporter { get; set; }

        public abstract void Configure(CommandLineApplication app);
    }
}