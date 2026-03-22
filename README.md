[日本語版はこちら / Japanese](README_JP.md)

# Easy Asset Package Tool

> A GUI tool that makes building `.unitypackage` files simple and repeatable.

![Easy Asset Package Tool](docs/images/ScreenShot0.png)

## Who is this for?

If you're creating and distributing Unity assets — on the **Unity Asset Store**, **Booth**, or anywhere else — you've probably run into these pain points:

- Your development project has test scenes, editor scripts, and other files that **shouldn't be in the final package**
- The assets you want to package are **spread across multiple folders**
- You have to **manually select files every time** you export a new version

**Easy Asset Package Tool** solves all of these. Set up your include/exclude paths once, and build your package with a single click — every time.

## Requirements

- Unity 2022.3 LTS or later

## Installation

### Via Unity Package Manager (recommended)

1. In Unity, go to **Window > Package Manager**
2. Click **+** > **Add package from git URL...**
3. Paste:
   ```
   https://github.com/Yu-Rin-Chi2/EasyAssetPackageTool.git?path=Assets/EasyAssetPackageTool
   ```

### Manual

1. Clone this repository:
   ```
   git clone https://github.com/Yu-Rin-Chi2/EasyAssetPackageTool.git
   ```
2. Copy `Assets/EasyAssetPackageTool/` into your project's `Assets/` folder.

## Usage

Open the tool from **Tools > EasyAssetPackageTool**.

### 1. Basic Settings

Configure your package output:

| Setting | Description |
|---------|-------------|
| **Output Directory** | Where the `.unitypackage` file will be saved (default: `ExportPackage/`) |
| **Package Name** | The file name. Use `{version}` as a placeholder — e.g. `MyAsset-v{version}` becomes `MyAsset-v1.2.0` |
| **Version** | Appears when `{version}` is used in the package name |
| **Include Dependencies** | When ON, Unity automatically includes dependent assets. When OFF, only the files you specify are included (recommended for precise control) |

### 2. Asset Paths Configuration

Define exactly what goes into your package:

**Include Paths** — Add the folders or files to include. For example:
  - `Assets/MyPlugin/Runtime`
  - `Assets/MyPlugin/Editor`
  - `Assets/MyPlugin/Resources/Icons`

**Exclude Paths** — Filter out files you don't want. Supports wildcards:
  - `**/Tests/**` — Exclude all test folders
  - `**/*.asmdef` — Exclude assembly definitions
  - `Assets/MyPlugin/Editor/Debug/` — Exclude a specific folder

### 3. Build

Click **Build UnityPackage** at the bottom. The tool shows a **live preview** of all files that will be included, so you can verify the contents before building.

## Example

A typical setup for an asset with runtime code, editor tools, and test files:

```
Include Paths:
  Assets/MyPlugin/Runtime
  Assets/MyPlugin/Editor
  Assets/MyPlugin/Resources

Exclude Paths:
  **/Tests/**
  **/*Debug*
```

This packages everything in Runtime, Editor, and Resources — while excluding any test folders and debug-related files.

## Contributing

Contributions are welcome! See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## License

[MIT License](LICENSE)
