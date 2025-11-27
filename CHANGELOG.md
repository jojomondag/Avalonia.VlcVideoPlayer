# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
