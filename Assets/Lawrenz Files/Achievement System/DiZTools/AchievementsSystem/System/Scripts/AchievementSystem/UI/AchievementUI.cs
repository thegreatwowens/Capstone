using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiZTools_AchievementsSystem
{
    public class AchievementUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_nameID = null;
        [SerializeField] private TextMeshProUGUI m_description = null;
        [SerializeField] private Image m_thumbnail = null;


        #region GETTERS

        public TextMeshProUGUI NameID { get => m_nameID; }
        public TextMeshProUGUI Description { get => m_description; }
        public Image Thumbnail { get => m_thumbnail; }

        #endregion
    }
}