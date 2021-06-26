
using UnityEngine;
using UnityEngine.UI;


public class SignUp : MonoBehaviour
{

    [Header("Sign Up Page")]
    public InputField[] inputFields;
    public Toggle[] toggles;

    public Button[] buttons;
    public GameObject[] pages;
    public static bool isDoneSignup;
    private void Start()
    {
        if (!isDoneSignup)
            SwitchPage(0);
        else
            SwitchPage(2);

        SetInputField();
        SetToggle();
        SetButton();
        

    }
    void SetInputField()
    {
        foreach (var item in inputFields)
        {
            item.onEndEdit.AddListener((string s) => SetInputField(item.GetComponent<InputFieldType>().inputMode, item.GetComponent<InputFieldType>().txt));
            if (item.GetComponent<InputFieldType>().inputMode == InputFieldType.InputMode.Mail)
                SetInputField(item.GetComponent<InputFieldType>().inputMode, "null");
        }
    }
    void SetToggle()
    {
        foreach (var item in toggles)
        {
            var txt = item.GetComponent<ToggleGroupType>().txt;
            //var txt = item.GetComponent<ToggleGroupType>().txt;
            item.onValueChanged.AddListener((bool b) => SetToggleGroup(item.GetComponent<ToggleGroupType>().togglesMode, txt));
            if (item.isOn)
                SetToggleGroup(item.GetComponent<ToggleGroupType>().togglesMode, txt);
        }
    }
    void SetButton()
    {
        foreach (var item in buttons)
        {
            item.onClick.AddListener(() => ButtonAction(item.GetComponent<ButtonType>().mode));
        }
    }
    void CheckIsFullSpecification()
    {
        DB.instance.infoAccount.IsFullAllSpecification((result) =>
        {
            //print("Is full");
            buttons[0].interactable = result;
        });
    }
    void SetInputField(InputFieldType.InputMode mode, string str)
    {
        switch (mode)
        {
            case InputFieldType.InputMode.Name:
                DB.instance.infoAccount.userName = str;
                break;
            case InputFieldType.InputMode.City:
                DB.instance.infoAccount.city = str;
                break;
            case InputFieldType.InputMode.Age:
                DB.instance.infoAccount.age = str;
                break;
            case InputFieldType.InputMode.Major:
                DB.instance.infoAccount.major = str;
                break;
            case InputFieldType.InputMode.Mail:
                DB.instance.infoAccount.mail = str.Length == 0 ? "null" : str;
                break;
        }
        CheckIsFullSpecification();
    }
    void SetToggleGroup(ToggleGroupType.TogglesMode mode, string str)
    {
        switch (mode)
        {
            case ToggleGroupType.TogglesMode.Gender:
                
                DB.instance.infoAccount.gender =(str);
                break;
            case ToggleGroupType.TogglesMode.Grade:
                
                DB.instance.infoAccount.education = (str);
                break;
        }
        //DB.instance.infoAccount.gender = str;
        CheckIsFullSpecification();
    }



    public void ButtonAction(ButtonType.Mode mode)
    {
        switch (mode)
        {
            case ButtonType.Mode.Next:
                SwitchPage(1);
                break;
            case ButtonType.Mode.Finish:
                GameRefremce.instance.Coin += 1000;
                SwitchPage(2);
                GameRefremce.instance.IsRun = true;
                isDoneSignup = true;
                break;
        }
    }
    void SwitchPage(int index)
    {
        foreach (var item in pages)
        {
            item.SetActive(false);
        }
        pages[index].SetActive(true);
    }


}
