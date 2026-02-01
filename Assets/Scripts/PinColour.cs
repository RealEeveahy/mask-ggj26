using UnityEngine;
using System.Collections.Generic;
public class PinColour : MonoBehaviour
{
    SpriteRenderer pinSprite = null;
    public List<Sprite> spritelist;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomn = Random.Range(0, spritelist.Count);
        pinSprite = GetComponent<SpriteRenderer>();
        pinSprite.sprite = spritelist[randomn];
    }

}
