using UnityEngine;

public class KingHead : MonoBehaviour
{
    void Update()
    {
        float MaxAngleDeflection = 30.0f;
        float SpeedOfPendulum = 1.0f;

        float angle = MaxAngleDeflection * Mathf.Sin(Time.time * SpeedOfPendulum);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
