using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FallingObject : MonoBehaviour
{
    GameObject fallingObject;
    public float sanityCost = 1f;
    public GameObject creatorObject = null;
    public float respawnTime = 0.5f;
    public Rigidbody2D rigidObject = null;
    public bool busy = false;
    public float throwStrength = 1000f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sanityCost = (float)DayManager.instance.currentDay; UNCOMMENT OUT WHEN IN THE GAME
        fallingObject = this.gameObject;
        rigidObject = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == this.tag) { return; }
        Debug.Log("FallingObject.OnTriggerEnter2d");
        if (collision != null)
        {
            //Destroy(this.gameObject);
            GetComponentInChildren<SpriteRenderer>().enabled =false;
            transform.position = creatorObject.transform.position;
            if (busy == false && this.tag == "Pin")
            {
                // Play pin dropping audio.
                StartCoroutine(RespawnPin());
            }
            GlobalManagement.instance.DecreaseSanity(sanityCost);
            if (busy == false && this.tag == "Note")
            {
                // Play pin dropping audio.
                int randomn = Random.Range(1, 4);
                Vector3 newVector = new Vector3(randomn, 0,0);
                transform.position = transform.position + newVector;
                StartCoroutine(RespawnNote());
            }
        }
    }
    IEnumerator RespawnPin()
    {
        busy = true;
        yield return new WaitForSeconds(respawnTime);
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        rigidObject.linearVelocity = Vector3.zero;
        busy = false;
    }
    IEnumerator RespawnNote()
    {
        busy = true;
        int randomn = Random.Range(1, 8);
        yield return new WaitForSeconds(randomn/2);
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        rigidObject.linearVelocity = Vector3.zero;
        busy = false;
    }
    public void Throw()
    {
        Debug.Log("FallingObject.Throw");
        float randomnX = Random.value*100;
        float randomnY = Random.value * 100;
        Vector2 randomnVector = new Vector2(randomnX, randomnY);
        rigidObject.AddForce(Vector2.up *throwStrength + randomnVector);
        rigidObject.AddTorque(randomnX);// * Time.fixedDeltaTime);
    }
    public void PlayNote()
    {
        Debug.Log("FallingObject.PlayNote");
        // Call Audio manager to play the note.
    }
}
