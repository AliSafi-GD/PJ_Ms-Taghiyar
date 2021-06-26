using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

[Serializable]
public struct DicAudios
{
    public string nameAudio;
    public AudioClip audioClip;
}
public class Windows10 : Task
{
    public static int wirless, backup, securityReport;
    public GameObject audioClipObj;
    [SerializeField] public List<DicAudios> audioClips;
    public float timeRequired;

    //IEnumerator wfdc;
    public delegate void _CheckTask(string taskName);

    public static _CheckTask _checkTask;
    public Text txtClock;

    [SerializeField] public TaskLevel[] taskLevels;
    public static int currentLevel = 0;
    public int ShowCurrentLevel;
    public NotificationSystem notificationSystem;
    public static bool connection = false;
    public static bool connectionBluetooth = false;

    [Header("Wifi Controller")]
    public WifiConnection wifiConnection;


    //backup slider level3
    [Header("expend object for mail")]
    public Slider slider;
    public Slider slider2;
    public GameObject objUpdate,objNextQuiz;
    void test()
    {
        foreach (var task in taskLevels)
        {
            if (task.nameLevel == "5")
                break;
            foreach (var content in task.taskContents)
            {
                content.IsDone = true;
            }
        }
    }
    private void Start()
    {
        //test();
        //starter(result =>
        //{

        //});
        _checkTask += (taskName) =>
        {
            foreach (var item in taskLevels[currentLevel].taskContents)
            {
                if (item.taskName == taskName)
                    item.IsDone = true;
            }
        };
    }
    public void CreateSound(AudioClip audioClip)
    {
        var sound = Instantiate(audioClipObj).GetComponent<AudioSource>();
        sound.clip = audioClip;
        sound.Play();
        Destroy(sound.gameObject, sound.clip.length);
    }
    public override void starter(Action<bool> result)
    {
        //print("Start system _ Level " + currentLevel);
        StartCoroutine(CheckTasks(r =>
        {
            result(r);
        }));
    }
    public void InvokeCheck(string str)
    {
        _checkTask(str);
    }

    private void FixedUpdate()
    {
#if UNITY_EDITOR
        ShowCurrentLevel = currentLevel;
#endif
        txtClock.text = $"{DateTime.Now.ToShortTimeString()}";

    }
    IEnumerator CheckTasks(Action<bool> result)
    {

        // for (int i = 0; i < notificationSystem.notificationContents.Length; i++)
        // {

        //     var b = false;
        //     StartCoroutine(notificationSystem.Starter(res =>
        //     {
        //         b = res;
        //     }, i));
        //     yield return new WaitUntil(() => b);

        // }
        // foreach (var item in taskLevels[currentLevel].taskContents)
        // {
        //     if (item.autoPlay)
        //     {
        //         item.PlayAnim("Show");
        //         yield return new WaitUntil(()=>item.isDone);
        //         item.PlayAnim("Close");
        //     }
        // }
        // yield return new WaitUntil(() => (from x in taskLevels[currentLevel].taskContents where !x.isDone select x).ToArray().Length == 0);

        foreach (var item in taskLevels[currentLevel].taskContents)
        {
            if (item.imgWink != null)
            {
                //print("Wink");
                gameObject.AddComponent<ItemSelectedAnimate>().Wink(item.imgWink);
                
            }
            // print(item.taskName);
            if (item.notificationContent.strTitle != "")
            {
                CreateSound(GetClip("Alarm"));
                bool b = false;
                // print($"title = {item.notificationContent.strTitle} _ description = {item.notificationContent.strDescription}" );
                StartCoroutine(FindObjectOfType<NotificationSystem>().Starter(x => b = x, item.notificationContent, item.taskName));
                yield return new WaitUntil(() => b);
                if(GetComponent<ItemSelectedAnimate>())
                    GetComponent<ItemSelectedAnimate>().StopWink();
            }
            else{
                yield return new WaitUntil(()=>item.isDone);
                if (GetComponent<ItemSelectedAnimate>())
                    GetComponent<ItemSelectedAnimate>().StopWink();
            }
        
                

        }
        //yield return new WaitUntil(() => (from x in taskLevels[currentLevel].taskContents where !x.isDone select x).ToArray().Length == 0);
        print("Is Done");
        var l = (from x in taskLevels[currentLevel].taskContents where x.taskName == "Logoff" select x).FirstOrDefault();
        yield return new WaitForSeconds(l == null ? 2 : 1);

        result(true);
        currentLevel++;

    }
    public AudioClip GetClip(string nameClip)
    {
        return audioClips.Find(x => x.nameAudio == nameClip).audioClip;
    }
    public void EndTask(string nametask){
        _checkTask(nametask);
    }
    // public IEnumerator DoubleClick(Action<bool> result){
    //     bool r = false;
    //     wfdc = WaitForDoubleClick(res=>{
    //         r = res;
    //     });
    //     StartCoroutine(wfdc);
    //     yield return new WaitUntil(()=> r);
    //     result(r);
    // }
    public int clickIcon;
    public bool StartRoutine;
    public void DoubleClick(UnityAction unityAction)
    {
        if(!StartRoutine)
        {
            StartCoroutine(WaitForDoubleClick(unityAction));
        }else{
            clickIcon++;
        }
    }
    public IEnumerator WaitForDoubleClick(UnityAction unityAction)
    {
        clickIcon++;
        StartRoutine = true;
        float timer = 0.0f;
        //print(clickIcon);
        //print (clickIcon <= 1);
        while (clickIcon <= 2)
        {

            //print("sdfsdfsdf");
            timer += Time.deltaTime;
            if (timer > timeRequired)
            {
               // print("if");
                break;
            }
            else if (timer <= timeRequired && clickIcon == 2)
            {
               // print("else");
                unityAction();
                break;
            }

            yield return null;
        }
        clickIcon = 0;
        StartRoutine = false;
    }
    public void CheckNumberWireless()
    {
        if (wirless == 3)
            DB.instance.SetGeneralOption("wireless", "3");
        else if(wirless == 0)
        {
            DB.instance.SetGeneralOption("wireless", "1");
        }
        else
            DB.instance.SetGeneralOption("wireless", "2");
    }
    
    public void CheckNumberbackup()
    {
        if (backup == 3)
            DB.instance.SetGeneralOption("backup","3");
        else if(backup == 0)
        {
            DB.instance.SetGeneralOption("backup", "1");
        }
        else
            DB.instance.SetGeneralOption("backup", "2");
    }
    public void AddNumberSecurityReport()
    {
        securityReport++;
    }
    public void CheckNumberSecurityReport()
    {
        if (securityReport == 3)
            DB.instance.SetGeneralOption("reportSecurity", "3");
        else if (securityReport == 0)
        {
            DB.instance.SetGeneralOption("reportSecurity", "1");
        }
        else
            DB.instance.SetGeneralOption("reportSecurity", "2");
    }

    public void StartReportSpam()
    {
        StartCoroutine(IProccessReportSpam());
    }
    IEnumerator IProccessReportSpam()
    {
        var lvl = FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel];
        lvl.isStop = true;
        while (slider.value!=1)
        {
            slider.value = Mathf.MoveTowards(slider.value, 1, 0.3f * Time.deltaTime);
            yield return null;
        }
        _checkTask("AnswerMail");
        GetComponent<ChromeBroweser>().OnlyMinimizeChrome();
        AddNumberSecurityReport();
        lvl.isStop = false;
    }
    public void StartUpdate()
    {
        StartCoroutine(IProccessUpdateInMail());
    }
    IEnumerator IProccessUpdateInMail()
    {

        while (slider2.value != 1)
        {
            slider2.value = Mathf.MoveTowards(slider2.value, 1, 0.3f * Time.deltaTime);
            yield return null;
        }
        //_checkTask("AnswerMail");
        //GetComponent<ChromeBroweser>().OnlyMinimizeChrome();
        //AddNumberSecurityReport();
        objUpdate.SetActive(false);
        objNextQuiz.SetActive(true);

    }


}
