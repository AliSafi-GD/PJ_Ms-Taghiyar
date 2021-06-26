using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soft98ir : MonoBehaviour
{
    public GameObject popup;
    public GameObject[] subsetPopup;

    public GameObject graphUserDownload;
    public GameObject footerDownload;

    public Image imgProgress, imgWink;

    public GameObject contentDownload;
    public bool downloading, downloadIsDone;

    public void ChangePagePopup(int index)
    {
        foreach (var item in subsetPopup)
        {
            item.SetActive(false);
        }
        subsetPopup[index].SetActive(true);
    }
    public void GetTemplate()
    {
        ChangePagePopup(1);
    }
    public void EndGetTemplate()
    {
        ChangePagePopup(2);
    }
    public void Question()
    {
        popup.SetActive(false);
    }

    public void Download()
    {
        if (!downloading)
        {
            contentDownload.SetActive(true);
            footerDownload.SetActive(true);
            StartCoroutine(Downloading());
        }
    }
    IEnumerator Downloading()
    {
        while (imgProgress.fillAmount != 1)
        {
            imgProgress.fillAmount = Mathf.MoveTowards(imgProgress.fillAmount, 1, 0.1f * Time.deltaTime);
            yield return null;
        }
        var wink = gameObject.AddComponent<ItemSelectedAnimate>();
        wink.Wink(imgWink);
        downloadIsDone = true;
    }
    public void StopWink()
    {
        if(GetComponent<ItemSelectedAnimate>())
            GetComponent<ItemSelectedAnimate>().StopWink();
    }
    public void ClickOnDownload()
    {
        if (downloadIsDone)
        {
            FindObjectOfType<ChromeBroweser>().MinimizeChrome();
            //print("Open installer");
            contentDownload.SetActive(false);
        }
        
    }
    public void CellLicense(int coin)
    { 
        GameRefremce.instance.Coin -= coin;
    }
    private void Start()
    {
        popup.SetActive(true);
        ChangePagePopup(0);
    }

}
