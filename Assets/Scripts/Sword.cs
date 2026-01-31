using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created\
    public Transform swordLandingPoint = null;
    public float speed = 0.05f;
    public float sanityCost = 0.05f;
    public bool busy = false;
    public Vector3 origin;
    public float respawnTime = 2f;
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, swordLandingPoint.position) < 2f) 
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            StartCoroutine(RespawnSword()); 
            return; 
        }
        transform.position = Vector3.MoveTowards(transform.position, swordLandingPoint.position,speed);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == this.tag) { return; }
        if (collision != null)
        {
            //Destroy(this.gameObject);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GlobalManagement.instance.DecreaseSanity(sanityCost);
            if (busy == false)
            {
                // Play sword hit sound dropping audio.
                StartCoroutine(RespawnSword());
            }
        }
    }
    IEnumerator RespawnSword()
    {
        busy = true;
        yield return new WaitForSeconds(respawnTime);
        transform.position = origin;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        busy = false;
    }
}
