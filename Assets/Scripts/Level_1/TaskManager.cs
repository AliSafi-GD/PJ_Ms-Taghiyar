using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Threading;

public class TaskManager : MonoBehaviour
{

    [SerializeField] public LevelManagerInGame[] levelManager;
    public Task siwtchTask;
    public GameObject endLevel;
    public Text txtCoin, txtScore;
    public Button btnEndLevel;
    public GameObject endGame;
    public Text endTxtCoin, endTxtScore;
    public Windows10 windows;
    

    //public void Starter()
    //{
        
      
        
    //    Initialize();



    //    StartCoroutine(ControlTask());

        




    //}
    private void Start()
    {

        Initialize();



        StartCoroutine(ControlTask());


        btnEndLevel.onClick.AddListener(() =>
            {
                if (LevelManager.currenLevel == 4)
                {
                    endLevel.SetActive(false);
                    endGame.SetActive(true);
                    endTxtCoin.text = DB.instance.infoAccount.coin;
                    endTxtScore.text = DB.instance.infoAccount.score;
                    SetDataGeneral();
                    GameRefremce.instance.IsRun = false;
                    int totalStopTime = (from x in levelManager select x.timeStop).Sum();
                    GameRefremce.instance._Timer -= totalStopTime;


                }

                else
                {
                    LevelManager.currenLevel++;
                    LoadingGame.instance.LoadScene("Login");
                }
            });


    }
    public void LoadSceneQuiz()
    {
        endGame.SetActive(false);
        LoadingGame.instance.LoadScene("Questionnaire");
    }
    void SetDataGeneral()
    {
        windows.CheckNumberWireless();
        windows.CheckNumberbackup();
        windows.CheckNumberSecurityReport();
    }
    

    void Initialize()
    {
        foreach (var item in levelManager[ LevelManager.currenLevel].tasks)
        {
            item.task.gameObject.SetActive(false);
        }
    }
    IEnumerator ControlTask()
    {
        btnEndLevel.interactable = false;
        endLevel.SetActive(false);
        // print($"Current game level { LevelManager.currenLevel}");
        var routin = StartCoroutine(ITimerLvl(levelManager[LevelManager.currenLevel]));
        foreach (var item in levelManager[ LevelManager.currenLevel].tasks)
        {
            
            bool b = false;
            item.task.gameObject.SetActive(true);
            item.task.starter(result =>
            {
                b = result;
            });
            yield return new WaitUntil(() => b);
            var b2 = false;
            if (item.switchPage)
                siwtchTask.starter(result => { b2 = result; });
            yield return new WaitForSeconds(0.5f);
            item.task.gameObject.SetActive(false);
            //currentTask++;
            //print($"end item {item.nameTask}");
            
        }
        StopCoroutine(routin);
        var res = false;
        endLevel.SetActive(true);
        StartCoroutine(StartEndLevel((r) => res = r));
        yield return new WaitUntil(() => res);

        btnEndLevel.interactable = true;
        
    }
    IEnumerator StartEndLevel(Action<bool> r)
    {
        var s = 0f;
        var sc = levelManager[LevelManager.currenLevel].score + ScoreFromTimeGame();

        GameRefremce.instance.Score += ScoreFromTimeGame();
        FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].score += ScoreFromTimeGame();


        while (s != sc)
        {
            s = Mathf.MoveTowards(s, sc, sc * Time.deltaTime);
            txtScore.text = "+ "+((int)s).ToString();
            yield return null;
        }
        var c = 0f;
        var co = levelManager[LevelManager.currenLevel].coin;
        co = Mathf.Clamp(co, 0, co);
        do 
        {
            c = Mathf.MoveTowards(c, co, 70 * Time.deltaTime);
            txtCoin.text =c < 0 ? "- " : "+ " + ((int)c).ToString();
            yield return null;
        } while (c != co);

      
        r(true);
    }
    int ScoreFromTimeGame()
    {
        int score = 0;

        int t = 0;
        t = levelManager[LevelManager.currenLevel].TimerLvl;
        t /= 60;
        switch (t)
        {
            case 1:
                score = 240;
                break;
            case 2:
                score = 180;
                break;
            case 3:
                score = 120;
                break;
            case 4:
                score = 60;
                break;
            default:
                score = 20;
                break;
        }
        print(levelManager[LevelManager.currenLevel].TimerLvl+"    "+t + "  _  " + score);
        return score;
    }
    IEnumerator ITimerLvl(LevelManagerInGame task)
    {
        
        var t = 0;
        var tStop = 0;
        while (true)
        {
            //print("p");
            if (task.isStop)
            {
                yield return new WaitForSeconds(1);
                tStop++;
                task.timeStop = tStop;
            }
            else
            {
                yield return new WaitForSeconds(1);
                t++;
                task.TimerLvl =t;
            }
                
            
        }
        
    }
}
[Serializable]
public class _Tasks
{
    public string nameTask;
    public bool switchPage = true;
    public Task task;
    
}
[Serializable]
public class LevelManagerInGame{
    public string nameLevel;
    public int mainScore,score, coin;
    [SerializeField] public _Tasks[] tasks;
    public int timeStop;
    public int timerLvl;
    public int TimerLvl
    {
        get { return timerLvl; }
        set
        {
            timerLvl = value;
            if (DB.instance != null)
                switch (LevelManager.currenLevel)
                {
                    case 0:
                        DB.instance.infoAccount.lvl1.timerLvl =$"{value/60}:{value%60}";
                        break;
                    case 1:
                        DB.instance.infoAccount.lvl2.timerLvl =$"{value/60}:{value%60}";
                        break;
                    case 2:
                        DB.instance.infoAccount.lvl3.timerLvl =$"{value/60}:{value%60}";
                        break;
                    case 3:
                        DB.instance.infoAccount.lvl4.timerLvl =$"{value/60}:{value%60}";
                        break;
                    case 4:
                        DB.instance.infoAccount.lvl5.timerLvl =$"{value/60}:{value%60}";
                        break;
                }

        }
    }
    public bool isStop;
}
