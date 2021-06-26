using UnityEngine;
using UnityEngine.UI;

public class ClickObject : MonoBehaviour {
    
    [HideInInspector] public Button btn;
    Animator anim;
    public Warehouse.ObjectType objectType;
    private void Awake() {
        if(anim==null) anim = GetComponent<Animator>();
        if(btn==null) btn=GetComponent<Button>();
    }
    public void PlayAnimation_Help(){
        anim.Play("Help");
    }
}