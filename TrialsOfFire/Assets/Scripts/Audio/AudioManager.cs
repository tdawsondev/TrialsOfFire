using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }


    public List<Sound> sounds = new List<Sound>();

    // Start is called before the first frame update
    void Start()
    {
        float masterVolumeValue = 25f;
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolumeValue = PlayerPrefs.GetFloat("MasterVolume");
        }
        SetMasterVolume(masterVolumeValue);

        float musicVolumeValue = 25f;
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolumeValue = PlayerPrefs.GetFloat("MusicVolume");
        }
        SetMusicVolume(musicVolumeValue);

        float sfxVolumeValue = 25f;
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolumeValue = PlayerPrefs.GetFloat("SFXVolume");
        }
        SetSFXVolume(sfxVolumeValue);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public KeyValuePair<AK.Wwise.Event, GameObject> Play(string soundName, GameObject otherObject = null)
    {
        Sound s = sounds.Find(i => i.soundName == soundName);
        KeyValuePair<AK.Wwise.Event, GameObject> result = new KeyValuePair<AK.Wwise.Event,GameObject>();
        if(s == null)
        {
            Debug.LogWarning("Sound " + soundName + " not Found.");
            return result;
        }
        if (otherObject != null)
        {
            s.soundEvent.Post(otherObject);
            result = new KeyValuePair<AK.Wwise.Event, GameObject>(s.soundEvent, otherObject);
            return result;
        }
        else
        {
            s.soundEvent.Post(Player.Instance.gameObject);
            result = new KeyValuePair<AK.Wwise.Event, GameObject>(s.soundEvent, Player.Instance.gameObject);
            return result;
        }
    }


    public void StopSound(string soundName, GameObject otherObject = null)
    {
        Sound s = sounds.Find(i => i.soundName == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }
        if (otherObject != null)
        {
            s.soundEvent.Stop(otherObject);
        }
        else
        {
            s.soundEvent.Stop(Player.Instance.gameObject);
        }
    }


    public void SetMasterVolume(float value)
    {
        AkSoundEngine.SetRTPCValue("MasterVolume", value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }
    public void SetMusicVolume(float value)
    {
        AkSoundEngine.SetRTPCValue("MusicVolume", value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    public void SetSFXVolume(float value)
    {
        AkSoundEngine.SetRTPCValue("SFXVolume", value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
