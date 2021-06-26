using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChromeBroweser : MonoBehaviour
{
    public Button btnIcon;
    public Text txtPrint,txtWrongPass;
    public GameObject[] objPages;
    public Button btnAgainConnection, btnSignUp, btnPrint;
    [SerializeField] NotificationBrowser[] notificationsBrowser = new NotificationBrowser[] { };
    // wfdc == Wait For Double Click

    public GameObject iconInTaskbar;
    public Animator anim;

    [Header("Signup")]
    public InputField inpName;
    public InputField inpLastName, inpMail, inpPassword, inpConfirmPassword;
    public string firstName, lastname,  password, confirm;
    public static string mail="";
    public bool isOpen;

    private void Start()
    {

        foreach (var item in notificationsBrowser)
        {
            item.SetEvent();
        }
        StartCoroutine(WaitForBtnsNotif());

       
        btnSignUp.onClick.AddListener(() =>
        {
            if(firstName != "" && lastname != "" && mail != "" && password != "" && confirm != "")
            {
                if ((password == confirm && password != ""))
                {
                    HandleDataForDB.instance.SetData($"{"passMail"}_{password}");
                    txtPrint.text = mail + "@prs.ir";
                    ChangePage(2);
                    SignupProblem(false, "");
                }
                   
                else
                {
                    SignupProblem(true, "گذرواژه ها مطابقت ندارند. دوباره امتحان کنید");
                }

            }
            else
            {
                SignupProblem(true, "لطفا فیلد ها را پر کنید");
            }
            
        });


        btnIcon.onClick.AddListener(() =>
        {
            FindObjectOfType<Windows10>().DoubleClick(OpenChrome);
        });
        btnPrint.onClick.AddListener(() =>
        {
            if(Windows10.currentLevel==1)
                MinimizeChrome();
        });


        inpName.onValueChanged.AddListener((x) =>
        {
            firstName = x;
        });
        inpLastName.onValueChanged.AddListener((x) =>
        {
            lastname = x;
        });
        inpMail.onValueChanged.AddListener((x) =>
        {
            mail = x;
        });
        inpPassword.onValueChanged.AddListener((x) =>
        {
            password = x;
        });
        inpConfirmPassword.onValueChanged.AddListener((x) =>
        {
            confirm = x;
        });
       // print("Set InputField");

        
    }
    public void MinimizeChrome()
    {
        anim.Play("Close");
        PrintMail();
    }
    public void OnlyMinimizeChrome()
    {
        anim.Play("Close");
        //PrintMail();
    }
    public void SignupProblem(bool active,string value)
    {
        txtWrongPass.text = value;
        txtWrongPass.gameObject.SetActive(active);
    }
    //void Update()
    //{
        
    //    var b = firstName != "" && lastname != "" && mail != "" && (password == confirm && password != "");
    //    if (b)
    //        btnSignUp.interactable = true;
    //    else
    //        btnSignUp.interactable = false;
    //}

    public void OpenChrome(int index)
    {
        isOpen = true;
        iconInTaskbar.SetActive(true);
        anim.gameObject.SetActive(true);
        anim.Play("Open");
        CheckConnection(index);
        btnAgainConnection.onClick.AddListener(() =>
        {
            CheckConnection(index);
        });
    }
    public void OpenChrome()
    {
        isOpen = true;
        iconInTaskbar.SetActive(true);
        anim.gameObject.SetActive(true);
        anim.Play("Open");
        btnAgainConnection.onClick.AddListener(() =>
        {
            CheckConnection(1);
        });
        CheckConnection(1);
    }
    void CheckConnection(int defaultIndex)
    {
        if (Windows10.connection)
            ChangePage(defaultIndex);
        else
            ChangePage(0);

    }
    public void ChangePage(int index)
    {
        foreach (var item in objPages)
        {
            item.SetActive(false);
        }
        objPages[index].SetActive(true);
    }
    IEnumerator WaitForBtnsNotif()
    {
        yield return new WaitUntil(() => (from x in notificationsBrowser where x.isClicked == true select x).ToArray().Length >= notificationsBrowser.Length);
        Windows10._checkTask("Create Mail");
        ChangePage(3);
    }
    void PrintMail()
    {
        Windows10._checkTask("Print Mail");
    }

}
[Serializable]
class NotificationBrowser
{
    public Toggle[] buttons = new Toggle[] { };
    public bool isClicked;
    [SerializeField] string taskName = null;
    public void SetEvent()
    {
        foreach (var item in buttons)
        {
            item.onValueChanged.AddListener((x) =>
            {
                isClicked = true;
                Windows10._checkTask(taskName);
            });
        }
    }
}
