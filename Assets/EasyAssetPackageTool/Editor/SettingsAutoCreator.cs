using UnityEngine;
using UnityEditor;
using System.IO;

namespace EasyAssetPackageTool.Editor
{
    /// <summary>
    /// 設定アセットが存在しない場合に自動生成するトリガー
    /// </summary>
    [InitializeOnLoad]
    internal static class SettingsAutoCreator
    {
        static SettingsAutoCreator()
        {
            EditorApplication.delayCall += EnsureSettingsExist;
        }

        private static void EnsureSettingsExist()
        {
            if (Application.isBatchMode)
                return;

            var settings = AssetDatabase.LoadAssetAtPath<EasyAssetPackageToolSettings>(
                EasyAssetPackageTool.SETTINGS_ASSET_PATH);
            if (settings != null)
                return;

            // 設定アセットを自動生成
            settings = ScriptableObject.CreateInstance<EasyAssetPackageToolSettings>();
            settings.ResetToDefault();

            string directory = Path.GetDirectoryName(EasyAssetPackageTool.SETTINGS_ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            AssetDatabase.CreateAsset(settings, EasyAssetPackageTool.SETTINGS_ASSET_PATH);
            AssetDatabase.SaveAssets();
            Debug.Log($"[EasyAssetPackageTool] Settings asset created: {EasyAssetPackageTool.SETTINGS_ASSET_PATH}");
        }
    }
}
