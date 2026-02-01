using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GlobalManagement;
public class Note : MonoBehaviour
{
    public float sanityCost = 1f;
    public bool busy = false;
    public GameObject creatorObject = null;
    public LuteBehaviour luteBehaviour = null;
    public float noteSpeed = 1f;
    SpriteRenderer renderer = null;
    public float fretBoardWidth = 1.25f;
    public int notePitch = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
       luteBehaviour = creatorObject.GetComponent<LuteBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * noteSpeed;
    }
    public void SetNotePosition()
    {
        int randomn = Random.Range(1, 4);
        notePitch = randomn;
        Vector3 newVector = new Vector3(notePitch * fretBoardWidth, 0, 0);
        transform.position = creatorObject.transform.position + newVector;

    }
    public void PlayNote(bool isSuccessful)
    {
        Debug.Log("FallingObject.PlayNote");
        if (isSuccessful)
        {
            // Call Audio manager to play the note.
            GlobalManagement.instance.PlaySound("LuteC3", GlobalManagement.SoundType.SFX, false);
        }
        else
        {
            // Play twang sound here.
            GlobalManagement.instance.PlaySound("Twang", GlobalManagement.SoundType.SFX, false);
        }

    }
    public IEnumerator RespawnNote(float timing)
    {
        busy = true;
        yield return new WaitForSeconds(timing);
        SetNotePosition();
        ChangeNoteTransparency(1f);
        //rigidObject.linearVelocity = Vector3.zero;
        busy = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == this.tag) { return; } // Ignore other notes.
            if (collision.tag == "Player" && busy == false ) // Player successfully hit note.
            {
                PlayNote(true);
                luteBehaviour.AddNoteToQueue(this);
                return;
            }
            ChangeNoteTransparency(0.5f); // Note hit the ground...
            luteBehaviour.AddNoteToQueue(this);
            GlobalManagement.instance.DecreaseSanity(sanityCost);
            PlayNote(false);
        }
    }
    public void ChangeNoteTransparency(float value)
    {
        Color colour = renderer.color;
        colour.a = value;
        renderer.color = colour;
    }
}
