using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public interface ITask
{
    
    float Duration { get; set; }
    bool TaskComplete { get; set; }
    void DoAction();
    void DecreaseSanity(float sanityCost);
}
public enum TaskName { Juggle, Lute, Wheel, Balance, Ventri }
