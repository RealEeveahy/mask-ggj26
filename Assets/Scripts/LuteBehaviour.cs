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
        StartCoroutine(QueueNote(note));
    }
    IEnumerator QueueNote(Note note)
    {
        busy = true;
        yield return new WaitForSeconds(spawnTime);
        int randomn2 = Random.Range(1, 4);
        StartCoroutine(note.RespawnNote(randomn2));
        queuedNotes.Remove(note);
        busy = false;
    }
}
