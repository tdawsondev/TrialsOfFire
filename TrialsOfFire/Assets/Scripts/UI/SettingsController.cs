using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider masterVolume;
    float masterVolumeValue;

    // Start is called before the first frame update
    void Start()
    {
        masterVolumeValue = 25f;
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolumeValue = PlayerPrefs.GetFloat("MasterVolume");
        }
        
        masterVolume.value = masterVolumeValue;
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
}
