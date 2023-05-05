using UnityEditor;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    [CustomEditor(typeof(Achievement))]
    public abstract class AchievementEditor : Editor
    {
        private SerializedProperty m_nameIDProperty;
        private SerializedProperty m_descriptionProperty;
        private SerializedProperty m_thumbnailProperty;

        private bool m_guiStyleInitialized;

        protected virtual void OnEnable()
        {
            m_nameIDProperty = serializedObject.FindProperty("m_nameID");
            m_descriptionProperty = serializedObject.FindProperty("m_description");
            m_thumbnailProperty = serializedObject.FindProperty("m_thumbnail");
        }

        public override void OnInspectorGUI()
        {
            InitializeGUIStyles();

            serializedObject.Update();

            DisplayBase();

            EditorGUILayout.Space(10f);

            DisplayCustom();

            serializedObject.ApplyModifiedProperties();
        }

        private void InitializeGUIStyles()
        {
            if (m_guiStyleInitialized)
                return;

            InitGUIStyle_GreenLabel();

            m_guiStyleInitialized = true;
        }

        private void DisplayBase()
        {
            GUILayout.Label("Base", m_greenLabelGUIStyle, GUILayout.Width(160f));

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Name ID", GUILayout.Width(160f));
            EditorGUILayout.PropertyField(m_nameIDProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Description", GUILayout.Width(160f));
            EditorGUILayout.PropertyField(m_descriptionProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Thumbnail", GUILayout.Width(160f));
            EditorGUILayout.PropertyField(m_thumbnailProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }

        protected abstract void DisplayCustom();

        #region GUI STYLE

        protected GUIStyle m_greenLabelGUIStyle;

        private Color GreenColor = new Color(0.25f, 0.75f, 0f);

        private void InitGUIStyle_GreenLabel()
        {
            m_greenLabelGUIStyle = new GUIStyle(GUI.skin.label);
            m_greenLabelGUIStyle.fontStyle = FontStyle.Bold;
            m_greenLabelGUIStyle.normal.textColor = GreenColor;
        }

        #endregion
    }
}