using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragName : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler
{
    public string mainName;
    Dreamweaver dreamweaver;
    Vector3 firstPos;
    public Image img;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        img.raycastTarget = false;
        firstPos = transform.position;   
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.raycastTarget = true;
        transform.position = firstPos;
    }

    void Start()
    {
        img = GetComponent<Image>();
        dreamweaver = FindObjectOfType<Dreamweaver>();
    }



    public void IsDone(){
        gameObject.SetActive(false);
    }
}
