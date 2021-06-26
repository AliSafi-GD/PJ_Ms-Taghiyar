using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    public static int currenLevel=0;
   
    public int CurrenLevel
    {
       
        set
        {
            
            
            GetComponent<AudioSource>().Play();
            if (currenLevel == 0)
                levels[currenLevel].UnlockLevel();
            else
                for (int i = 0; i < value + 1; i++)
                {
                    levels[i].UnlockLevel();
                }

            
        }
    }
 
    private void OnEnable()
    {
        CurrenLevel = currenLevel;
    }
  
    
   

}
