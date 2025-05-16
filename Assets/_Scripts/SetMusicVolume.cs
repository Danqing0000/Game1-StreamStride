using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetMusicVolume : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void SetLevel (float volume)
    {
        audioMixer.SetFloat("BGMVolume", volume);
        //Debug.Log("volume: " + Mathf.Log10(volume * 20).ToString());
    }

}
