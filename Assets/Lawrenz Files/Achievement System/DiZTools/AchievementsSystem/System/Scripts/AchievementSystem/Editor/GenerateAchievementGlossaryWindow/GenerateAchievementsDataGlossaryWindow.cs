using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    public class GenerateAchievementsDataGlossaryWindow : EditorWindow
    {
        //Editor Window
        private const string EDITOR_WINDOW_TITLE = "Generate Achievements Data Glossary Window";

        //Default Fields
        private const string DEFAULT_ACHIEVEMENTS_GLOSSARY_ASSEMBLY_ASSETS_PATH = "Assets/Plugins/DiZTools/AchievementsSystem/Customizables/";
        private const string DEFAULT_ACHIEVEMENTS_GLOSSARY_ASSEMBLY_PLUGINS_PATH = "/Plugins/DiZTools/AchievementsSystem/Customizables/";

        //Fields Structures
        private bool m_achievementsGlossaryAssemblyExists;
        private string m_achievementsGlossaryAssemblyName;
        private string m_achievementsGlossaryAssemblyPath;
        private string m_achievementsGlossaryClassName;
        private List<string> m_achievementsGlossaryConstants = new List<string>();

        //Fields Defaults
        private string m_defaultAchievementsGlossaryAssemblyName = "AchievementsDataGlossaryAssembly";
        private string m_defaultAchievementsGlossaryAssemblyPath = DEFAULT_ACHIEVEMENTS_GLOSSARY_ASSEMBLY_ASSETS_PATH;
        private string m_defaultAchievementsGlossaryClassName = "AchievementsDataGlossary";

        //Save Fields
        private string m_savedAchievementsGlossaryAssemblyName;
        private string m_savedAchievementsGlossaryAssemblyPath;

        //cache
        private bool m_windowInitialized;
        private bool m_guiStyleInitialized = false;
        private Vector2 m_scrollPos;
        private bool m_areFieldsDisplayed;
        private bool m_displayErrorMessages = false;
        private List<string> errorsToDisplay = new List<string>();
        private CodeDomProvider m_provider = CodeDomProvider.CreateProvider("C#");

        #region GUI LABEL CONSTS

        //Title
        private const string PANEL_TITLE = "Generate Achievements Data Glossary";

        //Fields
        private const string ASSEMBLY_NAME_LABEL = "Assembly Name";
        private const string ASSEMBLY_PATH_LABEL = "Assembly Directory Path";
        private const string BROWSE_ASSEMBLY_PATH_BUTTON = "Browse";
        private const string BROWSE_ASSEMBLY_PATH_TOOLTIP = "Browse the assembly directory path.";
        private const string BROWSE_ASSEMBLY_PATH_FOLDER_PANEL_TITLE = "Choose Assembly Directory Path";
        private const string CLASS_NAME_LABEL = "Class Name";
        private const string VALUE_LABEL = "Constant";
        private const string NUM_SYMBOL = "n°";
        private const string ADD = "+";
        private const string ADD_VALUE_BUTTON_TOOLTIP = "Add a custom constant to the class after this one.";
        private const string REMOVE = "-";
        private const string REMOVE_VALUE_BUTTON_TOOLTIP = "Remove this custom constant from the class.";

        //Add first assembly
        private const string ADD_FIRST_ASSEMBLY_BUTTON = "Add your first achievements data glossary !";

        //Sort Button
        private const string SORT_CONSTANTS_BUTTON = "Sort Constants";
        private const string SORT_CONSTANTS_BUTTON_TOOLTIP = "Sort Constants above.";

        //Reset fields
        private const string RESET_FIELDS_BUTTON = "Reset Fields";
        private const string RESET_FIELDS_BUTTON_TOOLTIP = "Reset the fields to the default values, corresponding to your current \"Achievements Data Glossary\" assembly if you already have one.";

        //Generate achievements glossary
        private const string GENERATE_ACHIEVEMENTS_GLOSSARY_BUTTON = "Generate Achievements Data Glossary";

        //Delete achievements glossary button
        private const string DELETE_ACHIEVEMENTS_GLOSSARY_ASSEMBLY_BUTTON = "Delete achievements data glossary";
        private const string DELETE_ACHIEVEMENTS_GLOSSARY_ASSEMBLY_POPUP_TITLE = "Delete achievements data glossary ?";
        private const string DELETE_ACHIEVEMENTS_GLOSSARY_ASSEMBLY_POPUP_DESC = "Are you sure you want to delete your achievements data glossary ?";

        #endregion

        [MenuItem(PathUtils.EDITOR_WINDOW_TOOL_PATH + EDITOR_WINDOW_TITLE, false, 20)]
        public static void ShowWindow()
        {
            GetWindow<GenerateAchievementsDataGlossaryWindow>(EDITOR_WINDOW_TITLE);
        }

        protected void OnEnable()
        {
            m_windowInitialized = false;

            SearchForExistentAchievementsGlossary();
            SaveFields();

            m_windowInitialized = true;
        }

        protected void OnGUI()
        {
            if (EditorApplication.isCompiling)
                GUI.enabled = false;
            else
                GUI.enabled = true;

            InitializeGUIStyle();

            if (!m_windowInitialized)
            {
                GUILayout.Label("Initializing...");
            }
            else
            {
                m_scrollPos = EditorGUILayout.BeginScrollView(m_scrollPos);

                DisplayTitle();

                GUILayout.Space(10f);

                EditorGUIUtility.labelWidth = 200f;

                m_areFieldsDisplayed = DisplayFields();

                if (!m_areFieldsDisplayed)
                {
                    AddFirstAssemblyButton();

                    if (m_displayErrorMessages)
                        ResetErrorsToDisplay();
                }
                else
                {
                    GUILayout.Space(10f);

                    SortAchievementsGlossaryButton();

                    GUILayout.Space(10f);

                    ResetFieldsButton();
                    GenerateAchievementsGlossaryButton();
                }

                DisplayErrorMessages();

                GUILayout.Space(10f);

                DeleteAchievementsGlossaryAssemblyButton();

                EditorGUILayout.EndScrollView();
            }
        }

        private void InitializeGUIStyle()
        {
            if (m_guiStyleInitialized)
                return;

            InitGUIStyle_PurpleLabel();
            InitGUIStyle_BlueLabel();
            InitGUIStyle_GreenLabel();

            InitGUIStyle_PurpleTextField();
            InitGUIStyle_BlueTextField();
            InitGUIStyle_GreenTextField();

            InitGUIStyle_WhiteFontButtonLabel();
            InitGUIStyle_RedFontButtonLabel();

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

        private bool DisplayFields()
        {
            if (!m_achievementsGlossaryAssemblyExists)
                return false;

            GUILayout.BeginHorizontal();
            GUILayout.Label(ASSEMBLY_NAME_LABEL, m_purpleLabelGUIStyle, GUILayout.Width(160f));
            m_achievementsGlossaryAssemblyName = EditorGUILayout.TextField(m_achievementsGlossaryAssemblyName, m_purpleTextFieldGUIStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(ASSEMBLY_PATH_LABEL, m_purpleLabelGUIStyle, GUILayout.Width(160f));
            m_achievementsGlossaryAssemblyPath = EditorGUILayout.TextField(m_achievementsGlossaryAssemblyPath, m_purpleTextFieldGUIStyle);
            if (GUILayout.Button(new GUIContent(BROWSE_ASSEMBLY_PATH_BUTTON, BROWSE_ASSEMBLY_PATH_TOOLTIP), GUILayout.Width(66f)))
            {
                string newPath = EditorUtility.OpenFolderPanel(BROWSE_ASSEMBLY_PATH_FOLDER_PANEL_TITLE, Application.dataPath + DEFAULT_ACHIEVEMENTS_GLOSSARY_ASSEMBLY_PLUGINS_PATH, "").ToAssetsPath();
                if (!newPath.IsNullOrEmpty())
                    m_achievementsGlossaryAssemblyPath = newPath;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(20f));
            GUILayout.Label(CLASS_NAME_LABEL, m_blueLabelGUIStyle, GUILayout.Width(160f));
            m_achievementsGlossaryClassName = EditorGUILayout.TextField(m_achievementsGlossaryClassName, m_blueTextFieldGUIStyle);
            GUILayout.EndHorizontal();

            for (int m_constantIndex = 0; m_constantIndex < m_achievementsGlossaryConstants.Count; m_constantIndex++)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("", GUILayout.Width(43f));
                GUILayout.Label(VALUE_LABEL + " " + NUM_SYMBOL + (m_constantIndex + 1).ToString(), m_greenLabelGUIStyle, GUILayout.Width(160f));
                m_achievementsGlossaryConstants[m_constantIndex] = EditorGUILayout.TextField(m_achievementsGlossaryConstants[m_constantIndex], m_greenTextFieldGUIStyle);
                GUI.backgroundColor = GreenColor;
                if (GUILayout.Button(new GUIContent(ADD, ADD_VALUE_BUTTON_TOOLTIP), m_whiteFontButtonGUIStyle, GUILayout.Width(20f)))
                {
                    m_achievementsGlossaryConstants.AddAt("", m_constantIndex + 1);
                }
                if (m_constantIndex != 0)
                {
                    if (GUILayout.Button(new GUIContent(REMOVE, REMOVE_VALUE_BUTTON_TOOLTIP), m_whiteFontButtonGUIStyle, GUILayout.Width(20f)))
                    {
                        m_achievementsGlossaryConstants.RemoveAt(m_constantIndex);
                        continue;
                    }
                }
                else
                {
                    GUILayout.Space(23f);
                }
                GUI.backgroundColor = WhiteColor;

                GUILayout.EndHorizontal();
            }

            return true;
        }

        private void AddFirstAssemblyButton()
        {
            if (GUILayout.Button(new GUIContent(ADD_FIRST_ASSEMBLY_BUTTON)))
            {
                m_achievementsGlossaryAssemblyExists = true;
                m_achievementsGlossaryAssemblyName = m_defaultAchievementsGlossaryAssemblyName;
                m_achievementsGlossaryAssemblyPath = m_defaultAchievementsGlossaryAssemblyPath;
                m_achievementsGlossaryClassName = m_defaultAchievementsGlossaryClassName;
                m_achievementsGlossaryConstants.Clear();
                m_achievementsGlossaryConstants.Add("");
            }
        }

        private void SortAchievementsGlossaryButton()
        {
            if (GUILayout.Button(new GUIContent(SORT_CONSTANTS_BUTTON, SORT_CONSTANTS_BUTTON_TOOLTIP)))
            {
                SortAchievementsGlossary();
            }
        }

        private void SortAchievementsGlossary()
        {
            m_achievementsGlossaryConstants.Sort();
        }

        private void ResetFieldsButton()
        {
            if (GUILayout.Button(new GUIContent(RESET_FIELDS_BUTTON, RESET_FIELDS_BUTTON_TOOLTIP)))
            {
                ResetErrorsToDisplay();

                m_windowInitialized = false;
                SearchForExistentAchievementsGlossary();
                m_windowInitialized = true;
            }
        }

        private void SearchForExistentAchievementsGlossary()
        {
            //Get the AchievementsGlossaryAssembly through the list of assemblies in the current application domain.
            Assembly achievementsGlossaryAssembly = GenerationUtils.GetAssemblyWithPrivateKey(GenerationUtils.ACHIEVEMENTS_GLOSSARY_ASSEMBLY_PRIVATE_KEY);

            //If the AchievementsGlossaryAssembly is found, set the different fields
            if (achievementsGlossaryAssembly != null)
            {
                m_achievementsGlossaryAssemblyName = achievementsGlossaryAssembly.GetName().Name;
                m_achievementsGlossaryAssemblyPath = achievementsGlossaryAssembly.Location.ToAssetsPath().Split(new string[] { m_achievementsGlossaryAssemblyName + ".dll" }, StringSplitOptions.None)[0];
                m_achievementsGlossaryClassName = achievementsGlossaryAssembly.GetTypes()[0].Name;

                m_achievementsGlossaryConstants.Clear();
                FieldInfo[] fields = achievementsGlossaryAssembly.GetTypes()[0].GetFields();
                for (int fieldIndex = 0; fieldIndex < fields.Length; fieldIndex++)
                {
                    m_achievementsGlossaryConstants.Add(fields[fieldIndex].GetRawConstantValue().ToString());
                }

                SortAchievementsGlossary();

                m_achievementsGlossaryAssemblyExists = true;
            }
            else
            {
                m_achievementsGlossaryAssemblyExists = false;
            }
        }

        private void GenerateAchievementsGlossaryButton()
        {
            if (GUILayout.Button(GENERATE_ACHIEVEMENTS_GLOSSARY_BUTTON))
            {
                ResetErrorsToDisplay();

                if (CheckGenerationData())
                    GenerateAchievementsGlossary();
            }
        }

        private bool CheckGenerationData()
        {
            bool doGenerate = true;

            //Check if the assembly name is a valid name
            if (!m_provider.IsValidIdentifier(m_achievementsGlossaryAssemblyName))
            {
                AddErrorToList("\"" + ASSEMBLY_NAME_LABEL + "\" is not a valid assembly name.");
                doGenerate = false;
            }

            //Check if the assembly path is not null nor empty nor full spaces, and if it is an existing path
            if (m_achievementsGlossaryAssemblyPath.IsNullOrEmpty())
            {
                AddErrorToList("\"" + ASSEMBLY_PATH_LABEL + "\" is empty.");
                doGenerate = false;
            }
            else if (!Directory.Exists(m_achievementsGlossaryAssemblyPath.ToAbsolutePath()))
            {
                AddErrorToList("\"" + ASSEMBLY_PATH_LABEL + "\" : " + m_achievementsGlossaryAssemblyPath + " does not exist.");
                doGenerate = false;
            }

            //Check if the achievements glossary class name is a valid name
            if (!m_provider.IsValidIdentifier(m_achievementsGlossaryClassName))
            {
                AddErrorToList("\"" + CLASS_NAME_LABEL + "\" is not a valid class name.");
                doGenerate = false;
            }

            //Check if the values names are valid and all different
            #region CHECK VALUES NAMES

            Dictionary<string, List<string>> valuesNamesAndOrdinals = new Dictionary<string, List<string>>();
            List<string> valuesNamesNotValidOrdinals = new List<string>();

            for (int m_valueIndex = 0; m_valueIndex < m_achievementsGlossaryConstants.Count; m_valueIndex++)
            {
                if (!m_provider.IsValidIdentifier(m_achievementsGlossaryConstants[m_valueIndex]) &&
                    !m_achievementsGlossaryConstants[m_valueIndex].Split(' ').AreSeveralAndAllNotNullOrEmpty())
                {
                    valuesNamesNotValidOrdinals.Add((m_valueIndex + 1).ToOrdinal());
                }
                else
                {
                    if (!valuesNamesAndOrdinals.ContainsKey(m_achievementsGlossaryConstants[m_valueIndex]))
                        valuesNamesAndOrdinals.Add(m_achievementsGlossaryConstants[m_valueIndex], new List<string>());

                    valuesNamesAndOrdinals[m_achievementsGlossaryConstants[m_valueIndex]].Add((m_valueIndex + 1).ToOrdinal());
                }
            }

            if (valuesNamesNotValidOrdinals.Count != 0)
            {
                if (valuesNamesNotValidOrdinals.Count == 1)
                    AddErrorToList("The " + valuesNamesNotValidOrdinals.ToEnumeration() + " \"" + VALUE_LABEL + "\" is not a valid constant name.");
                else
                    AddErrorToList("The " + valuesNamesNotValidOrdinals.ToEnumeration() + " \"" + VALUE_LABEL + "\" are not valid constants names.");
                doGenerate = false;
            }

            foreach (KeyValuePair<string, List<string>> valueNameAndOrdinals in valuesNamesAndOrdinals)
            {
                if (valueNameAndOrdinals.Value.Count > 1)
                {
                    AddErrorToList("The " + valueNameAndOrdinals.Value.ToEnumeration() + " \"" + VALUE_LABEL + "\" should not have the same name.");
                    doGenerate = false;
                }
            }

            #endregion

            return doGenerate;
        }

        private void GenerateAchievementsGlossary()
        {
            (AssemblyName aName, AssemblyBuilder ab, ModuleBuilder mb) = GenerationUtils.GenerateAssembly(m_achievementsGlossaryAssemblyName, m_achievementsGlossaryAssemblyPath.ToAbsolutePath());

            GenerationUtils.GenerateClass(mb, m_achievementsGlossaryClassName, m_achievementsGlossaryConstants);
            GenerationUtils.GeneratePrivateKey(mb, GenerationUtils.ACHIEVEMENTS_GLOSSARY_ASSEMBLY_PRIVATE_KEY);

            GenerationUtils.SaveAssembly(aName, ab);

            if (m_savedAchievementsGlossaryAssemblyName != m_achievementsGlossaryAssemblyName ||
                m_savedAchievementsGlossaryAssemblyPath != m_achievementsGlossaryAssemblyPath)
            {
                string assemblyPath = Path.Combine(m_savedAchievementsGlossaryAssemblyPath.ToAbsolutePath(), m_savedAchievementsGlossaryAssemblyName);
                if (File.Exists(assemblyPath + ".dll"))
                {
                    File.Delete(assemblyPath + ".dll");
                    File.Delete(assemblyPath + ".dll.meta");
                }

                SaveFields();
            }

            m_achievementsGlossaryAssemblyExists = true;

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
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

        private void SaveFields()
        {
            m_savedAchievementsGlossaryAssemblyName = m_achievementsGlossaryAssemblyName;
            m_savedAchievementsGlossaryAssemblyPath = m_achievementsGlossaryAssemblyPath;
        }

        private void DeleteAchievementsGlossaryAssemblyButton()
        {
            if (GUILayout.Button(DELETE_ACHIEVEMENTS_GLOSSARY_ASSEMBLY_BUTTON, m_orangeFontButtonGUIStyle))
            {
                if (EditorUtility.DisplayDialog(DELETE_ACHIEVEMENTS_GLOSSARY_ASSEMBLY_POPUP_TITLE, DELETE_ACHIEVEMENTS_GLOSSARY_ASSEMBLY_POPUP_DESC, "YES", "NO !"))
                {
                    List<Assembly> customGlossariesAssemblies = GenerationUtils.GetAssembliesWithPrivateKey(GenerationUtils.ACHIEVEMENTS_GLOSSARY_ASSEMBLY_PRIVATE_KEY);

                    for (int i = customGlossariesAssemblies.Count - 1; i >= 0; i--)
                    {
                        if (File.Exists(customGlossariesAssemblies[i].Location))
                        {
                            File.Delete(customGlossariesAssemblies[i].Location);
                            File.Delete(customGlossariesAssemblies[i].Location + ".meta");
                        }
                    }

                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    SearchForExistentAchievementsGlossary();
                    SaveFields();
                }
            }
        }

        #region GUI STYLE

        private GUIStyle m_purpleLabelGUIStyle;
        private GUIStyle m_blueLabelGUIStyle;
        private GUIStyle m_greenLabelGUIStyle;

        private GUIStyle m_purpleTextFieldGUIStyle;
        private GUIStyle m_blueTextFieldGUIStyle;
        private GUIStyle m_greenTextFieldGUIStyle;

        private GUIStyle m_whiteFontButtonGUIStyle;
        private GUIStyle m_orangeFontButtonGUIStyle;

        private GUIStyle m_errorLabelGUIStyle;

        private Color RedColor = new Color(1f, 0f, 0f);
        private Color OrangeColor = new Color(1f, 0.6f, 0f);
        private Color PurpleColor = new Color(0.4f, 0.15f, 1f);
        private Color BlueColor = new Color(0f, 0.6f, 0.75f);
        private Color GreenColor = new Color(0.25f, 0.75f, 0f);
        private Color WhiteColor = new Color(1f, 1f, 1f);

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

        private void InitGUIStyle_GreenLabel()
        {
            m_greenLabelGUIStyle = new GUIStyle(GUI.skin.label);
            m_greenLabelGUIStyle.fontStyle = FontStyle.Bold;
            m_greenLabelGUIStyle.normal.textColor = GreenColor;
        }

        private void InitGUIStyle_PurpleTextField()
        {
            m_purpleTextFieldGUIStyle = new GUIStyle(GUI.skin.textField);
            m_purpleTextFieldGUIStyle.normal.textColor = PurpleColor;
            m_purpleTextFieldGUIStyle.hover.textColor = PurpleColor;
            m_purpleTextFieldGUIStyle.focused.textColor = PurpleColor;
            m_purpleTextFieldGUIStyle.active.textColor = PurpleColor;
        }

        private void InitGUIStyle_BlueTextField()
        {
            m_blueTextFieldGUIStyle = new GUIStyle(GUI.skin.textField);
            m_blueTextFieldGUIStyle.normal.textColor = BlueColor;
            m_blueTextFieldGUIStyle.hover.textColor = BlueColor;
            m_blueTextFieldGUIStyle.focused.textColor = BlueColor;
            m_blueTextFieldGUIStyle.active.textColor = BlueColor;
        }

        private void InitGUIStyle_GreenTextField()
        {
            m_greenTextFieldGUIStyle = new GUIStyle(GUI.skin.textField);
            m_greenTextFieldGUIStyle.normal.textColor = GreenColor;
            m_greenTextFieldGUIStyle.hover.textColor = GreenColor;
            m_greenTextFieldGUIStyle.focused.textColor = GreenColor;
            m_greenTextFieldGUIStyle.active.textColor = GreenColor;
        }

        private void InitGUIStyle_WhiteFontButtonLabel()
        {
            m_whiteFontButtonGUIStyle = new GUIStyle(GUI.skin.button);
            m_whiteFontButtonGUIStyle.fontStyle = FontStyle.Bold;
            m_whiteFontButtonGUIStyle.normal.textColor = WhiteColor;
            m_whiteFontButtonGUIStyle.hover.textColor = WhiteColor;
            m_whiteFontButtonGUIStyle.focused.textColor = WhiteColor;
            m_whiteFontButtonGUIStyle.active.textColor = WhiteColor;
        }

        private void InitGUIStyle_RedFontButtonLabel()
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
