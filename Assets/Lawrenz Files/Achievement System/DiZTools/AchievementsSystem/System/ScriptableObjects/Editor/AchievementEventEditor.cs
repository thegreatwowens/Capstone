using UnityEditor;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    [CustomEditor(typeof(AchievementEvent))]
    public class AchievementEventEditor : AchievementEditor
    {
        private SerializedProperty m_achievementDataNameProperty;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_achievementDataNameProperty = serializedObject.FindProperty("m_achievementDataName");
        }

        protected override void DisplayCustom()
        {
            GUILayout.Label("Custom", m_greenLabelGUIStyle, GUILayout.Width(160f));

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Achievement Data Name", GUILayout.Width(160f));
            EditorGUILayout.PropertyField(m_achievementDataNameProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }
    }
}