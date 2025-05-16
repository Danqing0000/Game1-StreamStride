using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class StartUI : MonoBehaviour
{

    public DialogueTreeController dialogueTree;
    public GameObject UI;
    public GameObject boat;
    public RawImage rawImage;
    bool check = false;
    public GameObject LoadScene;
    public AudioMixerSnapshot moving;
    public AudioMixerSnapshot idle;

    void Update()
    {
    }


    public void StartGame() //船自动行驶 开始第一段对话 镜头保持跟随 侧面
    {
        UI.transform.DOMoveX(-500, 1f).SetEase(Ease.OutBack);
        StartCoroutine(Wait());
        //dialogueTree.StartDialogue();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void NewDayStart()
    {
        StartCoroutine(Hide());
        //StartCoroutine(WaitSceneLoad());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1);
        boat.GetComponent<Animation>().Play();
        moving.TransitionTo(0.5f);
        yield return new WaitForSecondsRealtime(2);
        dialogueTree.StartDialogue();
    }

    IEnumerator WaitSceneLoad()
    {
        yield return new WaitForSecondsRealtime(1);
        rawImage.GetComponent<SceneSwitch>().sceneEnding = true;
        //SceneManager.LoadScene("Scene1");
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(2f);
        rawImage.GetComponent<RawImage>().DOFade(1,1f);
        yield return new WaitForSecondsRealtime(1);
        idle.TransitionTo(0.5f);
        LoadScene.GetComponent<LoadManager>().LodeNextScene();
        //this.transform.DOScale(0, 1.5f);
    }
}
