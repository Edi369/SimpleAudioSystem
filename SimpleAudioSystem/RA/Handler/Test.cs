using Concentus.Enums;
using Concentus.Structs;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using NAudio.Wave;
using UnityEngine;
using System;
using System.Buffers;
using SimpleAudioSystem.SimpleAudioSystem.Audio;

namespace SimpleAudioSystem.SimpleAudioSystem.RA.Handler;

public class Test : MonoBehaviour
{
    public const double MS_PER_UPDATE = 0.01; // Update every 10ms

    private Speaker _speaker;
    private string _name;

    private WaveFileReader _waveFileReader;
    private WaveFormat _waveFormat;
    private OpusEncoder _encoder;

    private byte[] _pcmByteBuffer;
    private short[] _pcmShortBuffer;
    private byte[] _opusBuffer;
    private byte[] _finalPacket;

    private double _timeSinceLastUpdate;

    public bool IsPlaying { get; private set; }

    public void Initialize(Speaker speaker, string name)
    {
        _speaker = speaker;
        _name = name;
    }

    public void Awake()
    {
        Log.Debug("Test AW");
    }

    public void Start()
    {
        Log.Debug("Test");

        _waveFileReader = new WaveFileReader(_name);
        _waveFormat = _waveFileReader.WaveFormat;

        _encoder = new(ConstFormat.SAMPLE_RATE, ConstFormat.CHANNELS, OpusApplication.OPUS_APPLICATION_AUDIO);

        _pcmByteBuffer = new byte[ConstFormat.PCM_BYTES];
        _pcmShortBuffer = new short[ConstFormat.FRAME_SIZE];
        _opusBuffer = new byte[ConstFormat.MAX_PACKET_SIZE];
        _finalPacket = new byte[ConstFormat.MAX_BUFFER_SIZE];

        IsPlaying = true;

        Log.Debug($"Format: {_waveFormat.Encoding}");
        Log.Debug($"Bits: {_waveFormat.BitsPerSample}");
        Log.Debug($"Channels: {_waveFormat.Channels}");
        Log.Debug($"SampleRate: {_waveFormat.SampleRate}");

        Log.Debug("Started playing audio.");
    }

    public void Update()
    {
        if (!IsPlaying)
            return;

        _timeSinceLastUpdate += Time.deltaTime;

        while (_timeSinceLastUpdate >= MS_PER_UPDATE)
        {
            _timeSinceLastUpdate -= MS_PER_UPDATE;
            SendFrame();
        }
    }

    public void SendFrame()
    {
        int bytesRead = _waveFileReader.Read(_pcmByteBuffer, 0, ConstFormat.PCM_BYTES);

        if (bytesRead < ConstFormat.PCM_BYTES)
        {
            IsPlaying = false;
            Log.Debug("Finished playing audio.");
            return;
        }

        Buffer.BlockCopy(_pcmByteBuffer, 0, _pcmShortBuffer, 0, bytesRead);
        int encodedLength = _encoder.Encode(_pcmShortBuffer.AsSpan(0), ConstFormat.FRAME_SIZE, _opusBuffer, ConstFormat.MAX_PACKET_SIZE);

        if (encodedLength == 0)
        {
            Log.Error("Failed to encode audio frame.");
            return;
        }

        Buffer.BlockCopy(_opusBuffer, 0, _finalPacket, 0, Math.Min(encodedLength, ConstFormat.MAX_BUFFER_SIZE));

        _speaker.Play(_finalPacket, Math.Min(encodedLength, ConstFormat.MAX_BUFFER_SIZE));
    }
}