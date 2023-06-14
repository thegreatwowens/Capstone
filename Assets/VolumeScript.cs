
using UnityEngine.UI;
using UnityEngine;

public class VolumeScript : MonoBehaviour
{
    public string MixerName;
    public Slider slider;
public void OnValueChanged(){

    if(MixerName == "Master")
  SoundManager.Instance.VolumeSliderMaster(slider.value);
    if(MixerName == "BGMusics")
    SoundManager.Instance.VolumeSliderMusic(slider.value);
    if(MixerName == "SoundFX")
    SoundManager.Instance.VolumeSliderSoundFx(slider.value);


 }  
}
