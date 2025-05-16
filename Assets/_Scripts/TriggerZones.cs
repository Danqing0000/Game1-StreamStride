using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TriggerZones : MonoBehaviour
{
    public AudioMixerSnapshot stage1;
    public AudioMixerSnapshot stage2;
    public AudioMixerSnapshot stage3;
    public AudioMixerSnapshot stage4;

    public AudioMixerSnapshot MainVillage; //day1
    public AudioMixerSnapshot FisherVillage; //day2
    public AudioMixerSnapshot City; //day2
    public AudioMixerSnapshot Fisherman; //day2
    public AudioMixerSnapshot Shcool; //day3
    public AudioMixerSnapshot Witch; //day3


    public string zoneName1;
    public string zoneName2;
    public string zoneName3;
    public string zoneName4;

    public string ambientName1;
    public string ambientName2;
    public string ambientName3;
    public string ambientName4;
    public string ambientName5;
    public string ambientName6;

    public float transitionTime = 1.0f;
    public float transitionTime2 = 1.0f;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);

        if (other.name == zoneName1)
        {
            //Debug.Log("in zone 1");
            stage1.TransitionTo(transitionTime);
        }
        else if (other.name == zoneName2)
        {
            //Debug.Log("in zone 2");
            stage2.TransitionTo(transitionTime);
        }
        else if (other.name == zoneName3)
        {
            //Debug.Log("in zone 3");
            stage3.TransitionTo(transitionTime);
        }
        else if (other.name == zoneName4)
        {
            //Debug.Log("in zone 4");
            stage4.TransitionTo(transitionTime);
        }
        else if (other.name == ambientName1)
        {
            //Debug.Log("in ambient 1");
            MainVillage.TransitionTo(transitionTime2);
        }
        else if (other.name == ambientName2)
        {
            //Debug.Log("in ambient 2");
            FisherVillage.TransitionTo(transitionTime2);
        }
        else if (other.name == ambientName3)
        {
            //Debug.Log("in ambient 3");
            City.TransitionTo(transitionTime2);
        }
        else if (other.name == ambientName4)
        {
            //Debug.Log("in ambient 4");
            Fisherman.TransitionTo(transitionTime2);
        }
        else if (other.name == ambientName5)
        {
            //Debug.Log("in ambient 5");
            Shcool.TransitionTo(transitionTime2);
        }
        else if (other.name == ambientName6)
        {
            //Debug.Log("in ambient 6");
            Witch.TransitionTo(transitionTime2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
