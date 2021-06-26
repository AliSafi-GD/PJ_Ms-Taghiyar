using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GnsSite : MonoBehaviour
{

    public Image imgProgress, imgWink;
    public bool downloadIsDone,downloading;
    public GameObject footerDownload, contentDownload;
    IEnumerator Downloading()
    {
        while (imgProgress.fillAmount != 1)
        {
            contentDownload.SetActive(true);
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
            FindObjectOfType<ChromeBroweser>().MinimizeChrome();
            print("Open installer");
            contentDownload.SetActive(false);
        }

    }
    public void Download()
    {
        if (!downloading)
        {
            footerDownload.SetActive(true);
            StartCoroutine(Downloading());
            downloading = true;
        }
    }
}
