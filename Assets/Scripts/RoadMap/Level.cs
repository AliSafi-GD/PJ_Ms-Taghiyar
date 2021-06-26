using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Reflection.Emit;
public class Level : MonoBehaviour
{
    Animator anim;
    [SerializeField] bool unlock;
    Button button;
    public int neededLvl;
    public string nameScene;
  
    private void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=>
        {
            if(unlock&&LevelManager.currenLevel == neededLvl)
                LoadingGame.instance.LoadScene(nameScene);
                //LoadingGame.instance.LoadScene("Questionnaire");

                //print(name + " " + neededLvl + " button set Onclick " + "unlock = " + unlock + " _ " + "LevelManager.currenLevel = " + (LevelManager.currenLevel == neededLvl) + "   " + LevelManager.currenLevel);
            //print(LevelManager.currenLevel);
        });
    }

    public void UnlockLevel(){
        if(anim == null)
            anim = GetComponent<Animator>();
        anim.Play("OpenLevel");
        
        anim.Play(neededLvl == LevelManager.currenLevel ? "hightLight":"hightLightOff", 1);
        unlock = true;
       // print("Unlock");
    }
    
}
