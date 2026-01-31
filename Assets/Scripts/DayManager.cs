using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public int currentDay= 1;
    public int numberOfDays = 5;
    public List<Day> days = new List<Day>();
    public Phase currentPhase;
    public bool currentPhaseComplete = false;
    public bool currentDayComplete = false;
    public PlayerData player;

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
        player = GameObject.Find("Player").GetComponent<PlayerData>();
        currentPhase = Phase.Story;
        for (int i = 0; i < numberOfDays; i++)
        {
            days.Add(new Day(i));
        }
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
            //SwitchPhase();
            Debug.Log($"{currentPhase}");
        }
    }
    /*void SwitchPhase()
    {
        switch (currentPhase)
        {
            case Phase.Story:
                currentPhase = Phase.Minigame;
                //days[currentDay].TaskStarted()
                break;
            case Phase.Minigame:
                currentPhase = Phase.Story;
                break;
        }
    }*/
    void FinishDay()
    {
        currentDay++;
        currentPhase = Phase.Story;
        currentPhaseComplete = false;
    }
}
public enum Phase { Story , Minigame}
