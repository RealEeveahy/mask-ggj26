using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneManagement : MonoBehaviour
{
    public CutsceneGroup scene;
    public Image imageField;
    int sceneIndex = 0;
    int sceneSubIndex = 0;

    public Image FadeOverlay;
    public void OnClick()
    {
        ServeNext();
    }
    public void ServeNext()
    {
        if(sceneIndex < scene.sceneList.Count)
        {
            // logic
            if (sceneSubIndex < scene.sceneList[sceneIndex].messages.Count)
            {
                if (GlobalManagement.instance.SetMessage(scene.sceneList[sceneIndex].messages[sceneSubIndex]))
                {
                    sceneSubIndex++;
                    imageField.sprite = scene.sceneList[sceneIndex].sceneImage;
                }
            }
            else
            {
                sceneIndex++;
                sceneSubIndex = 0;
            }
        }
        else
        {
            // only end if the message has been fully displayed, setting an empty message queries whether or not the engine is busy!
            if (GlobalManagement.instance.SetMessage(""))
            {
                StartCoroutine(FadeOut());
            }
        }
    }
    IEnumerator FadeOut()
    {
        while (FadeOverlay.color.a < 1)
        {
            Color c = FadeOverlay.color;
            c.a += Time.deltaTime;
            FadeOverlay.color = c;
            yield return new WaitForSeconds(0.005f);
        }
        GlobalManagement.instance.ShowNextDay();
    }
}
