using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SimpleAudioSystem;

public class Config : IConfig
{
    [Description("Se o plugin deve ser ativado ou não.")]
    public bool IsEnabled { get; set; } = true;

    [Description("Se deve ser exibido mensagens de debug do plugin no console.")]
    public bool Debug { get; set; } = false;
}