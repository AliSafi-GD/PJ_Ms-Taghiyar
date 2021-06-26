using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkSpace : Task
{
    bool b;
    public Button btnStart;
    public override void starter(Action<bool> result)
    {
        btnStart.onClick.AddListener(()=>b=true);
       
        StartCoroutine(WaitForStatrt(res=>{
            result(res);
        }));
    }
    IEnumerator WaitForStatrt(Action<bool> res){
        yield return new WaitUntil(()=>b);
        res(b);
    }

}
