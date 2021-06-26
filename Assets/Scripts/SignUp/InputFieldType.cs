using UnityEngine;
using UnityEngine.UI;

public class InputFieldType : MonoBehaviour {
    public enum InputMode
    {
        Name,Age,Major,City,Mail
    }
    public InputMode inputMode;
    public string txt;
    private void Start()
    {
        GetComponent<InputField>().onValueChanged.AddListener((string s) => txt = s);
    }
}