using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSendData : MonoBehaviour
{
    Toggle tgl;
    public string query;

    private void Start()
    {
        tgl = GetComponent<Toggle>();
        tgl.onValueChanged.AddListener(result =>
        {
            if (result)
                HandleDataForDB.instance.SetData(query);
        });
    }
}
