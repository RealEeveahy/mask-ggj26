using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class JuggleBehaviour : MonoBehaviour
{
    public ITask task;
    public GameObject pinPrefab;
    public Transform spawnPosition = null;
    public float spawnTime = 1f;
    public int numberOfPins = 3;
    public List<GameObject> pins = new List<GameObject>();

    void Start()
    {
        spawnPosition = transform;
        StartCoroutine(StartTask());

        numberOfPins = numberOfPins * task.Difficulty;
        spawnTime = 1.1f - (task.Difficulty * 0.1f);
    }
    void Update()
    {
        //task.CurrentDuration += Time.deltaTime;
        //if (task.CurrentDuration >= task.Duration)
        //{
        //    task.TaskComplete = true;
        //    //Day.Equals=
        //}
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
        Debug.Log($"{hit}: was hit OnClick");
        if (hit)
        {
            Debug.Log($"{hit}: was hit OnClick");
            hit.transform.gameObject.SendMessage("Throw");
        }
    }
}
