using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class JuggleTask: MonoBehaviour,ITask
{
    public List<GameObject> pins = new List<GameObject>();
    public GameObject pinPrefab = null;
    public Transform spawnPosition = null;
    public float spawnTime = 1f;
    public int numberOfPins = 3;
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
        spawnPosition = this.transform;
        StartCoroutine(StartTask());
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
    public GameObject CreatePin()
    {
        Debug.Log("JuggleTask.CreatePin()");
        if (pinPrefab == null)
        {
            Debug.Log("No pin prefab assigned");
            return null;
        }
        GameObject instantiatedPin = Instantiate(pinPrefab, spawnPosition);
        instantiatedPin.transform.SetParent(transform, false);
        instantiatedPin.GetComponent<FallingObject>().creatorObject = this.gameObject;
        return instantiatedPin;
    }
    IEnumerator StartTask()
    {
        while (pins.Count < numberOfPins)
        {
            yield return new WaitForSeconds(spawnTime);
            pins.Add(CreatePin());
        }
    }
    public void OnClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
        if (hit)
        {
            hit.transform.gameObject.SendMessage("Throw");
        }
    }
}
