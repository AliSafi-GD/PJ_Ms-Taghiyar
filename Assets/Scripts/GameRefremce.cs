using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRefremce : MonoBehaviour {

    
    public Text txtTimer,txtCoint;
    Animator anim;
    AudioSource audioSource;
    public static GameRefremce instance;
    
    int timer;

    public int _Timer{
        get{
            return timer;
        }
        set{
            timer = value;
            var toS = $"{(value/60).ToString("00")} : {(value % 60).ToString("00") }";
            txtTimer.text = toS;
            DB.instance.infoAccount.main_timer = toS;
        }
    }

    int coin;
    public int Coin
    {
        get
        {
            return coin;
        }
        set
        {
            coin = value;
            txtCoint.text = value.ToString();
            DB.instance.infoAccount.coin = txtCoint.text;
        }
    }
    int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            DB.instance.infoAccount.score = value.ToString();
        }
    }


    IEnumerator ITimer;

    bool isRun;
    public bool IsRun{
        get{
            return isRun;
        }
        set{
            isRun = value;
            if(value){
                if (audioSource == null)
                    audioSource = GetComponent<AudioSource>();
                audioSource.Play();
                StartCoroutine(ITimer);
                anim.Play("Show_Timer");
            }
                
            else{
                StopCoroutine(ITimer);
                anim.Play("Hide_Timer");
            }
                
        }
    }
    public void ToggleSound(bool b)
    {
        AudioListener.volume = b ? 0 : 1;
    }
    private void Awake() {
        anim = GetComponent<Animator>();
        ITimer = Timer();
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
           
        
    }

    IEnumerator Timer(){
        while (isRun)
        {
            
            yield return new WaitForSeconds(1.0f);
            _Timer++;
        }
    }
    
}