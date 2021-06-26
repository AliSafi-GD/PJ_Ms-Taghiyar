using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMiniGame : MonoBehaviour
{
    public Coroutine routin;
    private void Start()
    {
        routin = StartCoroutine(Timer(LevelManager.currenLevel));
        //Debug.Log("Create Timer");
    }
    
    IEnumerator Timer(int x)
    {
        int t = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            t++;
            var time = $"{(t / 60).ToString("00")} : {(t % 60).ToString("00")}";
            switch (x)
            {
                case 0:
                    if(DB.instance!=null)
                        DB.instance.infoAccount.lvl1.timerMiniGame = time;
                    break;
                case 1:
                    if (DB.instance != null)
                        DB.instance.infoAccount.lvl2.timerMiniGame = time;
                    break;
                case 2:
                    if(DB.instance!=null)
                        DB.instance.infoAccount.lvl3.timerMiniGame = time;
                    break;
                case 3:
                    if(DB.instance!=null)
                        DB.instance.infoAccount.lvl4.timerMiniGame = time;
                    break;
                case 4:
                    if(DB.instance!=null)
                        DB.instance.infoAccount.lvl5.timerMiniGame = time;
                    break;
            }
        }
    }
 
   
}
