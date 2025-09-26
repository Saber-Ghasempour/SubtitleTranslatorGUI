# SubtitleTranslatorGUI

## Overview
SubtitleTranslatorGUI is a Windows desktop application for batch translation of subtitle files (`.srt`) and extraction/translation of embedded subtitles from video files (`.mkv`, `.mp4`, `.avi}`). It uses Google Gemini API for translation and FFmpeg for subtitle extraction.

## Features
- Add subtitle and video files via file/folder selection or drag & drop
- Extract embedded subtitles from video files
- Batch translation of subtitles using Google Gemini API
- Edit original and translated subtitles side-by-side
- Manage multiple API keys for translation
- Pause/Resume/Cancel processing
- Logging and status updates for each file

## Requirements
- .NET 8.0 Runtime
- FFmpeg (must be available in system PATH)
- Google Gemini API keys

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

## Project Structure
- `Forms/` - UI forms (MainForm, SubtitleEditForm, etc.)
- `Models/` - Data models (FileItem, enums)
- `Services/` - Business logic and helpers (SubtitleProcessingService, FileGridService, LoggerService)
- `Utils/` - Utility classes (SubtitleUtils, etc.)

## Contributing
Pull requests and suggestions are welcome!

## License
MIT License

## Download
You can download the full installer (includes FFmpeg and .NET Runtime) from the [Releases page](https://github.com/Saber-Ghasempour/SubtitleTranslatorGUI/releases).
> **Note:** This installer automatically sets up FFmpeg and .NET Runtime if not already installed.