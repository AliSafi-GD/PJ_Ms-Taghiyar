using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Warehouse : Task
{
    public enum ObjectType{
        Mouse,Keyboard,Monitor,Desk,Speaker,Case,Headphone,Chair,Printer
    }
    public ClickObject[] clickObjects;
    public UI_Object[] uI_Objects;
    int numberObjectsFound;
    public Button btnHelp;
    public int NumberObjectsFound { get => numberObjectsFound; set => numberObjectsFound = value; }

    public override void starter(Action<bool> result)
    {
        var t = new GameObject("timer", typeof(TimerMiniGame));
        btnHelp.onClick.AddListener(()=>{
            var objs = (from x in clickObjects where x.gameObject.activeSelf select x).ToList();
            var obj = objs[UnityEngine.Random.Range(0, objs.Count)];
            
            var desk = objs.Find(x=>x.name=="Desk") != null ? objs.Find(x=>x.name=="Desk").gameObject.activeSelf:false;
            
            while (obj.name=="Mouse"&&desk)
            {
                objs = (from x in clickObjects where x.gameObject.activeSelf select x).ToList();
                obj = objs[UnityEngine.Random.Range(0, objs.Count)];
            }
            
            obj.PlayAnimation_Help();
            btnHelp.interactable=false;
            print(obj.name);
            
                GameRefremce.instance.Coin -= 10;
            FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin -= 10;
        });
        foreach (var item in clickObjects)
        {
            item.btn.onClick.AddListener(()=>{
                item.gameObject.SetActive(false);
                btnHelp.interactable=true;
                foreach (var item_ui in uI_Objects)
                {
                    if(item.objectType == item_ui.objectType){
                        item_ui.IsSelected = true;
                        NumberObjectsFound++;
                        
                            GameRefremce.instance.Coin += 5;
                            GameRefremce.instance.Score += 10;
                        FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].score += 10;
                        FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin += 5;
                    }
                        
                }
            });
        }
        
        StartCoroutine(IsCompletedFind(r=> result(r),t));
    }
    IEnumerator IsCompletedFind(Action<bool> result,UnityEngine.Object obj){
        yield return new WaitWhile(()=>(from x in uI_Objects where !x.IsSelected select x).Any());
        result(true);
        Destroy(obj);
    }
}
