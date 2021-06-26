using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public string strQuestions;

    public List<string> str = new List<string>();

    [SerializeField] public List<QuizAndAnswer> quizAndAnswer;

    string[] strMode1 = new string[5] { "همیشه", "بیشتر اوقات", "گاهی", "به ندرت", "هرگز" },
             strMode2 = new string[5] { "کاملا موافق", "موافق", "نظری ندارم", "مخالف", "کاملا مخالف" },
             strMode3 = new string[5] { "بسیار کم", "کم", "متوسط", "زیاد", "بسیار زیاد" };

    public GameObject quizObj;
    public Transform trsParent;
    public void Create()
    {
        for (int i = 0; i < str.Count; i++)
        {
            var q = Instantiate(quizObj, trsParent).GetComponent<QuestionnaireToggle>();
            q.gameObject.name = (i + 1).ToString();
            quizAndAnswer.Add(new QuizAndAnswer((i + 1).ToString(),q));
        }
    }
    public void MinimizeWindow()
    {
        Screen.fullScreen = false;
    }
    public void Set()
    {
        str = strQuestions.Split('!').ToList();

        for (int i = 0; i < str.Count; i++)
        {
            if (str[i] == "")
                str.Remove(str[i]);
        }
        for (int i = 0; i < str.Count; i++)
        {
            quizAndAnswer[i].questionnaireToggles.StrQuiz = $"{i+1} - {str[i]}";
            switch (quizAndAnswer[i].answerMode)
            {
                case QuizAndAnswer.AnswerMode.Mode1:
                    quizAndAnswer[i].questionnaireToggles.StrToggles = strMode1;
                    break;
                case QuizAndAnswer.AnswerMode.Mode2:
                    quizAndAnswer[i].questionnaireToggles.StrToggles = strMode2;
                    break;
                case QuizAndAnswer.AnswerMode.Mode3:
                    quizAndAnswer[i].questionnaireToggles.StrToggles = strMode3;
                    break;
            }
        }
    }
}
[Serializable]
public class QuizAndAnswer
{
    public string nameObj;
    public QuestionnaireToggle questionnaireToggles;
    public enum AnswerMode
    {
        Mode1,Mode2,Mode3
    }
    public AnswerMode answerMode;

    public QuizAndAnswer(string s,QuestionnaireToggle questionnaireToggles)
    {
        this.nameObj = s;
        this.questionnaireToggles = questionnaireToggles;
    }
}
