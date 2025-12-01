# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.6.0] - 2025-12-01

### Changed
- **Simplified VLC library management** - No longer downloads VLC automatically
- **Windows**: Uses `VideoLAN.LibVLC.Windows` NuGet package (works out of the box)
- **macOS**: Uses system-installed VLC via Homebrew (`brew install vlc`) or VLC.app
- **Linux**: Uses system-installed VLC via package manager (`apt`, `dnf`, `pacman`)

### Added
- `VlcNotFoundException` exception with helpful installation instructions
- `VlcInstallationStatus` class to check VLC installation status
- `CheckInstallation()` method for pre-flight VLC verification
- `TryInitialize()` method for non-throwing initialization
- `GetInstallationInstructions()` method for platform-specific help
- Homebrew detection on macOS (`/opt/homebrew/lib`, `/usr/local/lib`)
- VLC Cellar detection for Homebrew installations
- Linux architecture-specific path detection (ARM64, x64)

### Removed
- Automatic VLC download from videolan.org
- DMG extraction code for macOS
- ZIP extraction code for Windows
- `VideoLAN.LibVLC.Mac` NuGet dependency (users install VLC system-wide instead)

## [1.5.0] - 2025-12-01

### Added
- **Automatic architecture detection** (x64, ARM64, x86)
- **macOS Apple Silicon (ARM64) support** - automatically downloads correct VLC version
- **macOS Intel (x64) support** - automatically downloads correct VLC version
- Architecture validation on macOS - detects wrong architecture and re-downloads correct version
- DMG extraction support - automatically mounts, extracts VLC libraries, and unmounts
- Linux ARM64 (aarch64) architecture support
- Linux distro detection with appropriate package manager instructions
- `PlatformInfo` property to get current platform and architecture
- `IsArm`, `IsX64`, `IsX86` static properties for architecture detection
- `DYLD_LIBRARY_PATH` and `DYLD_FALLBACK_LIBRARY_PATH` environment variables on macOS
- `LD_LIBRARY_PATH` environment variable on Linux
- Architecture-specific VLC storage directories (`vlc/arm64/`, `vlc/x64/`)

### Fixed
- **Fixed VLC library loading on macOS** - now properly sets dynamic library paths before initialization
- **Fixed architecture mismatch issues on Apple Silicon Macs** - no longer tries to load Intel VLC on ARM64
- Improved error messages with architecture information

### Changed
- VLC libraries are now stored in architecture-specific subdirectories
- Enhanced logging with detailed platform/architecture info

## [1.4.0] - 2025-11-27

### Added
- `Source` property - Set video path directly in XAML or code, auto-loads when set
- `ShowOpenButton` property - Hide the Open button for embedded player scenarios
- `ControlPanelBackground` property - Customize the control panel background color

### Changed
- Improved property change handling for dynamic updates

## [1.3.0] - 2025-11-27

### Changed
- Redesigned UI with clean white control panel background
- Improved button styling with custom template (no overlay issues)
- Dark text and icons for better contrast and readability
- Light gray buttons with visible borders

### Fixed
- Fixed dark overlay appearing over buttons in some themes
- Fixed button visibility issues

## [1.2.0] - 2025-11-27

### Changed
- Optimized NuGet package size from 90MB to 0.02MB
- VLC native libraries now delivered as transitive dependency via VideoLAN.LibVLC.Windows
- Added white foreground to icons for dark theme visibility

### Fixed
- Fixed seek bar dragging issues
- Fixed VLC path detection on Windows

## [1.0.0] - 2025-11-27

### Added
- Initial release
- `VideoPlayerControl` - Full-featured video player control
- `VlcInitializer` - Helper for VLC initialization with embedded library support
- Play, pause, stop, seek functionality
- Volume control with mute toggle
- Material Design icons
- Support for embedded VLC libraries (self-contained apps)
- Events: `PlaybackStarted`, `PlaybackPaused`, `PlaybackStopped`, `MediaEnded`
- Properties: `Volume`, `AutoPlay`, `ShowControls`, `IsPlaying`, `Position`, `Duration`
