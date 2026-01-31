using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public interface ITask
{
    enum TaskName { Juggle, Lute, Wheel, Balance, Ventri }
    float CurrentDuration { get; set; }
    float Duration { get; set; }
    float Speed { get; set; }
    bool TaskComplete { get; set; }
    GameObject View {  get; set; } //Instantiate gameObjects for task
    string PrefabPath { get; set; }
    void DoAction();
    void Render(); // Set background active?

}

