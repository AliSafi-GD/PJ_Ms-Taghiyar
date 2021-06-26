using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class EndManagerQuiz1 : MonoBehaviour
{
    // Start is called before the first frame update
    public QuestionnaireToggle[] toggles;
    public Button btnNext;
    public int totalScore;
    public Text txtResult;
    Coroutine TimerQuiz;
    public ScrollRect scrollRect;
    public Vector2 v;
    public Text txtCurrentQuiz;
    public int currentAnswer;

    public int CurrentAnswer
    {
        get => currentAnswer;
        set
        {
            currentAnswer = value;
            txtCurrentQuiz.text = $"{value+1} / 21 ";
        }
    }

    private void Start()
    {
        TimerQuiz = StartCoroutine(Timer());
        
    }
    IEnumerator Timer()
    {
        int t = 0;
        string time1 = "";
        while (true)
        {
            yield return new WaitForSeconds(1);
            t++;
            time1 = $"{(t / 60).ToString("00")} : {(t % 60).ToString("00")}";
           // print(time1);
            DB.instance.SetDataToQuestion1("timer", time1);
        }
    }
    public void SaveTime()
    {
        StopCoroutine(TimerQuiz);
    }
    public void TotalScore()
    {
        var dic = DB.instance.infoAccount.question1;
        foreach (var item in dic)
        {
            print(item.Value);
            if (item.Key == "timer")
                continue;
            totalScore += Int32.Parse(item.Value);
        }
        if (totalScore <= 49)
        {
            txtResult.text = "سطح رعایت نکات امنیتی توسط شما پایین است";
        }
        else if (totalScore <= 78 && totalScore >= 50)
        {
            txtResult.text = "سطح رعایت نکات امنیتی توسط شما متوسط است";
        }
        else if (totalScore >= 79)
        {
            txtResult.text = "سطح رعایت نکات امنیتی توسط شما بالا است";
        }

    }
    public void Next()
    {
        if (CurrentAnswer >= 20)
        {
            btnNext.gameObject.SetActive(true);
            return;
            
        }
            
        CurrentAnswer++;
        //print("Next"+currentAnswer);
        v.y = -toggles[CurrentAnswer].GetComponent<RectTransform>().anchoredPosition.y - (toggles[CurrentAnswer].GetComponent<RectTransform>().sizeDelta.y / 2);
        //print((toggles[CurrentAnswer].GetComponent<RectTransform>().sizeDelta.y / 2));
        StartCoroutine(INext(0.2f));
        
    }
    public void Back()
    {
        if (CurrentAnswer <= 0)
            return;
        CurrentAnswer--;
        v.y -= toggles[CurrentAnswer].GetComponent<RectTransform>().sizeDelta.y + 50;
        StartCoroutine(INext(0));

    }
    void Update()
    {
        
        //var t = (from x in toggles where x.isAnswer == false select x).ToArray();
        //btnNext.interactable = t.Length > 0 ? false : true;
        //var tt = (from x in toggles where x.isAnswer == false select x).ToArray();
        //foreach (var item in tt)
        //{
        //    item.txtQuiz.color = Color.gray;
        //}
    }
    IEnumerator INext(float sec)
    {
        yield return new WaitForSeconds(sec);
        while (scrollRect.content.anchoredPosition != v)
        {
            scrollRect.content.anchoredPosition = Vector2.Lerp(scrollRect.content.anchoredPosition, v, 5 * Time.deltaTime);
            if (Vector2.Distance(scrollRect.content.anchoredPosition, v) < 1)
                scrollRect.content.anchoredPosition = v;
            yield return null;
            

        }
        
    }
   
    

}
