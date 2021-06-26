using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Dreamweaver : MonoBehaviour
{
    int currentTrue;
    public ScrollRect scrollView;
    public float f;
    public Button btnHelp;
    public GameObject finishObj;
    public Animator anim;
    public int CurrentTrue
    {
        get
        {
            return currentTrue;
        }
        set
        {
            currentTrue = value;
            btnHelp.interactable = true;
            if (value == dropContents.Count)
            {
                finishObj.SetActive(true);
                Destroy(t);
            }
        }
    }
    public void Finish()
    {
        anim.enabled = true;
        anim.Play("Close");
        Windows10._checkTask("miniGame");
        //gameObject.SetActive(false);
    }
    public List<DropContentDreamweaver> dropContents;
    public DragName[] dragObj;

    private void Start()
    {
        t = new GameObject("timer", typeof(TimerMiniGame));
    }
    GameObject t;

    //private void Update()
    //{
    //    scrollView.verticalNormalizedPosition = f;
    //}
    public void StartSetDrop(){
        foreach (var item in dropContents)
        {
            item.Starter();
        }
        foreach (var item in dragObj)
        {
            item.gameObject.SetActive(true);
        }
    }
    public void Help()
    {
        var dropContentsbkp = (from x in dropContents where x.IsVisible == false select x).ToList();
        if (dropContentsbkp.Count != 0)
        {
            btnHelp.interactable = false;
            var r = Random.Range(0, dropContentsbkp.Count);

            var anim = gameObject.AddComponent<ItemSelectedAnimate>();
            anim.Wink(dropContentsbkp[r].transform.GetChild(1).GetComponent<Image>());
            var anim2 = gameObject.AddComponent<ItemSelectedAnimate>();
            print(r);
            var img = (from x in dragObj where x.mainName.ToLower() == dropContentsbkp[r].mainName.ToLower() select x).FirstOrDefault().GetComponent<Image>();

            anim2.Wink(img);
            //float normalizePosition = (float)img.transform.GetSiblingIndex() / (float)scrollView.content.transform.childCount;
            scrollView.verticalNormalizedPosition = dropContentsbkp[r].normalizedPos;
            dropContentsbkp.Remove(dropContentsbkp[r]);

            
            //    GameRefremce.instance.Coin -= 40;
            //FindObjectOfType<TaskManager>().levelManager[LevelManager.currenLevel].coin -= 40;
        }
       
        
        //dropContents[r].TrueObject((from x in dragObj where x.mainName == dropContents[r].mainName select x).FirstOrDefault().gameObject);
    }
    public void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
   
}
