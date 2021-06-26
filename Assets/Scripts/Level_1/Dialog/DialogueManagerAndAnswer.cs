using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerAndAnswer : Task
{
    public Button  btnHelp;
    bool clickHelp;
    public TypeDialog[] typeDialogs;
    public static float speedType = 0.07f;
    Coroutine nextDialog;
    private void Start()
    {
        btnHelp.onClick.AddListener(() => { clickHelp = true; print(clickHelp); });
        //starter(res=>{

        //});
    }
    public override void starter(Action<bool> result) => nextDialog = StartCoroutine(NextDialog(r =>
    {
        result(r);
    }));
    IEnumerator NextDialog(Action<bool> result)
    {
        foreach (var item in typeDialogs)
        {
            //item.objDialog.SetActive(true);
            clickHelp = false;
            btnHelp.gameObject.SetActive(false);
            bool endDialog = false;
            
            item.Starter(res => endDialog = res);
            yield return new WaitUntil(() => endDialog);
            
            if (breakDialog)
                break;
            if (item.typeMode == TypeDialog.TypeMode.Type)
            {
                btnHelp.gameObject.SetActive(true);
                yield return new WaitUntil(() => clickHelp);
            }
            if(!item.isVisible)
                item.objDialog.SetActive(false);
        }
        // =====>>> Finish
        result(true);
    }
    public void ForceFalseActiveDialogueObj()
    {
        foreach (var item in typeDialogs)
        {
            if(item.objDialog!=null)
                item.objDialog.SetActive(false);
        }
    }
    bool breakDialog = false;
    public void ChangeEndDialog()
    {
        breakDialog = true;
    }
}
