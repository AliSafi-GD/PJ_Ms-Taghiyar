using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class MiniGame4 : MonoBehaviour, IPointerDownHandler
{
    public GameObject wrongObj;
    public List<GameObject> wrongList,txtList, bkpTxtList;
    public Button btnRun,btnHelp;
    public Text txtShowCurrentAnswer;
    int currentAnswer;
    enum WrongMode{
        Help,User
    }
    GameObject t;
    public GameObject finishObj;
    public Animator anim;

    public int CurrentAnswer
    {
        get => currentAnswer;
        set
        {
            currentAnswer = value;
            txtShowCurrentAnswer.text =$"{value} / 8";
        }
    }

    //WrongMode wrongMode;
    private void Start()
    {
        t = new GameObject("timer", typeof(TimerMiniGame));
        CheckBtnRunInteractable();
        bkpTxtList = new List<GameObject>(txtList);
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (wrongList.Count < txtList.Count)
        {
            InstantiateWrong(eventData.position,WrongMode.User);
        }
        
    }
    void InstantiateWrong(Vector3 pos,WrongMode wrong)
    {
        var w = Instantiate(wrongObj, transform);
        var win = FindObjectOfType<Windows10>();
        win.CreateSound(win.GetClip("MouseClick"));
        wrongList.Add(w);
        w.name = $"Wrong {wrongList.Count}";
        w.GetComponent<WrongImage>().miniGame = this;
        w.transform.position = pos;
        switch (wrong)
        {
            case WrongMode.Help:
                w.GetComponent<WrongImage>().IsHelp = true;
                var r = Random.Range(0, bkpTxtList.Count);
                var txt = bkpTxtList[r];
                bkpTxtList.Remove(txt);
                w.transform.position = txt.transform.position;
                w.GetComponent<WrongImage>().isTrue = true;
                break;
            case WrongMode.User:
                w.GetComponent<WrongImage>().IsHelp = false;
                var check = w.GetComponent<WrongImage>().Overlaps(bkpTxtList);
                if (check)
                {
                    w.GetComponent<WrongImage>().isTrue = true;
                    bkpTxtList.Remove(w.GetComponent<WrongImage>().objOverlaps);
                }
                   
                break;
        }
        CurrentAnswer++;
        CheckBtnRunInteractable();


    }
    void CheckBtnRunInteractable()
    {
        if (wrongList.Count == txtList.Count)
            btnRun.interactable = true;
        else
            btnRun.interactable = false;
    }
    void CheckBtnHelpInteractable()
    {
        if ((from x in wrongList where x.GetComponent<WrongImage>().isTrue select x).Count() == txtList.Count)
            btnHelp.interactable = false;
        else
            btnHelp.interactable = true;
    }
    public void DestroyWrong(GameObject obj)
    {
        CurrentAnswer--;
        wrongList.Remove(obj);
        if(obj.GetComponent<WrongImage>().objOverlaps)
            bkpTxtList.Add(obj.GetComponent<WrongImage>().objOverlaps);
        Destroy(obj);
        CheckBtnRunInteractable();
    }
    public void Finish()
    {
        anim.enabled = true;
        anim.Play("Close");
        Windows10._checkTask("MiniGame4");
        //transform.parent.gameObject.SetActive(false);
        //gameObject.SetActive(false);
        
    }

   
    public void CheckWin()
    {
        var listTrue = (from x in wrongList where x.GetComponent<WrongImage>().isTrue select x).ToList();
        var isWin = listTrue.Count == txtList.Count ? true : false;
        
        foreach (var item in listTrue)
        {
            item.GetComponent<WrongImage>().IsHelp = true;
        }
        
        if (isWin)
        {
            Destroy(t);
            finishObj.SetActive(true);
                GameRefremce.instance.Coin += 8*20;
                GameRefremce.instance.Score += 40;
            FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin += 8 * 20;
            FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].score +=40;
        }
        else
        {
            Reset();
           
            
        }
        CurrentAnswer = listTrue.Count;
        //print(isWin ? "Win" : "Loss");
    }

    void Reset()
    {
        var listWrong2 = (from x in wrongList where !x.GetComponent<WrongImage>().isTrue select x).ToList();
        foreach (var item in listWrong2)
        {
            DestroyWrong(item);
            if (GameRefremce.instance)
                GameRefremce.instance.Coin -= 10;
            FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin -= 10;
        }
        CheckBtnHelpInteractable();
        CheckBtnRunInteractable();
    }
    public void Help()
    {
        if (wrongList.Count < txtList.Count)
        {
            InstantiateWrong(Vector3.zero, WrongMode.Help);
        }
        else
        {
            var w = (from x in wrongList where !x.GetComponent<WrongImage>().isTrue select x).FirstOrDefault();
            if (w)
            {
                DestroyWrong(w);
                InstantiateWrong(Vector3.zero, WrongMode.Help);
            }
           
        }
        if (GameRefremce.instance)
            GameRefremce.instance.Coin -= 40;
        FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin -= 40;
        CheckBtnHelpInteractable();


    }
}
