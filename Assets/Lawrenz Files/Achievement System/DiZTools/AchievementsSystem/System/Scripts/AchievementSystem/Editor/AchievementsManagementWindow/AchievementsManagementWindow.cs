using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public class AchievementsManagementWindow : EditorWindow
    {
        //Editor Window
        private const string EDITOR_WINDOW_TITLE = "Achievements Management Window";

        //Paths
        private const string ACHIEVEMENTS_LIST_DEFAULT_PATH = "Assets/Plugins/DiZTools/AchievementsSystem/Customizables/Data/";
        private const string DEFAULT_ACHIEVEMENTS_ASSETS_ASSETS_PATH = "Assets/Plugins/DiZTools/AchievementsSystem/Customizables/Data/Achievements/";
        private const string DEFAULT_ACHIEVEMENTS_ASSETS_PLUGINS_PATH = "/Plugins/DiZTools/AchievementsSystem/Customizables/Data/Achievements/";

        //Constants
        private const string ACHIEVEMENTS_ASSETS_PATH_EDITOR_PREF = "DiZTools_AchievementsSystem_AchievementsManagementWindow_AchievementsAssetsPath_Prefs";
        private const string ACHIEVEMENT_ASSET_NAME_SUFFIX = "_ACHIEVEMENT";

        //Data
        private AchievementsList m_achievementsList;
        private List<Editor> m_achievementsEditorsList = new List<Editor>();
        private AchievementsDataSO m_achievementsDataSO;
        private string m_achievementsAssetsPath;

        private List<Type> m_achievementTypeList = new List<Type>();
        private Dictionary<Type, int> m_achievementTypeToIndexDict = new Dictionary<Type, int>();
        private Dictionary<Type, Type> m_achievementTypeToEditorDict = new Dictionary<Type, Type>();
        private string[] m_achievementTypeStringArray;

        //cache
        private bool m_editorsInitialized;
        private bool m_guiStyleInitialized;
        private Vector2 m_scrollPos;
        private string m_oldAchievementsAssetsPath;
        private bool m_displayAchievementsList;
        private int m_popupSelectedIndex;
        private bool m_needOnEnable;
        private bool m_firstGUILayoutCallOnLostFocus;
        private List<float> m_organisationButtonsBlockHeightList = new List<float>();
        private bool m_displayErrorMessages = false;
        private List<string> errorsToDisplay = new List<string>();

        #region GUI LABEL CONSTS

        //Title
        private const string PANEL_TITLE = "Achievements Management";

        //Fields
        private const string NUM_SYMBOL = ".";

        //Description
        private const string DESCRIPTION = "Here is the list of achievements, that you can setup and organize however you like.";

        //Achievements assets path
        private const string ACHIEVEMENTS_PATH_LABEL = "Achievements Assets Path";
        private const string BROWSE_ACHIEVEMENTS_PATH_BUTTON = "Browse";
        private const string BROWSE_ACHIEVEMENTS_PATH_TOOLTIP = "Browse the achievements assets directory path.";
        private const string BROWSE_ACHIEVEMENTS_PATH_FOLDER_PANEL_TITLE = "Choose Achievements Assets Directory Path";

        //Save Achievements
        private const string SAVE_ACHIEVEMENTS_BUTTON = "Save Achievements Assets & Generate Achievements Database";

        //Achievement type drop down
        private const string ACHIEVEMENT_TYPE_DROP_DOWN_LABEL = "Achievement type";

        //Buttons
        private const string ADD_FIRST_ACHIEVEMENT_BUTTON = "Add you first achievement !";
        private const string ADD = "+";
        private const string REMOVE = "-";
        private const char UP_ARROW = '\u25B4';
        private const char DOWN_ARROW = '\u25BE';

        //Warnings
        private const string ONLY_ONE_COMPLETIONIST_WARNING_TITLE = "Only one completionist achievement is authorized";
        private const string ONLY_ONE_COMPLETIONIST_WARNING_MESSAGE = "There is already one completionist achievement in the list !";

        #endregion

        [MenuItem(PathUtils.EDITOR_WINDOW_TOOL_PATH + EDITOR_WINDOW_TITLE, false, 10)]
        public static void ShowWindow()
        {
            GetWindow<AchievementsManagementWindow>(EDITOR_WINDOW_TITLE);
        }

        private void OnLostFocus()
        {
            m_firstGUILayoutCallOnLostFocus = true;
            m_needOnEnable = true;
        }

        protected void OnEnable()
        {
            //Search for AchievementsList ScriptableObject
            m_achievementsList = EditorUtils.FindFirstAssetOfType<AchievementsList>();

            //Create an AchievementsList ScriptableObject if you don't find one
            if (m_achievementsList == null)
            {
                AchievementsList asset = ScriptableObject.CreateInstance<AchievementsList>();
                string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(ACHIEVEMENTS_LIST_DEFAULT_PATH + typeof(AchievementsList).Name + ".asset");
                AssetDatabase.CreateAsset(asset, assetPathAndName);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                m_achievementsList = EditorUtils.FindFirstAssetOfType<AchievementsList>();
            }

            //Search for AchievementsDataSO ScriptableObject
            m_achievementsDataSO = EditorUtils.FindFirstAssetOfType<AchievementsDataSO>();

            //Create an AchievementsDataSO ScriptableObject if you don't find one
            if (m_achievementsDataSO == null)
            {
                AchievementsDataSO asset = ScriptableObject.CreateInstance<AchievementsDataSO>();
                string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(ACHIEVEMENTS_LIST_DEFAULT_PATH + typeof(AchievementsDataSO).Name.Replace("SO", "") + ".asset");
                AssetDatabase.CreateAsset(asset, assetPathAndName);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                m_achievementsDataSO = EditorUtils.FindFirstAssetOfType<AchievementsDataSO>();
            }

            //Initialize the default achievements assets path
            if (EditorPrefs.HasKey(ACHIEVEMENTS_ASSETS_PATH_EDITOR_PREF))
                m_achievementsAssetsPath = EditorPrefs.GetString(ACHIEVEMENTS_ASSETS_PATH_EDITOR_PREF);
            else
                m_achievementsAssetsPath = DEFAULT_ACHIEVEMENTS_ASSETS_ASSETS_PATH;

            //Initialize list and dictionary of achievement type and editor
            InitializeAchievementTypeDictionaries();

            m_editorsInitialized = false;
            m_guiStyleInitialized = false;
        }

        protected void OnGUI()
        {
            CheckNeedingOnEnable();

            if (EditorApplication.isCompiling)
                GUI.enabled = false;
            else
                GUI.enabled = true;

            InitializeEditors();
            InitializeGUIStyles();

            m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos);

            DisplayTitle();

            GUILayout.Space(10f);

            DisplayDescription();

            GUILayout.Space(10f);

            m_displayAchievementsList = DisplayAchievementsAssetsPath();
            SaveAchievementsAssetsButton();

            DisplayErrorMessages();

            //If the achievements assets path is a valid one
            if (m_displayAchievementsList)
            {
                EditorGUIUtility.labelWidth = 200f;

                //Diplay the AchievementsList
                EditorGUILayout.Space(10f);
                HorizontalLine(Color.gray);
                EditorGUILayout.Space(20f);

                for (int index = 0; index < m_achievementsEditorsList.Count; index++)
                {
                    m_organisationButtonsBlockHeightList.SetupListSize(m_achievementsEditorsList.Count);

                    EditorGUILayout.BeginHorizontal(GUILayout.Width(600f));

                    EditorGUILayout.BeginVertical(GUILayout.Width(500f));

                    DisplayAchievementType(index);

                    EditorGUILayout.Space(10f);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.Space(50f);
                    EditorGUILayout.BeginVertical();
                    m_achievementsEditorsList[index].OnInspectorGUI();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.EndVertical();

                    if (Event.current.type == EventType.Repaint)
                        m_organisationButtonsBlockHeightList[index] = GUILayoutUtility.GetLastRect().height;

                    DisplayOrganisationButtons(index, m_organisationButtonsBlockHeightList[index]);

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space(20f);
                    HorizontalLine(Color.gray);
                    EditorGUILayout.Space(20f);
                }

                if (m_achievementsEditorsList.Count == 0)
                {
                    AddFirstAchievementButton();

                    if (m_displayErrorMessages)
                        ResetErrorsToDisplay();
                }
            }

            EditorGUILayout.EndScrollView();

            m_firstGUILayoutCallOnLostFocus = false;
        }

        private void CheckNeedingOnEnable()
        {
            if (m_needOnEnable && !m_firstGUILayoutCallOnLostFocus && Event.current.type != EventType.Repaint && InternalEditorUtility.isApplicationActive)
            {
                OnEnable();
                m_needOnEnable = false;
            }
        }

        private void InitializeEditors()
        {
            if (m_editorsInitialized)
                return;

            m_achievementsEditorsList.Clear();
            for (int i = 0; i < m_achievementsList.achievements.Count; i++)
            {
                m_achievementsEditorsList.Add(Editor.CreateEditor(m_achievementsList.achievements[i], m_achievementTypeToEditorDict[m_achievementsList.achievements[i].GetType()]));
            }

            m_editorsInitialized = true;
        }

        private void InitializeGUIStyles()
        {
            if (m_guiStyleInitialized)
                return;

            InitGUIStyle_HorizontalLine();

            InitGUIStyle_PurpleLabel();
            InitGUIStyle_BlueLabel();

            InitGUIStyle_PurpleTextField();
            InitGUIStyle_BluePopup();

            InitGUIStyle_OrangeFontButtonLabel();

            InitGUIStyle_ErrorLabel();

            m_guiStyleInitialized = true;
        }

        private void DisplayTitle()
        {
            if (!EditorApplication.isCompiling)
            {
                GUILayout.Label(PANEL_TITLE, EditorStyles.boldLabel);
            }
            else
            {
                GUILayout.Label(PANEL_TITLE + " ( Compiling ... )", EditorStyles.boldLabel);
            }
        }

        private void DisplayDescription()
        {
            GUILayout.Label(DESCRIPTION);
        }

        private bool DisplayAchievementsAssetsPath()
        {
            m_oldAchievementsAssetsPath = m_achievementsAssetsPath;

            GUILayout.BeginHorizontal();
            GUILayout.Label(ACHIEVEMENTS_PATH_LABEL, m_purpleLabelGUIStyle, GUILayout.Width(170f));
            m_achievementsAssetsPath = EditorGUILayout.TextField(m_achievementsAssetsPath, m_purpleTextFieldGUIStyle);
            if (GUILayout.Button(new GUIContent(BROWSE_ACHIEVEMENTS_PATH_BUTTON, BROWSE_ACHIEVEMENTS_PATH_TOOLTIP), GUILayout.Width(66f)))
            {
                string newPath = EditorUtility.OpenFolderPanel(BROWSE_ACHIEVEMENTS_PATH_FOLDER_PANEL_TITLE, Application.dataPath + DEFAULT_ACHIEVEMENTS_ASSETS_PLUGINS_PATH, "").ToAssetsPath();
                if (!newPath.IsNullOrEmpty())
                    m_achievementsAssetsPath = newPath;
            }
            GUILayout.EndHorizontal();

            if (m_achievementsAssetsPath != m_oldAchievementsAssetsPath)
                EditorPrefs.SetString(ACHIEVEMENTS_ASSETS_PATH_EDITOR_PREF, m_achievementsAssetsPath);

            return true;
        }

        private void DisplayAchievementType(int index)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(25f);
            GUILayout.Label((index + 1).ToString() + NUM_SYMBOL + " " + ACHIEVEMENT_TYPE_DROP_DOWN_LABEL, m_blueLabelGUIStyle, GUILayout.Width(160f));
            m_popupSelectedIndex = EditorGUILayout.Popup(m_achievementTypeToIndexDict[m_achievementsList.achievements[index].GetType()], m_achievementTypeStringArray, m_bluePopupGUIStyle);
            EditorGUILayout.Space(25f);
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
                ReplaceAchievement(index, m_popupSelectedIndex);
        }

        private void DisplayOrganisationButtons(int index, float sideBlockHeight)
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(100f), GUILayout.Height(sideBlockHeight));
            GUILayout.FlexibleSpace();

            EditorGUILayout.BeginHorizontal(GUILayout.Height(20f));
            GUILayout.FlexibleSpace();
            AddAchievementButton(index);
            SwitchUpAchievementButton(index);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal(GUILayout.Height(20f));
            GUILayout.FlexibleSpace();
            RemoveAchievementButton(index);
            SwitchDownAchievementButton(index);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndVertical();
        }

        private bool CheckGenerationData()
        {
            bool doGenerate = true;

            //Check if the achievements assets path is not null nor empty nor full spaces, and if it is an existing path
            if (m_achievementsAssetsPath.IsNullOrEmpty())
            {
                AddErrorToList("\"" + ACHIEVEMENTS_PATH_LABEL + "\" is empty.");
                doGenerate = false;
            }
            else if (!Directory.Exists(m_achievementsAssetsPath.ToAbsolutePath()))
            {
                AddErrorToList("\"" + ACHIEVEMENTS_PATH_LABEL + "\" : " + m_achievementsAssetsPath + " does not exist.");
                doGenerate = false;
            }

            //Check if the nameID of all achievements are all different
            #region CHECK NAME_ID

            Dictionary<string, List<string>> nameIDsAndOrdinals = new Dictionary<string, List<string>>();

            for (int i = 0; i < m_achievementsList.achievements.Count; i++)
            {
                if (!nameIDsAndOrdinals.ContainsKey(m_achievementsList.achievements[i].NameID))
                    nameIDsAndOrdinals.Add(m_achievementsList.achievements[i].NameID, new List<string>());

                nameIDsAndOrdinals[m_achievementsList.achievements[i].NameID].Add((i + 1).ToOrdinal());
            }

            foreach (KeyValuePair<string, List<string>> nameIDAndOrdinals in nameIDsAndOrdinals)
            {
                if (nameIDAndOrdinals.Value.Count > 1)
                {
                    AddErrorToList("The " + nameIDAndOrdinals.Value.ToEnumeration() + " achievements should not have the same nameID.");
                    doGenerate = false;
                }
            }

            #endregion

            return doGenerate;
        }

        private void SaveAchievementsAssetsButton()
        {
            if (GUILayout.Button(SAVE_ACHIEVEMENTS_BUTTON, m_orangeFontButtonGUIStyle))
            {
                if (m_displayErrorMessages)
                    ResetErrorsToDisplay();

                if (!CheckGenerationData())
                    return;

                //Rename assets
                string newName, renameOut = "";
                bool renameSuccess = false;
                while (!renameSuccess)
                {
                    renameSuccess = true;

                    for (int i = 0; i < m_achievementsList.achievements.Count; i++)
                    {
                        newName = i + "_" + m_achievementsList.achievements[i].NameID.Replace(" ", "_") + ACHIEVEMENT_ASSET_NAME_SUFFIX;
                        if (m_achievementsList.achievements[i].name != newName)
                            renameOut = AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(m_achievementsList.achievements[i]), newName);

                        if (renameOut != "")
                        {
                            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(m_achievementsList.achievements[i]), newName + " temp");

                            renameSuccess = false;
                        }
                    }
                }

                //Generate Achievements Database
                GenerateAchievementsDatabase();

                //Save all modifications
                AssetDatabase.SaveAssets();
            }
        }

        private void GenerateAchievementsDatabase()
        {
            m_achievementsDataSO.AchievementsDone.Clear();
            m_achievementsDataSO.AchievementsDataBool.Clear();
            m_achievementsDataSO.AchievementsDataInt.Clear();

            string achievementDataName;
            for (int i = 0; i < m_achievementsList.achievements.Count; i++)
            {
                m_achievementsDataSO.AchievementsDone.Add(new StringBoolTuple(m_achievementsList.achievements[i].NameID, false));

                if (m_achievementsList.achievements[i].GetType() == typeof(AchievementEvent))
                {
                    achievementDataName = (m_achievementsList.achievements[i] as AchievementEvent).AchievementDataName;
                    if (!achievementDataName.ExistsInTupleList(m_achievementsDataSO.AchievementsDataBool))
                        m_achievementsDataSO.AchievementsDataBool.Add(new StringBoolTuple(achievementDataName, false));
                }
                else if (m_achievementsList.achievements[i].GetType() == typeof(AchievementAction))
                {
                    achievementDataName = (m_achievementsList.achievements[i] as AchievementAction).AchievementDataName;
                    if (!achievementDataName.ExistsInTupleList(m_achievementsDataSO.AchievementsDataInt))
                        m_achievementsDataSO.AchievementsDataInt.Add(new StringIntTuple(achievementDataName, 0));
                }
            }

            EditorUtility.SetDirty(m_achievementsDataSO);
        }

        private void DisplayErrorMessages()
        {
            if (m_displayErrorMessages)
            {
                GUILayout.Label("ERRORS :", m_errorLabelGUIStyle);

                for (int errorIndex = 0; errorIndex < errorsToDisplay.Count; errorIndex++)
                {
                    GUILayout.Label("- " + errorsToDisplay[errorIndex], m_errorLabelGUIStyle);
                }
            }
        }

        #region ORGANISATION BUTTONS

        private void AddFirstAchievementButton()
        {
            if (GUILayout.Button(ADD_FIRST_ACHIEVEMENT_BUTTON))
            {
                AddAchievement(0);
            }
        }

        private void AddAchievementButton(int index)
        {
            if (GUILayout.Button(ADD, GUILayout.Width(20f)))
            {
                if (m_displayErrorMessages)
                    ResetErrorsToDisplay();

                AddAchievement(index + 1);
            }
        }

        private void RemoveAchievementButton(int index)
        {
            if (GUILayout.Button(REMOVE, GUILayout.Width(20f)))
            {
                if (m_displayErrorMessages)
                    ResetErrorsToDisplay();

                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(m_achievementsList.achievements[index]));

                m_achievementsList.achievements.RemoveAt(index);

                m_achievementsEditorsList.RemoveAt(index);

                EditorUtility.SetDirty(m_achievementsList);
            }
        }

        private void SwitchUpAchievementButton(int index)
        {
            if (GUILayout.Button(UP_ARROW.ToString(), GUILayout.Width(20f)))
            {
                if (m_displayErrorMessages)
                    ResetErrorsToDisplay();

                if (index == 0)
                    return;

                m_achievementsList.achievements.Switch(index, index - 1);

                m_achievementsEditorsList.Switch(index, index - 1);

                EditorUtility.SetDirty(m_achievementsList);
            }
        }

        private void SwitchDownAchievementButton(int index)
        {
            if (GUILayout.Button(DOWN_ARROW.ToString(), GUILayout.Width(20f)))
            {
                if (m_displayErrorMessages)
                    ResetErrorsToDisplay();

                if (index == m_achievementsList.achievements.Count - 1)
                    return;

                m_achievementsList.achievements.Switch(index, index + 1);

                m_achievementsEditorsList.Switch(index, index + 1);

                EditorUtility.SetDirty(m_achievementsList);
            }
        }

        #endregion

        #region ORGANISATION ACHIEVEMENTS

        private void InitializeAchievementTypeDictionaries()
        {
            m_achievementTypeList.Clear();
            m_achievementTypeToIndexDict.Clear();
            m_achievementTypeToEditorDict.Clear();

            m_achievementTypeList.Add(typeof(AchievementEvent));
            m_achievementTypeToEditorDict.Add(typeof(AchievementEvent), typeof(AchievementEventEditor));

            m_achievementTypeList.Add(typeof(AchievementAction));
            m_achievementTypeToEditorDict.Add(typeof(AchievementAction), typeof(AchievementActionEditor));

            m_achievementTypeList.Add(typeof(AchievementCompletionist));
            m_achievementTypeToEditorDict.Add(typeof(AchievementCompletionist), typeof(AchievementCompletionistEditor));

            m_achievementTypeStringArray = new string[m_achievementTypeList.Count];
            for (int i = 0; i < m_achievementTypeList.Count; i++)
            {
                m_achievementTypeToIndexDict.Add(m_achievementTypeList[i], i);

                m_achievementTypeStringArray[i] = m_achievementTypeList[i].Name.ToString();
            }
        }

        private void AddAchievement(int index)
        {
            Achievement achievement = CreateAchievementAsset(m_achievementTypeList[0], index);

            m_achievementsList.achievements.AddAt(achievement, index);

            m_achievementsEditorsList.AddAt(Editor.CreateEditor(m_achievementsList.achievements[index], m_achievementTypeToEditorDict[m_achievementTypeList[0]]), index);

            EditorUtility.SetDirty(m_achievementsList);
        }

        private Achievement CreateAchievementAsset(Type achievementType, int index, string name = "")
        {
            Achievement asset = ScriptableObject.CreateInstance(achievementType) as Achievement;

            string newPath = m_achievementsAssetsPath + index + "_" + name.Replace(" ", "_") + ACHIEVEMENT_ASSET_NAME_SUFFIX + ".asset";

            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(newPath));
            AssetDatabase.SaveAssets();

            return asset;
        }

        private void ReplaceAchievement(int index, int newAchievementTypeIndex)
        {
            //If the new achievement type is a "Completionist Achievement", check if we already have one, because we don't want two of these
            if (m_achievementTypeList[newAchievementTypeIndex] == typeof(AchievementCompletionist))
            {
                for (int i = 0; i < m_achievementsList.achievements.Count; i++)
                {
                    if (m_achievementsList.achievements[i].GetType() == m_achievementTypeList[newAchievementTypeIndex])
                    {
                        if (EditorUtility.DisplayDialog(ONLY_ONE_COMPLETIONIST_WARNING_TITLE, ONLY_ONE_COMPLETIONIST_WARNING_MESSAGE, "OK"))
                            return;
                    }
                }
            }

            //Get old achievement
            Achievement oldAchievement = m_achievementsList.achievements[index];

            //Create and update new achievement
            Achievement newAchievement = CreateAchievementAsset(m_achievementTypeList[newAchievementTypeIndex], index, oldAchievement.NameID);
            newAchievement.PasteValues(oldAchievement.NameID, oldAchievement.Description, oldAchievement.Thumbnail);

            //Delete the old achievement asset
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(oldAchievement));

            //Replace old achievement by new achievement in the achievements list
            m_achievementsList.achievements[index] = newAchievement;

            //Replace old achievement editor by new achievement editor
            m_achievementsEditorsList[index] = Editor.CreateEditor(m_achievementsList.achievements[index], m_achievementTypeToEditorDict[m_achievementsList.achievements[index].GetType()]);

            EditorUtility.SetDirty(m_achievementsList);
        }

        #endregion

        #region GUI STYLE

        private GUIStyle horizontalLine;

        private GUIStyle m_purpleLabelGUIStyle;
        private GUIStyle m_blueLabelGUIStyle;

        private GUIStyle m_purpleTextFieldGUIStyle;
        private GUIStyle m_bluePopupGUIStyle;

        private GUIStyle m_orangeFontButtonGUIStyle;

        private GUIStyle m_errorLabelGUIStyle;

        private Color RedColor = new Color(1f, 0f, 0f);
        private Color OrangeColor = new Color(1f, 0.6f, 0f);
        private Color PurpleColor = new Color(0.4f, 0.15f, 1f);
        private Color BlueColor = new Color(0f, 0.6f, 0.75f);

        private void InitGUIStyle_HorizontalLine()
        {
            horizontalLine = new GUIStyle();
            horizontalLine.normal.background = EditorGUIUtility.whiteTexture;
            horizontalLine.margin = new RectOffset(25, 25, 4, 4);
            horizontalLine.fixedHeight = 1;
        }

        private void InitGUIStyle_PurpleLabel()
        {
            m_purpleLabelGUIStyle = new GUIStyle(GUI.skin.label);
            m_purpleLabelGUIStyle.fontStyle = FontStyle.Bold;
            m_purpleLabelGUIStyle.normal.textColor = PurpleColor;
        }

        private void InitGUIStyle_BlueLabel()
        {
            m_blueLabelGUIStyle = new GUIStyle(GUI.skin.label);
            m_blueLabelGUIStyle.fontStyle = FontStyle.Bold;
            m_blueLabelGUIStyle.normal.textColor = BlueColor;
        }

        private void InitGUIStyle_PurpleTextField()
        {
            m_purpleTextFieldGUIStyle = new GUIStyle(GUI.skin.textField);
            m_purpleTextFieldGUIStyle.normal.textColor = PurpleColor;
            m_purpleTextFieldGUIStyle.hover.textColor = PurpleColor;
            m_purpleTextFieldGUIStyle.focused.textColor = PurpleColor;
            m_purpleTextFieldGUIStyle.active.textColor = PurpleColor;
        }

        private void InitGUIStyle_BluePopup()
        {
            m_bluePopupGUIStyle = new GUIStyle(EditorStyles.popup);
            m_bluePopupGUIStyle.normal.textColor = BlueColor;
            m_bluePopupGUIStyle.hover.textColor = BlueColor;
            m_bluePopupGUIStyle.focused.textColor = BlueColor;
            m_bluePopupGUIStyle.active.textColor = BlueColor;
        }

        private void InitGUIStyle_OrangeFontButtonLabel()
        {
            m_orangeFontButtonGUIStyle = new GUIStyle(GUI.skin.button);
            m_orangeFontButtonGUIStyle.fontStyle = FontStyle.Bold;
            m_orangeFontButtonGUIStyle.normal.textColor = OrangeColor;
            m_orangeFontButtonGUIStyle.hover.textColor = OrangeColor;
            m_orangeFontButtonGUIStyle.focused.textColor = OrangeColor;
            m_orangeFontButtonGUIStyle.active.textColor = OrangeColor;
        }

        private void InitGUIStyle_ErrorLabel()
        {
            m_errorLabelGUIStyle = new GUIStyle(GUI.skin.label);
            m_errorLabelGUIStyle.normal.textColor = RedColor;
            m_errorLabelGUIStyle.wordWrap = true;
        }

        private void HorizontalLine(Color color)
        {
            var c = GUI.color;
            GUI.color = color;
            GUILayout.Box(GUIContent.none, horizontalLine);
            GUI.color = c;
        }

        #endregion

        #region Errors Management

        private void AddErrorToList(string error)
        {
            m_displayErrorMessages = true;

            errorsToDisplay.Add(error);
        }

        private void ResetErrorsToDisplay()
        {
            m_displayErrorMessages = false;

            errorsToDisplay.Clear();
        }

        #endregion
    }
}
