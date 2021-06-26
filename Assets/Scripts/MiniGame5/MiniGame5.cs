using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame5 : MonoBehaviour
{
    [SerializeField] public AnswerObject[] answerObjects;

    public string answer;
    public Text txtAnswer;
    public int currentAnswerObject;

    public GameObject finishObj;
    public Animator anim;
    public string Answer
    {
        get
        {
            return answer;
        }
        set
        {
            answer = value;
            txtAnswer.text = answer;
            if(value == answerObjects[CurrentAnswerObject].strAnswer)
            {
                
                    GameRefremce.instance.Coin += 20;
                    GameRefremce.instance.Score += 20;
                FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin += 20;
                FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].score += 20;
                foreach (var item in answerObjects[CurrentAnswerObject].btnAnswers)
                {
                    item.IsWin = true;
                }
                var txtP = answerObjects[CurrentAnswerObject].txtPreview;
                for (int i = 0; i < txtP.Length; i++)
                {
                    txtP[i].text = answerObjects[CurrentAnswerObject].strAnswer[(txtP.Length - 1) - i].ToString();
                }
                answerObjects[CurrentAnswerObject].win = true;
                if (FindObjectOfType<MiniGame5>().CurrentAnswerObject < FindObjectOfType<MiniGame5>().answerObjects.Length-1)
                    FindObjectOfType<MiniGame5>().CurrentAnswerObject++;
                
            }
        }
    }
    public void ActivFalse()
    {
        gameObject.SetActive(false);
    }
    public void ResetAfterTot()
    {
        CurrentAnswerObject = 0;
    }
    public int CurrentAnswerObject
    {
        get
        {
            return currentAnswerObject;
        }
        set
        {
            
            currentAnswerObject = value;
            foreach (var item in answerObjects)
            {
                item.parentObj.SetActive(false);
                foreach (var item2 in answerObjects[value].btnAnswers)
                {
                    //item2.Init();
                    item2.IsSelected = false;
                }
            }
            answerObjects[value].parentObj.SetActive(true);
            Answer = "";
            Init();
        }
    }
    GameObject t;
    private void Start()
    {
        t = new GameObject("timer", typeof(TimerMiniGame));
        CurrentAnswerObject = 0;
        StartCoroutine(WaitForEndGame());
    }
    IEnumerator WaitForEndGame()
    {
        yield return new WaitUntil(() => (from x in answerObjects where x.win select x).Count() >= answerObjects.Length);
        Destroy(t);
        finishObj.SetActive(true);
    }
    public void Finish()
    {
        anim.enabled = true;
        anim.Play("Close");
        //gameObject.SetActive(false);
        Windows10._checkTask("MiniGame5");
    }
    public void Init()
    {
        foreach (var item in answerObjects[CurrentAnswerObject].btnAnswers)
        {
            item.Btn.onClick.RemoveAllListeners();
            //item.Init();
            item.Btn.onClick.AddListener(()=>{
                if (!item.IsWin)
                {
                    if (!item.IsSelected)
                    {
                        item.IsSelected = true;
                        Answer += item.Answer;
                    }
                    else
                    {
                        Answer = Answer.Remove(Answer.IndexOf(item.Answer), 1);
                        item.IsSelected = false;
                    }
                }
                
            });
        }
        foreach (var item in answerObjects)
        {
            item.txtPreview = item.preview.GetComponentsInChildren<Text>();
        }
    }
    public void ChangeCurrentAnswer(int index)
    {
        //CurrentAnswerObject = index;
    }
    public void Help()
    {
        for (int i = answerObjects[CurrentAnswerObject].txtPreview.Length-1; i >= 0; i--)
        {
            var txt = answerObjects[CurrentAnswerObject].txtPreview[i];
            if(txt.text == "")
            {
                
                GameRefremce.instance.Coin -= 40;
                FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin -= 40;
                txt.text = answerObjects[CurrentAnswerObject].strAnswer[(answerObjects[CurrentAnswerObject].txtPreview.Length - 1) - i].ToString();
                break;
            }
        }
        
    }
}
[Serializable]
public class AnswerObject
{
    public string name;
    public string strAnswer;
    public BtnAnswer[] btnAnswers;
    public GameObject parentObj;
    public int numberAnswer;
    public GameObject preview;
    public Text[] txtPreview=new Text[] { };
    public bool win;
    
}
