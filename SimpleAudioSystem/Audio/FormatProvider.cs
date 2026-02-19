using SimpleAudioSystem.SimpleAudioSystem.Audio.Formats;
using System;
using System.Collections.ObjectModel;

namespace SimpleAudioSystem.SimpleAudioSystem.Audio;

public class FormatProvider 
{
    private static IFormatProvider[] Services { get; set; }
    public static ReadOnlySpan<IFormatProvider> AvailableServicesSpan => Services;
    public static ReadOnlyCollection<IFormatProvider> AvailableServices => new(Services);

    static FormatProvider()
    {
        Services =
        [
            new WaveFormatProvider()
        ];
    }

    public static bool TryGetFormatProvider(string filePath, out IFormatProvider provider)
    {
        foreach (var service in Services)
        {
            if (service.IsValidFormat(filePath))
            {
                provider = service;
                return true;
            }
        }

        provider = null;
        return false;
    }

    public static IFormatProvider GetFormatProvider(string filePath)
    {
        foreach (var service in Services)
        {
            if (service.IsValidFormat(filePath))
                return service;
        }

        throw new NotSupportedException($"No format provider found for the file: {filePath}");
    }

    public static bool IsAvailableService(string filePath)
    {
        foreach (var service in Services)
        {
            if (service.IsValidFormat(filePath))
                return true;
        }
        return false;
    }

    //public static (bool, IFormatProvider) HasAvailableService(string filePath)
    //{
    //    foreach (var service in _services)
    //    {
    //        if (service.IsValidFormat(filePath))
    //            return new(true, service);
    //    }
    //
    //    return new(false, null);
    //}

}