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
    public float spawnTime = 0.5f;
    public int maxNumberOfNotes = 4;
    public List<GameObject> notes = new List<GameObject>();
    public List<Note> queuedNotes = new List<Note>();
    
    public bool busy = false;
    void Start()
    {
        Debug.Log($"Task difficulty = {task.Difficulty}");
        maxNumberOfNotes = maxNumberOfNotes * task.Difficulty;
        Debug.Log($"Max numbers = {maxNumberOfNotes}");
        spawnTime = 1 / task.Difficulty;
        Debug.Log($"Spawn time = {spawnTime}");
        StartCoroutine(StartTask());

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
        instantiatedPin.transform.SetParent(transform, true);
        Note instanstiatedNote = instantiatedPin.GetComponent<Note>();
        instanstiatedNote.creatorObject = this.gameObject;
        instanstiatedNote.noteSpeed = 2 * (task.Difficulty);
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
    public void AddNoteToQueue(Note note) 
    {
        queuedNotes.Add(note);
        try
        {
            if (this.gameObject.activeInHierarchy)
            {
                StartCoroutine(QueueNote(note, spawnTime));
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
    IEnumerator QueueNote(Note note, float waitForSeconds)
    {
        busy = true;
        yield return new WaitForSeconds(waitForSeconds);
        
        try
        {
            if (this.gameObject.activeInHierarchy)
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
