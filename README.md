[日本語版はこちら / Japanese](README_JP.md)

# Easy Asset Package Tool

A simple Unity Editor tool for building `.unitypackage` files from an EditorWindow GUI.

Configure include/exclude paths, manage versions, and export packages with a single click.

## Features

- GUI-based EditorWindow for intuitive package configuration
- Include/exclude path management with add/remove controls
- Wildcard pattern matching (`*.cs`, `**/*.cs`) for flexible exclusion rules
- Version substitution using `{version}` placeholder in package names
- Optional Unity dependency inclusion
- Persistent settings via ScriptableObject
- Real-time asset preview with excluded file statistics
- One-click package export
- Auto-initialization of settings on project load

## Screenshots

<!-- Add screenshot files to docs/images/ and uncomment below -->
<!-- ![EditorWindow](docs/images/editor-window.png) -->

## Requirements

- Unity 2022.3 LTS or later
- Editor only (Windows / macOS)

## Installation

### Via Unity Package Manager (recommended)

1. Open **Window > Package Manager** in Unity.
2. Click **+** > **Add package from git URL...**
3. Enter:
   ```
   https://github.com/Yu-Rin-Chi2/EasyAssetPackageTool.git?path=Assets/EasyAssetPackageTool
   ```

### Manual

1. Download or clone this repository:
   ```
   git clone https://github.com/Yu-Rin-Chi2/EasyAssetPackageTool.git
   ```
2. Copy the `Assets/EasyAssetPackageTool` folder into your Unity project's `Assets` directory.
3. Unity will automatically compile the Editor scripts.

## Usage

1. Open the tool from the menu: **Tools > EasyAssetPackageTool**
2. Configure the output path and package name.
3. Add include paths (folders or files to package).
4. Add exclude patterns to filter out unwanted files (supports wildcards).
5. Enter a version string (replaces `{version}` in the package name).
6. Click **Build** to export the `.unitypackage` file.

## Configuration

| Setting | Description |
|---------|-------------|
| Output Path | Directory for exported packages (default: `ExportPackage/`) |
| Package Name | File name with optional `{version}` placeholder |
| Include Dependencies | Toggle Unity dependency inclusion in the package |
| Include Paths | List of directories/files to include |
| Exclude Paths | Wildcard patterns to exclude (e.g., `**/*.meta`, `**/Tests/**`) |

Settings are stored as a ScriptableObject at `Assets/EasyAssetPackageTool/EasyAssetPackageToolSettings.asset`.

## Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.
