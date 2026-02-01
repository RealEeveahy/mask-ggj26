using UnityEngine;
using UnityEngine.UI;
public class ImageResize : MonoBehaviour
{
    private Image image;
    private RectTransform rectTransform;
    void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        image.SetNativeSize();
        rectTransform.sizeDelta = new Vector2((float)(rectTransform.sizeDelta.x * 1.2), (float)(rectTransform.sizeDelta.y * 1.2));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 size = image.rectTransform.sizeDelta;
        image.rectTransform.sizeDelta = new Vector2(size.x -= Time.deltaTime *2, size.y -= Time.deltaTime*2);
    }
}
