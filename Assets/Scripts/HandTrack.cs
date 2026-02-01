using UnityEngine;
using UnityEngine.InputSystem;

public class HandTrack : MonoBehaviour
{
    float GetXPosition()
    {
        float mx = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x;
        if (mx > 2.5f) mx = 2.5f;
        if (mx < -2.5f) mx = -2.5f;
        return mx;
    }

    void Update()
    {
        transform.position = new Vector3(GetXPosition(), transform.position.y, transform.position.z);
    }
}
