# SubtitleTranslatorGUI

## Overview
SubtitleTranslatorGUI is a Windows desktop application for batch translation of subtitle files (`.srt`) and extraction/translation of embedded subtitles from video files (`.mkv`, `.mp4`, `.avi}`). It uses Google Gemini API for translation and FFmpeg for subtitle extraction.  
**New:** If a video file does not contain any subtitle tracks, the app can automatically generate subtitles from the audio using speech-to-text powered by Gemini.

## Features
- Add subtitle and video files via file/folder selection or drag & drop
- Extract embedded subtitles from video files
- **Automatic subtitle generation from video audio (speech-to-text) if no subtitle track exists**
- Batch translation of subtitles using Google Gemini API
- Edit original and translated subtitles side-by-side
- Manage multiple API keys for translation
- Pause/Resume/Cancel processing
- Logging and status updates for each file
- Progress bar based on number of processed files
- Automatic cleanup of temporary audio and chunk files

## Requirements
- .NET 8.0 Runtime
- FFmpeg (must be available in system PATH)
- Google Gemini API keys

## Download
You can download the full installer (includes FFmpeg and .NET Runtime) from the [Releases page](https://github.com/Saber-Ghasempour/SubtitleTranslatorGUI/releases).
> **Note:** This installer automatically sets up FFmpeg and .NET Runtime if not already installed.

## Getting Started

### 1. Clone the repository

### 2. Build the project
Open the solution in Visual Studio 2022 and build.

### 3. Configure API Keys
- Click on the "API Key Manager" button in the app to add your Google Gemini API keys.

### 4. Add Files
- Use the "Open File" or "Open Folder" menu to add subtitle/video files.
- Or drag & drop files/folders into the grid.

### 5. Start Processing
- Click "Start" to begin extraction/translation.
- You can pause/resume or cancel the process.

## Usage Notes
- Make sure FFmpeg is installed and accessible via command line.
- Supported subtitle format: SRT
- Supported video formats: MKV, MP4, AVI
- **Automatic subtitle generation:** If a video has no subtitle track, the app will:
  1. Extract audio from the video using FFmpeg.
  2. Split the audio into 5-minute chunks.
  3. Send each chunk to Gemini for speech-to-text conversion.
  4. Merge and renumber the resulting SRT blocks, adjusting timestamps and block numbers.
  5. Clean up all temporary audio files after processing.

## Project Structure
- `Forms/` - UI forms (MainForm, SubtitleEditForm, etc.)
- `Models/` - Data models (FileItem, enums)
- `Services/` - Business logic and helpers (SubtitleProcessingService, FileGridService, LoggerService, FFmpegAudioService)
- `Utils/` - Utility classes (SubtitleUtils, etc.)

## Contributing
Pull requests and suggestions are welcome!

## License
MIT License