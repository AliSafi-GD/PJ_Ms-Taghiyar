using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoftwereInstallerGNS : MonoBehaviour
{
    public GameObject GNSInstaller;
    public Slider progressInstall;
    public GameObject finishInstall;
    public Image imgWink;
    public void InstallGNS()
    {
        if (FindObjectOfType<GnsSite>().downloadIsDone)
        {
            GNSInstaller.SetActive(true);

        }

    }
    public void StartInstallSoftwere()
    {
        StartCoroutine(IProgress());
    }
    IEnumerator IProgress()
    {
        while (progressInstall.value < 1)
        {
            progressInstall.value = Mathf.MoveTowards(progressInstall.value, 1, 0.5f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        var wink = gameObject.AddComponent<ItemSelectedAnimate>();
        wink.Wink(imgWink);
        finishInstall.SetActive(true);
    }
    public void StopWink()
    {
        if(FindObjectOfType<ItemSelectedAnimate>()!=null)
        FindObjectOfType<ItemSelectedAnimate>().StopWink();
    }
}
