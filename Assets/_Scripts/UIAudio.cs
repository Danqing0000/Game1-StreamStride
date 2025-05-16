using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    
    public AudioSource myAudioSource;
    public List<AudioClip> myclip;
    
    // Start is called before the first frame update
    public void UISoundPlay()
    {
        int i = Random.Range(0, myclip.Count);
        myAudioSource.clip = myclip[i];
        myAudioSource.Play();
    }
}
