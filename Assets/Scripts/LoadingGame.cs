using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    public GameObject objLoading;
    GameObject objInstantiate;
    public static LoadingGame instance;
    public Text txtProgress;
    public Button btnStartGame;
    AsyncOperation async;
    public GameObject objLevel,objLogin,objQuiz;
    public TaskManager taskManager;
    private void Awake()
    {
        
        //Resources.UnloadUnusedAssets();
        Application.backgroundLoadingPriority = UnityEngine.ThreadPriority.Low;
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void InitLoading()
    {
        objInstantiate = Instantiate(objLoading);

        btnStartGame = objInstantiate.transform.Find("Image/BtnStart").GetComponent<Button>();
        txtProgress = objInstantiate.transform.Find("Image/TxtProgress").GetComponent<Text>();
        btnStartGame.gameObject.SetActive(false);
    }
    public void DestroyLoading()
    {
        btnStartGame.onClick.RemoveAllListeners();
        Destroy(objInstantiate);
    }
    
    public void LoadScene(string nameScene)
    {
        //print("Level Start");
        //if (nameScene == "Level_1")
        //{
        //    objLevel.SetActive(true);
        //    objLogin.SetActive(false);
        //    taskManager.Starter();
            
        //}
        //else if(nameScene == "Questionnaire")
        //{
        //    objLevel.SetActive(false);
        //    objLogin.SetActive(false);
        //    objQuiz.SetActive(true);
        //}
        //else
        //{
        //    objLevel.SetActive(false);
        //    objLogin.SetActive(true);
        //    print("roadmap Start");
        //}
       
        //Application.LoadLevel(nameScene);
        InitLoading();
        StartCoroutine(LoadSceneAsynce(nameScene));
        
    }

    private void Async_completed()
    {
        DestroyLoading();
    }
    IEnumerator LoadSceneAsynce(string nameScene)
    {
        yield return new WaitForFixedUpdate();
        async = SceneManager.LoadSceneAsync(nameScene);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if(async.progress >= 0.9f)
            {
                txtProgress.text = ($"{ ((async.progress*100) + 10)} %");
                btnStartGame.gameObject.SetActive(true);
                btnStartGame.onClick.AddListener(() => async.allowSceneActivation = true);
            }
                
                
            yield return null;
            //print(async.progress +"    "+ async.isDone);
        }
        //
        Async_completed();
    }
    public void  StartWaiting()
    {
        InitLoading();
    }
    public void EndWaitin()
    {
        DestroyLoading();
    }
}
