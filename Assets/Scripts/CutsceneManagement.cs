using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneManagement : MonoBehaviour
{
    public CutsceneGroup scene;
    public Image imageField;
    int sceneIndex = 0;
    int sceneSubIndex = 0;

    public GameObject fadeOverlay;
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
                if (GlobalManagement.instance.SetMessage(scene.sceneList[sceneIndex].messages[sceneSubIndex], "Jester"))
                {
                    sceneSubIndex++;
                    imageField.sprite = scene.sceneList[sceneIndex].sceneImage;

                    //make a random page turn sound
                    int r_int = Random.Range(0, 2);
                    string key = r_int == 0 ? "page1" : "page2";
                    GlobalManagement.instance.PlaySound(key, GlobalManagement.SoundType.SFX);
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
                fadeOverlay.GetComponent<FadingElement>().FadeOut(GlobalManagement.instance.ShowNextDay);
            }
        }
    }
}
