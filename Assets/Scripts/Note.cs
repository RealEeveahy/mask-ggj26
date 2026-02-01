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
    public List<string> noteSoundNames = new List<string>();
    public int notePitch = 0;
    Color defaultColour;
    public Vector3 respawnPoint = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       renderer = GetComponentInChildren<SpriteRenderer>();
       luteBehaviour = creatorObject.GetComponent<LuteBehaviour>();
        respawnPoint = luteBehaviour.spawnPosition.position;
        noteSoundNames.Add("LuteC3");
        noteSoundNames.Add("LuteE3");
        noteSoundNames.Add("LuteG3");
        noteSoundNames.Add("LuteC4");
        defaultColour = renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * noteSpeed;
    }
    public void SetNotePosition()
    {
        int randomn = UnityEngine.Random.Range(0, 3);
        notePitch = randomn;
        Vector3 newVector = new Vector3(notePitch * fretBoardWidth, 0, 0);
        transform.position = respawnPoint + newVector;
        renderer.color = defaultColour;
        GetComponent<Collider2D>().enabled = true;
    }
    public void PlayNote(bool isSuccessful)
    {
        Debug.Log($"FallingObject.PlayNote{isSuccessful}");
        if (isSuccessful)
        {
            //GlobalManagement.instance.PlaySound("Lute", GlobalManagement.SoundType.SFX);
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
            if (busy == false)
            {
                GetComponent<Collider2D>().enabled = false;
                if (collision.tag == "Player") // Player successfully hit note.
                {
                    PlayNote(true);
                    luteBehaviour.AddNoteToQueue(this);
                    ConfirmNote();
                    return;
                } else if (collision.tag == "Ground")
                {
                    ChangeNoteTransparency(0.2f); // Note hit the ground...
                    luteBehaviour.AddNoteToQueue(this);
                    GlobalManagement.instance.DecreaseSanity(sanityCost);
                    PlayNote(false);
                }
            }
        }
    }
    public void ConfirmNote()
    {
        Color color = renderer.color;
        color.a = 0.75f;
        color.b = 0.75f;
        renderer.color = color; 
    }
    public void ChangeNoteTransparency(float value)
    {
        Color colour = renderer.color;
        colour.a = value;
        renderer.color = colour;
    }
}
