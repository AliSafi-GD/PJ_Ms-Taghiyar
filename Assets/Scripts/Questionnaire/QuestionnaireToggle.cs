using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class QuestionnaireToggle : MonoBehaviour
{
    //public Text txtQuiz;
    private string strQuiz;

    public Text[] txtToggles;
    public Toggle[] tgls;
    private string[] strToggles;
   // public bool isAnswer;
    string quizSetData;
    public string nameQuiz;
    public int numberQuiz;

    void SetTgls()
    {
        tgls = GetComponentsInChildren<Toggle>();
        //print(tgls.Length);
        foreach (var item in tgls)
        {
            item.onValueChanged.RemoveAllListeners();
            item.onValueChanged.AddListener(res =>
            {
                if (numberQuiz == 1 && res)
                {
                    QuizSetData = item.GetComponent<SetDataQuiz1>().number;
                    FindObjectOfType<EndManagerQuiz1>().Next();
                }
                    
                else if (numberQuiz == 2 && res)
                {
                    QuizSetData = item.GetComponent<SetDataQuiz1>().number;
                    FindObjectOfType<EndManagerQuiz2>().Next();
                }
                    
            });
        }
    }
    //public void CheckAnswer(bool res)
    //{
    //    print("set next");
        
    //        if (numberQuiz == 1 && res)
    //            FindObjectOfType<EndManagerQuiz1>().Next();
    //        else if (numberQuiz == 2 && res)
    //            FindObjectOfType<EndManagerQuiz2>().Next();
       
    //    //isAnswer = true;
    //    // txtQuiz.color = new Color(0.9333333f, 0.9333333f, 0.9333333f, 1);
    //}
    private void Awake()
    {
        nameQuiz = name;
        SetTgls();
    }
    public string StrQuiz
    {
        get
        {
            return strQuiz;
        }
        set
        {
            strQuiz = value;
            //txtQuiz.text = value;
        }
    }

    public string[] StrToggles
    {
        get
        {
            return strToggles;
        }
        set
        {
            strToggles = value;
            for (int i = 0; i < value.Length; i++)
            {
                txtToggles[i].text = value[i];
            }
        }
    }

    public string QuizSetData
    {

        //get => quizSetData;
        set {
            quizSetData = nameQuiz + "_" + value;
            print(quizSetData);
            if (numberQuiz == 1)
                HandleDataForDB.instance.SetDataQ1(quizSetData);
            else
                HandleDataForDB.instance.SetDataQ2(quizSetData);
        }

    }
}
