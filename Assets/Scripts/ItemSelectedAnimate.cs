using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectedAnimate : MonoBehaviour
{
    public AnimationCurve anim=new AnimationCurve(new Keyframe[3]
        {
            new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1f, 0)
        });

    Image imgSelected;
    
    public void Wink(Image img)
    {
        imgSelected = img;
        colorAnim = imgSelected.color;
        coroutine = StartCoroutine(StartWink());        
    }
    Coroutine coroutine;
    Color colorAnim=new Color();
    float time;
    IEnumerator StartWink()
    {
        while (true)
        {
            colorAnim.a = anim.Evaluate(time);
            time += Time.deltaTime;
            if (time >= anim.keys[anim.keys.Length-1].time)
                time = 0;
            imgSelected.color = colorAnim;
            yield return null;
        }
       
    }
    public void StopWink()
    {
        var c = imgSelected.color;
        c.a = 1;
        imgSelected.color = c;
        StopCoroutine(coroutine);
        Destroy(this);
    }
}
