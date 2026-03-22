# コーディング規約

## 全般

- 4 スペースインデント
- UTF-8 (BOM なし)
- ファイル末尾に改行を入れる

## 命名規則

| 対象 | スタイル | 例 |
|------|---------|-----|
| クラス | PascalCase | `EasyAssetPackageTool` |
| public メソッド | PascalCase | `BuildPackage()` |
| private メソッド | PascalCase | `LoadOrCreateSettings()` |
| public フィールド | camelCase | `outputPath`, `packageName` |
| private フィールド | camelCase | `mainScrollPosition` |
| 定数 | UPPER_SNAKE_CASE | `SETTINGS_ASSET_PATH` |
| ローカル変数 | camelCase | `assetPaths` |
| パラメータ | camelCase | `directoryPath` |

## 名前空間

- すべてのクラスは `EasyAssetPackageTool.Editor` 名前空間に配置

## Unity Editor 固有

- パスの正規化: `Replace("\\", "/")` で Unity 形式に統一
- ログ出力: `[EasyAssetPackageTool]` プレフィックスを付ける
  - 情報: `Debug.Log`
  - 警告: `Debug.LogWarning`
  - エラー: `Debug.LogError`
- 設定変更時は `EditorUtility.SetDirty(settings)` を呼ぶ
- `EditorGUI.BeginChangeCheck()` / `EndChangeCheck()` で変更検知

## XML ドキュメント

- public メソッドには `<summary>` を付ける
- private メソッドは任意（複雑な処理には付ける）

## エラーハンドリング

- ビルド処理は try-catch で囲み、`EditorUtility.DisplayDialog` でユーザーに通知
- ファイルシステム操作の失敗は `Debug.LogWarning` で記録して続行
