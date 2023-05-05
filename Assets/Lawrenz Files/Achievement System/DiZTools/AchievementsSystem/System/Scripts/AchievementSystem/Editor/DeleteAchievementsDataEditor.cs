using System.IO;
using UnityEditor;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public class DeleteAchievementsDataEditor
    {
        [MenuItem(PathUtils.EDITOR_WINDOW_TOOL_PATH + "Delete Achievements Data")]
        private static void DeleteAchievementsDataEditorFunction()
        {
            string achievementFolderPath = Path.Combine(Application.persistentDataPath, PathUtils.achievementFolderName);
            string achievementSavePath = Path.Combine(achievementFolderPath, PathUtils.achievementFileName + PathUtils.saveExtension);

            // check if file exists
            if (!File.Exists(achievementSavePath))
            {
                Debug.Log("The file " + achievementSavePath + " does not exist yet, or already deleted.");
            }
            else
            {
                File.Delete(achievementSavePath);

                Debug.Log("The file " + achievementSavePath + " has been deleted.");
            }
        }
    }
}