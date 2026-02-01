using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Note : MonoBehaviour
{
    public float sanityCost = 1f;
    public bool busy = false;
    public GameObject creatorObject = null;
    public LuteBehaviour luteBehaviour = null;
    public float noteSpeed = 2f;
    SpriteRenderer renderer = new();
    public float fretBoardWidth = 1.5f;
    public List<String> noteSoundNames = new List<String>();
    public int notePitch = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       renderer = GetComponentInChildren<SpriteRenderer>();
       luteBehaviour = creatorObject.GetComponent<LuteBehaviour>();
        noteSoundNames.Add("LuteC3");
        noteSoundNames.Add("LuteD3");
        noteSoundNames.Add("LuteE3");
        noteSoundNames.Add("LuteF3");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * noteSpeed;
    }
    public void SetNotePosition()
    {
        int randomn = UnityEngine.Random.Range(1, 4);
        notePitch = randomn;
        Vector3 newVector = new Vector3(notePitch * fretBoardWidth, 0, 0);
        transform.position = creatorObject.transform.position + newVector;
    }
    public void PlayNote(bool isSuccessful)
    {
        Debug.Log($"FallingObject.PlayNote{isSuccessful}");
        if (isSuccessful)
        {
            GlobalManagement.instance.PlaySound(noteSoundNames[notePitch], GlobalManagement.SoundType.SFX);
        }
        else
        {
            GlobalManagement.instance.PlaySound("Twang", GlobalManagement.SoundType.SFX);
        }

    }
    public void RespawnNote()
    {
        SetNotePosition();
        ChangeNoteTransparency(1f);
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
            if (busy == false)
            {
                ChangeNoteTransparency(0.5f); // Note hit the ground...
                luteBehaviour.AddNoteToQueue(this);
                GlobalManagement.instance.DecreaseSanity(sanityCost);
                PlayNote(false);
            }
        }
    }
    public void ChangeNoteTransparency(float value)
    {
        Color colour = renderer.color;
        colour.a = value;
        renderer.color = colour;
    }
}
