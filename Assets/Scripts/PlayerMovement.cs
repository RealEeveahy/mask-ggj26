using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 finalPos;
    public Vector3 enterStagePosition;
    void Start()
    {
        finalPos = transform.position;
        transform.position = enterStagePosition;
    }
    void Update()
    {
        if(!Mathf.Approximately(transform.position.x,finalPos.x))
        {
            Vector3 direction = finalPos - transform.position;
            transform.position += direction * Time.deltaTime * 1.2f;
        }
    }
}
