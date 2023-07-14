using UnityEngine;
using DiZTools_AchievementsSystem;
using TMPro;
using UnityEngine.UI;

public class AchievementIconEnabler : MonoBehaviour
{
    public  GameObject panel;
    private TextMeshProUGUI _AchievementName;
    private TextMeshProUGUI _Description;
    private Image image;

    private void OnEnable()
    {
        _AchievementName = panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _Description = panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        image = panel.transform.GetChild(2).GetComponent<Image>();
    }

    


}
