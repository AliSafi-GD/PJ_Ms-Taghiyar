using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckOS : MonoBehaviour
{
    public static bool isAndroid=false;
    public GameObject checkLand,checkMaximize;

    void Start()
    {
        var strOs = SystemInfo.operatingSystem;
        if (strOs.Contains("Android"))
        {
            isAndroid = true;
        }
    }

    private void Update()
    {
        CheckLandscape();
        //Screen.fullScreen = true;
        //TouchScreenKeyboard.Open("Write", TouchScreenKeyboardType.Default);
        checkMaximize.SetActive(!Screen.fullScreen);
    }

    public void CheckLandscape()
    {
        if (Screen.width > Screen.height)
        {
            
            checkLand.SetActive(false);
        }
        else
        {
            
            checkLand.SetActive(true);
        }
    }
}
