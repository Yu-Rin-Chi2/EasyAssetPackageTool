[English](README.md)

# Easy Asset Package Tool

Unity Editor 上で `.unitypackage` ファイルを簡単にビルドできる EditorWindow ツールです。

インクルード/除外パスの設定、バージョン管理、ワンクリックでのパッケージエクスポートが可能です。

## 主な機能

- GUI ベースの直感的なパッケージ設定
- インクルード/除外パスの管理
- ワイルドカードパターン（`*.cs`、`**/*.cs`）による柔軟な除外ルール
- `{version}` プレースホルダによるバージョン置換
- Unity 依存関係のオプション付与
- ScriptableObject による設定の永続化
- リアルタイムのアセットプレビューと除外ファイル統計
- ワンクリックでパッケージをエクスポート
- プロジェクト起動時の設定自動初期化

## スクリーンショット

<!-- docs/images/ にスクリーンショットを追加し、以下のコメントを解除してください -->
<!-- ![EditorWindow](docs/images/editor-window.png) -->

## 動作要件

- Unity 2022.3 LTS 以降
- Editor 専用（Windows / macOS）

## インストール

### Unity Package Manager（推奨）

1. Unity で **Window > Package Manager** を開きます。
2. **+** > **Add package from git URL...** をクリックします。
3. 以下を入力します:
   ```
   https://github.com/Yu-Rin-Chi2/EasyAssetPackageTool.git?path=Assets/EasyAssetPackageTool
   ```

### 手動インストール

1. リポジトリをクローンまたはダウンロードします:
   ```
   git clone https://github.com/Yu-Rin-Chi2/EasyAssetPackageTool.git
   ```
2. `Assets/EasyAssetPackageTool` フォルダを、お使いの Unity プロジェクトの `Assets` ディレクトリにコピーします。

## 使い方

1. メニューから **Tools > EasyAssetPackageTool** を開きます。
2. 出力パスとパッケージ名を設定します。
3. インクルードパス（パッケージに含めるフォルダ/ファイル）を追加します。
4. 除外パターンで不要なファイルをフィルタリングします。
5. バージョン文字列を入力します（パッケージ名の `{version}` が置換されます）。
6. **Build** ボタンをクリックして `.unitypackage` をエクスポートします。

## 設定項目

| 設定 | 説明 |
|------|------|
| Output Path | パッケージの出力先ディレクトリ（デフォルト: `ExportPackage/`） |
| Package Name | ファイル名（`{version}` プレースホルダ使用可） |
| Include Dependencies | Unity 依存関係をパッケージに含めるかの切り替え |
| Include Paths | パッケージに含めるディレクトリ/ファイルのリスト |
| Exclude Paths | 除外するワイルドカードパターン（例: `**/*.meta`、`**/Tests/**`） |

設定は `Assets/EasyAssetPackageTool/EasyAssetPackageToolSettings.asset` に ScriptableObject として保存されます。

## コントリビュート

コントリビュートを歓迎します！ガイドラインは [CONTRIBUTING.md](CONTRIBUTING.md) をご覧ください。

## ライセンス

MIT License - 詳細は [LICENSE](LICENSE) を参照してください。
