using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class PlayerData : MonoBehaviour
{
    public EventHandler<float> OnSanityChanged;
    [SerializeField] private float sanity = 0.7f;
    public float Sanity
    {
        get { return sanity; }
        set { 
            if (sanity == value) return;
            sanity = value;

            // notify listeners of change in sanity value
            OnSanityChanged?.Invoke(this, sanity);
        }
    }
}
