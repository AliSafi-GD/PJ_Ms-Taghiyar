using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnAnswer : MonoBehaviour
{
    //private string answer;
    private bool isSelected;
    //private Button btn;
    public Color[] colors = new Color[3]
    {
        new Color(0.2901961f, 0.2901961f, 0.2901961f, 1),
        new Color(0.01001244f, 0.4941176f, 0.7058824f, 1),
        new Color(0.2193387f, 0.5254902f, 0.1137255f,1f)
    };
    //private Image img;
    private bool isWin;

    public bool IsWin
    {
        get
        {
            return isWin;
        }
        set
        {
            isWin = value;
            if (isWin)
            {
                Img.color = colors[2];
                
            }
        }
    }
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;
            Img.color = value ? colors[1] : colors[0];
        }
    }

    public string Answer { get => GetComponentInChildren<Text>().text; }
    public Button Btn { get => GetComponent<Button>(); }
    public Image Img { get => GetComponent<Image>();}


}
