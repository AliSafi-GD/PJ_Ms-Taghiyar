using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GnsPuzzle : MonoBehaviour
{
    public PiecePuzzleGns[] piecesPuzzle;
    public RectTransform placePieces;
    public Transform puzzlePlace;
    public int currentTrue;
    public GameObject answer;
    public GameObject finishObj;
    public Animator anim;

    public int CurrentTrue {

        get
        {
            return currentTrue;
        }
        set
        {
            currentTrue = value;
            if (value >= piecesPuzzle.Length)
            {
                finishObj.SetActive(true);
                Destroy(t);
            }
                

        }
    }
    public void Finish()
    {
        anim.enabled = true;
        anim.Play("Close");
        Windows10._checkTask("mini game");
        //gameObject.SetActive(false);
    }
    public void FalseActiveObject()
    {
        gameObject.SetActive(false);
    }
    GameObject t;
    void Start()
    {
        Init();
        t = new GameObject("timer", typeof(TimerMiniGame));
    }

    public void Init()
    {
        foreach (var item in piecesPuzzle)
        {
            item.InitParametr(placePieces);
        }
        answer.SetActive(true);
    }
    public void StartAuto(float indexWait)
    {
        if(GameRefremce.instance!=null)
            GameRefremce.instance.Coin -= 50;
        StartCoroutine(AutoPlaySimPieces(indexWait));
    }

    IEnumerator AutoPlaySimPieces(float indexWait)
    {
        List<PiecePuzzleGns> listPieces = new List<PiecePuzzleGns>();
        LevelManagerInGame lvl = null;
        if (FindObjectOfType<TaskManager>())
        {
            lvl= FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel];
            lvl.isStop = true;
        }
       
        for (int i = 0; i < piecesPuzzle.Length/2; i++)
        {
            bool res = false;
            var rnd = UnityEngine.Random.Range(0, piecesPuzzle.Length);
            while (listPieces.Contains(piecesPuzzle[rnd]))
            {
                rnd = UnityEngine.Random.Range(0, piecesPuzzle.Length);
            }
            listPieces.Add(piecesPuzzle[rnd]);
            
            StartCoroutine(piecesPuzzle[rnd].MoveToMainPlace(result => res = result));
            //yield return new WaitUntil(() => res);
            yield return new WaitForSeconds(indexWait);
        }
        if (FindObjectOfType<TaskManager>())
            lvl.isStop = false;
        
    }

    

}

