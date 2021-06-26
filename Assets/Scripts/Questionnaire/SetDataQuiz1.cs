using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDataQuiz1 : MonoBehaviour
{
    public QuestionnaireToggle toggles;
    public Toggle toggle;
    public string number;
    private void Awake()
    {
        toggles = transform.GetComponentInParent<QuestionnaireToggle>();
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(result =>
        {
            
            if (result)
                SetData();
        });
    }
    public void SetData()
    {
        toggles.QuizSetData = number;
    }
}
