using UnityEngine;

public class LuteTask : MonoBehaviour, ITask
{
    public float CurrentDuration { get; set; }
    public float Speed { get; set; }
    public float Duration { get; set; }
    public bool TaskComplete { get; set; }
    public GameObject View { get; set; }
    public void Render()
    {

        // Set background active?
    }
    void Start()
    {
        CurrentDuration = 0f;


        //Duration = duration;
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
