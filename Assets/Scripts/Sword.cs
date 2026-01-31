using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Sword : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created\
    public Transform swordLandingPoint = null;
    public float speed = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, swordLandingPoint.position,speed);
    }
}
