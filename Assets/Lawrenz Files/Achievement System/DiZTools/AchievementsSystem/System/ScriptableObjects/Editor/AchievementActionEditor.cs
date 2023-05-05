using UnityEditor;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    [CustomEditor(typeof(AchievementAction))]
    public class AchievementActionEditor : AchievementEditor
    {
        private SerializedProperty m_achievementDataNameProperty;
        private SerializedProperty m_doneConditionProperty;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_achievementDataNameProperty = serializedObject.FindProperty("m_achievementDataName");
            m_doneConditionProperty = serializedObject.FindProperty("m_doneCondition");
        }

        protected override void DisplayCustom()
        {
            GUILayout.Label("Custom", m_greenLabelGUIStyle, GUILayout.Width(160f));

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Achievement Data Name", GUILayout.Width(160f));
            EditorGUILayout.PropertyField(m_achievementDataNameProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Done Condition", GUILayout.Width(160f));
            EditorGUILayout.PropertyField(m_doneConditionProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }
    }
}