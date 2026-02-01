using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class LuteBehaviour : MonoBehaviour
{
    public ITask task;
    public GameObject notePrefab;
    public Transform spawnPosition = null;
    public float spawnTime = 2f;
    public int maxNumberOfNotes = 4;
    public List<GameObject> notes = new List<GameObject>();
    public List<Note> queuedNotes = new List<Note>();
    public bool busy = false;
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
        Note instanstiatedNote = instantiatedPin.GetComponent<Note>();
        instanstiatedNote.creatorObject = this.gameObject;
        instanstiatedNote.SetNotePosition();
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
            Debug.Log($"{hit}: was hit OnClick");
            hit.transform.gameObject.SendMessage("PlayNote");
            //hit.transform.GetComponent<Note>(StartCoroutine(RespawnNote(3f))); // if we click it make it reset as well.
        }
    }
    public void AddNoteToQueue(Note note) 
    {
        queuedNotes.Add(note);
        try
        {
            if (this.gameObject.activeSelf)
                StartCoroutine(QueueNote(note));
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
    IEnumerator QueueNote(Note note)
    {
        busy = true;
        yield return new WaitForSeconds(spawnTime);
        int randomn2 = UnityEngine.Random.Range(1, 4);
        try
        {
            if (this.gameObject.activeSelf)
            note.RespawnNote();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
        queuedNotes.Remove(note);
        busy = false;
    }
}
