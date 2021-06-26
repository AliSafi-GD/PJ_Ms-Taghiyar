using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class ManageIconDesktop : MonoBehaviour
{
    [SerializeField] public Icon[] icons;
    public static int numberShowIcon;
    private void Start()
    {
        for (int i = 0; i < numberShowIcon; i++)
        {
            var windows10 = FindObjectOfType<Windows10>();
            icons[i].ShowIcon(windows10);
        }
    }
    public void FindIco(string str)
    {
        numberShowIcon++;
        var item = (from n in icons where n.itemName == str select n).FirstOrDefault();
        var windows10 = FindObjectOfType<Windows10>();
        item.ShowIcon(windows10);
        
    }
}
[Serializable]
public class Icon
{
    public string itemName;
    public GameObject ico, nameIco;
    public Button btn;

    public GameObject software;
    public void ShowIcon(Windows10 windows10)
    {
        ico.SetActive(true);
        nameIco.SetActive(true);
        btn = ico.transform.parent.GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            windows10.DoubleClick(()=>software.SetActive(true));            
        });
    }
}
