using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Object : MonoBehaviour
{
    GameObject objCheck;
    public Warehouse.ObjectType objectType;
    private void Start() {
        objCheck = transform.Find("Check").gameObject;
    }
    bool isSelected;

    public bool IsSelected {
        get{
            return isSelected;
        }
        set{
            isSelected= value;
            objCheck.SetActive(value);
        }
    }
}
