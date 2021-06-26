using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class PiecePuzzleGns : MonoBehaviour , IDragHandler,IPointerUpHandler,IPointerDownHandler
{

    public Vector2 mainPosition, movePosition;
    //public Image img;
    public RectTransform rectTransform;
    GnsPuzzle puzzle;
    Vector2 mainSize,minSize;
    public bool isDrag=true;
   
    public void InitParametr(RectTransform trs)
    {
       
        puzzle = FindObjectOfType<GnsPuzzle>();
        rectTransform = GetComponent<RectTransform>();
        mainSize = rectTransform.sizeDelta;
        mainPosition = rectTransform.anchoredPosition;
        transform.SetParent(trs);
        movePosition = MovePosition(trs);
        

        minSize = new Vector2(rectTransform.sizeDelta.x / 2, rectTransform.sizeDelta.y / 2);

        moveToBack = StartCoroutine(MoveToPlace(rectTransform, movePosition));
    }

    public Vector2 MovePosition(RectTransform placePieces)
    {
        Vector2 vec = new Vector2();
        vec.x = UnityEngine.Random.Range(-(placePieces.sizeDelta.x / 2) + 20, (placePieces.sizeDelta.x / 2) - 20);
        vec.y = UnityEngine.Random.Range(-(placePieces.sizeDelta.y / 2) + 20, (placePieces.sizeDelta.y / 2) - 20);
        return vec;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDrag)
        {
            transform.position = eventData.position;
            rectTransform.sizeDelta = mainSize;
        }
        
    }

    IEnumerator MoveToPlace(RectTransform rect, Vector2 movePos)
    {
        //yield return new WaitForSeconds(1.0f);
        
        while (Vector2.Distance(rect.anchoredPosition, movePos) > 0.5f && isDrag)
        {
            rect.sizeDelta = Vector2.Lerp(rect.sizeDelta, minSize, 6 * Time.deltaTime);
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, movePos, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            //print("Moved to main place " + rect.gameObject.name);
        }
        
    }
    public IEnumerator MoveToMainPlace(Action<bool> result)
    {
        //print("Move to Main place "+ gameObject.name);
        isDrag = false;
        //yield return new WaitForSeconds(1.0f);
        rectTransform.SetParent(puzzle.puzzlePlace);
        while (Vector2.Distance(rectTransform.anchoredPosition, mainPosition) !=0)
        {
            rectTransform.sizeDelta = Vector2.MoveTowards(rectTransform.sizeDelta, mainSize, 850 * Time.deltaTime);
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, mainPosition, 800 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            //print("Moved " + rectTransform.gameObject.name);
            
        }
        //print(Vector2.Distance(rectTransform.position, mainPosition));
        result(true);
        puzzle.CurrentTrue++;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //print("Distance :"+ Vector3.Distance(rectTransform.position, mainPosition));
        if (isDrag)
        {
            rectTransform.SetParent(puzzle.puzzlePlace);
            if (Vector3.Distance(rectTransform.anchoredPosition, mainPosition) < 20)
            {
                
                rectTransform.anchoredPosition = mainPosition;
                //print("True");
                isDrag = false;
                puzzle.CurrentTrue++;
                    GameRefremce.instance.Coin += 20;
                    GameRefremce.instance.Score += 20;
                FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin += 20;
                FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].score += 20;
            }
            else
            {
                if (GameRefremce.instance != null)
                    GameRefremce.instance.Coin -= 10;
                FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin -= 10;
                //print("false");
                rectTransform.SetParent(puzzle.placePieces);
                moveToBack = StartCoroutine(MoveToPlace(rectTransform, movePosition));
               
            }
        }
        
    }
    public Coroutine moveToBack;
    public void OnPointerDown(PointerEventData eventData)
    {
        StopCoroutine(moveToBack);
        //print("Down");
    }
}
