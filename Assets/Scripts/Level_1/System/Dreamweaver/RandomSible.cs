using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSible : MonoBehaviour
{
    public Transform trsContent2;
    public Transform contentPlace;

    public void SetContent()
    {
        var value = trsContent2.childCount;
        for (int i = 0; i < value; i++)
        {
            var r = Random.Range(0, trsContent2.childCount);
            trsContent2.GetChild(r).SetParent(contentPlace);
        }
    }
}
