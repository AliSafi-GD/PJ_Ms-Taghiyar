using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOpenUsb : MonoBehaviour
{
    public void EndCopy()
    {
        Windows10._checkTask("EndCopy");
        gameObject.SetActive(false);
    }
}
