using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TypeDialog : MonoBehaviour
{
    public Text txtDialog;
    public string strDialog="";
    public Button answer1, answer2, answer3;
    public bool isVisible;
    public DialogueManagerAndAnswer answer;
    public enum TypeMode
    {
        Type, Answer,EndDialog
    }
    public TypeMode typeMode;
    public GameObject objDialog;
    public void Starter(Action<bool> result) {
        if (objDialog == null)
            objDialog = transform.GetChild(0).gameObject;

        objDialog.SetActive(true);
        switch (typeMode)
        {
            case TypeMode.Type:
                
                txtDialog.text = "";
                strDialog = strDialog.Replace("*", "\n");
                StartCoroutine(_Typing((r) => {
                    result(r);
                }));
                break;
            case TypeMode.Answer:

                answer1.onClick.AddListener(() =>
                {
                    answer.ForceFalseActiveDialogueObj();
                    result(true);
                });
                answer2.onClick.AddListener(() =>
                {
                    result(true);
                });
                answer3.onClick.AddListener(() =>
                {
                    result(true);
                });
                break;
        }
       
    }
    public IEnumerator _Typing(Action<bool> result){
        AudioType.PlayAudioType = true;
        var s = "";
        for (int i = 0; i < strDialog.Length; i++)
        {
            s += strDialog[i];
            txtDialog.text = s;
            yield return new WaitForSeconds(DialogManager.speedType);
            if (Input.GetMouseButton(0))
            {
                txtDialog.text = strDialog;
                break;
            }
        }
        AudioType.PlayAudioType = false;
        yield return new WaitForSeconds(1);
        //objDialog.SetActive(false);
        result(true);
    }
    
}
