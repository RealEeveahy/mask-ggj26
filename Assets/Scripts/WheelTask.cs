using UnityEngine;

public class WheelTask : ITask
{
    public string PrefabPath { get; set; }
    public float CurrentDuration { get; set; }
    public float Speed { get; set; }
    public float Duration { get; set; }
    public bool TaskComplete { get; set; }
    public GameObject View { get; set; }
    public int Difficulty { get; set; }
    public WheelTask(float duration)
    {
        Duration = duration;
        PrefabPath = "Wheel Task";
    }
    public void Render()
    {

        // Set background active?
    }
    public void DoAction()
    {
        //if item is clicked use an upward force to keep object in the air.
    }
}
