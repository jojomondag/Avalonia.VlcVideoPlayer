# FFmpegVideoPlayer.Avalonia

An FFmpeg-based video player control for Avalonia UI with **full cross-platform support including ARM64 macOS (Apple Silicon)**.

[![NuGet](https://img.shields.io/nuget/v/FFmpegVideoPlayer.Avalonia.svg)](https://www.nuget.org/packages/FFmpegVideoPlayer.Avalonia/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

![Video Player Screenshot](https://raw.githubusercontent.com/jojomondag/Avalonia.VlcVideoPlayer/main/screenshot.png)

## Features

- 🎬 Full-featured video player control for Avalonia
- 🖥️ **True cross-platform** (Windows, macOS Intel, macOS ARM64, Linux)
- 🍎 **Native Apple Silicon (M1/M2/M3) support** via FFmpeg
- 🎨 Clean, modern UI with Material Design icons
- ⚡ Uses FFmpeg.AutoGen for maximum codec support
- 🎛️ Built-in controls: Play/Pause, Stop, Seek bar, Volume slider, Mute
- 🔊 OpenAL-based audio playback

## Installation

```bash
dotnet add package FFmpegVideoPlayer.Avalonia
```

### FFmpeg Installation

**macOS:** FFmpeg is **automatically installed via Homebrew** if not found! No manual setup needed! 🎉

**Windows & Linux:** FFmpeg must be installed on your system.

| Platform | Installation |
|----------|-------------|
| **macOS (Intel & ARM64)** | **Automatic via Homebrew!** ✅ |
| **Windows** | `winget install ffmpeg` or `choco install ffmpeg` |
| **Linux (Debian/Ubuntu)** | `sudo apt install ffmpeg libavcodec-dev libavformat-dev libavutil-dev libswscale-dev` |
| **Linux (Fedora)** | `sudo dnf install ffmpeg ffmpeg-devel` |
| **Linux (Arch)** | `sudo pacman -S ffmpeg` |

### Platform Support

| Platform | FFmpeg Source | Status |
|----------|---------------|--------|
| **macOS Intel (x64)** | Auto-install via Homebrew | ✅ **Zero config!** |
| **macOS ARM64 (Apple Silicon)** | Auto-install via Homebrew | ✅ **Zero config!** |
| **Windows (x64/x86/ARM64)** | winget/choco/manual | ✅ Tested |
| **Linux (x64/ARM64)** | System package | ✅ Tested |

> **Note:** On macOS, FFmpeg and Homebrew are automatically installed if not present!

## Quick Start

### Step 1: Add Material Icons to App.axaml

**Important:** The video player uses Material Icons for its controls. You must add the `MaterialIconStyles` to your `App.axaml`:

```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:materialIcons="clr-namespace:Material.Icons.Avalonia;assembly=Material.Icons.Avalonia"
             x:Class="YourApp.App">
    <Application.Styles>
        <FluentTheme />
        <!-- Required for video player icons -->
        <materialIcons:MaterialIconStyles />
    </Application.Styles>
</Application>
```

### Step 2: Initialize FFmpeg at Startup

In your `Program.cs` or `App.axaml.cs`, initialize FFmpeg before creating any windows.

**On macOS, FFmpeg is automatically installed via Homebrew if not found!**

```csharp
using Avalonia.VlcVideoPlayer;

public class Program
{
    public static void Main(string[] args)
    {
        // Initialize FFmpeg - on macOS, auto-installs via Homebrew if needed!
        // Set autoInstall: false to disable automatic installation
        FFmpegInitializer.Initialize();
        
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
```

**Optional: Subscribe to status updates during initialization:**

```csharp
FFmpegInitializer.StatusChanged += (message) => Console.WriteLine(message);
FFmpegInitializer.Initialize();
// Output on macOS without FFmpeg:
// "Initializing FFmpeg for macos-arm64..."
// "FFmpeg not found. Installing via Homebrew (this may take a few minutes)..."
// "FFmpeg installed successfully!"
// "FFmpeg initialized successfully (libavcodec: 61.3.100)"
```

### Step 3: Add the VideoPlayerControl to your Window

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ffmpeg="clr-namespace:Avalonia.VlcVideoPlayer;assembly=Avalonia.VlcVideoPlayer"
        Title="My Video Player" Width="800" Height="600">
    
    <ffmpeg:VideoPlayerControl x:Name="VideoPlayer" />
    
</Window>
```

### Step 4: Play a Video

Use the built-in "Open" button, or load programmatically:

```csharp
// Play a local file
VideoPlayer.Open(@"C:\Videos\movie.mp4");

// Or play from URL
VideoPlayer.OpenUri(new Uri("https://example.com/video.mp4"));
```

## Complete Example

Here's a minimal working example:

**MyApp.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Avalonia" Version="11.3.6" />
    <PackageReference Include="Avalonia.Desktop" Version="11.3.6" />
    <PackageReference Include="Avalonia.Themes.Fluent" Version="11.3.6" />
    <PackageReference Include="FFmpegVideoPlayer.Avalonia" Version="2.0.0" />
  </ItemGroup>
</Project>
```

**App.axaml:**
```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:materialIcons="clr-namespace:Material.Icons.Avalonia;assembly=Material.Icons.Avalonia"
             x:Class="MyApp.App">
    <Application.Styles>
        <FluentTheme />
        <materialIcons:MaterialIconStyles />
    </Application.Styles>
</Application>
```

**Program.cs:**
```csharp
using Avalonia;
using Avalonia.VlcVideoPlayer;

namespace MyApp;

class Program
{
    public static void Main(string[] args)
    {
        FFmpegInitializer.Initialize();
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}
```

## Embedded Player (No Open Button)

For scenarios where you want to play a specific video without the file browser, use the `Source` property and hide the Open button:

```xml
<!-- XAML: Embedded player with custom background -->
<ffmpeg:VideoPlayerControl 
    Source="C:\Videos\intro.mp4"
    AutoPlay="True"
    ShowOpenButton="False"
    ControlPanelBackground="#2d2d2d" />
```

Or set programmatically:

```csharp
// Hide the Open button and set source
VideoPlayer.ShowOpenButton = false;
VideoPlayer.AutoPlay = true;
VideoPlayer.Source = @"C:\Videos\movie.mp4";

// Customize the control panel background
VideoPlayer.ControlPanelBackground = new SolidColorBrush(Color.Parse("#1a1a1a"));
```

### Custom Control Panel Colors

The control panel background can be customized to match your app's theme:

```xml
<!-- Dark theme -->
<ffmpeg:VideoPlayerControl ControlPanelBackground="#1a1a1a" />

<!-- Match your app's accent color -->
<ffmpeg:VideoPlayerControl ControlPanelBackground="{DynamicResource SystemAccentColor}" />

<!-- Transparent (overlay style) -->
<ffmpeg:VideoPlayerControl ControlPanelBackground="Transparent" />
```

## API Reference

### VideoPlayerControl Properties

| Property | Type | Description |
|----------|------|-------------|
| `Volume` | `int` | Volume level (0-100) |
| `AutoPlay` | `bool` | Auto-play when media is loaded |
| `ShowControls` | `bool` | Show/hide playback controls |
| `ShowOpenButton` | `bool` | Show/hide the Open button (default: true) |
| `Source` | `string` | Video source path - set to auto-load video |
| `ControlPanelBackground` | `IBrush` | Background color of the control panel (default: White) |
| `IsPlaying` | `bool` | Whether media is currently playing |
| `Position` | `long` | Current playback position in milliseconds |
| `Duration` | `long` | Total media duration in milliseconds |

### VideoPlayerControl Methods

| Method | Description |
|--------|-------------|
| `Open(string path)` | Open a local file |
| `OpenUri(Uri uri)` | Open from URL |
| `Play()` | Start/resume playback |
| `Pause()` | Pause playback |
| `Stop()` | Stop playback |
| `Seek(float position)` | Seek to position (0.0-1.0) |
| `ToggleMute()` | Toggle audio mute |
| `TogglePlayPause()` | Toggle between play and pause |

### VideoPlayerControl Events

| Event | Description |
|-------|-------------|
| `PlaybackStarted` | Fired when playback starts |
| `PlaybackPaused` | Fired when playback is paused |
| `PlaybackStopped` | Fired when playback stops |
| `MediaEnded` | Fired when media reaches the end |

### FFmpegInitializer Static Methods

| Method | Description |
|--------|-------------|
| `Initialize(string? customPath, bool autoInstall)` | Initialize FFmpeg. On macOS, auto-installs via Homebrew if `autoInstall` is true (default) |
| `InitializeAsync(string? customPath, bool autoInstall)` | Async version of Initialize |
| `TryInitialize(string? customPath, out string? error, bool autoInstall)` | Try to initialize without throwing |
| `CheckInstallation()` | Check FFmpeg installation status |
| `GetInstallationInstructions()` | Get platform-specific install instructions |
| `IsHomebrewInstalled()` | Check if Homebrew is installed (macOS only) |
| `TryInstallFFmpegOnMacOS()` | Manually trigger FFmpeg installation via Homebrew |
| `TryInstallFFmpegOnWindows()` | Manually trigger FFmpeg installation via winget |

### FFmpegInitializer Properties

| Property | Type | Description |
|----------|------|-------------|
| `IsInitialized` | `bool` | Whether FFmpeg has been successfully initialized |
| `FFmpegPath` | `string?` | Path to the FFmpeg libraries being used |
| `PlatformInfo` | `string` | Current platform and architecture (e.g., "macos-arm64") |
| `IsMacOS` | `bool` | Whether running on macOS |
| `IsWindows` | `bool` | Whether running on Windows |
| `IsLinux` | `bool` | Whether running on Linux |
| `IsArm` | `bool` | Whether running on ARM architecture |

### FFmpegInitializer Events

| Event | Description |
|-------|-------------|
| `StatusChanged` | Fired with status messages during initialization (useful for showing progress) |

## Migration from VLC Version

If you're migrating from the VLC-based version (v1.x):

1. Update the package: `dotnet add package FFmpegVideoPlayer.Avalonia`
2. **macOS users: No setup needed!** FFmpeg auto-installs via Homebrew
3. **Windows/Linux users:** Install FFmpeg (see Installation section)
4. Change `VlcInitializer.Initialize()` to `FFmpegInitializer.Initialize()` in your Program.cs
5. The rest of the API remains the same!

## Troubleshooting

### FFmpeg not found
On macOS, FFmpeg should auto-install. If it fails, run `FFmpegInitializer.GetInstallationInstructions()` for manual steps.

### No audio playback
Ensure OpenAL is installed:
- **Windows**: Usually included with graphics drivers
- **macOS**: Included with the system
- **Linux**: `sudo apt install libopenal1`

### Video plays but no picture
This can happen with certain codecs. Ensure you have a complete FFmpeg installation with all codecs enabled.

## License

MIT License - see [LICENSE](LICENSE) for details.

## Credits

- [FFmpeg](https://ffmpeg.org/) - The leading multimedia framework
- [FFmpeg.AutoGen](https://github.com/Ruslan-B/FFmpeg.AutoGen) - FFmpeg bindings for .NET
- [OpenTK](https://opentk.net/) - OpenAL bindings for audio playback
- [Material.Icons.Avalonia](https://github.com/SKProCH/Material.Icons.Avalonia) - Material Design icons
