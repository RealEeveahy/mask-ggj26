using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallingObject : MonoBehaviour
{
    GameObject fallingObject;
    public float sanityCost = 1f;
    public GameObject creatorObject = null;
    public float respawnTime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sanityCost = (float)DayManager.instance.currentDay;
        fallingObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            transform.position = creatorObject.transform.position;
            StartCoroutine(RespawnPin());
            GlobalManagement.instance.DecreaseSanity(sanityCost);
            //creatorObj
        }
    }
    IEnumerator RespawnPin()
    {
        yield return new WaitForSeconds(respawnTime);
        this.gameObject.SetActive(true);
    }
}
