using System;
using UnityEngine;

public class LuteTask : ITask
{
    public string PrefabPath { get; set; }
    public float CurrentDuration { get; set; }
    public float Speed { get; set; }
    public float Duration { get; set; }
    public bool TaskComplete { get; set; }
    public GameObject View { get; set; }
    public Action Miss { get; set; }
    public LuteTask()
    {
        PrefabPath = "Juggling Task";
    }
    public void Render()
    {

        // Set background active?
    }
    public void DoAction()
    {
        //if item is clicked use an upward force to keep object in the air.
    }

    // Update is called once per frame
    void Update()
    {
        CurrentDuration += Time.deltaTime;
        if (CurrentDuration >= Duration)
        {
            TaskComplete = true;
            //Day.Equals=
        }
    }
}
