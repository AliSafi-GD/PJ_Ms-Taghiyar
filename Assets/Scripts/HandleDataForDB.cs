using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDataForDB : MonoBehaviour
{
    public static HandleDataForDB instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        
    }
   
    public void SetData(string query)
    {
        var data = query.Split('_');
        if (DB.instance != null)
            DB.instance.SetData(LevelManager.currenLevel, data[0], data[1]);
    }
    public void SetDataQ1(string query)
    {
        var data = query.Split('_');
       // print(query.Length + "  number Query split");
        if (DB.instance != null)
            DB.instance.SetDataToQuestion1(data[0], data[1]);
    }
    public void SetDataQ2(string query)
    {
        var data = query.Split('_');
        if (DB.instance != null)
            DB.instance.SetDataToQuestion2(data[0], data[1]);
    }
}
