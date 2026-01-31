using UnityEngine;
using System.Collections;
public class StartGame : MonoBehaviour
{
    public GameObject mask;
    bool hasClicked = false;
    public void OnClick()
    {
        if (!hasClicked)
        {
            mask.GetComponent<FadingElement>().FadeOut(GlobalManagement.instance.ShowNextCutscene);
            hasClicked = true;
        }
    }

}
