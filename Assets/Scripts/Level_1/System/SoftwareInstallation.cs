using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoftwareInstallation : MonoBehaviour
{
    public GameObject DreamwaeverInstaller;
    public GameObject installState;
    public Image progressinstallationDreamweaver, imgWink;
    public void InstallDreamweaver()
    {
        if (FindObjectOfType<Soft98ir>().downloadIsDone){
            DreamwaeverInstaller.SetActive(true);
            
        }
            
    }
    public void ClickInstallDreamweaver()
    {
        installState.SetActive(true);
        StartCoroutine(ProgressInstallation());
    }

    IEnumerator ProgressInstallation(){
        progressinstallationDreamweaver.fillAmount=0;
        while (progressinstallationDreamweaver.fillAmount!=1)
        {
            progressinstallationDreamweaver.fillAmount = Mathf.MoveTowards(progressinstallationDreamweaver.fillAmount,1,0.1f*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        var wink = gameObject.AddComponent<ItemSelectedAnimate>();
        wink.Wink(imgWink);
        DreamwaeverInstaller.SetActive(false);
        FindObjectOfType<ManageIconDesktop>().FindIco("Dreamweaver");
        Windows10._checkTask("install dreamweaver");
    }
    public void StopWink()
    {
        if (FindObjectOfType<ItemSelectedAnimate>() != null)
            FindObjectOfType<ItemSelectedAnimate>().StopWink();
    }
}
