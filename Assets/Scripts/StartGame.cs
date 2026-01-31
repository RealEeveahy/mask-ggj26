using UnityEngine;
using System.Collections;
public class StartGame : MonoBehaviour
{
    public UnityEngine.UI.Image mask;
    private void Start()
    {
        Color c = mask.color;
        c.a = 0;
        mask.color = c;
    }
    public void OnClick()
    {
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        while (mask.color.a < 1)
        {
            Color c = mask.color;
            c.a += Time.deltaTime;
            mask.color = c;
            yield return new WaitForSeconds(0.008f);
        }
        GlobalManagement.instance.ShowNextCutscene();
    }
}
