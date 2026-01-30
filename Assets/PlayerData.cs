using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private float sanity = 0.7f;
    public float Sanity
    {
        get { return sanity; }
        set { sanity = value; }
    }
}
