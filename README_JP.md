[English](README.md)

# Easy Asset Package Tool

> GUI だけで `.unitypackage` を作成 — スクリプト不要

![Easy Asset Package Tool](docs/images/ScreenShot0.png)

## これは何？

Unity Editor 上で `.unitypackage` ファイルを視覚的に作成できる拡張ツールです。含めたいフォルダを選び、除外パターンを設定して、**Build** ボタンを押すだけ。

### 主な機能

- **ビジュアルパッケージビルダー** — EditorWindow からすべて設定可能
- **柔軟なフィルタリング** — `**/*.cs` や `**/Tests/**` などのワイルドカードで除外
- **バージョン管理** — パッケージ名に `{version}` を使って自動バージョニング
- **ライブプレビュー** — ビルド前に含まれるファイルをリアルタイム確認
- **設定の永続化** — ScriptableObject で設定を自動保存

## 動作要件

- Unity 2022.3 LTS 以降

## はじめ方

### Unity Package Manager でインストール（推奨）

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
2. `Assets/EasyAssetPackageTool/` フォルダをプロジェクトの `Assets/` にコピー

### パッケージを作成する

1. **Tools > EasyAssetPackageTool** を開く
2. 出力先ディレクトリとパッケージ名を設定
3. 含めたいフォルダ/ファイルを追加
4. （任意）除外パターンで不要なファイルをフィルタリング
5. **Build UnityPackage** をクリック

## コントリビュート

コントリビュートを歓迎します！ガイドラインは [CONTRIBUTING.md](CONTRIBUTING.md) をご覧ください。

## ライセンス

[MIT License](LICENSE)
