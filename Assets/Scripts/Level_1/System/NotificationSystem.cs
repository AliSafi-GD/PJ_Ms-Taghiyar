using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
public class NotificationSystem : MonoBehaviour
{

    public Text txtTitle, txtDesctipntion;
    public Button btnYes,
        btnNo,
        btnLater,
        confirmDescription,
        showDes,
        noShow,
        laterShow,
        btnConnectAgain,
        btnShowLink,
        answer1,
        answer2,
        btnBlutooth,
        acceptTrasform,
        finishTransfer,
        startMiniGame,
        btnSpamMail,
        btnReportScurity,
        btnAuto1,btnAuto2,btnAuto3,
        btnBackupNo,
        btnBackupYes;
    public GameObject[] objPanel;
    public Animator animator;
    public Slider sliderProgress;
    public float speedProgress;
    public int notiForLevel;
    public GameObject miniGame5;
    void CreateSoundBtn()
    {
        GetComponent<Windows10>().CreateSound(GetComponent<Windows10>().audioClips.Find(x => x.nameAudio == "Button").audioClip);
    }
    public IEnumerator Starter(Action<bool> result, NotificationContent content, string strName)
    {
        
        yield return new WaitForEndOfFrame();
        btnLater.onClick.RemoveAllListeners();
        btnConnectAgain.onClick.RemoveAllListeners();
        btnNo.onClick.RemoveAllListeners();
        btnYes.onClick.RemoveAllListeners();
        btnShowLink.onClick.RemoveAllListeners();
        SetNotif(content.strTitle, content.strDescription);
        animator.Play("Show");
        switch (content.type)
        {
            case NotificationContent.TypeActionNotification.Answer:
                ChangePanel(0);
                btnLater.onClick.AddListener(() =>
                {
                    HandleDataForDB.instance.SetData($"{strName}_{"2"}");
                    CreateSoundBtn();
                    animator.Play("Close");
                    result(true);
                    Windows10._checkTask(strName);
                });
                btnNo.onClick.AddListener(() =>
                {
                    HandleDataForDB.instance.SetData($"{strName}_{"1"}");
                    CreateSoundBtn();
                    animator.Play("Close");
                    result(true);
                    Windows10._checkTask(strName);
                });
                btnYes.onClick.AddListener(() =>
                {
                    if (content.coin != 0)
                        if(GameRefremce.instance!=null)
                            GameRefremce.instance.Coin -= content.coin;
                    if (Windows10.connection)
                    {
                        CreateSoundBtn();
                        StartCoroutine(UpdateProcess(res =>
                        {
                            
                            animator.Play("Close");
                            result(res);
                        }));
                        HandleDataForDB.instance.SetData($"{strName}_{"3"}");
                        Windows10._checkTask(strName);
                    }
                    else
                        NoConnection();
                });
                btnConnectAgain.onClick.AddListener(() =>
                {
                    if (Windows10.connection)
                    {
                        CreateSoundBtn();
                        HandleDataForDB.instance.SetData($"{strName}_{"3"}");
                        StartCoroutine(UpdateProcess(res =>
                        {
                            animator.Play("Close");
                            result(res);
                        }));
                        Windows10._checkTask(strName);
                    }
                    else
                        NoConnection();
                });
                break;
            case NotificationContent.TypeActionNotification.ShowLink:
                ChangePanel(3);
                btnShowLink.onClick.AddListener(() =>
                {
                    CreateSoundBtn();
                    var chrome = FindObjectOfType<ChromeBroweser>();

                    switch (strName)
                    {
                        case "Link1":

                            chrome.OpenChrome(4);

                            break;
                        case "Link2":

                            chrome.OpenChrome(6);

                            break;
                        case "Link3":

                            chrome.OpenChrome(7);

                            break;
                        case "Link4":

                            chrome.OpenChrome(8);

                            break;
                        case "Link5":

                            chrome.OpenChrome(9);

                            break;
                        case "Link6":

                            chrome.OpenChrome(10);

                            break;
                        case "Link7":

                            chrome.OpenChrome(11);

                            break;
                        case "Link8":
                            chrome.OpenChrome(12);
                            break;
                    }

                    Windows10._checkTask(strName);
                    animator.Play("Close");
                    result(true);
                });
                
                break;
            case NotificationContent.TypeActionNotification.ShowDescription:

                ChangePanel(11);
                showDes.onClick.AddListener(() =>
                {
                    var lvl = FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel];
                    lvl.isStop = true;
                    HandleDataForDB.instance.SetData($"{strName}_{"3"}");
                    CreateSoundBtn();
                    ChangePanel(12);
                    confirmDescription.onClick.AddListener(() =>
                    {
                        lvl.isStop = false;
                        CreateSoundBtn();
                        animator.Play("Close");
                        Windows10._checkTask(strName);
                        result(true);
                    });
                });
                noShow.onClick.AddListener(() =>
                {

                    HandleDataForDB.instance.SetData($"{strName}_{"1"}");
                    CreateSoundBtn();
                    animator.Play("Close");
                    Windows10._checkTask(strName);
                    result(true);
                });
                laterShow.onClick.AddListener(() =>
                {

                    HandleDataForDB.instance.SetData($"{strName}_{"2"}");
                    CreateSoundBtn();
                    animator.Play("Close");
                    Windows10._checkTask(strName);
                    result(true);
                });
                break;
            case NotificationContent.TypeActionNotification.Answer2:
                ChangePanel(4);
                answer1.onClick.AddListener(() =>
                {
                    CreateSoundBtn();
                StartCoroutine(UpdateProcessFake(res =>
                    {
                        animator.Play("Close");
                        result(res);
                    }));
                    Windows10._checkTask(strName);
                });
                answer2.onClick.AddListener(() =>
                {
                    CreateSoundBtn();
                    StartCoroutine(UpdateProcess(res =>
                    {
                        animator.Play("Close");
                        result(res);
                    }));
                    Windows10._checkTask(strName);
                });
                break;
            case NotificationContent.TypeActionNotification.bluetooth:
                ChangePanel(5);
                btnBlutooth.onClick.AddListener(() =>
                {
                    CreateSoundBtn();
                    animator.Play("Close");
                    result(true);
                    Windows10._checkTask(strName);
                    Windows10.connectionBluetooth = true;
                });

                break;
            case NotificationContent.TypeActionNotification.TransferFile:

                ChangePanel(6);
                acceptTrasform.onClick.AddListener(() =>
                {
                    CreateSoundBtn();
                    StartCoroutine(UpdateProcess(res =>
                    {
                        animator.Play("Close");
                        result(res);
                    }));
                    Windows10._checkTask(strName);
                });

                break;
            case NotificationContent.TypeActionNotification.FinishFileTransfer:
                ChangePanel(7);
                finishTransfer.onClick.AddListener(() =>
                {
                    CreateSoundBtn();
                    animator.Play("Close");
                    result(true);
                    Windows10._checkTask(strName);
                });
                break;
            case NotificationContent.TypeActionNotification.StartMiniGame5:
                ChangePanel(8);
                startMiniGame.onClick.AddListener(() =>
                {
                    CreateSoundBtn();
                    animator.Play("Close");
                    result(true);
                    Windows10._checkTask(strName);
                    miniGame5.SetActive(true);
                });
                break;
            case NotificationContent.TypeActionNotification.SpamMail:
                ChangePanel(9);
                btnSpamMail.onClick.AddListener(() =>
                {
                    CreateSoundBtn();
                    animator.Play("Close");
                    result(true);
                    Windows10._checkTask(strName);
                    
                });
                btnReportScurity.onClick.AddListener(() =>
                {
                    CreateSoundBtn();
                    StartCoroutine(UpdateProcess(res =>
                    {
                        animator.Play("Close");
                        result(res);
                    }));
                    Windows10._checkTask(strName);
                });
                
                break;
            case NotificationContent.TypeActionNotification.AutoOpenUsb:
                ChangePanel(10);
                btnAuto1.onClick.AddListener(() =>
                {
                    HandleDataForDB.instance.SetData($"{strName}_{"1"}");
                    CreateSoundBtn();
                    animator.Play("Close");
                    result(true);
                    Windows10._checkTask(strName);
                    
                });
                btnAuto2.onClick.AddListener(() =>
                {
                    HandleDataForDB.instance.SetData($"{strName}_{"2"}");
                    CreateSoundBtn();
                    animator.Play("Close");
                    result(true);
                    Windows10._checkTask(strName);
                   
                });
                btnAuto3.onClick.AddListener(() =>
                {
                    HandleDataForDB.instance.SetData($"{strName}_{"3"}");
                    CreateSoundBtn();
                    animator.Play("Close");
                    result(true);
                    Windows10._checkTask(strName);
                    
                });
                break;
            case NotificationContent.TypeActionNotification.backup:
                ChangePanel(13);
                btnBackupNo.onClick.AddListener(() =>
                {
                    CreateSoundBtn();
                    animator.Play("Close");
                    result(true);
                    Windows10._checkTask(strName);
                });
                btnBackupYes.onClick.AddListener(() =>
                {
                    Windows10.backup++;


                    CreateSoundBtn();
                    StartCoroutine(UpdateProcess(res =>
                    {
                        HandleDataForDB.instance.SetData($"{strName}_{"3"}");
                        animator.Play("Close");
                        result(res);
                    }));
                    Windows10._checkTask(strName);


                });
                break;
        }
        
        yield return null;


        sliderProgress.value = 0;

    }

    IEnumerator UpdateProcess(Action<bool> result)
    {
        var lvl = FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel];
        lvl.isStop = true;
        ChangePanel(2);
        while (sliderProgress.value != 1)
        {
            sliderProgress.value = Mathf.MoveTowards(sliderProgress.value, 1.0f, speedProgress * Time.deltaTime);
            yield return null;
        }
        lvl.isStop = false;
        result(true);
    }
    IEnumerator UpdateProcessFake(Action<bool> result)
    {
        //var lvl = FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel];
        //lvl.isStop = true;
        ChangePanel(2);
        while (sliderProgress.value != 1)
        {
            sliderProgress.value = Mathf.MoveTowards(sliderProgress.value, 1.0f, speedProgress * Time.deltaTime);
            yield return null;
        }
        //lvl.isStop = false;
        result(true);
    }
    void NoConnection()
    {
        ChangePanel(1);
    }
    void ChangePanel(int currentPanel)
    {
        foreach (var item in objPanel)
        {
            item.SetActive(false);
        }
        objPanel[currentPanel].SetActive(true);
    }
    void SetNotif(string strTitle, string strDescription)
    {
        // print($"title = {strTitle} _ description = {strDescription}" );
        txtTitle.text = strTitle;
        txtDesctipntion.text = strDescription;
    }
}
