using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasyAssetPackageTool.Editor
{
    public class EasyAssetPackageTool : EditorWindow
    {
        // 設定アセット
        private EasyAssetPackageToolSettings settings;

        // スクロール位置
        private Vector2 mainScrollPosition;
        private Vector2 scrollPosition;
        private Vector2 pathScrollPosition;

        // 新規パス入力用
        private string newIncludePath = "";
        private string newExcludePath = "";

        // 設定アセットのパス
        internal const string SETTINGS_ASSET_PATH = "Assets/EasyAssetPackageTool/EasyAssetPackageToolSettings.asset";

        [MenuItem("Tools/EasyAssetPackageTool")]
        public static void ShowWindow()
        {
            var window = GetWindow<EasyAssetPackageTool>("EasyAssetPackageTool");
            window.minSize = new Vector2(400, 300);
            window.LoadOrCreateSettings();
        }

        private void OnEnable()
        {
            LoadOrCreateSettings();
        }

        private void OnDisable()
        {
            SaveSettings();
        }

        private void OnGUI()
        {
            // ウィンドウ全体をスクロールビューでラップ
            mainScrollPosition = EditorGUILayout.BeginScrollView(mainScrollPosition);

            GUILayout.Label("EasyAssetPackageTool", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // ===== セクション1: フォルダ設定 =====
            EditorGUILayout.LabelField("Basic Settings", EditorStyles.boldLabel);

            // Output Directory（Openボタン付き）
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            settings.outputPath = EditorGUILayout.TextField("Output Directory", settings.outputPath);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(settings);
            }
            if (GUILayout.Button("Open", GUILayout.Width(50)))
            {
                OpenOutputFolder();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            settings.packageName = EditorGUILayout.TextField("Package Name", settings.packageName);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(settings);
            }

            // {version}が含まれている場合はバージョン入力フィールドを表示
            if (settings.packageName.Contains("{version}"))
            {
                EditorGUI.BeginChangeCheck();
                settings.lastInputVersion = EditorGUILayout.TextField("Version", settings.lastInputVersion);
                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(settings);
                }
            }

            EditorGUILayout.HelpBox(
                "パッケージ名で {version} を使用すると、上のVersionフィールドの値で置換されます。",
                MessageType.Info);

            EditorGUILayout.Space();

            // 依存関係オプション
            EditorGUI.BeginChangeCheck();
            settings.includeDependencies = EditorGUILayout.Toggle("Include Dependencies", settings.includeDependencies);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(settings);
            }
            if (!settings.includeDependencies)
            {
                EditorGUILayout.HelpBox(
                    "依存関係を含めない場合、除外パターンに一致するファイルは確実に除外されます。" +
                    "（DLL別途提供時に推奨）",
                    MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox(
                    "依存関係を含める場合、Unityが自動的に必要なアセットを追加します。" +
                    "除外パターンは無視される可能性があります。",
                    MessageType.Warning);
            }

            EditorGUILayout.Space();

            // ===== セクション2: パッケージ対象パス設定 + Save/Reset =====
            DrawPathSettings();

            EditorGUILayout.Space();

            // コントロールボタン
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save Settings"))
            {
                SaveSettings();
                ShowNotification(new GUIContent("Settings Saved!"));
            }
            if (GUILayout.Button("Reset to Default"))
            {
                ResetToDefault();
            }
            EditorGUILayout.EndHorizontal();

            // 設定アセット参照表示
            EditorGUILayout.Space();
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Settings Asset", settings, typeof(EasyAssetPackageToolSettings), false);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();

            // ===== セクション3: ビルド =====
            // ビルドボタン
            if (GUILayout.Button("Build UnityPackage", GUILayout.Height(40)))
            {
                BuildPackage();
            }

            EditorGUILayout.Space();

            // 含まれるアセットの表示
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));

            var assetPaths = GetFinalAssetPaths();
            var (totalExcluded, excludedCsCount) = GetExcludedFilesInfo();

            EditorGUILayout.LabelField($"Final Asset List: {assetPaths.Count} files", EditorStyles.boldLabel);

            if (totalExcluded > 0)
            {
                EditorGUILayout.HelpBox(
                    $"Excluded: {totalExcluded} files ({excludedCsCount} .cs files)",
                    MessageType.Info);
            }

            foreach (var path in assetPaths)
            {
                EditorGUILayout.LabelField($"• {path}");
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// 出力フォルダを開く
        /// </summary>
        private void OpenOutputFolder()
        {
            string fullPath = Path.GetFullPath(settings.outputPath);

            // フォルダが存在しない場合は作成
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
                Debug.Log($"[EasyAssetPackageTool] Created output directory: {fullPath}");
            }

            // フォルダを開く
            EditorUtility.RevealInFinder(fullPath);
        }

        private void DrawPathSettings()
        {
            EditorGUILayout.LabelField("Asset Paths Configuration", EditorStyles.boldLabel);

            pathScrollPosition = EditorGUILayout.BeginScrollView(pathScrollPosition, GUILayout.Height(200));

            // Include Paths
            EditorGUILayout.LabelField("Include Paths:", EditorStyles.boldLabel);
            for (int i = 0; i < settings.includePaths.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                settings.includePaths[i] = EditorGUILayout.TextField(settings.includePaths[i]);
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    settings.includePaths.RemoveAt(i);
                    i--;
                    EditorUtility.SetDirty(settings);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            newIncludePath = EditorGUILayout.TextField("New Include Path:", newIncludePath);
            if (GUILayout.Button("Add", GUILayout.Width(50)))
            {
                if (!string.IsNullOrEmpty(newIncludePath) && !settings.includePaths.Contains(newIncludePath))
                {
                    settings.includePaths.Add(newIncludePath);
                    newIncludePath = "";
                    EditorUtility.SetDirty(settings);
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // Exclude Paths
            EditorGUILayout.LabelField("Exclude Paths:", EditorStyles.boldLabel);
            for (int i = 0; i < settings.excludePaths.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                settings.excludePaths[i] = EditorGUILayout.TextField(settings.excludePaths[i]);
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    settings.excludePaths.RemoveAt(i);
                    i--;
                    EditorUtility.SetDirty(settings);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            newExcludePath = EditorGUILayout.TextField("New Exclude Path:", newExcludePath);
            if (GUILayout.Button("Add", GUILayout.Width(50)))
            {
                if (!string.IsNullOrEmpty(newExcludePath) && !settings.excludePaths.Contains(newExcludePath))
                {
                    settings.excludePaths.Add(newExcludePath);
                    newExcludePath = "";
                    EditorUtility.SetDirty(settings);
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }

        private List<string> GetFinalAssetPaths()
        {
            var allAssetFiles = new List<string>();

            // Include pathsから実際のファイルを再帰的に取得
            foreach (var includePath in settings.includePaths)
            {
                if (Directory.Exists(includePath))
                {
                    // ディレクトリの場合、すべてのファイルを再帰的に取得
                    var files = GetAllFilesRecursive(includePath);
                    allAssetFiles.AddRange(files);
                }
                else if (File.Exists(includePath))
                {
                    // 個別ファイルの場合、直接追加
                    allAssetFiles.Add(includePath);
                }
            }

            // 除外パターンに一致するファイルを除外
            var finalPaths = allAssetFiles.Where(file => !IsExcluded(file)).ToList();

            return finalPaths.OrderBy(p => p).ToList();
        }

        /// <summary>
        /// ディレクトリ内のすべてのファイルを再帰的に取得（.metaファイルを除く）
        /// </summary>
        private List<string> GetAllFilesRecursive(string directoryPath)
        {
            var files = new List<string>();

            try
            {
                // 現在のディレクトリ内のファイルを取得
                var currentFiles = Directory.GetFiles(directoryPath)
                    .Where(f => !f.EndsWith(".meta", System.StringComparison.OrdinalIgnoreCase))
                    .Select(f => f.Replace("\\", "/")) // Unity形式のパスに統一
                    .ToList();
                files.AddRange(currentFiles);

                // サブディレクトリを再帰的に処理
                var subdirectories = Directory.GetDirectories(directoryPath);
                foreach (var subdir in subdirectories)
                {
                    files.AddRange(GetAllFilesRecursive(subdir));
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"[EasyAssetPackageTool] Failed to read directory {directoryPath}: {e.Message}");
            }

            return files;
        }

        /// <summary>
        /// ファイルパスが除外パターンに一致するかチェック
        /// </summary>
        private bool IsExcluded(string filePath)
        {
            var normalizedPath = filePath.Replace("\\", "/");

            foreach (var excludePattern in settings.excludePaths)
            {
                if (string.IsNullOrEmpty(excludePattern))
                    continue;

                var normalizedPattern = excludePattern.Replace("\\", "/");

                // ワイルドカード対応（**/*.cs のようなパターン）
                if (normalizedPattern.Contains("*"))
                {
                    if (MatchesWildcardPattern(normalizedPath, normalizedPattern))
                        return true;
                }
                // 単純な前方一致
                else if (normalizedPath.StartsWith(normalizedPattern, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ワイルドカードパターンマッチング
        /// </summary>
        private bool MatchesWildcardPattern(string path, string pattern)
        {
            // **/ パターン（任意の深さのディレクトリ）
            if (pattern.Contains("**/"))
            {
                var parts = pattern.Split(new[] { "**/" }, System.StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    var prefix = parts[0];
                    var suffix = parts[1];

                    // 前方一致チェック
                    if (!path.StartsWith(prefix, System.StringComparison.OrdinalIgnoreCase))
                        return false;

                    // 後方のパターンをチェック（*.csなど）
                    return MatchesSimplePattern(path, suffix);
                }
            }

            // 単純な *.ext パターン
            return MatchesSimplePattern(path, pattern);
        }

        /// <summary>
        /// 単純なワイルドカードパターン（*.ext）のマッチング
        /// </summary>
        private bool MatchesSimplePattern(string path, string pattern)
        {
            if (pattern.StartsWith("*"))
            {
                var extension = pattern.Substring(1); // *.cs → .cs
                return path.EndsWith(extension, System.StringComparison.OrdinalIgnoreCase);
            }

            return path.EndsWith(pattern, System.StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 除外されたファイルの情報を取得
        /// </summary>
        private (int totalExcluded, int excludedCsCount) GetExcludedFilesInfo()
        {
            var allFiles = new List<string>();

            // すべてのファイルを取得
            foreach (var includePath in settings.includePaths)
            {
                if (Directory.Exists(includePath))
                {
                    allFiles.AddRange(GetAllFilesRecursive(includePath));
                }
                else if (File.Exists(includePath))
                {
                    allFiles.Add(includePath);
                }
            }

            // 除外されたファイルをカウント
            var excludedFiles = allFiles.Where(file => IsExcluded(file)).ToList();
            var excludedCsFiles = excludedFiles.Where(f => f.EndsWith(".cs", System.StringComparison.OrdinalIgnoreCase)).Count();

            return (excludedFiles.Count, excludedCsFiles);
        }

        private void BuildPackage()
        {
            try
            {
                var assetPaths = GetFinalAssetPaths();

                if (assetPaths.Count == 0)
                {
                    EditorUtility.DisplayDialog("Error", "パッケージに含めるアセットが見つかりません。", "OK");
                    return;
                }

                // バージョンを置換
                string finalPackageName = settings.packageName;
                if (settings.packageName.Contains("{version}"))
                {
                    if (string.IsNullOrEmpty(settings.lastInputVersion))
                    {
                        EditorUtility.DisplayDialog("Error", "バージョンを入力してください。", "OK");
                        return;
                    }
                    finalPackageName = settings.packageName.Replace("{version}", settings.lastInputVersion);
                }

                // 出力ディレクトリの作成
                if (!Directory.Exists(settings.outputPath))
                {
                    Directory.CreateDirectory(settings.outputPath);
                }

                string fullPath = Path.Combine(settings.outputPath, $"{finalPackageName}.unitypackage");

                // パッケージビルドオプションの設定
                var exportOptions = ExportPackageOptions.Recurse;
                if (settings.includeDependencies)
                {
                    exportOptions |= ExportPackageOptions.IncludeDependencies;
                }

                // パッケージビルド
                AssetDatabase.ExportPackage(
                    assetPaths.ToArray(),
                    fullPath,
                    exportOptions
                );

                // 除外情報を取得してログ出力
                var (totalExcluded, excludedCsCount) = GetExcludedFilesInfo();

                Debug.Log($"[EasyAssetPackageTool] UnityPackage created: {fullPath}");
                Debug.Log($"[EasyAssetPackageTool] Included {assetPaths.Count} asset files");
                Debug.Log($"[EasyAssetPackageTool] Excluded {totalExcluded} files ({excludedCsCount} .cs files)");
                Debug.Log($"[EasyAssetPackageTool] IncludeDependencies: {settings.includeDependencies}");

                // 成功ダイアログ
                if (EditorUtility.DisplayDialog(
                    "Package Built Successfully!",
                    $"UnityPackage has been created:\n{fullPath}\n\nOpen output folder?",
                    "Open Folder",
                    "Close"))
                {
                    EditorUtility.RevealInFinder(fullPath);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[EasyAssetPackageTool] Package build failed: {e.Message}");
                EditorUtility.DisplayDialog("Build Failed", $"Failed to build package:\n{e.Message}", "OK");
            }
        }

        /// <summary>
        /// 設定をロードまたは新規作成
        /// </summary>
        private void LoadOrCreateSettings()
        {
            // 既存の設定アセットを検索
            settings = AssetDatabase.LoadAssetAtPath<EasyAssetPackageToolSettings>(SETTINGS_ASSET_PATH);

            if (settings == null)
            {
                // 設定アセットが存在しない場合は新規作成
                settings = CreateInstance<EasyAssetPackageToolSettings>();
                settings.ResetToDefault();

                // ディレクトリを確認
                string directory = Path.GetDirectoryName(SETTINGS_ASSET_PATH);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // アセットを作成
                AssetDatabase.CreateAsset(settings, SETTINGS_ASSET_PATH);
                AssetDatabase.SaveAssets();
                Debug.Log($"[EasyAssetPackageTool] Created new settings asset: {SETTINGS_ASSET_PATH}");
            }
        }

        /// <summary>
        /// 設定を保存
        /// </summary>
        private void SaveSettings()
        {
            if (settings != null)
            {
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
            }
        }

        /// <summary>
        /// デフォルト設定にリセット
        /// </summary>
        private void ResetToDefault()
        {
            if (settings != null)
            {
                settings.ResetToDefault();
                EditorUtility.SetDirty(settings);
                SaveSettings();
            }
        }
    }
}
