using UnityEngine;
using System.Collections.Generic;

namespace EasyAssetPackageTool.Editor
{
    /// <summary>
    /// EasyAssetPackageToolのプロジェクト固有設定を保存するScriptableObject
    /// </summary>
    public class EasyAssetPackageToolSettings : ScriptableObject
    {
        [Header("Basic Settings")]
        [Tooltip("パッケージの出力先ディレクトリ")]
        public string outputPath = "ExportPackage/";

        [Tooltip("パッケージ名（{version}でビルド時にバージョン入力）")]
        public string packageName = "MyPackage-v{version}";

        [Tooltip("依存関係を含めるかどうか")]
        public bool includeDependencies = false;

        [Header("Include Paths")]
        [Tooltip("パッケージに含めるパス（ディレクトリまたはファイル）")]
        public List<string> includePaths = new List<string>();

        [Header("Exclude Paths")]
        [Tooltip("除外するパス（ワイルドカード対応: **/*.cs）")]
        public List<string> excludePaths = new List<string>();

        [Header("Version")]
        [Tooltip("前回入力したバージョン（次回のデフォルト値として使用）")]
        public string lastInputVersion = "1.0.0";

        /// <summary>
        /// デフォルト設定にリセット
        /// </summary>
        public void ResetToDefault()
        {
            outputPath = "ExportPackage/";
            packageName = "MyPackage-v{version}";
            includeDependencies = false;
            lastInputVersion = "1.0.0";

            includePaths = new List<string>
            {
                "Assets/MyPackage"
            };

            excludePaths = new List<string>();
        }
    }
}
