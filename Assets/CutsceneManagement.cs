using UnityEngine;
using UnityEngine.UI;

public class CutsceneManagement : MonoBehaviour
{
    public CutsceneGroup scene;
    public Image imageField;
    int sceneIndex = 0;
    int sceneSubIndex = 0;
    public void Start()
    {
        ServeNext();
    }
    public void OnClick()
    {
        ServeNext();
    }
    void ServeNext()
    {
        if(sceneIndex < scene.sceneList.Count)
        {
            // logic
            imageField.sprite = scene.sceneList[sceneIndex].sceneImage;

            if (sceneSubIndex < scene.sceneList[sceneIndex].messages.Count)
            {
                if(GlobalManagement.instance.SetMessage(scene.sceneList[sceneIndex].messages[sceneSubIndex]))
                    sceneSubIndex++;

            }
            else
            {
                sceneIndex++;
                sceneSubIndex = 0;
            }
        }
        else
        {
            //end the scene

        }
    }
}
