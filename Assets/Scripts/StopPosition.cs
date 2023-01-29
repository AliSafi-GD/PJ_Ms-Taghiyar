using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopPosition : MonoBehaviour
{
    public ScrollRect scrollRect;
    

    // Update is called once per frame
    public void ChangeVisible(bool b)
    {
        transform.position = scrollRect.transform.position;
        scrollRect.vertical = b;
    }
}
