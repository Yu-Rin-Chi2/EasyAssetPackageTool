# コンセプト

## ビジョン

Unity プロジェクトから .unitypackage を手軽にビルドできる Editor ツールを提供する。
パス指定・除外パターン・バージョン管理を GUI で操作でき、設定を ScriptableObject として永続化する。

## ターゲットユーザー

- Unity アセットを .unitypackage 形式で配布したい開発者
- GitHub 等でアセットパッケージを公開する個人・チーム

## MVP 定義

現在の実装が MVP に該当する:

1. EditorWindow による GUI 操作
2. Include/Exclude パスの管理
3. ワイルドカードによる除外パターン (`**/*.cs` 等)
4. パッケージ名のバージョン置換 (`{version}`)
5. 依存関係の包含/除外オプション
6. ScriptableObject による設定永続化
7. ビルド実行と出力フォルダを開く機能

## 配布方針

- GitHub リポジトリで公開
- Editor フォルダ配下に配置するだけで動作する構成
