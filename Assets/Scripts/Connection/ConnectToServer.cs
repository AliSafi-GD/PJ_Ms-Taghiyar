using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
public class ConnectToServer : MonoBehaviour
{
    public string url = "";
    // Start is called before the first frame update
    public static ConnectToServer instance;
    

    private void Awake()
    {
        instance = this;
        
    }
    public void Send(GameObject waiting)
    {
        StartCoroutine(Connection(waiting));
    }
    IEnumerator Connection(GameObject waiting)
    {
        waiting.SetActive(true);
        var j = JsonConvert.SerializeObject(DB.instance.infoAccount);
        Debug.Log(j);
        WWWForm form = new WWWForm();
        form.AddField("json", j);
        var req = UnityWebRequest.Post(url, form);
        yield return req.SendWebRequest();
        Debug.Log(req.downloadHandler.text);
        
        waiting.SetActive(false);
        
        
    }
    
    
}
