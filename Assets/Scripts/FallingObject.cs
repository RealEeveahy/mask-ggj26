using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FallingObject : MonoBehaviour
{
    GameObject fallingObject;
    public float sanityCost = 0.1f;
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
        if (collision != null)
        {
            if (collision.tag == this.tag) { return; }
            if (collision.tag == "Player")
            {
                Throw();
                GlobalManagement.instance.PlaySound("Voice", GlobalManagement.SoundType.SFX);
                return;
            }
            GlobalManagement.instance.PlaySound("Woosh", GlobalManagement.SoundType.SFX);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GlobalManagement.instance.DecreaseSanity(sanityCost);
            if (busy == false)
            {
                // Play pin dropping audio.
                try
                {
                    if (this.gameObject.activeSelf)
                        StartCoroutine(RespawnPin());
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
            }

        }
    }
    IEnumerator RespawnPin()
    {
        busy = true;
        yield return new WaitForSeconds(respawnTime);
        transform.position = creatorObject.transform.position;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        rigidObject.linearVelocity = Vector3.down;
        busy = false;
    }

    public void Throw()
    {
        // reset velocity before applying
        rigidObject.linearVelocityY = 0;

        //calculate x value as a velocity towards the centre
        int magnitude = transform.position.x > 0 ? -1 : 1;
        float randomnX = UnityEngine.Random.value * 100 * magnitude;

        //random value upwards
        float randomnY = UnityEngine.Random.value * 100; 

        Vector2 randomnVector = new Vector2(randomnX, randomnY);
        rigidObject.AddTorque(magnitude * (UnityEngine.Random.value * 100));
        rigidObject.AddForce(Vector2.up * throwStrength + randomnVector);
        rigidObject.AddTorque(randomnX);// * Time.fixedDeltaTime);
    }

    
}