using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAudioSystem.SimpleAudioSystem.Audio;

public static class ConstFormat
{
    public const int SAMPLE_RATE = 48000;
    public const int CHANNELS = 1;
    public const int FRAME_SIZE = 480;
    public const int PCM_BYTES = FRAME_SIZE * 2; // 16-bit = 2 bytes
    public const int MAX_PACKET_SIZE = 4000; // Max safe size for Opus packets
    public const int MAX_BUFFER_SIZE = 256; // Max size for sending to speaker (must be <= MAX_PACKET_SIZE)
}
