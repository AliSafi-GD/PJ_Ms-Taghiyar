using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyboardAndroid : MonoBehaviour
{
    Animator anim;
    public Text[] txtPreview;
    public string strPreview;
    public InputField currentInp;
    public Button[] keys;
    public static KeyboardAndroid instance;
    public string StrPreview
    {
        get
        {
            return strPreview;
        }
        set
        {
            strPreview = value;
            txtPreview[0].text = value;
            txtPreview[1].text = value;
            
            currentInp.text = StrPreview;
            currentInp.Select();
        }
    }
    private void Start()
    {
        foreach (var item in keys)
        {
            item.onClick.AddListener(()=> {
                AddLetter(item.gameObject.GetComponent<Text>().text);
                print("Click");
            });
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideKeboard();
        }
    }
    public void AddLetter(string letter)
    {
        StrPreview += letter;
        
    }
    public void RemoveLetter()
    {
        if(StrPreview.Length > 0)
        StrPreview = StrPreview.Remove(StrPreview.Length - 1);
    }
    public void SetInputField(InputField index)
    {
        currentInp = index;
        StrPreview = "";
    }
    public void ShowKeyboard()
    {
        if(CheckOS.isAndroid)
        anim.Play("Open");
    }
    public void HideKeboard()
    {
        anim.Play("Hide");
    }
    
}
