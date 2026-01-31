using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public int currentDay= 1;
    public int numberOfDays = 5;
    public List<Day> days = new List<Day>();
    public Phase currentPhase;
    public bool currentPhaseComplete = false;
    public bool currentDayComplete = false;

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
        currentPhaseComplete = false;
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
