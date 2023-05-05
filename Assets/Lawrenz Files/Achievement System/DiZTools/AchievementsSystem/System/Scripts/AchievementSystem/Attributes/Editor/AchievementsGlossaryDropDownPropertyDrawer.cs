using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DiZTools_AchievementsSystem
{
    [CustomPropertyDrawer(typeof(AchievementsGlossaryDropDownAttribute))]
    public class AchievementsGlossaryDropDownPropertyDrawer : PropertyDrawer
    {
        //cache
        private bool isPropertyDrawerInitialized = false;
        private List<string> achievementsGlossaryConstantsList;
        private int m_dropdownIndex;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!isPropertyDrawerInitialized)
                InitializePropertyDrawer(property);

            EditorGUI.BeginProperty(position, label, property);

            //Draw the popup box with the current selected index
            m_dropdownIndex = EditorGUI.Popup(position, label.text, m_dropdownIndex, achievementsGlossaryConstantsList.ToArray());

            //Adjust the actual string value of the property based on the selection
            property.stringValue = achievementsGlossaryConstantsList[m_dropdownIndex];

            EditorGUI.EndProperty();
        }

        private void InitializePropertyDrawer(SerializedProperty property)
        {
            //Get the AchievementsGlossaryAssembly through the list of assemblies in the current application domain.
            Assembly achievementsGlossaryAssembly = GenerationUtils.GetAssemblyWithPrivateKey(GenerationUtils.ACHIEVEMENTS_GLOSSARY_ASSEMBLY_PRIVATE_KEY);

            achievementsGlossaryConstantsList = new List<string>();

            //Generate list for the drop down
            FieldInfo[] achievementsGlossaryFields = achievementsGlossaryAssembly.GetTypes()[0].GetFields();
            for (int fieldIndex = 0; fieldIndex < achievementsGlossaryFields.Length; fieldIndex++)
            {
                achievementsGlossaryConstantsList.Add(achievementsGlossaryFields[fieldIndex].GetRawConstantValue().ToString());
            }

            achievementsGlossaryConstantsList.Sort();
            achievementsGlossaryConstantsList.Insert(0, "NONE");

            //Initialize dropdown index
            m_dropdownIndex = -1;
            if (property.stringValue == "")
            {
                //Default value
                m_dropdownIndex = 0;
            }
            else
            {
                //Check if there is a match between the entry and all entries in the list, and get the index
                //Skip index 0 as that is a special custom case, but if no entry is found, put 0 as index
                for (int i = 1; i < achievementsGlossaryConstantsList.Count; i++)
                {
                    if (achievementsGlossaryConstantsList[i] == property.stringValue)
                    {
                        m_dropdownIndex = i;
                        break;
                    }
                }

                if (m_dropdownIndex == -1)
                    m_dropdownIndex = 0;
            }

            isPropertyDrawerInitialized = true;
        }
    }
}