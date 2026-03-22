# 非機能要件

## 互換性

| 項目 | 要件 |
|------|------|
| Unity バージョン | 2022.3 LTS 以上 |
| プラットフォーム | Unity Editor 専用 (Windows / macOS) |
| .NET | Unity 標準の .NET バージョン |

## パフォーマンス

- ファイルスキャン: 数千ファイル規模で実用的な速度であること
- GUI: EditorWindow の描画が体感でもたつかないこと

## 保守性

- クラス数を最小限に保つ（EditorWindow + Settings の 2 クラス構成）
- Unity Editor API の標準パターンに従う
- Assembly Definition で Editor 専用に分離

## セキュリティ

- 設定ファイルに秘密情報を含めない
- ファイルシステム操作は Unity プロジェクト内に限定

## 配布

- 追加パッケージへの依存なし（Unity 標準 API のみ使用）
- Editor フォルダに配置するだけで動作
