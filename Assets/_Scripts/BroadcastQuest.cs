using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class BroadcastQuest : MonoBehaviour
{
    public TMP_Text questBroadcastText;
    public AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOScale(0, 0);
        gameObject.GetComponent<Image>().DOFade(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuestBroadcast(string name)
    {
        questBroadcastText.text = name;
        gameObject.GetComponent<Image>().DOFade(1, 0.5f);
        this.transform.DOScale(1f, 1f).SetEase(Ease.OutBack);
        StartCoroutine(WaitforSound());
        //this.transform.DOColo
        StartCoroutine(HideBroadcast());
    }
    
    IEnumerator WaitforSound()
    {
        yield return new WaitForSeconds(0.5f);
        myAudioSource.Play();
    }

    IEnumerator HideBroadcast()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log("Hide");
        gameObject.GetComponent<Image>().DOFade(0,1f);
        this.transform.DOScale(0, 1.5f);
    }
}
