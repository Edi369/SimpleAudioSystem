using Exiled.API.Features;
using System;

namespace SimpleAudioSystem;

public class SimpleAudioSystemHandler : Plugin<Config, Translation>
{
    public override string Name => "SimpleAudioSystem";
    public override string Author => "Edi";
    public override string Prefix => "SimpleAudioSystem";
    public override Version Version => new(1, 0, 0);
    public override Version RequiredExiledVersion => new(9, 13, 1);

    public static SimpleAudioSystemHandler Instance;

    public override void OnEnabled()
    {
        Instance = this;

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        base.OnDisabled();
    }
}