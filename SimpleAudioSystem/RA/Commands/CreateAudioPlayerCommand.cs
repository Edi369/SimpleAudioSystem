using CommandSystem;
using System;
using SimpleAudioSystem.SimpleAudioSystem.RA.Commands.Parents;

namespace SimpleAudioSystem.SimpleAudioSystem.RA.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class CreateAudioPlayerCommand : ParentCommand
{
    public override string Command => "SecretAudioSystem";

    public override string[] Aliases => [ "sas", "aum", "am" ];

    public override string Description => string.Empty;

    public CreateAudioPlayerCommand() 
    {
        LoadGeneratedCommands();
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        // Execute only if no parent command is executed
        response = "Unknown command. You can use the <b>help</b> command to see all available commands.";
        return false;
    }

    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new PlayCommandParent());
        RegisterCommand(new AvailableFormatsCommandParent());
        RegisterCommand(new HelpCommandParent(AllCommands));
    }
}
