using UnityEngine;

public class JuggleTask: MonoBehaviour,ITask
{
    //float duration;
    public float Duration { get; set; }
    public bool TaskComplete { get; set; }
    void Start()
    {
        //Duration = duration;
    }
    public void DoAction()
    {
        //if item is clicked use an upward force to keep object in the air.
    }
    public void DecreaseSanity(float sanityCost)
    {
        //if item hits the floor take sanity.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
