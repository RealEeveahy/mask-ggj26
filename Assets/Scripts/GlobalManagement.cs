using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

/// <summary>
/// Singleton Class. <br></br>
/// Public methods and variables can be accessed by any object in the scene with 'GlobalManagement.instance'. <br></br>
/// Use this class to call methods in other management classes.
/// </summary>
public class GlobalManagement : MonoBehaviour
{
    public GameObject overlay;
    public static GlobalManagement instance { get; private set; }

    public CutsceneGroup introduction;
    public CutsceneGroup GoodEnding;
    public CutsceneGroup NeutralEnding;
    public CutsceneGroup BadEnding;

    public List<CutsceneGroup> GoodPerformanceByDay;
    public List<CutsceneGroup> BadPerformanceByDay;

    private CutsceneGroup nextCutscene;

    AudioManager audioManager;
    UIManagement ui_mgr;
    TextManagement textManager;

    // variables to track game state
    public enum Phase { Intro, Story, Minigame, Ending }
    public Phase gamePhase;
    public enum SoundType { SFX, MUSIC }
    public int currentDay = 0;
    public List<Day> days = new List<Day>();
    public Day GetDay() => days[currentDay];
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
        audioManager = GetComponent<AudioManager>();
        textManager = GetComponent<TextManagement>();
        // define day queue
        days = new List<Day> {
            new Day(2),
            new Day(3),
            new Day(4),
            new Day(5),
            new Day(7)
        };

        //run method on start
        OnSceneSwitch();
    }
    public void DayComplete()
    {
        currentDay += 1;
        ui_mgr.fadeOverlay.GetComponent<FadingElement>().FadeOut(ShowNextCutscene);
        GetComponent<PlayerData>().Sanity += 0.2f; //award sanity for completing level
    }
    // move the next few methods to other management classes later
    public void ShowNextCutscene()
    {
        // logic to calculate cutscene here
        //if final day
        if (nextCutscene == null)
        {
            nextCutscene = introduction;
        }
        else if (currentDay < days.Count)
        {
            if (CurrentPlayerSanity() > 0.6f)
                nextCutscene = GoodPerformanceByDay[0];
            else
                nextCutscene = BadPerformanceByDay[0];
        }
        else
        {
            if (CurrentPlayerSanity() > 0.6f)
                nextCutscene = GoodEnding;
            else if (CurrentPlayerSanity() <= 0.05f)
                nextCutscene = BadEnding; // only achievable by losing on the final level
            else
                nextCutscene = NeutralEnding;
        }

        gamePhase = Phase.Story;

        StartCoroutine(LoadScene("StoryScene"));
    }
    public void ShowNextDay()
    {
        if (currentDay == days.Count-1)
        {
            StartCoroutine(LoadScene("CreditScene"));
            gamePhase = Phase.Ending;
            return;
        }
        StartCoroutine(LoadScene("SampleScene"));
    }
    public void RoundLoss()
    {
        FindFirstObjectByType<DayManager>().PlayerDeath();
        ui_mgr.tryAgainButton.gameObject.SetActive(true);
        audioManager.StopAllAudio();

    }
    // initiated by the try again button
    public void RestartDay()
    {
        GetComponent<PlayerData>().Sanity = 0.8f;
        StartCoroutine(LoadScene("SampleScene"));
    }
    public void PlaySound(string key, SoundType type, bool randomise = false)
    {
        if (type == SoundType.SFX)
        {
            if (randomise)
                audioManager.PlaySoundRandomPitch(key);
            else
                audioManager.PlaySoundEffect(key);
        }
        else if (type == SoundType.MUSIC) audioManager.SetMusic(key);
    }
    public void OnSceneSwitch()
    {
        if (gamePhase == Phase.Intro)
        {
            //do nothing on the intro, set to show story next
            gamePhase = Phase.Story;
        }
        else
        {
            GetComponent<TextManagement>().ConversationTextField = FindFirstObjectByType<TMP_Text>();
            if (gamePhase == Phase.Story)
            {
                audioManager.SetMusic("Downtime_Intro");

                CutsceneManagement c_mgr = FindFirstObjectByType<CutsceneManagement>();
                c_mgr.scene = nextCutscene;
                c_mgr.ServeNext();

                gamePhase = Phase.Minigame;
            }
            else if (gamePhase == Phase.Minigame)
            {
                int r = Random.Range(0, 2);
                string phaseMusic = r == 0 ? "Minigame_Theme_1" : "Minigame_Theme_2";
                audioManager.SetMusic(phaseMusic);

                ui_mgr = FindFirstObjectByType<UIManagement>();
                ui_mgr.DayProgressConfig(GetDay().tasks.Count);
                // change later
                GetComponent<PlayerData>().OnSanityChanged += (sender, value) =>
                {
                    ui_mgr.UpdateSanity(value);
                };
                
                ui_mgr.UpdateSanity(GetComponent<PlayerData>().Sanity);
                DayManager day_mgr = FindFirstObjectByType<DayManager>();
                day_mgr.OnProgressChanged += (sender, value) =>
                {
                    ui_mgr.UpdateProgress(value);
                };
                StartCoroutine(day_mgr.Initiate());
            }
        }
    }

    /// <summary>
    /// Recieves a message from the caller and attempts to display it on screen.
    /// </summary>
    /// <param name="message"></param>
    /// <returns>True if the message is successfully initiated, <br></br>
    /// False if the engine is busy displaying another message.</returns>
    public bool SetMessage(string message, string speaker = null)
    {
        if(!textManager.isBusy)
        {
            StartCoroutine(textManager.ShowMessage(message, speaker));
            return true;
        }
        else
        {
            textManager.Hurry();
            return false;
        }
    }
    public bool EndMessage()
    {
        if (!textManager.isBusy)
        {
            textManager.Close();
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
        GetComponent<PlayerData>().Sanity -= sanityCost;
        //if item hits the floor take sanity.
    }
    public void ToggleOverlay(bool state)
    {
        GlobalManagement.instance.overlay.SetActive(state);
    }
    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!ao.isDone)
        {
            yield return null;
        }

        // code to run after scene is loaded

        OnSceneSwitch();
    }
}
