using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using SimpleAudioSystem.SimpleAudioSystem.Audio;
using SimpleAudioSystem.SimpleAudioSystem.RA.Handler;
using System;

namespace SimpleAudioSystem.SimpleAudioSystem.RA.Commands.Parents;

public class PlayCommandParent : ICommand
{
    public string Command => "play";

    public string[] Aliases => [ "p" ];

    public string Description => "Spawn speaker in player and play an audio from a specified file.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count < 2)
        {
            response = "You must provide arguments <player> <filePath>.\nEx: play me CoolAssSong.wav\nYou can use 'me' to refer to yourself. The file path can be autocompleted if in the Audios folder, or absolute. No URL Support.";
            return false;
        }

        if (!Round.IsStarted)
        {
            response = "That command can only be executed during a round. Please wait for the round to start or force start the round to use this command.";
            return false;
        }

        Player player = arguments.At(0) == "me" ? Player.Get(sender) : Player.Get(arguments.At(0));
        string filePath = arguments.At(1).Replace("\"", "");

        if (player is null)
        {
            response = "This command can only be executed by a player. Or the player could not be found. Did you provide a valid player name or ID?";
            return false;
        }

        if (!FileManager.FileExists(filePath))
        {
            response = $"The specified file ({filePath}) does not exist or could not be found.";
            return false;
        }

        if (!FormatProvider.IsAvailableService(filePath))
        {
            response = "No available format provider found for the specified file.";
            return false;
        }

        Speaker speaker = Speaker.Create(player.Transform, true);
        speaker.ControllerId = (byte)new Random().Next(byte.MinValue, byte.MaxValue);
        Test test = speaker.GameObject.AddComponent<Test>();
        test.Initialize(speaker, filePath);

        response = "Created audio service player successfully.";
        return true;
    }
}