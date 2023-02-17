using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicOnSceneLoad : MonoBehaviour
{
    public string music;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play(music);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
