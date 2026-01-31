using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class JuggleTask: ITask
{
    public string PrefabPath { get; set; }
    public float CurrentDuration { get; set; }
    public float Speed { get; set; }
    public float Duration { get; set; }
    public bool TaskComplete { get; set; }
    public GameObject View { get; set; }
    public JuggleTask()
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
}
