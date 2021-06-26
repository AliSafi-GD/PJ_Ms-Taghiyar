using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class WifiConnection : MonoBehaviour
{

    public Button btnTaskbarWifi, btnFastWifi, btnFastBluetooth;
    public Button btnWifiInWifiList;
    public Button btnConnect;
    public Image imgWifiTaskbar, imgFastWifi, imgFastBluetooth;
    public Sprite[] sprStateWifi, sprFastBluetooth;
    public Animator animWifiList, animWifiSelection;

    private void Start()
    {
        CheckConnection();
        btnTaskbarWifi.onClick.AddListener(() =>
        {
            if (!Windows10.connection)
                animWifiList.Play("Show");
        });
        btnWifiInWifiList.onClick.AddListener(() =>
        {
            animWifiSelection.Play("Show");
        });
        btnConnect.onClick.AddListener(() => StartCoroutine(WaitForConnect()));
        btnFastWifi.onClick.AddListener(() =>
        {
            if (Windows10.currentLevel == 1 || Windows10.currentLevel == 4 || Windows10.currentLevel == 10)
            {
                btnFastWifi.interactable = false;
                FastDisconnect();
                if(Windows10.currentLevel !=10)
                    Windows10.wirless++;
            }
                
        });
        btnFastBluetooth.onClick.AddListener(() =>
        {
            if (Windows10.currentLevel == 10)
            {
                btnFastBluetooth.interactable = false;
                FastDisconnectBluetooth();
                Windows10.wirless++;
            }

        });
    }
    IEnumerator WaitForConnect()
    {
        animWifiSelection.Play("Close");
        yield return new WaitForSeconds(0.3f);
        animWifiList.Play("Close");
        Windows10.connection = true;
        Windows10._checkTask("Connection");
        CheckConnection();
    }
    public void CheckConnection()
    {
        imgWifiTaskbar.sprite = Windows10.connection ? sprStateWifi[1] : sprStateWifi[0];
        imgFastWifi.sprite = Windows10.connection ? sprStateWifi[1] : sprStateWifi[0];
        imgFastBluetooth.sprite = Windows10.connectionBluetooth ? sprFastBluetooth[1] : sprFastBluetooth[0];
    }
    
    public void FastDisconnect()
    {
        Windows10.connection = !Windows10.connection;
        CheckConnection();
    }
    public void FastDisconnectBluetooth()
    {
        Windows10.connectionBluetooth = !Windows10.connectionBluetooth;
        CheckConnection();
    }
}