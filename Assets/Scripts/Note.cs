using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Note : MonoBehaviour
{
    public float sanityCost = 1f;
    public bool busy = false;
    public GameObject creatorObject = null;
    public float noteSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * noteSpeed;
    }
    public void SetNotePosition()
    {
        int randomn = Random.Range(1, 4);
        Vector3 newVector = new Vector3(randomn, 0, 0);
        transform.position = creatorObject.transform.position + newVector;

    }
    public void PlayNote()
    {
        Debug.Log("FallingObject.PlayNote");
        // Call Audio manager to play the note.
    }
    public IEnumerator RespawnNote(float timing)
    {
        busy = true;
        yield return new WaitForSeconds(timing);
        SetNotePosition();
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        //rigidObject.linearVelocity = Vector3.zero;
        busy = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == this.tag) { return; }
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GlobalManagement.instance.DecreaseSanity(sanityCost);
            if (busy == false)
            {
                // Play pin dropping audio.
                int randomn2 = Random.Range(1, 4);
                StartCoroutine(RespawnNote(randomn2));

            }
        }
    }
}
