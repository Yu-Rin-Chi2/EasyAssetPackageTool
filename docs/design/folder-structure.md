# ディレクトリ構成

```
GameAssetPackage/
├── Assets/
│   └── EasyAssetPackageTool/
│       └── Editor/                          # ツール本体 (Editor 専用)
│           ├── EasyAssetPackageTool.cs       # EditorWindow: GUI + ビルドロジック
│           ├── EasyAssetPackageToolSettings.cs # ScriptableObject: 設定データ
│           ├── SettingsAutoCreator.cs        # [InitializeOnLoad]: 設定自動生成
│           └── EasyAssetPackageTool.asmdef   # Assembly Definition
├── ExportPackage/                           # デフォルト出力先 (自動生成)
├── docs/
│   ├── requirements/                        # 要件定義
│   └── design/                              # 設計ドキュメント
├── Packages/                                # Unity Package Manager
├── ProjectSettings/                         # Unity プロジェクト設定
└── CLAUDE.md
```

## 責務

| ディレクトリ | 責務 |
|-------------|------|
| `Assets/EasyAssetPackageTool/Editor/` | ツールのソースコード。Editor 専用 |
| `ExportPackage/` | .unitypackage の出力先。デフォルトパス |
| `docs/` | プロジェクトドキュメント |
