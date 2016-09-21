using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuGet.MSBuild.Tools
{
    internal static class CommandLineBuilderExtensions
    {
        public static void AppendSwitchIfTrue(this CommandLineBuilder builder, string switchName, bool value)
        {
            if (value)
            {
                builder.AppendSwitch(switchName);
            }
        }

        public static void AppendTextUnquotedIfNotNullOrEmpty(this CommandLineBuilder builder, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                builder.AppendTextUnquoted(text);
            }
        }
    }
}
