using CommandSystem;
using SimpleAudioSystem.SimpleAudioSystem.Audio;
using System;
using System.Text;

using IFormatProvider = SimpleAudioSystem.SimpleAudioSystem.Audio.IFormatProvider;

namespace SimpleAudioSystem.SimpleAudioSystem.RA.Commands.Parents;

public class AvailableFormatsCommandParent : ICommand
{
    public string Command => "availableformats";

    public string[] Aliases => [ "af", "formats", "extensions", "services" ];

    public string Description => "List all available formats.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        ReadOnlySpan<IFormatProvider> formatProviders = FormatProvider.AvailableServicesSpan;
        var sb = new StringBuilder();

        sb.AppendLine("Available formats:\n");  

        for (int i = 0; i < formatProviders.Length; i++)
        {
            sb.Append("- ")
              .Append(formatProviders[i].Name)
              .Append(" (")
              .Append(formatProviders[i].Extension)
              .AppendLine(")");
        }

        sb.Append("Support for ")
          .Append(formatProviders.Length)
          .Append(" format(s).");

        response = sb.ToString();
        return true;
    }
}