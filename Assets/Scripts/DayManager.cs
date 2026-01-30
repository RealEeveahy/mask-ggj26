using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public int currentDay= 1;
    public Phase currentPhase;
    public bool currentPhaseComplete = false;
    public bool currentDayComplete = false;
    //public PlayerData player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
        currentPhase = Phase.Story;
    }

    void Update()
    {
        if (currentPhaseComplete)
        {
            if (currentDayComplete)
            {
                FinishDay();
                return;
            }
            SwitchPhase();
            Debug.Log($"{currentPhase}");
        }
    }
    void SwitchPhase()
    {
        switch (currentPhase)
        {
            case Phase.Story:
                currentPhase = Phase.Minigame;
                break;
            case Phase.Minigame:
                currentPhase = Phase.Story;
                break;
        }
    }
    void FinishDay()
    {
        currentDay++;
        currentPhase = Phase.Story;
    }
}
public enum Phase { Story , Minigame}
