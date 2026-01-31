using UnityEngine;
using TMPro;
/// <summary>
/// Singleton Class. <br></br>
/// Public methods and variables can be accessed by any object in the scene with 'GlobalManagement.instance'. <br></br>
/// Use this class to call methods in other management classes.
/// </summary>
public class GlobalManagement : MonoBehaviour
{
    public GameObject overlay;
    public static GlobalManagement instance { get; private set; }
    public float CurrentPlayerSanity() => GetComponent<PlayerData>().Sanity;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        // change later
        GetComponent<PlayerData>().OnSanityChanged += (sender, value) => { GetComponent<UIManagement>().UpdateSanity(value); };
    }

    // move the next few methods to other management classes later
    public void ShowNextDay()
    {
        
    }
    public void TaskComplete()
    {

    }



    public void OnSceneSwitch()
    {
        GetComponent<TextManagement>().ConversationTextField = FindFirstObjectByType<TMP_Text>();
    }

    /// <summary>
    /// Recieves a message from the caller and attempts to display it on screen.
    /// </summary>
    /// <param name="message"></param>
    /// <returns>True if the message is successfully initiated, <br></br>
    /// False if the engine is busy displaying another message.</returns>
    public bool SetMessage(string message)
    {
        TextManagement textManager = GetComponent<TextManagement>();
        if(!textManager.isBusy)
        {
            StartCoroutine(textManager.ShowMessage(message));
            return true;
        }
        else
        {
            textManager.Hurry();
            return false;
        }
    }
    public void DecreaseSanity(float sanityCost)
    {
        GetComponent<PlayerData>().Sanity += sanityCost;
        //if item hits the floor take sanity.
    }
    public void ToggleOverlay(bool state)
    {
        GlobalManagement.instance.overlay.SetActive(state);
    }
}
