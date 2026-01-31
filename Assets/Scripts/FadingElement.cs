using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FadingElement : MonoBehaviour
{
    bool busy = false;
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void FadeIn(Action runAfter = null)
    {
        if (!busy)
        {
            image.color = new Color(0, 0, 0, 1f);
            StartCoroutine(FadeEnum(true, runAfter));
        }
    }
    public void FadeOut(Action runAfter = null)
    {
        if (!busy)
        {
            image.color = new Color(0, 0, 0, 0);
            StartCoroutine(FadeEnum(false, runAfter));
        }
    }
    IEnumerator FadeEnum(bool fadingIn = true, Action runAfter = null)
    {
        busy = true;
        if(fadingIn)
        {
            while (image.color.a > 0)
            {
                Color c = image.color;
                c.a -= Time.deltaTime;
                image.color = c;
                yield return new WaitForSeconds(0.008f);
            }
        }
        else
        {
            while (image.color.a < 1)
            {
                Color c = image.color;
                c.a += Time.deltaTime*2;
                image.color = c;
                yield return new WaitForSeconds(0.008f);
            }
        }
        runAfter?.Invoke();
        busy = false;
    }
}
