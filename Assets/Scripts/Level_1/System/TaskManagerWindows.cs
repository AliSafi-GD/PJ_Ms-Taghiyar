using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class TaskManagerWindows : MonoBehaviour
{
    

    [Header("UI System Handler")]
    public Button btnChangeStateTaskManager;
    public Animator animator;

    [SerializeField] bool isOpenTaskManager=false;

    
    void Start()
    {
        //print("Set Method");
        btnChangeStateTaskManager.onClick.AddListener(()=>{
            animator.Play(isOpenTaskManager?"Close":"Open");
            isOpenTaskManager = !isOpenTaskManager;
        });
    }
    
    
    
}

