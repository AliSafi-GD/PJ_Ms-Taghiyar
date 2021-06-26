using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropContentDreamweaver : MonoBehaviour, IDropHandler
{
    public string mainName;
    bool isVisible;
    Dreamweaver dreamweaver;
    public float normalizedPos;
    public GameObject border;
    private void Start()
    {
        mainImg = GetComponent<Image>();
        border = transform.GetChild(1).gameObject;
    }
    
    public bool IsVisible
    {
        get
        {
            return isVisible;
        }
        set
        {
            isVisible = value;
            if (value)
            {
                var c = mainImg.color;
                c.a = 1;
                mainImg.color = c;
                border.gameObject.SetActive(false);
                dreamweaver.CurrentTrue++;
            }
            else
            {
                var c = mainImg.color;
                c.a = 0;
                mainImg.color = c;
                //.SetActive(true);
                
            }
        }
    }
    public Image mainImg;

    public void OnDrop(PointerEventData eventData)
    {
        //
        if (eventData.rawPointerPress != null)
        {
            //print($"{eventData.rawPointerPress.GetComponent<DragName>().mainName} == {mainName}");
            if (eventData.rawPointerPress.GetComponent<DragName>().mainName == mainName.ToLower())
            {
                
                
                GameRefremce.instance.Coin += 20;
                GameRefremce.instance.Score += 20;

                FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin += 20;
                FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].score += 20;
                TrueObject(eventData.pointerDrag);
                
            }
            else
            {
               
                GameRefremce.instance.Coin -=10;

                FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin -= 10;
            }
        }
    }
    public void TrueObject(GameObject obj)
    {
        IsVisible = true;
        obj.SetActive(false);

    }
    public void Starter()
    {
        dreamweaver = FindObjectOfType<Dreamweaver>();
        
        IsVisible = false;

    }
    
}
