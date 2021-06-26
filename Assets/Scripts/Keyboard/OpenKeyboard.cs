using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenKeyboard : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] UnityEvent unityEvent=null;

    private void Start()
    {
        unityEvent.AddListener(() =>
        {
            KeyboardAndroid.instance.ShowKeyboard();
            KeyboardAndroid.instance.SetInputField(GetComponent<InputField>());
        });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        unityEvent.Invoke();
    }
}
