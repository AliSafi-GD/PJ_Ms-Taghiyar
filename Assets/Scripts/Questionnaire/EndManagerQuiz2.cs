using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class EndManagerQuiz2 : MonoBehaviour
{
    // Start is called before the first frame update
    public QuestionnaireToggle[] toggles;
    public Button btnNext;
    Coroutine TimerQuiz;
    public GameObject waiting;
    public ScrollRect scrollRect;
    public Vector2 v;
    public Text txtCurrentQuiz;
    int currentAnswer;

    public int CurrentAnswer
    {
        get => currentAnswer;
        set
        {
            currentAnswer = value;
            txtCurrentQuiz.text = $"{value + 1} / 12 ";
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
            print(time1);
            DB.instance.SetDataToQuestion2("timer", time1);
        }
    }
    public void SaveTime()
    {
       
    }
    void Update()
    {
        //var t = (from x in toggles where x.isAnswer == false select x).ToArray();
        //btnNext.interactable = t.Length > 0 ? false : true;
        //var tt = (from x in toggles where x.isAnswer == false select x).ToArray();
        //foreach (var item in tt)
        //{
        //    //item.txtQuiz.color = Color.gray;
        //}
    }
    public void Next()
    {
 
           
        if (CurrentAnswer >= 11)
        {
                btnNext.gameObject.SetActive(true);
                return;
        }
           
        
        CurrentAnswer++;
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
    public void End()
    {
        StopCoroutine(TimerQuiz);
        
        FindObjectOfType<ConnectToServer>().Send(waiting);
    }
  
    IEnumerator INext(float sc)
    {
        yield return new WaitForSeconds(sc);
        while (scrollRect.content.anchoredPosition != v)
        {
            scrollRect.content.anchoredPosition = Vector2.Lerp(scrollRect.content.anchoredPosition, v, 10 * Time.deltaTime);
            if (Vector2.Distance(scrollRect.content.anchoredPosition, v) < 1)
                scrollRect.content.anchoredPosition = v;
            yield return null;
        }

    }
}
