[English](README.md)

# Easy Asset Package Tool

> `.unitypackage` の作成をシンプル・確実にする GUI ツール

![Easy Asset Package Tool](docs/images/ScreenShot0.png)

## こんな方に

**Unity Asset Store** や **Booth** などでアセットを販売・配布している方で、こんな悩みはありませんか？

- 開発プロジェクトにテストシーンやデバッグ用スクリプトがあり、**パッケージに含めたくないファイルがある**
- パッケージ化したいアセットが**複数のフォルダに分散している**
- バージョンを更新するたびに**毎回手動でファイルを選択するのが面倒**

**Easy Asset Package Tool** なら、含めるパスと除外パスを一度設定すれば、あとはワンクリックでパッケージをビルドできます。

## 動作要件

- Unity 2022.3 LTS 以降

## インストール

### Unity Package Manager（推奨）

1. Unity で **Window > Package Manager** を開く
2. **+** > **Add package from git URL...** をクリック
3. 以下を貼り付け:
   ```
   https://github.com/Yu-Rin-Chi2/EasyAssetPackageTool.git?path=Assets/EasyAssetPackageTool
   ```

### 手動インストール

1. リポジトリをクローン:
   ```
   git clone https://github.com/Yu-Rin-Chi2/EasyAssetPackageTool.git
   ```
2. `Assets/EasyAssetPackageTool/` をプロジェクトの `Assets/` にコピー

## 使い方

**Tools > EasyAssetPackageTool** からツールを開きます。

### 1. Basic Settings（基本設定）

パッケージの出力に関する設定を行います。

| 設定 | 説明 |
|------|------|
| **Output Directory** | `.unitypackage` の保存先（デフォルト: `ExportPackage/`） |
| **Package Name** | ファイル名。`{version}` をプレースホルダとして使用可能 — 例: `MyAsset-v{version}` → `MyAsset-v1.2.0` |
| **Version** | Package Name に `{version}` を含む場合に表示されます |
| **Include Dependencies** | ON にすると Unity が依存アセットを自動追加。OFF なら指定したファイルのみ含まれます（正確な制御が必要な場合は OFF 推奨） |

### 2. Asset Paths Configuration（パス設定）

パッケージに含める内容を指定します。

**Include Paths（含めるパス）** — パッケージに入れたいフォルダやファイルを追加します。例:
  - `Assets/MyPlugin/Runtime`
  - `Assets/MyPlugin/Editor`
  - `Assets/MyPlugin/Resources/Icons`

**Exclude Paths（除外パス）** — 含めたくないファイルをフィルタリングします。ワイルドカード対応:
  - `**/Tests/**` — すべてのテストフォルダを除外
  - `**/*.asmdef` — Assembly Definition ファイルを除外
  - `Assets/MyPlugin/Editor/Debug/` — 特定フォルダを除外

### 3. Build（ビルド）

画面下部の **Build UnityPackage** をクリックします。ビルド前に**含まれるファイルのライブプレビュー**が表示されるので、内容を確認してからビルドできます。

## 設定例

ランタイムコード、エディタツール、テストファイルを含むアセットの典型的な設定:

```
Include Paths:
  Assets/MyPlugin/Runtime
  Assets/MyPlugin/Editor
  Assets/MyPlugin/Resources

Exclude Paths:
  **/Tests/**
  **/*Debug*
```

Runtime、Editor、Resources のすべてをパッケージに含めつつ、テストフォルダやデバッグ関連ファイルを除外します。

## コントリビュート

コントリビュートを歓迎します！ガイドラインは [CONTRIBUTING.md](CONTRIBUTING.md) をご覧ください。

## ライセンス

[MIT License](LICENSE)
