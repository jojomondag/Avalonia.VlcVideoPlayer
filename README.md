# Avalonia.VlcVideoPlayer

[![NuGet](https://img.shields.io/nuget/v/Avalonia.VlcVideoPlayer.svg)](https://www.nuget.org/packages/Avalonia.VlcVideoPlayer/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A self-contained VLC-based video player control for Avalonia UI applications. Drop-in video player with built-in controls - works with or without VLC installed.

## Features

- 🎬 **Full-featured video player control** - Play, pause, stop, seek, volume
- 🎨 **Material Design icons** - Clean, modern UI
- 🔊 **Volume control** - Slider with mute toggle
- ⏯️ **Playback controls** - Play, pause, stop buttons
- 📊 **Seek bar** - With current time and duration display
- 🎯 **Self-contained** - Works without VLC installed (when libraries are embedded)
- 🖥️ **Cross-platform** - macOS, Windows, Linux
- 📦 **Easy to use** - Just add the control to your view

## Installation

```bash
dotnet add package Avalonia.VlcVideoPlayer
```

## Quick Start

### 1. Initialize VLC in Program.cs

Call `VlcInitializer.Initialize()` **before** building the Avalonia app:

```csharp
using Avalonia.VlcVideoPlayer;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        // Initialize VLC FIRST
        VlcInitializer.Initialize();
        
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
```

### 2. Add Material Icons styles to App.axaml

```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:materialIcons="clr-namespace:Material.Icons.Avalonia;assembly=Material.Icons.Avalonia"
             x:Class="YourApp.App"
             RequestedThemeVariant="Dark">

    <Application.Styles>
        <FluentTheme />
        <materialIcons:MaterialIconStyles />
    </Application.Styles>
</Application>
```

### 3. Add the VideoPlayerControl to your view

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vlcPlayer="using:Avalonia.VlcVideoPlayer"
        x:Class="YourApp.MainWindow"
        Title="My Video App">
    
    <vlcPlayer:VideoPlayerControl x:Name="VideoPlayer" AutoPlay="True" />
    
</Window>
```

### 4. Control playback from code (optional)

```csharp
// Open a file programmatically
VideoPlayer.Open("/path/to/video.mp4");

// Or open a URL
VideoPlayer.OpenUri(new Uri("https://example.com/video.mp4"));

// Control playback
VideoPlayer.Play();
VideoPlayer.Pause();
VideoPlayer.Stop();
VideoPlayer.TogglePlayPause();

// Seek to position (0.0 to 1.0)
VideoPlayer.Seek(0.5f); // Seek to 50%

// Volume control (0-100)
VideoPlayer.Volume = 75;
VideoPlayer.ToggleMute();
```

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Volume` | `int` | `100` | Volume level (0-100) |
| `AutoPlay` | `bool` | `false` | Auto-play when media is opened |
| `ShowControls` | `bool` | `true` | Show/hide playback controls |
| `IsPlaying` | `bool` | - | (Read-only) Whether media is playing |
| `Position` | `long` | - | (Read-only) Current position in ms |
| `Duration` | `long` | - | (Read-only) Total duration in ms |

## Events

| Event | Description |
|-------|-------------|
| `PlaybackStarted` | Fired when playback starts |
| `PlaybackPaused` | Fired when playback is paused |
| `PlaybackStopped` | Fired when playback is stopped |
| `MediaEnded` | Fired when media reaches the end |

## Embedding VLC Libraries (Self-Contained App)

For a fully self-contained app that works without VLC installed, embed the VLC libraries in your project.

### macOS

Add this to your `.csproj`:

```xml
<PropertyGroup>
  <!-- Path to VLC.app - install VLC first to copy libraries -->
  <VlcAppPath>/Applications/VLC.app/Contents/MacOS</VlcAppPath>
</PropertyGroup>

<!-- Copy VLC libraries on build -->
<Target Name="CopyVlcLibraries" AfterTargets="Build" 
        Condition="$([MSBuild]::IsOSPlatform('OSX')) AND Exists('$(VlcAppPath)/lib')">
  <MakeDir Directories="$(OutputPath)vlc/lib" />
  <MakeDir Directories="$(OutputPath)vlc/plugins" />
  
  <ItemGroup>
    <VlcLibFiles Include="$(VlcAppPath)/lib/*.dylib" />
    <VlcPluginFiles Include="$(VlcAppPath)/plugins/*.dylib" />
  </ItemGroup>
  
  <Copy SourceFiles="@(VlcLibFiles)" DestinationFolder="$(OutputPath)vlc/lib" SkipUnchangedFiles="true" />
  <Copy SourceFiles="@(VlcPluginFiles)" DestinationFolder="$(OutputPath)vlc/plugins" SkipUnchangedFiles="true" />
</Target>
```

### Windows

For Windows, you can use the [VideoLAN.LibVLC.Windows](https://www.nuget.org/packages/VideoLAN.LibVLC.Windows/) NuGet package.

### Linux

Install VLC via your package manager:
```bash
# Ubuntu/Debian
sudo apt install vlc libvlc-dev

# Fedora
sudo dnf install vlc vlc-devel
```

## Advanced Usage

### Embed in any view

```xml
<UserControl xmlns:vlcPlayer="using:Avalonia.VlcVideoPlayer">
    <Grid RowDefinitions="Auto,*">
        <TextBlock Grid.Row="0" Text="Video Section" />
        <vlcPlayer:VideoPlayerControl Grid.Row="1" />
    </Grid>
</UserControl>
```

### Multiple players

```xml
<Grid ColumnDefinitions="*,*">
    <vlcPlayer:VideoPlayerControl Grid.Column="0" x:Name="Player1" />
    <vlcPlayer:VideoPlayerControl Grid.Column="1" x:Name="Player2" />
</Grid>
```

## Requirements

- .NET 8.0 or later
- Avalonia 11.x
- VLC 3.x (installed or embedded)

## License

MIT License - see [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
