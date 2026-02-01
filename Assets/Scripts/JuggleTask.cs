using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class JuggleTask: ITask
{
    public string PrefabPath { get; set; }
    public float Speed { get; set; }
    public float Duration { get; set; }
    public int Difficulty { get; set; }
    public Action Miss { get; set; }
    public JuggleTask(float duration, int diff)
    {
        Difficulty = diff;
        Duration = duration;
        PrefabPath = "Juggling Task";
    }
}
