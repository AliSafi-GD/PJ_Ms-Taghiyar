using System;
using UnityEngine;

public abstract class TaskWindows : MonoBehaviour {
    public abstract void Starter(Action<bool> result); 
}