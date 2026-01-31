using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

/// <summary>
/// Manages interactions that exist purely in the Game Scene
/// </summary>
public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public GameObject sceneOverlay, TaskParent;
    int tasksCompleted = 0;
    //public int currentDay= 1;
    //public int numberOfDays = 5;
    //public List<Day> days = new List<Day>();
    //public bool currentPhaseComplete = false;
    //public bool currentDayComplete = false;
    //public PlayerData player;

    public void ShowNextTask()
    {
        if (tasksCompleted < GlobalManagement.instance.GetDay().tasks.Count)
        {
            ITask nextTask = GlobalManagement.instance.GetDay().tasks[tasksCompleted];
            ClearTaskParent();

            string path = nextTask.PrefabPath;

            GameObject toRender = Resources.Load<GameObject>(path);
            GameObject instTask = Instantiate(toRender);
            if (instTask.GetComponent<JuggleBehaviour>() ==null)
            {
                instTask.GetComponent<LuteBehaviour>().task = nextTask;
            }
            else
            {
                instTask.GetComponent<JuggleBehaviour>().task = nextTask;
            }
            instTask.transform.SetParent(TaskParent.transform);
        }
        else
        {
            // signal end of tasks for day
        }
    }
    public void ClearTaskParent()
    {
        foreach(Transform child in TaskParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }
    //    DontDestroyOnLoad(this.gameObject);
    //}

    //void Start()
    //{
    //    player = GameObject.Find("Player").GetComponent<PlayerData>();
    //    GlobalManagement.instance.gamePhase = GlobalManagement.Phase.Story;
    //    for (int i = 0; i < numberOfDays; i++)
    //    {
    //        days.Add(new Day(i));
    //    }
    //}

    //// move to events rather than checks
    //void Update()
    //{
    //    if (currentPhaseComplete)
    //    {
    //        if (currentDayComplete)
    //        {
    //            FinishDay();
    //            return;
    //        }
    //        //SwitchPhase();
    //        Debug.Log($"{GlobalManagement.instance.gamePhase}");
    //    }
    //}
    ///*void SwitchPhase()
    //{
    //    switch (currentPhase)
    //    {
    //        case Phase.Story:
    //            currentPhase = Phase.Minigame;
    //            //days[currentDay].TaskStarted()
    //            break;
    //        case Phase.Minigame:
    //            currentPhase = Phase.Story;
    //            break;
    //    }
    //}*/
    //void FinishDay()
    //{
    //    currentDay++;
    //    GlobalManagement.instance.gamePhase = GlobalManagement.Phase.Story;
    //    currentPhaseComplete = false;
    //}
}
