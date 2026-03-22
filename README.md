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

<!-- TODO: Add screenshots of the EditorWindow -->

## Requirements

- Unity 2022.3 LTS or later
- Editor only (Windows / macOS)

## Installation

1. Download or clone this repository:
   ```
   git clone https://github.com/Yu-Rin-Chi2/EawsyAssetPackageTool.git
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

Settings are stored as a ScriptableObject at `Assets/UnityPackageBuilder/UnityPackageBuilderSettings.asset`.

## Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

---

# Easy Asset Package Tool (Japanese / 日本語)

Unity Editor 上で `.unitypackage` ファイルを簡単にビルドできる EditorWindow ツールです。

## 主な機能

- GUI ベースの直感的なパッケージ設定
- インクルード/除外パスの管理
- ワイルドカードパターンによる柔軟な除外ルール
- `{version}` プレースホルダによるバージョン置換
- ワンクリックでパッケージをエクスポート
- ScriptableObject による設定の永続化

## 動作要件

- Unity 2022.3 LTS 以降
- Editor 専用（Windows / macOS）

## インストール

1. リポジトリをクローンまたはダウンロードします:
   ```
   git clone https://github.com/Yu-Rin-Chi2/EawsyAssetPackageTool.git
   ```
2. `Assets/EasyAssetPackageTool` フォルダを、お使いの Unity プロジェクトの `Assets` ディレクトリにコピーします。

## 使い方

1. メニューから **Tools > EasyAssetPackageTool** を開きます。
2. 出力パスとパッケージ名を設定します。
3. インクルードパス（パッケージに含めるフォルダ/ファイル）を追加します。
4. 除外パターンで不要なファイルをフィルタリングします。
5. バージョン文字列を入力します（パッケージ名の `{version}` が置換されます）。
6. **Build** ボタンをクリックして `.unitypackage` をエクスポートします。

## ライセンス

MIT License - 詳細は [LICENSE](LICENSE) を参照してください。
