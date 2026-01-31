using UnityEngine;

public class PinColour : MonoBehaviour
{
    SpriteRenderer pinSprite = null;
    public Sprite sprite1, sprite2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomn = Random.Range(0, 2);
        pinSprite = GetComponent<SpriteRenderer>();
        pinSprite.sprite = randomn ==0 ? sprite1 : sprite2;
    }

}
