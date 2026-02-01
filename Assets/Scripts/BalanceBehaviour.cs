using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BalanceBehaviour : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;
    void Start()
    {
        ball.GetComponent<Rigidbody2D>().AddTorque(20);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToMouse();   
    }
    void MoveToMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - player.transform.position;

        player.GetComponent<Rigidbody2D>().AddForceX(direction.x * 1);
    }
}
