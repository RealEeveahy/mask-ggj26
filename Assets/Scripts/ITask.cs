using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public interface ITask
{
    enum TaskName { Juggle, Lute, Wheel, Balance, Ventri }
    float Duration { get; set; }
    float Speed { get; set; }
    string PrefabPath { get; set; }
    int Difficulty { get; set; }
}

