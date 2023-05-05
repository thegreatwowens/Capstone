using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public static class PathUtils
    {
        #region Saves

        public const string saveExtension = ".save";

        //Achievements Folder on Application.persistentDataPath
        public const string achievementFolderName = "Achievements";
        public const string achievementFileName = "AchievementsData";

        #endregion

        #region Editor

        //Editor Window Tool Path
        public const string EDITOR_WINDOW_TOOL_PATH = "Tools/DiZ Tools/Achievements System/";

        public static string ToAssetsPath(this string absolutePath)
        {
            return absolutePath.Replace("\\", "/").Replace(Application.dataPath, "Assets");
        }

        public static string ToAbsolutePath(this string assetsPath)
        {
            return assetsPath.Replace("Assets", Application.dataPath);
        }

        #endregion
    }
}
