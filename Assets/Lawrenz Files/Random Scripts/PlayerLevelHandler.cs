using UnityEngine;
using QInventory;
using UnityEngine.Events;
public class PlayerLevelHandler : MonoBehaviour
{
    float currentXp;
    float maxXP;
    float level;
    GameObject achievementObject;

    [SerializeField]
    ConversionGamePanel panel;

    [SerializeField]
    AudioClip audioClip =null;
    [SerializeField]
    AudioSource source = null;
    public UnityEvent OnLevelUP;

    private void Start()
    {
    }

    
    private void Update()
    {
        achievementObject = GameObject.Find("AchievementsManager");
        currentXp = InventoryManager.GetPlayerAttributeCurrentValue("Experience");
        maxXP = InventoryManager.GetPlayerAttributeMaxValue("Experience");
        level = InventoryManager.GetPlayerAttributeCurrentValue("Level");
        if (currentXp >= maxXP)
        {

            LevelUp();
        }
    }

    public void ExpGrant(float value)
    {
        InventoryManager.ChangePlayerAttributeValue("Experience",value, Effect.Restore);
      

    }
    private void PlayLevelUpAudio()
    {
        source.PlayOneShot(audioClip);
    }
    public void LevelUp()
    {
            InventoryManager.SetPlayerAttributeValue("Experience", 0, SetType.CurrentValue);
        InventoryManager.ChangePlayerAttributeValue("Level", 1, Effect.Restore);
        PlayLevelUpAudio();
        OnLevelUP?.Invoke();
        

    }
    public void Reset()
    {
        if(achievementObject != null)
        {
            Destroy(achievementObject);
        }
    }

}

  

