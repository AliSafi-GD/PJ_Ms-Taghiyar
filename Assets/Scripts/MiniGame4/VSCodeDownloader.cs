using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSCodeDownloader : MonoBehaviour
{
    public Image imgProgress, imgWink;
    bool downloadIsDone, downloading;
    public GameObject footerDownload;
    public GameObject vsCodeSoftwere,contentDownload;
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
        if (GetComponent<ItemSelectedAnimate>())
            GetComponent<ItemSelectedAnimate>().StopWink();
    }
    public void ClickOnDownload()
    {
        if (downloadIsDone)
        {
            FindObjectOfType<ChromeBroweser>().OnlyMinimizeChrome();
            vsCodeSoftwere.SetActive(true);
            print("Open installer");
            contentDownload.SetActive(false);
        }

    }
    public void Download()
    {
        if (!downloading)
        {
            footerDownload.SetActive(true);
            contentDownload.SetActive(true);
            StartCoroutine(Downloading());
            downloading = true;
        }
    }
}
