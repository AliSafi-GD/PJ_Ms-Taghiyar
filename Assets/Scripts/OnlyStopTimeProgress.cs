using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyStopTimeProgress : MonoBehaviour
{
    public void StopTime()
    {
        var lvl = FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel];
        lvl.isStop = true;
    }
    public void StartTime()
    {
        var lvl = FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel];
        lvl.isStop = false;
    }
}
