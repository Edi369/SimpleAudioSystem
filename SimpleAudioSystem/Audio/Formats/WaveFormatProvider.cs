using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioSystem.SimpleAudioSystem.Audio.Formats;

public class WaveFormatProvider : IFormatProvider
{
    public string Name => "Wave";

    public string Extension => ".wav";

    public bool IsValidFormat(string filePath) => filePath.EndsWith(Extension, StringComparison.OrdinalIgnoreCase);
}
