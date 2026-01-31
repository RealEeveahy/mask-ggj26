using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class LuteBehaviour : MonoBehaviour
{
    public ITask task;
    public GameObject notePrefab;
    public Transform spawnPosition = null;
    public float spawnTime = 1f;
    public int maxNumberOfNotes = 4;
    public List<GameObject> notes = new List<GameObject>();

    void Start()
    {
        //task.CurrentDuration = 0f;
        //spawnPosition = transform;
        StartCoroutine(StartTask());
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
    public GameObject CreateNote()
    {
        Debug.Log("LuteBehavouir.CreateNote()");
        if (notePrefab == null)
        {
            Debug.Log("No note prefab assigned");
            return null;
        }
        GameObject instantiatedPin = Instantiate(notePrefab, spawnPosition);
        instantiatedPin.transform.SetParent(transform, false);
        instantiatedPin.GetComponent<FallingObject>().creatorObject = this.gameObject;
        return instantiatedPin;
    }
    IEnumerator StartTask()
    {
        while (notes.Count < maxNumberOfNotes)
        {
            yield return new WaitForSeconds(spawnTime);
            notes.Add(CreateNote());
        }
    }
    public void OnClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero);
        if (hit)
        {
            hit.transform.gameObject.SendMessage("PlayNote");
        }
    }
}
