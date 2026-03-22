# アーキテクチャ

## 概要

3 クラスで構成されるシンプルな Unity Editor 拡張。

```
EasyAssetPackageTool (EditorWindow)
├── GUI 描画 (OnGUI)
├── ファイルスキャン・フィルタリング
├── パッケージビルド (AssetDatabase.ExportPackage)
└── 設定の読み書き
    └── EasyAssetPackageToolSettings (ScriptableObject)
        ├── outputPath, packageName
        ├── includePaths, excludePaths
        ├── includeDependencies
        └── lastInputVersion

SettingsAutoCreator ([InitializeOnLoad])
└── 設定アセット未存在時に自動生成
```

## 技術選定理由

| 選定 | 理由 |
|------|------|
| EditorWindow | Unity 標準の Editor UI。追加ライブラリ不要 |
| ScriptableObject | Unity 標準の設定永続化。Inspector でも編集可能 |
| IMGUI (OnGUI) | EditorWindow のデフォルト UI システム。安定性重視 |
| Assembly Definition | Editor 専用であることを明示。ビルド分離 |

## データフロー

```
[設定 (ScriptableObject)]
    ↓
[ファイルスキャン] → includePaths から再帰取得
    ↓
[フィルタリング] → excludePaths でワイルドカードマッチ
    ↓
[プレビュー表示] → GUI に最終アセット一覧を表示
    ↓
[ビルド] → AssetDatabase.ExportPackage で .unitypackage 出力
```

## メニュー登録

`Tools/EasyAssetPackageTool` から EditorWindow を開く。
