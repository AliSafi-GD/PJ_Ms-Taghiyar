using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class TaskContent{
    // FFFFFF37
    // 72FF00FF
    public string taskName;
    public Image imgCheck;
    public Image imgWink;
    [SerializeField] public NotificationContent notificationContent;
    public bool isDone;

    public bool IsDone{
        get{
            return isDone;
        }
        set{
            isDone = value;
            if(imgCheck!=null)
                imgCheck.color = value?TaskLevel.colors[1]:TaskLevel.colors[0];
        }
    }
}
[Serializable]
public class TaskLevel{
    public string nameLevel;
    public static Color[] colors = new Color[2]{
        new Color(1f,1f,1f,0.2156863f),
        new Color(0.5f,1f,0f,1)
    };
    [SerializeField] public TaskContent[] taskContents;
}
[Serializable]
public class NotificationContent
{
    
    public enum TypeActionNotification
    {
        ShowLink, Answer , Answer2 , bluetooth , TransferFile,FinishFileTransfer,StartMiniGame5, SpamMail, AutoOpenUsb,backup, ShowDescription
    }
    public string strTitle, strDescription;
    public int coin;
    public TypeActionNotification type;


}