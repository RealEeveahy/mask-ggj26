using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static GlobalManagement;
using static UnityEngine.Rendering.DebugUI;

public class Sword : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created\
    public Transform swordLandingPoint = null;
    public GameObject creatorObject = null;
    public float speed = 0.05f;
    public float sanityCost = 0.05f;
    public bool busy = false;
    public Vector2 origin;
    SpriteRenderer renderer = new();
    public float respawnTime = 2f;
    public float rotationSpeedZ = 100f;
    public float rotationSpeedX = 100f;
    public Rigidbody2D rigidBody = null;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        origin = transform.position; //creatorObject.GetComponent<WheelBehaviour>().spawnPosition;
        renderer = GetComponentInChildren<SpriteRenderer>();
        float mx = swordLandingPoint.position.x - transform.position.x;
        if (mx > 0f) rotationSpeedX = 0f;
        if (mx > 0f) rotationSpeedZ = -100f;
        if (mx < 0f) rotationSpeedX = 0f;
        if (mx < 0f) rotationSpeedZ = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, swordLandingPoint.position, speed * Time.deltaTime);
        transform.Rotate(0, 0, rotationSpeedZ * Time.deltaTime);
        if (busy)
        {
            DecreaseTranspancyOvertime();
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision != null)
        {
            if (collision.tag == this.tag || collision.tag == "Untagged") { return; } // Impact unknown tag or another sword... ignore it.

            if (busy == true) { return; }
            GetComponent<CapsuleCollider2D>().enabled = false;
            Debug.Log($"Sword impacted {collision.tag}");
            try
            {
                if (this.gameObject.activeInHierarchy)
                    StartCoroutine(RespawnSword()); //if hit a player or ground
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
            if (collision.tag == "Player") // Impacted a player
            {
                GlobalManagement.instance.DecreaseSanity(sanityCost);
                GlobalManagement.instance.PlaySound("Jester", GlobalManagement.SoundType.SFX, true);
            } else
            {
                GlobalManagement.instance.PlaySound("Impact", GlobalManagement.SoundType.SFX);
            }
                 //Impacted 
                                                                                           //GetComponentInChildren<SpriteRenderer>().enabled = false;

        }
    }
    IEnumerator RespawnSword()
    {
        busy = true;
        yield return new WaitForSeconds(respawnTime);
        busy = false;
        ResetSword();
        
    }
    public void DecreaseTranspancyOvertime()
    {
        Color colour = renderer.color;
        colour.a -= Time.deltaTime;
        renderer.color = colour;
    }
    public void ResetSword()
    {
        transform.position = origin;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        Color colour = renderer.color;
        colour.a = 1;
        renderer.color = colour;
        GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
