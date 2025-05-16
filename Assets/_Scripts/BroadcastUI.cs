using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class BroadcastUI : MonoBehaviour
{
    public TMP_Text broadcastText;
    public List<AudioClip> myClips;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ShowBroadcast(string name)
    {
        audioSource.clip = myClips[0];
        audioSource.Play();
        broadcastText.text = name;
        this.transform.DOMoveX(1750, 1f).SetEase(Ease.OutBack);
        StartCoroutine(HideBroadcast());
    }

    public void ShowFishBroadcast(string name)
    {
        audioSource.clip = myClips[1];
        audioSource.Play();
        broadcastText.text = name;
        this.transform.DOMoveX(1750, 1f).SetEase(Ease.OutBack);
        StartCoroutine(HideBroadcast());
    }

    IEnumerator HideBroadcast()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Hide");
        this.transform.DOMoveX(2100, 1f).SetEase(Ease.OutBack);
    }
}
