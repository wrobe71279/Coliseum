using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    //for planned Scene Transition Animations
    //public Animator transitionAnim;
    
    //declare scene name
    //public string sceneName;

    //declare UI Menus
    public GameObject ButtonUI;
    public GameObject TutorialMessage;

    public void Play()
    {
        //for planned Scene Transition Animations
        //StartCoroutine(LoadScene());
        ButtonUI.SetActive(false);
        TutorialMessage.SetActive(true);
    }

    public void StartTutorial()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Start Tutorial...");
    }

    public void SkipTutorial()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        Debug.Log("Skip Tutorial...");
    }

    public void Cancel()
    {
        ButtonUI.SetActive(true);
        TutorialMessage.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quitting Scene...");
        Application.Quit();
    }

    //for planned Scene Transition Animations
    /*
    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
    */
}
