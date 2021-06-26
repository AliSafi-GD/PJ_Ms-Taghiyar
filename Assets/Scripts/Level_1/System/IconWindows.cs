using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconWindows : MonoBehaviour
{


    public Button btnWindows, btnPower, btnLogoff;

    public Animator animWindow, animPower;
    
    private void Start()
    {
        btnWindows.onClick.AddListener(() =>
        {
            if (Windows10.currentLevel == 1 || Windows10.currentLevel == 4 || Windows10.currentLevel == 10)
            {
                GetComponent<WifiConnection>().CheckConnection();
                animWindow.gameObject.SetActive(true);
                animWindow.Play("Open");
            }
        });
        btnPower.onClick.AddListener(() =>
        {
            animPower.gameObject.SetActive(true);
            animPower.Play("Open");
        });
        btnLogoff.onClick.AddListener(() =>
        {
            //if (Windows10.currentLevel == 1 || Windows10.currentLevel == 9)
            //{
                Windows10._checkTask("Logoff");
                //Windows10.currentLevel++;
            //}
        });

    }

}
