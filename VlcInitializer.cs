using System;
using System.IO;
using System.Runtime.InteropServices;
using LibVLCSharp.Shared;

namespace Avalonia.VlcVideoPlayer;

/// <summary>
/// Handles VLC initialization and environment setup for embedded VLC libraries.
/// </summary>
public static class VlcInitializer
{
    // P/Invoke to set environment variable at the native level (works on macOS/Linux)
    [DllImport("libc", SetLastError = true)]
    private static extern int setenv(string name, string value, int overwrite);

    private static bool _isInitialized;
    private static string? _vlcLibPath;

    /// <summary>
    /// Gets whether VLC has been initialized.
    /// </summary>
    public static bool IsInitialized => _isInitialized;

    /// <summary>
    /// Gets the path to the VLC library directory being used.
    /// </summary>
    public static string? VlcLibPath => _vlcLibPath;

    /// <summary>
    /// Initializes VLC with embedded or system libraries.
    /// Call this method BEFORE creating any Avalonia windows or VLC instances.
    /// Typically called at the very start of Main() in Program.cs.
    /// </summary>
    /// <param name="customVlcPath">Optional custom path to VLC libraries. If not provided, will search for embedded or system VLC.</param>
    /// <returns>True if initialization succeeded, false otherwise.</returns>
    public static bool Initialize(string? customVlcPath = null)
    {
        if (_isInitialized)
        {
            return true;
        }

        try
        {
            // Set up plugin path environment variable
            var pluginPath = FindPluginPath(customVlcPath);
            if (pluginPath != null)
            {
                SetPluginPath(pluginPath);
            }

            // Find and initialize the VLC library
            _vlcLibPath = FindVlcLibPath(customVlcPath);
            
            if (_vlcLibPath != null)
            {
                Console.WriteLine($"[VlcInitializer] Using VLC from: {_vlcLibPath}");
                Core.Initialize(_vlcLibPath);
            }
            else
            {
                Console.WriteLine("[VlcInitializer] Using system default VLC");
                Core.Initialize();
            }

            _isInitialized = true;
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[VlcInitializer] Failed to initialize VLC: {ex.Message}");
            return false;
        }
    }

    private static void SetPluginPath(string pluginPath)
    {
        Console.WriteLine($"[VlcInitializer] Setting VLC_PLUGIN_PATH to: {pluginPath}");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            // Use native setenv to set VLC_PLUGIN_PATH before the native library reads it
            setenv("VLC_PLUGIN_PATH", pluginPath, 1);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Environment.SetEnvironmentVariable("VLC_PLUGIN_PATH", pluginPath);
        }
    }

    private static string? FindPluginPath(string? customPath)
    {
        // Check custom path first
        if (!string.IsNullOrEmpty(customPath))
        {
            var customPluginPath = Path.Combine(customPath, "plugins");
            if (Directory.Exists(customPluginPath))
            {
                return customPluginPath;
            }
        }

        // Check for embedded plugins relative to the executable
        var baseDir = AppContext.BaseDirectory;
        var embeddedPluginPath = Path.Combine(baseDir, "vlc", "plugins");
        if (Directory.Exists(embeddedPluginPath))
        {
            return embeddedPluginPath;
        }

        // macOS: Check VLC.app bundle
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var macOsPluginPath = "/Applications/VLC.app/Contents/MacOS/plugins";
            if (Directory.Exists(macOsPluginPath))
            {
                return macOsPluginPath;
            }
        }

        return null;
    }

    private static string? FindVlcLibPath(string? customPath)
    {
        // Check custom path first
        if (!string.IsNullOrEmpty(customPath))
        {
            var customLibPath = Path.Combine(customPath, "lib");
            if (Directory.Exists(customLibPath))
            {
                return customLibPath;
            }
            // Maybe the custom path IS the lib path
            if (Directory.Exists(customPath) && 
                (File.Exists(Path.Combine(customPath, "libvlc.dylib")) ||
                 File.Exists(Path.Combine(customPath, "libvlc.so")) ||
                 File.Exists(Path.Combine(customPath, "libvlc.dll"))))
            {
                return customPath;
            }
        }

        // Check for embedded VLC libraries relative to the executable
        var baseDir = AppContext.BaseDirectory;
        var embeddedLibPath = Path.Combine(baseDir, "vlc", "lib");
        if (Directory.Exists(embeddedLibPath))
        {
            return embeddedLibPath;
        }

        // macOS: Check VLC.app bundle
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var macOsLibPath = "/Applications/VLC.app/Contents/MacOS/lib";
            if (Directory.Exists(macOsLibPath))
            {
                return macOsLibPath;
            }
        }

        // Linux: Common VLC paths
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            var linuxPaths = new[]
            {
                "/usr/lib/x86_64-linux-gnu",
                "/usr/lib64",
                "/usr/lib"
            };

            foreach (var path in linuxPaths)
            {
                if (File.Exists(Path.Combine(path, "libvlc.so")))
                {
                    return path;
                }
            }
        }

        return null;
    }
}
