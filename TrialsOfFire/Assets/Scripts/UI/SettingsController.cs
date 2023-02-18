using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider masterVolume;
    float masterVolumeValue;
    [SerializeField] private Slider musicVolume;
    float musicVolumeValue;
    [SerializeField] private Slider sfxVolume;
    float sfxVolumeValue;

    // Start is called before the first frame update
    void Start()
    {
        masterVolumeValue = 25f;
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolumeValue = PlayerPrefs.GetFloat("MasterVolume");
        }
        masterVolume.value = masterVolumeValue;

        musicVolumeValue = 25f;
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolumeValue = PlayerPrefs.GetFloat("MusicVolume");
        }
        musicVolume.value = musicVolumeValue;

        sfxVolumeValue = 25f;
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolumeValue = PlayerPrefs.GetFloat("SFXVolume");
        }
        sfxVolume.value = sfxVolumeValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume()
    {
        masterVolumeValue = masterVolume.value;
        AudioManager.instance.SetMasterVolume(masterVolumeValue);
    }
    public void SetMusicVolume()
    {
        musicVolumeValue = musicVolume.value;
        AudioManager.instance.SetMusicVolume(musicVolumeValue);
    }
    public void SetSFXVolume()
    {
        sfxVolumeValue = sfxVolume.value;
        AudioManager.instance.SetSFXVolume(sfxVolumeValue);
    }

}
