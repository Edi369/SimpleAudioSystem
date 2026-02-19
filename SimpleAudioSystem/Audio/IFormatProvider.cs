using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioSystem.SimpleAudioSystem.Audio;

public interface IFormatProvider
{
    string Name { get; }
    string Extension { get; }

    
    bool IsValidFormat(string filePath);
}
