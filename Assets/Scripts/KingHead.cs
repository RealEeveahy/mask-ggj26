using UnityEngine;

public class KingHead : MonoBehaviour
{
    float target = 1f;
    void Update()
    {
        if(Mathf.Approximately(transform.rotation.z, target))
        {
            target = -target;
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, Mathf.Lerp(transform.rotation.z, target, Time.deltaTime)));
        }
    }
}
