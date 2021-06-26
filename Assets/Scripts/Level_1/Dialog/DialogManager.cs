using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : Task
{
    
    public Button btnHelp;
    bool clickHelp;
    public TypeDialog[] typeDialogs;
    public static float speedType = 0.04f;
    private void Start() {
        btnHelp.onClick.AddListener(() => { clickHelp = true;});
    }
    public override void starter(Action<bool> result) => StartCoroutine(NextDialog(r =>
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
            btnHelp.gameObject.SetActive(true);
            yield return new WaitUntil(() => clickHelp);
            item.objDialog.SetActive(false);
        }
        // =====>>> Finish
        result(true);

    }


}
