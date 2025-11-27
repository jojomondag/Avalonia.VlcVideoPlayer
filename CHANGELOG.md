# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
