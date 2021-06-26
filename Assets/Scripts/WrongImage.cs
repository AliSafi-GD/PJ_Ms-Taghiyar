using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEditor;
//[ExecuteInEditMode]
public class WrongImage : MonoBehaviour , IPointerDownHandler
{
    public MiniGame4 miniGame;

    [SerializeField] bool isHelp;
    public bool isTrue;
    public GameObject objOverlaps;
    public bool IsHelp
    {
        get
        {
            return isHelp;
        }
        set
        {
            isHelp = value;
            if (value)
            {
                var color = new Color(0, 1, 0, 0.3411765f);
                GetComponent<Image>().color = color;
            }
            
        }
    }

    public bool Overlaps(List<GameObject> trs)
    {
        
        bool isCheck = false;
        foreach (var item in trs)
        {
            //var myPos = transform.position;
            //var sizeDelta = item.GetComponent<RectTransform>().sizeDelta;

            //var mysizeDelta = GetComponent<RectTransform>().sizeDelta;
            //var minX = (item.transform.position.x - (sizeDelta.x / 2)) ;
            //// 
            //var maxX = (item.transform.position.x + (sizeDelta.x / 2)) ;
            ////x = maxX;
            //var minY = (item.transform.position.y - (sizeDelta.y / 2));
            //var maxY = (item.transform.position.y + (sizeDelta.y / 2));
            ////y = maxY;
            //print($"minx = {minX} | maxX {maxX} | minY {minY} | maxY {maxY}");
            ////print($"minx = {minX}  MainX = {trs.position} | MyPos = {myPos}    {mysizeDelta}");
            //print(item.transform.position);
            //print($"myPos.x = {myPos.x} | myPos.y { myPos.y} _____ overLaps {(myPos.x < maxX && myPos.x > minX) && (myPos.y < maxY && myPos.y > minY)}");

            //if ((myPos.x < maxX && myPos.x > minX) && (myPos.y < maxY && myPos.y > minY))
            //{
            //    isCheck = true;
            //    //break;
            //}
            //else
            //{
            //    isCheck = false;
            //}
            //print(Vector3.Distance(transform.position, item.transform.position));
            if (Vector3.Distance(transform.position, item.transform.position) < 15)
            {
                
                objOverlaps = item;
                isCheck = true;
                break;
            }
            else
            {
                isCheck = false;
            }



            
        }

        return isCheck;
        //if ((myPos.x < minX))
        //        return true;
        //else
        //    return false;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isHelp)
            miniGame.DestroyWrong(gameObject);
    }
}
