using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Day : MonoBehaviour
{
    // does this need to be do not destroy object? Are we loading each minigame as a separate scene?
    // what about the end of day?
    public List<ITask> tasks = new List<ITask>();
    void Start()
    {
        int randomn = Random.Range(0,5);
        //tasks.Add(ITask.TaskName(randomn));  //Trying to generate randomn tasks for each day. Turn this into a for loop to add seven tasks.
        // I may need help to figure out how to cycle this yet? Does the task finish come from the task monobehaviour or elsewhere?
        foreach (var task in tasks)
        {
            task.Duration = 55f + 5 * (float)DayManager.instance.currentDay;
        }
    }

    void Update()
    {
        if (tasks.Count == 0)
        {
            DayManager.instance.currentDay++;
        }
    }
    void TaskCompleted(ITask task)
    {
        tasks.Remove(task);
    }

}
