using UnityEngine;

public class MaskHead : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        float MaxAngleDeflection = 360f;
        float SpeedOfPendulum = 1.0f;

        float angle = MaxAngleDeflection * Mathf.Abs(Mathf.Sin(Time.time * SpeedOfPendulum));
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
