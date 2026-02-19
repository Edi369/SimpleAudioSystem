using CommandSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAudioSystem.SimpleAudioSystem.RA.Commands.Parents;

public class HelpCommandParent : ICommand
{
    public string Command => "help";

    public string[] Aliases => ["help", "?"];
    
    public string Description => "Display the help message.";

    private readonly ICommand[] _commands;

    public HelpCommandParent(IEnumerable<ICommand> commands)
    {
        _commands = commands.ToArray();
    }

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Available {_commands.Length} commands:");

        for (int i = 0; i < _commands.Length; i++)
        {
            sb.Append(i + 1)
              .Append(". ")
              .Append(_commands[i].Command);

            if (_commands[i].Aliases.Length > 0)
            {
                sb.Append(" (");
                for (int j = 0; j < _commands[i].Aliases.Length; j++)
                {
                    sb.Append("<i>")
                      .Append(_commands[i].Aliases[j])
                      .Append("</i>");
                    if (j < _commands[i].Aliases.Length - 1)
                        sb.Append(", ");
                }
                sb.Append(")");
            }

            sb.Append(" - ")
              .AppendLine(_commands[i].Description);
        }

        response = sb.ToString();
        return true;
    }
}