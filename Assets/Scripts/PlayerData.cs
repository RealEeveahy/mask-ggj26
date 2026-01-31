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
            if(value < 0f)
            {
                sanity = 0f;
                GlobalManagement.instance.RoundLoss();
                return;
            }
            if (value > 1f)
            {
                sanity = 1f;
            }
            sanity = value;

            // notify listeners of change in sanity value
            OnSanityChanged?.Invoke(this, sanity);
        }
    }
}
