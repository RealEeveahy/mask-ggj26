using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Day
{
    // does this need to be do not destroy object? Are we loading each minigame as a separate scene?
    // what about the end of day?
    public List<ITask> tasks = new List<ITask>();
    public int numberOfTasks = 2;
    public Day(int difficultInt)
    {
        GenerateTaskList(difficultInt);
        //NumberOfTasks = DayManager.instance.currentDay + 1;
        //tasks.Add(ITask.TaskName(randomn));  //Trying to generate randomn tasks for each day. Turn this into a for loop to add seven tasks.
        // I may need help to figure out how to cycle this yet? Does the task finish come from the task monobehaviour or elsewhere?

        foreach (var task in tasks)
            {
                task.Speed = (float)difficultInt;
            }
    }
    public void TaskStarted (ITask task)
    {
        //DayManager.instance
        GlobalManagement.instance.ToggleOverlay(true);
    }
    public void TaskCompleted(ITask task)
    {
        tasks.Remove(task);
        GlobalManagement.instance.ToggleOverlay(false);
        if (tasks.Count == 0)
        {
            //DayManager.instance.currentDayComplete = true;
            
        }
    }
    public List<ITask> GenerateTaskList(int taskNumber)
    {
        for (int i = 0; i < taskNumber; i++) 
        {
            GenerateTask();
        }
        return tasks;
        // Make list of tasks

    }
    public ITask GenerateTask()
    {
        int random = (Random.Range(3, 5)); //temporarily juggle only
        switch (random)
        {
            case 0:
                JuggleTask juggleTask = new JuggleTask(10f);
                tasks.Add(juggleTask);
                return juggleTask;
            case 1:
                LuteTask luteTask = new LuteTask(10f);
                tasks.Add(luteTask);
                return luteTask;
            case 2:
                WheelTask wheelTask = new WheelTask(10f);
                tasks.Add(wheelTask);
                return wheelTask;
            default:
                JuggleTask defaultTask = new JuggleTask(10f);
                tasks.Add(defaultTask);
                return defaultTask;
        }
    }
}
